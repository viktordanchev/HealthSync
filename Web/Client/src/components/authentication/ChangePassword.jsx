import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors } from '../../constants/errors';
import { recoverPassword } from '../../services/account';
import Messages from './Messages.jsx';

function ChangePassword({ token }) {
    const navigate = useNavigate();
    const [messages, setMessage] = useState([]);

    const validations = Yup.object({
        password: Yup.string()
            .min(6, 'Password must be at least 6 characters')
            .required('Password' + authErrors.RequiredField),
        confirmPassword: Yup.string()
            .required('Confirm password' + authErrors.RequiredField)
            .oneOf([Yup.ref('password'), null], 'Passwords must match')
    });

    const handleSubmit = async (values) => {
        const response = await recoverPassword(values);

        if (response.ok) {
            navigate("/login");
        }
        else {
            const errors = await response.json();

            for (const [key, message] of Object.entries(errors)) {
                setMessage((prevMessages) => [...prevMessages, message]);
            }
        }

        setTimeout(() => { setMessage(''); }, 3000);
    };

    return (
        <>
            {messages.length != 0 ? (
                <Messages values={messages} type={'error'} />
            ) : null}

            <section className="flex items-center justify-center">
                <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                    <p className="text-3xl text-center text-white">Recover password</p>
                    <hr className="my-4" />
                    <Formik
                        initialValues={{ password: '', confirmPassword: '', token: token }}
                        validationSchema={validations}
                        onSubmit={handleSubmit}
                    >
                        <Form>
                            <div>
                                <label className="text-md font-bold">Password</label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                    type="password"
                                    name="password"
                                />
                                <ErrorMessage name="password" component="div" className="text-red-500 text-md" />
                            </div>
                            <div>
                                <label className="text-md font-bold">Confirm Password</label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                    type="password"
                                    name="confirmPassword"
                                />
                                <ErrorMessage name="confirmPassword" component="div" className="text-red-500 text-md" />
                            </div>
                            <div className="flex justify-center">
                                <button
                                    className="w-1/2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-6"
                                    type="submit">
                                    Recover
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </div>
            </section >
        </>
    );
}

export default ChangePassword;