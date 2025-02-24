import React, { useState, useEffect } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors } from "../constants/errors";
import apiRequest from '../services/apiRequest';
import { useLoading } from '../contexts/LoadingContext';
import { useMessage } from '../contexts/MessageContext';
import { useAuthContext } from '../contexts/AuthContext';
import Loading from '../components/Loading';

function UserSettingsPage() {
    const { update, isStillAuth } = useAuthContext();
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const [userData, setUserData] = useState({});
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);

    const validationSchema = Yup.object({
        firstName: Yup.string()
            .required('First name' + authErrors.RequiredField),
        lastName: Yup.string()
            .required('Last name' + authErrors.RequiredField),
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
        const receiveData = async () => {
            try {
                const response = await apiRequest('account', 'getUserData', undefined, localStorage.getItem('accessToken'), 'GET', false);

                setUserData(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoadingOnReceive(false);
            }
        };

        receiveData();
    }, []);

    const handleSubmit = async (values, { resetForm }) => {
        const isAuth = await isStillAuth();

        if (!isAuth) {
            return;
        }

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
        <>
            {isLoadingOnReceive ? <Loading type={'big'} /> :
                <section className="w-96 border border-zinc-500 text-gray-700 p-8 bg-zinc-400 bg-opacity-75 shadow-2xl shadow-gray-400 rounded-xl sm:w-full">
                    <Formik
                        initialValues={{
                            firstName: userData.firstName,
                            lastName: userData.lastName,
                            phoneNumber: userData.phoneNumber || '',
                            currentPassword: '',
                            newPassword: '',
                            confirmPassword: ''
                        }}
                        validationSchema={validationSchema}
                        onSubmit={handleSubmit}
                    >
                        {({ dirty }) => (
                            <Form className="flex flex-col space-y-2 text-gray-700">
                                <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                    <div className="w-1/2 sm:w-full">
                                        <label className="font-medium">First name</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                            type="text"
                                            name="firstName"
                                        />
                                    </div>
                                    <div className="w-1/2 sm:w-full">
                                        <label className="font-medium">Last name</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
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
                                        className="opacity-75 rounded w-full py-1 px-2 border-2 border-white cursor-default focus:outline-none"
                                        value={userData.email}
                                        title={userData.email}
                                        readOnly
                                    />
                                </div>
                                <div>
                                    <label className="font-medium">Phone number</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                        type="tel"
                                        name="phoneNumber"
                                    />
                                    <ErrorMessage name="phoneNumber" component="div" className="text-red-500" />
                                </div>
                                <div>
                                    <label className="font-medium">Current Password</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                        type="password"
                                        name="currentPassword"
                                    />
                                    <ErrorMessage name="currentPassword" component="div" className="text-red-500" />
                                </div>
                                <div>
                                    <label className="font-medium">New Password</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                        type="password"
                                        name="newPassword"
                                    />
                                    <ErrorMessage name="newPassword" component="div" className="text-red-500" />
                                </div>
                                <div>
                                    <label className="font-medium">Confirm Password</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                        type="password"
                                        name="confirmPassword"
                                    />
                                    <ErrorMessage name="confirmPassword" component="div" className="text-red-500" />
                                </div>
                                <div className="text-center pt-6">
                                    <button
                                        className={`bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded 
                                        ${dirty ? 'hover:bg-white hover:text-blue-500' : 'opacity-75 cursor-default'}`}
                                        type="submit"
                                        onClick={(e) => {
                                            if (!dirty) {
                                                e.preventDefault();
                                            }
                                        }}                                    >
                                        Submit
                                    </button>
                                </div>
                            </Form>)}
                    </Formik>
                </section>}
        </>
    );
}

export default UserSettingsPage;