import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { useMessage } from '../contexts/MessageContext';
import { useLoading } from '../contexts/LoadingContext';
import useTimer from '../hooks/useTimer';
import { authErrors } from "../constants/errors";

function RegisterPage() {
    const { secondsLeft, start } = useTimer();
    const { setIsLoading } = useLoading();
    const { showMessage } = useMessage();
    const navigate = useNavigate();

    const validationSchema = Yup.object({
        firstName: Yup.string()
            .required('First name' + authErrors.RequiredField),
        lastName: Yup.string()
            .required('Last name' + authErrors.RequiredField),
        email: Yup.string()
            .email(authErrors.InvalidEmail)
            .required('Email' + authErrors.RequiredField),
        vrfCode: Yup.string()
            .required('Verification code' + authErrors.RequiredField),
        password: Yup.string()
            .min(6, authErrors.InvalidPass)
            .required('Password' + authErrors.RequiredField),
        confirmPassword: Yup.string()
            .required('Confirm password' + authErrors.RequiredField)
            .oneOf([Yup.ref('password'), null], authErrors.PassMatch)
    });

    const handleRegister = async (values) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'register', values, undefined, 'POST', false);

            if (response.error) {
                showMessage(response.error, 'error');
            } else {
                navigate('/login');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    const sendVrfCode = async (email) => {
        if (!email) return;

        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'sendVrfCode', email, undefined, 'POST', false);

            if (response.error) {
                showMessage(response.error, 'error');
            } else {
                start(60);
                showMessage(response.message, 'message');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <section className="w-96 border border-zinc-500 bg-maincolor rounded-xl shadow-2xl shadow-gray-400 px-8 py-8 sm:w-full">
            <p className="text-3xl text-center text-white">Register</p>
            <hr className="my-4" />
            <Formik
                initialValues={{ firstName: '', lastName: '', email: '', vrfCode: '', password: '', confirmPassword: '' }}
                validationSchema={validationSchema}
                onSubmit={handleRegister}
            >
                {({ values, setTouched }) => (
                    <Form className="flex flex-col space-y-2 text-gray-700">
                        <div className="flex flex-row space-x-4">
                            <div>
                                <label className="font-medium">First name</label>
                                <Field
                                    className="rounded w-full py-1 px-2 border-2 border-white focus:border-blue-500 focus:outline-none"
                                    placeholder="Alex"
                                    type="text"
                                    name="firstName"
                                />
                            </div>
                            <div>
                                <label className="font-medium">Last name</label>
                                <Field
                                    className="rounded w-full py-1 px-2 border-2 border-white focus:border-blue-500 focus:outline-none"
                                    placeholder="Ivanov"
                                    type="text"
                                    name="lastName"
                                />
                            </div>
                        </div>
                        <ErrorMessage name="firstName" component="div" className="text-red-500" />
                        <ErrorMessage name="lastName" component="div" className="text-red-500" />
                        <div>
                            <label className="font-medium">Email</label>
                            <Field
                                className="rounded w-full py-1 px-2 border-2 border-white focus:border-blue-500 focus:outline-none"
                                placeholder="user@mail.com"
                                type="email"
                                name="email"
                            />
                            <ErrorMessage name="email" component="div" className="text-red-500" />
                        </div>
                        <div>
                            <label className="font-medium">Verification code</label>
                            <div className="flex">
                                <Field
                                    className="rounded-l w-full py-1 px-2 border-y-2 border-l-2 border-white focus:border-blue-500 focus:outline-none"
                                    type="text"
                                    name="vrfCode"
                                    maxLength="6"
                                />
                                <button
                                    className={`w-1/2 bg-blue-500 border-2 border-blue-500 text-white font-medium rounded-r 
                                    ${secondsLeft > 0 ? 'cursor-not-allowed' : 'cursor-pointer hover:bg-white hover:text-blue-500'}`}
                                    disabled={secondsLeft > 0}
                                    type="button"
                                    onClick={() => {
                                        setTouched({ email: true });
                                        sendVrfCode(values.email);
                                    }}
                                >
                                    {secondsLeft > 0 ?
                                        <span className="flex justify-center space-x-1">
                                            <p>Resend:</p>
                                            <p className="font-normal">{secondsLeft}</p>
                                        </span> :
                                        <p>Get code</p>}
                                </button>
                            </div>
                            <ErrorMessage name="vrfCode" component="div" className="text-red-500" />
                        </div>
                        <div>
                            <label className="font-medium">Password</label>
                            <Field
                                className="rounded w-full py-1 px-2 border-2 border-white focus:border-blue-500 focus:outline-none"
                                type="password"
                                name="password"
                            />
                            <ErrorMessage name="password" component="div" className="text-red-500" />
                        </div>
                        <div>
                            <label className="font-medium">Confirm Password</label>
                            <Field
                                className="rounded w-full py-1 px-2 border-2 border-white focus:border-blue-500 focus:outline-none"
                                type="password"
                                name="confirmPassword"
                            />
                            <ErrorMessage name="confirmPassword" component="div" className="text-red-500" />
                        </div>
                        <div className="text-center pt-6">
                            <button
                                className="bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                                type="submit">
                                Register
                            </button>
                        </div>
                    </Form>)}
            </Formik>
        </section>
    );
}

export default RegisterPage;