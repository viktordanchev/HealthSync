import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors } from '../../constants/errors';
import { verifyAccount, resendVrfCode } from '../../services/account';
import Messages from './Messages.jsx';

function Verification() {
    const navigate = useNavigate();
    const [messages, setMessage] = useState([]);
    const [messageType, setMessageType] = useState('');
    const userEmail = sessionStorage.getItem('email');

    const validations = Yup.object({
        email: Yup.string()
            .email(authErrors.InvalidEmail)
            .required('Email' + authErrors.RequiredField),
        vrfCode: Yup.string()
            .required('Code' + authErrors.RequiredField)
    });

    const handleSubmit = async (values) => {
        const response = await verifyAccount(values);

        if (response.ok) {
            navigate('/home');
        }
        else {
            const errors = await response.json();

            for (const [key, message] of Object.entries(errors)) {
                setMessage((prevMessages) => [...prevMessages, message]);
            }

            setMessageType('error');
        }
    };

    const handleResend = async (email) => {
        const response = await resendVrfCode(email);
        const data = await response.json();

        for (const [key, message] of Object.entries(data)) {
            setMessage((prevMessages) => [...prevMessages, message]);
        }

        if (response.ok) {
            sessionStorage.setItem('email', email);
            setMessageType('message');
        }
        else {
            setMessageType('error');
        }

        setTimeout(() => { setMessage(''); }, 3000);
    };

    return (
        <>
            {messages.length != 0 ? (
                <Messages values={messages} type={messageType} />
            ) : null}

            <section className="flex items-center justify-center">
                <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                    <p className="text-3xl text-center text-white">Verify</p>
                    <hr className="my-4" />
                    <Formik
                        initialValues={{ email: userEmail || '', vrfCode: '' }}
                        validationSchema={validations}
                        onSubmit={handleSubmit}
                    >
                        {({ values }) => (
                            <Form>
                                <div>
                                    <label className="text-md font-bold">Email</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                        type="email"
                                        name="email"
                                    />
                                    <ErrorMessage name="email" component="div" className="text-red-500 text-md" />
                                </div>
                                <div>
                                    <label className="text-md font-bold">Verification code</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                        type="text"
                                        name="vrfCode"
                                    />
                                    <ErrorMessage name="vrfCode" component="div" className="text-red-500 text-md" />
                                    <p className="text-md text-center text-white mt-1">Check your email!</p>
                                </div>
                                <div className="flex justify-evenly pt-6">
                                    <button
                                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                        type="button"
                                        onClick={() => handleResend(userEmail || values.email)}>
                                        Resend
                                    </button>
                                    <button
                                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                        type="submit">
                                        Submit
                                    </button>
                                </div>
                            </Form>
                        )}
                    </Formik>
                </div>
            </section>
        </>
    );
}

export default Verification;