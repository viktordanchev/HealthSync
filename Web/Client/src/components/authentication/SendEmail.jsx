import React, { Fragment, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors } from '../../constants/errors';
import { sendRecoverPasswordEmail, sendVrfCode } from '../../services/account';
import Messages from './Messages.jsx';
import useTimer from '../../hooks/useTimer.js';

function SendEmail() {
    const { isButtonDisabled, seconds, resetTimer } = useTimer();
    const navigate = useNavigate();
    const [messages, setMessage] = useState([]);
    const [messageType, setMessageType] = useState('');
    const [isVerification, setisVerification] = useState(location.pathname.includes('/verify'));

    const validations = Yup.object({
        email: Yup.string()
            .email(authErrors.InvalidEmail)
            .required('Email' + authErrors.RequiredField)
    });

    const handleSubmit = async (values) => {
        let response;

        if (isVerification) {
            response = await sendVrfCode(values.email);
        } else {
            response = await sendRecoverPasswordEmail(values.email);
        }

        const data = await response.json();

        if (response.ok) {
            if (isVerification) {
                sessionStorage.setItem('email', values.email);
                window.location.reload();
            }
            else {
                setMessageType('message');
            }

            resetTimer();
        }
        else {
            setMessageType('error');
        }

        for (const [key, message] of Object.entries(data)) {
            setMessage((prevMessages) => [...prevMessages, message]);
        }

        setTimeout(() => { setMessage(''); }, 3000);
    };

    return (
        <div className="flex flex-col space-y-6">
            {messages.length != 0 ? (
                <Messages values={messages} type={messageType} />
            ) : null}

            <section className="flex items-center justify-center">
                <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                    <p className="text-3xl text-center text-white">{isVerification ? 'Verify Email' : 'Password Recover Email'}</p>
                    <hr className="my-4" />
                    <Formik
                        initialValues={{ email: '' }}
                        validationSchema={validations}
                        onSubmit={handleSubmit}
                    >
                        <Form>
                            <div>
                                <label className="text-md font-bold">Email</label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                    placeholder="user@gmail.com"
                                    type="email"
                                    name="email"
                                />
                                <ErrorMessage name="email" component="div" className="text-red-500 text-md" />
                            </div>
                            <div className="flex justify-center">
                                <button
                                    disabled={isButtonDisabled}
                                    className="w-1/2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-6"
                                    type="submit">
                                    {isButtonDisabled ? seconds : "Send"}
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </div>
            </section >
        </div>
    );
}

export default SendEmail;