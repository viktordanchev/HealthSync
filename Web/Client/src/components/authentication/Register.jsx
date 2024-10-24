﻿import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { register } from '../../services/apiRequests/account';
import { validateFirstName, validateLastName, validateEmail, validatePassword, validateConfirmPassword } from '../../services/validationSchemas';
import useCheckAuth from '../../hooks/useCheckAuth';
import Message from './Message';

function Register() {
    const navigate = useNavigate();
    const [message, setMessage] = useState('');
    const { isAuthenticated } = useCheckAuth();

    if (isAuthenticated) {
        navigate('/home');
    }

    const validationSchema = Yup.object({
        firstName: validateFirstName,
        lastName: validateLastName,
        email: validateEmail,
        password: validatePassword,
        confirmPassword: validateConfirmPassword
    });

    const handleRegister = async (values) => {
        const data = await register(values);

        if (!data) {
            navigate('/account/verify');
        } else {
            setMessage(data.error);
        }

        setTimeout(() => { setMessage(''); }, 3000);
    };

    return (
        <div className="flex flex-col space-y-6">
            <Message message={message} type={'error'} />

            <section className="flex items-center justify-center mx-6">
                <div className="w-80 bg-maincolor rounded-xl shadow-md px-8 py-8 sm:w-full">
                    <p className="text-3xl text-center text-white">Register</p>
                    <hr className="my-4" />
                    <Formik
                        initialValues={{ firstName: '', lastName: '', email: '', password: '', confirmPassword: '' }}
                        validationSchema={validationSchema}
                        onSubmit={handleRegister}
                    >
                        <Form className="flex flex-col space-y-2">
                            <div className="flex flex-row space-x-4">
                                <div>
                                    <label className="text-md font-bold">First name</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                        placeholder="Alex"
                                        type="text"
                                        name="firstName"
                                    />
                                </div>
                                <div>
                                    <label className="text-md font-bold">Last name</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                        placeholder="Ivanov"
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
                                    placeholder="user@mail.com"
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
                                    type="submit">
                                    Register
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </div>
            </section>
        </div>
    );
}

export default Register;