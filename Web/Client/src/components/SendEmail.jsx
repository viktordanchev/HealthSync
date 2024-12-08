import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { validateEmail } from '../services/validationSchemas';
import useTimer from '../hooks/useTimer';
import { useMessage } from '../contexts/MessageContext';
import { useLoading } from '../contexts/LoadingContext';

function SendEmail({ type }) {
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const { secondsLeft, start } = useTimer();
    let action;
    let typeText;

    switch (type) {
        case 'recoverPass':
            action = 'sendRecoverPassEmail';
            typeText = 'Recover password';
            break;
        default:
            throw new Error('Invalid action type');
    }

    const validationEmailSchema = Yup.object().shape({ email: validateEmail });

    const sendEmail = async (values) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', action, values.email, undefined, 'POST', false);

            if (response.error) {
                showMessage(response.error, 'error');
            } else {
                start(60);
                showMessage(response.message, 'message');

                sessionStorage.setItem('email', values.email);
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <section className="w-80 bg-maincolor rounded-xl shadow-2xl shadow-gray-400 px-8 py-8 sm:w-full">
            <div className="text-3xl text-center text-white flex flex-col">
                <p>Send</p>
                <p className="font-thin">{typeText}</p>
            </div>
            <hr className="my-4" />
            <Formik
                initialValues={{ email: '' }}
                validationSchema={validationEmailSchema}
                onSubmit={sendEmail}
            >
                <Form className="text-gray-700">
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
                    <div className="flex justify-evenly pt-6">
                        <button
                            disabled={secondsLeft > 0}
                            className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                            type="submit">
                            {secondsLeft > 0 ? secondsLeft : "Send"}
                        </button>
                    </div>
                </Form>
            </Formik>
        </section>
    );
}

export default SendEmail;