import React, { useState, useEffect } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import ProfilePhoto from '../components/becomeDoctorPage/ProfilePhoto';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';

function DoctorProfilePage() {
    const [profilePhoto, setProfilePhoto] = useState(null);
    const [doctorData, setDoctorData] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    
    useEffect(() => {
        const receiveData = async () => {
            try {
                const response = await apiRequest('doctors', 'getDoctorInfo', undefined, localStorage.getItem('accessToken'), 'GET', false);

                setDoctorData(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, []);

    const handleSubmit = async (values) => {

    };

    return (
        <section className="mx-20 text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">Doctor Profile</h2>
            {isLoading ? <Loading type={'big'} /> :
                <div className="flex space-x-6">
                    <article className="p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col items-center lg:w-full md:w-full sm:w-full">
                        <div className="flex flex-col items-center space-y-3">
                            <ProfilePhoto
                                setProfilePhoto={setProfilePhoto}
                                currentImage={doctorData.imgUrl}
                            />
                            <div className="flex flex-col items-center text-2xl">
                                <p>{doctorData.firstName}</p>
                                <p>{doctorData.specialty}</p>
                            </div>
                        </div>
                        <hr className="border-e border-white w-full my-3" />
                        <div>
                            <Formik
                                initialValues={{
                                    firstName: doctorData.firstName,
                                    lastName: doctorData.lastName,
                                    contactEmail: doctorData.email || '',
                                    contactPhoneNumber: doctorData.phoneNumber || '',
                                    hospitalId: '',
                                    specialtyId: ''
                                }}
                                
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
                                                    name="contactEmail"
                                                />
                                                <ErrorMessage name="contactEmail" component="div" className="text-red-500 text-md" />
                                            </div>
                                            <div className="w-1/2 sm:w-full">
                                                <label className="text-base font-bold">Phone number</label>
                                                <Field
                                                    className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                                    type="tel"
                                                    name="contactPhoneNumber"
                                                />
                                                <ErrorMessage name="contactPhoneNumber" component="div" className="text-red-500 text-md" />
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
                        </div>
                    </article>
                </div>}
        </section>
    );
}

export default DoctorProfilePage;