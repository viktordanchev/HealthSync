﻿import React, { useEffect, useState } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import DropdownMenu from '../components/becomeDoctorPage/DropdownMenu';
import ProfilePhoto from '../components/becomeDoctorPage/ProfilePhoto';
import Loading from '../components/Loading';
import { validateContactEmail, validateHospital, validateSpecialty } from '../services/validationSchemas';

function BecomeDoctorPage() {
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);
    const [hospitals, setHospitals] = useState([]);
    const [specialties, setSpecialties] = useState([]);
    const [userData, setUserData] = useState({});

    const validationSchema = Yup.object({
        email: validateContactEmail,
        hospitalId: validateHospital,
        specialtyId: validateSpecialty
    });

    useEffect(() => {
        const receiveUserData = async () => {
            try {
                setIsLoadingOnReceive(true);

                const userData = await apiRequest('account', 'getUserData', undefined, localStorage.getItem('accessToken'), 'GET', false);
                const hospitals = await apiRequest('hospitals', 'getHospitals', undefined, undefined, 'GET', false);
                const specialties = await apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false);

                setUserData(userData);
                setHospitals(hospitals);
                setSpecialties(specialties);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoadingOnReceive(false);
            }
        };

        receiveUserData();
    }, []);

    const handleSubmit = (values) => {
        console.log(values);
    };

    return (
        <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">Become part of us!</h2>
            {isLoadingOnReceive ? <Loading type={'big'} /> :
                <article className="w-2/3 p-8 bg-zinc-400 bg-opacity-75 shadow-2xl shadow-gray-400 rounded-xl space-y-4 md:w-full sm:w-full">
                    <ProfilePhoto />
                    <Formik
                        initialValues={{
                            firstName: userData.firstName,
                            lastName: userData.lastName,
                            email: userData.email || '',
                            phoneNumber: userData.phoneNumber || '',
                            hospitalId: '',
                            specialtyId: ''
                        }}
                        validationSchema={validationSchema}
                        onSubmit={handleSubmit}
                    >
                        {({ setFieldValue }) => (
                            <Form className="flex flex-col space-y-2 text-gray-700">
                                <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                    <div className="w-1/2 sm:w-full">
                                        <label className="text-base font-bold">First name</label>
                                        <Field
                                            className="opacity-75 rounded w-full py-1 px-2 text-gray-700 border-2 border-white cursor-default focus:outline-none"
                                            type="text"
                                            name="firstName"
                                            readOnly
                                        />
                                    </div>
                                    <div className="w-1/2 sm:w-full">
                                        <label className="text-base font-bold">Last name</label>
                                        <Field
                                            className="opacity-75 rounded w-full py-1 px-2 text-gray-700 border-2 border-white cursor-default focus:outline-none"
                                            type="text"
                                            name="lastName"
                                            readOnly
                                        />
                                    </div>
                                </div>
                                <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                    <div className="w-1/2 sm:w-full">
                                        <label className="text-base font-bold">Contact email</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                            type="email"
                                            name="email"
                                        />
                                        <ErrorMessage name="currentPassword" component="div" className="text-red-500 text-md" />
                                    </div>
                                    <div className="w-1/2 sm:w-full">
                                        <label className="text-base font-bold">Phone number</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                            type="tel"
                                            name="phoneNumber"
                                        />
                                        <ErrorMessage name="newPassword" component="div" className="text-red-500 text-md" />
                                    </div>
                                </div>
                                <div>
                                    <label className="text-base font-bold">Choose Hospital</label>
                                    <DropdownMenu
                                        options={hospitals}
                                        optionType="All Hospitals"
                                        setSelectedOption={(value) => setFieldValue('hospitalId', value)}
                                    />
                                    <ErrorMessage name="hospitalId" component="div" className="text-red-500 text-md" />
                                </div>
                                <div>
                                    <label className="text-base font-bold">Choose Specialty</label>
                                    <DropdownMenu
                                        options={specialties}
                                        optionType="All Specialties"
                                        setSelectedOption={(value) => setFieldValue('specialtyId', value)}
                                    />
                                    <ErrorMessage name="specialtyId" component="div" className="text-red-500 text-md" />
                                </div>
                                <div className="text-center pt-6">
                                    <button
                                        className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                                        type="submit">
                                        Submit
                                    </button>
                                </div>
                            </Form>)}
                    </Formik>
                </article>}
        </section>
    );
}

export default BecomeDoctorPage;