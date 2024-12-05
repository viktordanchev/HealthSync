import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { validateEmail, validateVrfCode } from '../services/validationSchemas';
import useTimer from '../hooks/useTimer';
import { useMessage } from '../contexts/MessageContext';

function VerificationPage({ setIsLoading }) {
    const { showMessage } = useMessage();
    const navigate = useNavigate();
    const { isButtonDisabled, seconds, resetTimer } = useTimer();
    const userEmail = sessionStorage.getItem('email') || '';

    const validationEmailSchema = Yup.object().shape({ email: validateEmail });

    const validationVrfCodeSchema = Yup.object().shape({ vrfCode: validateVrfCode });

    const submitCode = async (values) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'verifyAccount', values, undefined, 'POST', false);

            if (response.error) {
                showMessage(response.error, 'error');
            } else {
                navigate('/home');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    const sendCode = async (email) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'sendVrfCode', values, undefined, 'POST', false);

            if (response.error) {
                showMessage(response.error, 'error');
            } else {
                sessionStorage.setItem('email', email);

                showMessage(response.message, 'message');

                resetTimer();
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <section className="flex items-center justify-center">
            <div className="w-80 bg-maincolor rounded-xl shadow-2xl shadow-gray-400 px-8 py-8 sm:w-full">
                <p className="text-3xl text-center text-white">{userEmail ? "Verify" : "Send verification code"}</p>
                <hr className="my-4" />
                <Formik
                    initialValues={{ email: userEmail || '', vrfCode: '' }}
                    validationSchema={userEmail ? validationVrfCodeSchema : validationEmailSchema}
                    onSubmit={userEmail ? submitCode : (values) => sendCode(values.email)}
                >
                    <Form className="text-gray-700">
                        {userEmail ?
                            <div>
                                <label className="text-md font-bold">Code</label>
                                <Field
                                    className="rounded w-full py-1 px-2 focus:outline-none"
                                    type="text"
                                    name="vrfCode"
                                />
                                <ErrorMessage name="vrfCode" component="div" className="text-red-500 text-md" />
                            </div> :
                            <div>
                                <label className="text-md font-bold">Email</label>
                                <Field
                                    placeholder="user@gmail.com"
                                    className="rounded w-full py-1 px-2 focus:outline-none"
                                    type="email"
                                    name="email"
                                />
                                <ErrorMessage name="email" component="div" className="text-red-500 text-md" />
                            </div>}
                        <div className="flex justify-evenly pt-6">
                            {userEmail ?
                                <button
                                    disabled={isButtonDisabled}
                                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                    type="button"
                                    onClick={() => sendCode(userEmail)}>
                                    {isButtonDisabled ? seconds : "Resend"}
                                </button> : null}
                            <button
                                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                type="submit">
                                {userEmail ? "Submit" : "Send"}
                            </button>
                        </div>
                    </Form>
                </Formik>
            </div>
        </section>
    );
}

export default VerificationPage;