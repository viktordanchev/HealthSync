import React, { Fragment, useState } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors } from '../../constants/errors';
import { sendRecoverPasswordEmail } from '../../services/account';
import Messages from './Messages.jsx';

function SendEmail() {
    const [messages, setMessage] = useState([]);
    const [messageType, setMessageType] = useState('');

    const validations = Yup.object({
        email: Yup.string()
            .email(authErrors.InvalidEmail)
            .required('Email' + authErrors.RequiredField)
    });

    const handleSubmit = async (values) => {
        const response = await sendRecoverPasswordEmail(values.email);
        const data = await response.json();

        if (response.ok) {
            setMessageType('message');
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
                    <p className="text-3xl text-center text-white">Recover password</p>
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
                                    className="w-1/2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-6"
                                    type="submit">
                                    Send
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