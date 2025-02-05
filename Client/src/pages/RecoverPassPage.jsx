import React from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { useMessage } from '../contexts/MessageContext';
import { useLoading } from '../contexts/LoadingContext';
import { authErrors } from "../constants/errors";
import SendEmail from '../components/SendEmail';

function RecoverPassPage() {
    const navigate = useNavigate();
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const [searchParams] = useSearchParams();
    const token = searchParams.get('token') ? searchParams.get('token').replace(/ /g, '+') : undefined;

    const validationPasswordSchema = Yup.object({
        password: Yup.string()
            .min(6, authErrors.InvalidPass)
            .required('Password' + authErrors.RequiredField),
        confirmPassword: Yup.string()
            .required('Confirm password' + authErrors.RequiredField)
            .oneOf([Yup.ref('password'), null], authErrors.PassMatch)
    });

    const submitPassword = async (values) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'recoverPass', values, undefined, 'POST', false);

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

    return (
        <>
            {!token ? <SendEmail type={'recoverPass'} /> :
                <>
                    <section className="w-80 bg-maincolor rounded-xl shadow-2xl shadow-gray-400 px-8 py-8 sm:w-full">
                        <p className="text-3xl text-center text-white">Recover password</p>
                        <hr className="my-4" />
                        <Formik
                            initialValues={{ password: '', confirmPassword: '', token: token }}
                            validationSchema={validationPasswordSchema}
                            onSubmit={submitPassword}
                        >
                            <Form className="text-gray-700 space-y-2">
                                <div>
                                    <label className="text-md font-bold">Password</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:border-blue-500 focus:outline-none"
                                        type="password"
                                        name="password"
                                    />
                                    <ErrorMessage name="password" component="div" className="text-red-500 text-md" />
                                </div>
                                <div>
                                    <label className="text-md font-bold">Confirm password</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:border-blue-500 focus:outline-none"
                                        type="password"
                                        name="confirmPassword"
                                    />
                                    <ErrorMessage name="confirmPassword" component="div" className="text-red-500 text-md" />
                                </div>
                                <div className="flex justify-evenly pt-6">
                                    <button
                                        className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                                        type="submit"
                                    >
                                        Submit
                                    </button>
                                </div>
                            </Form>
                        </Formik>
                    </section>
                </>}
        </>
    );
}

export default RecoverPassPage;