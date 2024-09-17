﻿import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors } from '../../constants/errors';
import { register, isAuthenticated } from '../../services/account';
import Loading from '../Loading.jsx';

function Register() {
    const navigate = useNavigate();
    const [messages, setMessage] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const checkUserStatus = async () => {
            const isUserAuthenticated = await isAuthenticated();

            if (isUserAuthenticated) {
                navigate('/home');
            }
            else {
                setLoading(false);
            }
        }

        checkUserStatus();
    }, [navigate]);

    const validations = Yup.object({
        firstName: Yup.string()
            .required('First name' + authErrors.RequiredField),
        lastName: Yup.string()
            .required('Last name' + authErrors.RequiredField),
        email: Yup.string()
            .email(authErrors.InvalidEmail)
            .required('Email' + authErrors.RequiredField),
        password: Yup.string()
            .min(6, 'Password must be at least 6 characters')
            .required('Password' + authErrors.RequiredField),
        confirmPassword: Yup.string()
            .required('Confirm password' + authErrors.RequiredField),
    });

    const handleRegister = async (values, { setSubmitting, setErrors }) => {
        const response = await register(values);

        if (response.ok) {
        } else {
            const errors = await response.json();

            for (const [key, message] of Object.entries(errors)) {
                setMessage((prevMessages) => [...prevMessages, message]);
            }
        }

        setTimeout(() => { setMessage(''); }, 3000);
    };

    return (
        <>
            {loading ? <Loading /> :
                <>
                    {messages.length > 0 ? (
                        <div className="flex flex-col items-center space-y-4">
                            <div className="max-w-xs text-center text-xl bg-red-500 rounded-xl p-4">
                                {messages.map((message) => (
                                    <div className="text-white">
                                        {message}
                                    </div>
                                ))}
                            </div>
                        </div>
                    ) : null}

                    <section className="flex items-center justify-center">
                        <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                            <p className="text-3xl text-center text-white">Welcome</p>
                            <hr className="my-4" />
                            <Formik
                                initialValues={{ firstName: '', lastName: '', email: '', password: '', confirmPassword: '' }}
                                validationSchema={validations}
                                onSubmit={handleRegister}
                            >
                                {({ isSubmitting }) => (
                                    <Form className="flex flex-col space-y-2">
                                        <div className="flex flex-row space-x-4">
                                            <div>
                                                <label className="text-md font-bold">First name</label>
                                                <Field
                                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                                    type="text"
                                                    name="firstName"
                                                />
                                            </div>
                                            <div>
                                                <label className="text-md font-bold">Last name</label>
                                                <Field
                                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                                    type="text"
                                                    name="lastName"
                                                />
                                            </div>
                                        </div>
                                        <ErrorMessage name="firstName" component="div" className="text-red-500 text-md" />
                                        <ErrorMessage name="lastName" component="div" className="text-red-500 text-md" />
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
                                        <div className="text-center pt-6">
                                            <button
                                                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                                type="submit"
                                                disabled={isSubmitting}
                                            >
                                                Register
                                            </button>
                                        </div>
                                    </Form>
                                )}
                            </Formik>
                        </div>
                    </section>
                </>}
        </>
    );
}

export default Register;