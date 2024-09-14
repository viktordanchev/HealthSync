import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors } from '../../constants/errors';
import { login, isAccessTokenExpired } from '../../services/apiRequests/account';

function Login() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const checkAccessToken = async () => {
            const isExpired = await isAccessTokenExpired()
                .then(response => response.json())
                .then(data => data.isExpired);

            if (!isExpired) {
                navigate('/home');
            }
            else {
                setLoading(false)
            }
        }

        checkAccessToken();
    }, [navigate]);

    const validations = Yup.object({
        email: Yup.string()
            .email(authErrors.InvalidEmail)
            .required(authErrors.RequiredField),
        password: Yup.string()
            .required(authErrors.RequiredField),
    });

    const handleLogin = async (values, { setSubmitting, setErrors }) => {
        try {
            const response = await login(values);

            if (response.ok) {
                const data = await response.json();

                if (data.redirectTo) {
                    navigate(data.redirectTo);
                }
            } else {
                setErrors({ email: Errors.InvalidLoginData });
            }
        } catch (error) {
            console.error('Error:', error);
            setErrors({ email: Errors.InvalidLoginData });
        }
        setSubmitting(false);
    };

    if (loading) {
        return null;
    }

    return (
        <section className="flex items-center justify-center">
            <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                <p className="text-3xl text-center text-white">Welcome back</p>
                <hr className="my-4" />
                <Formik
                    initialValues={{ email: '', password: '', rememberMe: false }}
                    validationSchema={validations}
                    onSubmit={handleLogin}
                >
                    {({ isSubmitting }) => (
                        <Form className="flex flex-col space-y-2">
                            <div>
                                <label className="text-md font-bold">
                                    Email
                                </label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
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
                                <label className="inline-flex items-center">
                                    <Field
                                        type="checkbox"
                                        name="rememberMe"
                                        className="form-checkbox text-blue-600"
                                    />
                                    <span className="ml-1 text-md text-white">Remember me</span>
                                </label>
                                <button className="inline-block align-baseline text-sm text-blue-500 hover:text-blue-800">
                                    Forgot Password?
                                </button>
                            </div>
                            <div className="text-center pt-6">
                                <button
                                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                    type="submit"
                                    disabled={isSubmitting}
                                >
                                    Sign In
                                </button>
                            </div>
                        </Form>
                    )}
                </Formik>
            </div>
        </section>
    );
}

export default Login;