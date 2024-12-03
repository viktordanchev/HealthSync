import React, { useState, useEffect } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { useLoading } from '../contexts/LoadingContext';
import Loading from '../components/Loading';
import { useMessage } from '../contexts/MessageContext';

function UserSettingsPage() {
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const [userData, setUserData] = useState({});
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);

    //const validationSchema = Yup.object({
    //    email: validateEmail,
    //    password: validateLoginPassword
    //});

    useEffect(() => {
        const receiveUserData = async () => {
            try {
                setIsLoadingOnReceive(true);

                const response = await apiRequest('account', 'getUserData', undefined, localStorage.getItem('accessToken'), 'GET', true);

                setUserData(response);

            } catch (error) {
                console.error(error);
            } finally {
                setIsLoadingOnReceive(false);
            }
        };

        receiveUserData();
    }, []);

    const handleSubmit = async (values) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'updateUser', values, localStorage.getItem('accessToken'), 'POST', true);

            if (response.error) {
                showMessage(response.error, 'error');
            } else {
                setUserData(response);
                showMessage(response.message, 'message');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">Change settings</h2>
            {isLoadingOnReceive ? <Loading type={'big'} /> :
                <article className="w-2/3 p-8 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl">
                    <Formik
                        initialValues={{
                            firstName: userData.firstName,
                            lastName: userData.lastName,
                            email: userData.email,
                            phoneNumber: userData.phoneNumber || '',
                            currentPassword: '',
                            newPassword: '',
                            confirmPassword: ''
                        }}
                        onSubmit={handleSubmit}
                    >
                        <Form className="flex flex-col space-y-2 text-gray-700">
                            <div className="flex flex-row space-x-4">
                                <div className="w-1/2">
                                    <label className="text-md font-bold">First name</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none focus:shadow-md focus:shadow-gray-500"
                                        type="text"
                                        name="firstName"
                                    />
                                </div>
                                <div className="w-1/2">
                                    <label className="text-md font-bold">Last name</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none focus:shadow-md focus:shadow-gray-500"
                                        type="text"
                                        name="lastName"
                                    />
                                </div>
                            </div>
                            <ErrorMessage name="firstName" component="div" className="text-red-500 text-md" />
                            <ErrorMessage name="lastName" component="div" className="text-red-500 text-md" />
                            <div className="flex flex-row space-x-4">
                                <div className="w-1/2">
                                    <label className="text-md font-bold">Email</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 cursor-default focus:outline-none"
                                        type="email"
                                        name="email"
                                        readOnly
                                    />
                                </div>
                                <div className="w-1/2">
                                    <label className="text-md font-bold">Phone number</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none focus:shadow-md focus:shadow-gray-500"
                                        type="tel"
                                        name="phoneNumber"
                                    />
                                </div>
                            </div>
                            <ErrorMessage name="email" component="div" className="text-red-500 text-md" />
                            <ErrorMessage name="phoneNumber" component="div" className="text-red-500 text-md" />
                            <div>
                                <label className="text-md font-bold">Current Password</label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none focus:shadow-md focus:shadow-gray-500"
                                    type="password"
                                    name="currentPassword"
                                />
                                <ErrorMessage name="currentPassword" component="div" className="text-red-500 text-md" />
                            </div>
                            <div>
                                <label className="text-md font-bold">New Password</label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none focus:shadow-md focus:shadow-gray-500"
                                    type="password"
                                    name="newPassword"
                                />
                                <ErrorMessage name="newPassword" component="div" className="text-red-500 text-md" />
                            </div>
                            <div>
                                <label className="text-md font-bold">Confirm Password</label>
                                <Field
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none focus:shadow-md focus:shadow-gray-500"
                                    type="password"
                                    name="confirmPassword"
                                />
                                <ErrorMessage name="confirmPassword" component="div" className="text-red-500 text-md" />
                            </div>
                            <div className="text-center pt-6">
                                <button
                                    className="bg-blue-500 border-2 border-blue-500 hover:bg-white hover:text-blue-500 text-white font-bold py-1 px-2 rounded"
                                    type="submit">
                                    Submit
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </article>}
        </section>
    );
}

export default UserSettingsPage;