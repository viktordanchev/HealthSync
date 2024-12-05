import React, { useState, useEffect } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { validateFirstName, validateLastName } from '../services/validationSchemas';
import { authErrors } from "../constants/errors";
import apiRequest from '../services/apiRequest';
import { useLoading } from '../contexts/LoadingContext';
import { useMessage } from '../contexts/MessageContext';
import { useAuthContext } from '../contexts/AuthContext';
import Loading from '../components/Loading';

function UserSettingsPage() {
    const { update } = useAuthContext();
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const [userData, setUserData] = useState({});
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);

    const validationSchema = Yup.object({
        firstName: validateFirstName,
        lastName: validateLastName,
        currentPassword: Yup.string().test(
            'currentPasswordRequired',
            'Current Password' + authErrors.RequiredField,
            function (value) {
                const { newPassword } = this.parent;
                return !(newPassword && !value);
            }
        ),
        newPassword: Yup.string().test(
            'newPasswordRequired',
            'New Password' + authErrors.RequiredField,
            function (value) {
                const { currentPassword } = this.parent;
                return !(currentPassword && !value);
            }
        ),
        confirmPassword: Yup.string().test(
            'confirmPasswordRequired',
            'Confirm Password' + authErrors.RequiredField,
            function (value) {
                const { newPassword } = this.parent;
                return !(newPassword && !value);
            }
        ).oneOf([Yup.ref('newPassword')], authErrors.PassMatch),
    });

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

    const handleSubmit = async (values, { resetForm }) => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'updateUser', values, localStorage.getItem('accessToken'), 'PUT', true);
            
            if (response.error) {
                showMessage(response.error, 'error');
            } else {
                update(response.token);
            
                setUserData(response);
                showMessage(response.message, 'message');
            
                resetForm({
                    values: {
                        ...values,
                        currentPassword: '',
                        newPassword: '',
                        confirmPassword: '',
                    },
                });
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
                <article className="w-2/3 p-8 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl md:w-full sm:w-full">
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
                        validationSchema={validationSchema}
                        onSubmit={handleSubmit}
                    >
                        <Form className="flex flex-col space-y-2 text-gray-700">
                            <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                <div className="w-1/2 sm:w-full">
                                    <label className="text-md font-bold">First name</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none focus:shadow-md focus:shadow-gray-500"
                                        type="text"
                                        name="firstName"
                                    />
                                </div>
                                <div className="w-1/2 sm:w-full">
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
                            <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                <div className="w-1/2 sm:w-full">
                                    <label className="text-md font-bold">Email</label>
                                    <Field
                                        className="opacity-75 rounded w-full py-1 px-2 text-gray-700 cursor-default focus:outline-none"
                                        type="email"
                                        name="email"
                                        readOnly
                                    />
                                </div>
                                <div className="w-1/2 sm:w-full">
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
                                    className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-white hover:text-blue-500"
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