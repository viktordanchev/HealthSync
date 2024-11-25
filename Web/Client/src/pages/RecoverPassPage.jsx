import React, { useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { validateEmail, validatePassword, validateConfirmPassword } from '../services/validationSchemas';
import useTimer from '../hooks/useTimer';
import Message from '../components/Message';

function RecoverPassPage() {
    const navigate = useNavigate();
    const { isButtonDisabled, seconds, resetTimer } = useTimer();
    const [message, setMessage] = useState('');
    const [messageType, setMessageType] = useState('');
    const [searchParams] = useSearchParams();
    const token = searchParams.get('token') ? searchParams.get('token').replace(/ /g, '+') : null;

    const validationEmailSchema = Yup.object().shape({ email: validateEmail });

    const validationPasswordSchema = Yup.object().shape({
        password: validatePassword,
        confirmPassword: validateConfirmPassword
    });

    const submitPassword = async (values) => {
        try {
            const response = await apiRequest('account', 'recoverPass', values, undefined, 'POST', false);

            if (response.error) {
                setMessage(response.error);
                setMessageType('error');
            } else {
                navigate('/home');
            }

            setTimeout(() => { setMessage(''); }, 3000);
        } catch (error) {
            console.error(error);
        }
    };

    const sendLink = async (email) => {
        try {
            const response = await apiRequest('account', 'sendRecoverPassEmail', values, undefined, 'POST', false);

            if (response.error) {
                setMessage(response.error);
                setMessageType('error');
            } else {
                sessionStorage.setItem('email', email);

                setMessage(response.message);
                setMessageType('message');

                resetTimer();
            }

            setTimeout(() => { setMessage(''); }, 3000);
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <>
            <Message message={message} type={messageType} />

            <section className="flex items-center justify-center">
                <div className="w-80 bg-maincolor rounded-xl shadow-md px-8 py-8 sm:w-full">
                    <p className="text-3xl text-center text-white">{token ? "Recover password" : "Send recover link"}</p>
                    <hr className="my-4" />
                    <Formik
                        initialValues={token ? { password: '', confirmPassword: '', token: token } : { email: '' }}
                        validationSchema={token ? validationPasswordSchema : validationEmailSchema}
                        onSubmit={token ? submitPassword : (values) => sendLink(values.email)}
                    >
                        <Form className="text-gray-700">
                            {token ?
                                <>
                                    <div>
                                        <label className="text-md font-bold">Password</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 focus:outline-none"
                                            type="password"
                                            name="password"
                                        />
                                        <ErrorMessage name="password" component="div" className="text-red-500 text-md" />
                                    </div>
                                    <div>
                                        <label className="text-md font-bold">Confirm password</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                            type="password"
                                            name="confirmPassword"
                                        />
                                        <ErrorMessage name="confirmPassword" component="div" className="text-red-500 text-md" />
                                    </div>
                                </> :
                                <div>
                                    <label className="text-md font-bold">Email</label>
                                    <Field
                                        placeholder="user@gmail.com"
                                        className="rounded w-full py-1 px-2 focus:outline-none"
                                        type="email"
                                        name="email"
                                    />
                                    <ErrorMessage name="email" component="div" className="text-red-500 text-md" />
                                </div>}
                            <div className="flex justify-evenly pt-6">
                                <button
                                    disabled={token ? false : isButtonDisabled}
                                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                    type="submit">
                                    {token ? "Submit" : isButtonDisabled ? seconds : "Send"}
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </div>
            </section>
        </>
    );
}

export default RecoverPassPage;