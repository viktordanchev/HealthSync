import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { validateEmail, validateLoginPassword } from '../services/validationSchemas';
import { useAuthContext } from '../contexts/AuthContext';
import { useLoading } from '../contexts/LoadingContext';
import { useMessage } from '../contexts/MessageContext';
import jwtDecoder from '../services/jwtDecoder';

function LoginPage() {
    const navigate = useNavigate();
    const { login } = useAuthContext();
    const { setIsLoading } = useLoading();
    const { showMessage } = useMessage();

    const validationSchema = Yup.object({
        email: validateEmail,
        password: validateLoginPassword
    });

    const handleLogin = async (values) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'login', values, undefined, 'POST', true);

            if (response.token) {
                login(response.token);

                const { isEmailConfirmed } = jwtDecoder();
                
                if (!isEmailConfirmed) {
                    navigate('/account/verify');
                } else {
                    navigate('/home');
                }
            } else {
                showMessage(response.error, 'error');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <section className="w-80 bg-maincolor rounded-xl shadow-2xl shadow-gray-400 px-8 py-8 sm:w-full">
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
                            className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:border-blue-500 focus:outline-none"
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
                            className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:border-blue-500 focus:outline-none"
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
                            className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                            type="submit">
                            Sign In
                        </button>
                    </div>
                </Form>
            </Formik>
        </section>
    );
}

export default LoginPage;