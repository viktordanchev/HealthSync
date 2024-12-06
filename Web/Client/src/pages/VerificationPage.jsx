import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import useTimer from '../hooks/useTimer';
import { validateVrfCode } from '../services/validationSchemas';
import { useMessage } from '../contexts/MessageContext';
import { useLoading } from '../contexts/LoadingContext';
import SendEmail from '../components/SendEmail';

function VerificationPage() {
    const navigate = useNavigate();
    const { isButtonDisabled, seconds, resetTimer } = useTimer();
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const userEmail = sessionStorage.getItem('email') || '';
    
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

    const resendCode = async () => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'sendVrfCode', userEmail, undefined, 'POST', false);

            if (response.error) {
                showMessage(response.error, 'error');
            } else {
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
        <>
            {!userEmail ? <SendEmail type={'vrfCode'} /> :
                <>
                    <section className="w-80 bg-maincolor rounded-xl shadow-2xl shadow-gray-400 px-8 py-8 sm:w-full">
                        <p className="text-3xl text-center text-white">Verify</p>
                        <hr className="my-4" />
                        <Formik
                            initialValues={{ email: userEmail || '', vrfCode: '' }}
                            validationSchema={validationVrfCodeSchema}
                            onSubmit={submitCode}
                        >
                            <Form className="text-gray-700">
                                <div>
                                    <label className="text-md font-bold">Code</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 focus:outline-none"
                                        type="text"
                                        name="vrfCode"
                                    />
                                    <ErrorMessage name="vrfCode" component="div" className="text-red-500 text-md" />
                                </div>
                                <div className="flex justify-evenly pt-6">
                                    <button
                                        disabled={isButtonDisabled}
                                        className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                                        onClick={resendCode}
                                    >
                                        {isButtonDisabled ? seconds: "Send"}
                                    </button>
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

export default VerificationPage;