import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { validateEmail, validateVrfCode } from '../services/validationSchemas';
import useTimer from '../hooks/useTimer';
import useAuth from '../hooks/useAuth';
import Message from '../components/Message';

function VerificationPage() {
    const navigate = useNavigate();
    const { isButtonDisabled, seconds, resetTimer } = useTimer();
    const { isAuthenticated } = useAuth();
    const [message, setMessage] = useState('');
    const [messageType, setMessageType] = useState('');
    const userEmail = sessionStorage.getItem('email') || '';

    if (isAuthenticated) {
        navigate('/home');
    }

    const validationEmailSchema = Yup.object().shape({ email: validateEmail });

    const validationVrfCodeSchema = Yup.object().shape({ vrfCode: validateVrfCode });

    const submitCode = async (values) => {
        try {
            const response = await apiRequest('account', 'verifyAccount', values, undefined, 'POST', false);

            if (response.error) {
                setMessage(response.error);
                setMessageType('error');
            } else {
                navigate('/home');
            }

            setTimeout(() => { setMessages(''); }, 3000);
        } catch (error) {
            console.error(error);
        }
    };

    const sendCode = async (email) => {
        try {
            const response = await apiRequest('account', 'sendVrfCode', values, undefined, 'POST', false);

            if (response.error) {
                setMessageType('error');
                setMessage(response.error);
            } else {
                sessionStorage.setItem('email', email);

                setMessageType('message');
                setMessage(response.message);

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
                    <p className="text-3xl text-center text-white">{userEmail ? "Verify" : "Send verification code"}</p>
                    <hr className="my-4" />
                    <Formik
                        initialValues={{ email: userEmail || '', vrfCode: '' }}
                        validationSchema={userEmail ? validationVrfCodeSchema : validationEmailSchema}
                        onSubmit={userEmail ? submitCode : (values) => sendCode(values.email)}
                    >
                        <Form>
                            {userEmail ?
                                <div>
                                    <label className="text-md font-bold">Code</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                        type="text"
                                        name="vrfCode"
                                    />
                                    <ErrorMessage name="vrfCode" component="div" className="text-red-500 text-md" />
                                </div> :
                                <div>
                                    <label className="text-md font-bold">Email</label>
                                    <Field
                                        placeholder="user@gmail.com"
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                        type="email"
                                        name="email"
                                    />
                                    <ErrorMessage name="email" component="div" className="text-red-500 text-md" />
                                </div>}
                            <div className="flex justify-evenly pt-6">
                                {userEmail ?
                                    <button
                                        disabled={isButtonDisabled}
                                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                        type="button"
                                        onClick={() => sendCode(userEmail)}>
                                        {isButtonDisabled ? seconds : "Resend"}
                                    </button> : null}
                                <button
                                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                    type="submit">
                                    {userEmail ? "Submit" : "Send"}
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </div>
            </section>
        </>
    );
}

export default VerificationPage;