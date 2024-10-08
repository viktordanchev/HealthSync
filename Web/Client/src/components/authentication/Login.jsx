import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { login } from '../../services/apiRequests/account';
import { validateEmail, validatePassword } from '../../services/validationSchemas';
import useCheckAuth from '../../hooks/useCheckAuth';
import Messages from './Messages';
import ComponentLoading from '../ComponentLoading';

function Login() {
    const navigate = useNavigate();
    const [messages, setMessages] = useState([]);
    const { isAuthenticated, loading } = useCheckAuth();

    if (isAuthenticated) {
        navigate('/home');
    }

    const validationSchema = Yup.object({
        email: validateEmail,
        password: validatePassword
    });

    const handleLogin = async (values) => {
        const response = await login(values);
        const data = await response.json();

        if (response.ok) {
            sessionStorage.setItem('accessToken', data.token);
            navigate('/home');
            window.location.reload();
        } else {
            if (data.notVerified) {
                navigate('/account/verify');
            }

            setMessages(data);
        }

        setTimeout(() => { setMessages(''); }, 3000);
    };

    return (
        <>
            {loading ? <ComponentLoading /> : 
                <div className="flex flex-col space-y-6">
                    <Messages data={messages} type={'error'} />

                    <section className="flex items-center justify-center">
                        <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                            <p className="text-3xl text-center text-white">Login</p>
                            <hr className="my-4" />
                            <Formik
                                initialValues={{ email: '', password: '', rememberMe: false }}
                                validationSchema={validationSchema}
                                onSubmit={handleLogin}
                            >
                                <Form className="flex flex-col space-y-2">
                                    <div>
                                        <label className="text-md font-bold">
                                            Email
                                        </label>
                                        <Field
                                            className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                            placeholder="user@gmail.com"
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
                                        <a href="/account/recoverPassword" className="inline-block align-baseline text-sm text-blue-500 hover:text-blue-800">
                                            Forgot Password?
                                        </a>
                                    </div>
                                    <div className="text-center pt-6">
                                        <button
                                            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                            type="submit">
                                            Sign In
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

export default Login;