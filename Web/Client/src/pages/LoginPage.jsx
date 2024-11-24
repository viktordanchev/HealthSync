import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { validateEmail, validateLoginPassword } from '../services/validationSchemas';
import useAuth from '../hooks/useAuth';
import Message from '../components/Message';

function LoginPage() {
    const navigate = useNavigate();
    const [message, setMessage] = useState('');
    const { isAuthenticated } = useAuth();

    if (isAuthenticated) {
        navigate('/home');
    }

    const validationSchema = Yup.object({
        email: validateEmail,
        password: validateLoginPassword
    });

    const handleLogin = async (values) => {
        try {
            const response = await apiRequest('account', 'login', values, undefined, 'POST', false);

            if (response.token) {
                localStorage.setItem('accessToken', response.token);
                navigate('/home');
            } else {
                if (response.notVerified) {
                    navigate('/account/verify');
                }

                setMessage(response.error);
            }

            setTimeout(() => { setMessage(''); }, 3000);
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <>
            <Message message={message} type={'error'} />

            <section className="flex items-center justify-center">
                <div className="w-80 bg-maincolor rounded-xl shadow-md px-8 py-8 sm:w-full">
                    <p className="text-3xl text-center text-white">Login</p>
                    <hr className="my-4" />
                    <Formik
                        initialValues={{ email: '', password: '', rememberMe: false }}
                        validationSchema={validationSchema}
                        onSubmit={handleLogin}
                    >
                        <Form className="flex flex-col space-y-2 text-gray-700">
                            <div>
                                <label className="text-md font-bold">
                                    Email
                                </label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                    placeholder="user@mail.com"
                                    type="email"
                                    name="email"
                                />
                                <ErrorMessage name="email" component="div" className="text-red-500 text-md" />
                            </div>
                            <div>
                                <label className="text-md font-bold">
                                    Password
                                </label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                    type="password"
                                    name="password"
                                />
                                <ErrorMessage name="password" component="div" className="text-red-500 text-md" />
                            </div>
                            <div className="flex items-center justify-between">
                                <label className="inline-flex items-center cursor-pointer">
                                    <Field
                                        type="checkbox"
                                        name="rememberMe"
                                        className="form-checkbox text-blue-600 cursor-pointer"
                                    />
                                    <span className="ml-1 text-md text-white">Remember me</span>
                                </label>
                                <a href="/account/recoverPassword" className="inline-block align-baseline text-sm text-blue-500 underline hover:text-blue-800">
                                    Forgot Password?
                                </a>
                            </div>
                            <div className="text-center pt-6">
                                <button
                                    className="bg-blue-500 border-2 border-blue-500 hover:bg-white hover:text-blue-500 text-white font-bold py-1 px-2 rounded"
                                    type="submit">
                                    Sign In
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </div>
            </section>
        </>
    );
}

export default LoginPage;