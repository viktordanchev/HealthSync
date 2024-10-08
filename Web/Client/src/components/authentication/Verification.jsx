import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { verifyAccount, sendVrfCode } from '../../services/apiRequests/account';
import { validateEmail, validateVrfCode } from '../../services/validationSchemas';
import useTimer from '../../hooks/useTimer';
import useCheckAuth from '../../hooks/useCheckAuth';
import Messages from './Messages';
import ComponentLoading from '../ComponentLoading';

function Verification() {
    const navigate = useNavigate();
    const { isButtonDisabled, seconds, resetTimer } = useTimer();
    const { isAuthenticated, loading } = useCheckAuth();
    const [messages, setMessages] = useState([]);
    const [messageType, setMessageType] = useState('');
    const userEmail = sessionStorage.getItem('email') || '';

    if (isAuthenticated) {
        navigate('/home');
    }

    const validationEmailSchema = Yup.object().shape({ email: validateEmail });

    const validationVrfCodeSchema = Yup.object().shape({ vrfCode: validateVrfCode });

    const submitCode = async (values) => {
        const response = await verifyAccount(values);

        if (response.ok) {
            navigate('/home');
        }
        else {
            const error = await response.json();
            setMessages(error);
            setMessageType('error');
        }

        setTimeout(() => { setMessages(''); }, 3000);
    };

    const sendCode = async (email) => {
        const response = await sendVrfCode(email);
        const data = await response.json();

        setMessages(data);

        if (response.ok) {
            sessionStorage.setItem('email', email);

            setMessageType('message');
            resetTimer();
        }
        else {
            setMessageType('error');
        }

        setTimeout(() => { setMessages(''); }, 3000);
    };

    return (
        <>
            {loading ? <ComponentLoading /> :
                <div className="flex flex-col space-y-6">
                    <Messages data={messages} type={messageType} />

                    <section className="flex items-center justify-center">
                        <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
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
                </div>}
        </>
    );
}

export default Verification;