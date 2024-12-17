import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCamera, faXmark } from '@fortawesome/free-solid-svg-icons';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import DropdownMenu from '../components/becomeDoctorPage/DropdownMenu';
import Loading from '../components/Loading';

function BecomeDoctorPage() {
    const [imageSrc, setImageSrc] = useState("/profile.jpg");
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);
    const [hospitals, setHospitals] = useState([]);
    const [specialties, setSpecialties] = useState([]);

    useEffect(() => {
        const receiveUserData = async () => {
            try {
                setIsLoadingOnReceive(true);

                const hospitals = await apiRequest('hospitals', 'getHospitals', undefined, undefined, 'GET', false);
                const specialties = await apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false);

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

    const handleFileChange = (e) => {
        const file = e.target.files[0];

        if (file) {
            const reader = new FileReader();

            reader.onload = (event) => {
                setImageSrc(event.target.result);
            };

            reader.readAsDataURL(file);
        }
    };

    const handleSubmit = (values) => {
        
    };

    return (
        <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">Become part of us!</h2>
            {isLoadingOnReceive ? <Loading type={'big'} /> :
                <article className="w-2/3 p-8 bg-zinc-400 bg-opacity-75 shadow-2xl shadow-gray-400 rounded-xl space-y-4 md:w-full sm:w-full">
                    <div className="flex flex-col items-center">
                        <h2 className="text-lg font-bold">Profile Photo</h2>
                        <div className="relative group">
                            <div className="w-36 h-36 flex justify-center items-center bg-zinc-700 rounded-full sm:w-28 sm:h-28">
                                <img
                                    src={imageSrc}
                                    className="w-32 h-32 object-cover rounded-full sm:w-24 sm:h-24"
                                />
                            </div>
                            <label
                                className="absolute top-0 right-0 text-white bg-zinc-700 w-9 h-9 flex items-center justify-center rounded-full text-base cursor-pointer opacity-0 transition-opacity duration-200 group-hover:opacity-100 sm:opacity-100"
                            >
                                <FontAwesomeIcon icon={faCamera} />
                                <input
                                    type="file"
                                    accept="image/jpeg, image/png, image/jpg"
                                    className="hidden"
                                    onChange={handleFileChange}
                                />
                            </label>
                            <label
                                className="absolute top-0 left-0 text-white bg-zinc-700 w-9 h-9 flex items-center justify-center rounded-full text-xl cursor-pointer opacity-0 transition-opacity duration-200 group-hover:opacity-100 sm:opacity-100"
                                onClick={() => setImageSrc("/profile.jpg")}
                            >
                                <FontAwesomeIcon icon={faXmark} />
                            </label>
                        </div>
                    </div>
                    <Formik
                        initialValues={{
                            firstName: '',
                            lastName: '',
                            email: '',
                            phoneNumber: '',
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