import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { useLoading } from '../contexts/LoadingContext';
import { useMessage } from '../contexts/MessageContext';
import { useAuthContext } from '../contexts/AuthContext';
import Loading from '../components/Loading';
import ProfilePhoto from '../components/ProfilePhoto';
import DropdownMenu from '../components/DropdownMenu';
import { authErrors } from "../constants/errors";

function BecomeDoctorPage() {
    const navigate = useNavigate();
    const { isStillAuth, update } = useAuthContext();
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);
    const [hospitals, setHospitals] = useState([]);
    const [specialties, setSpecialties] = useState([]);
    const [userData, setUserData] = useState({});
    const [profilePhoto, setProfilePhoto] = useState(null);

    const validationSchema = Yup.object({
        email: Yup.string()
            .email(authErrors.InvalidEmail),
        hospitalId: Yup.string()
            .required('Hospital' + authErrors.RequiredField),
        specialtyId: Yup.string()
            .required('Specialty' + authErrors.RequiredField)
    });

    useEffect(() => {
        const receiveData = async () => {
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

        receiveData();
    }, []);

    const handleSubmit = async (values) => {
        const isAuth = await isStillAuth();

        if (!isAuth) {
            return;
        }

        try {
            const formData = new FormData();
            formData.append('profilePhoto', profilePhoto);
            formData.append('contactEmail', values.contactEmail);
            formData.append('contactPhoneNumber', values.contactPhoneNumber);
            formData.append('hospitalId', values.hospitalId);
            formData.append('specialtyId', values.specialtyId);
            
            setIsLoading(true);

            const token = localStorage.getItem('accessToken');
            const response = await fetch('https://localhost:7080/doctors/becomeDoctor', {
                method: 'POST',
                headers: {
                    "Authorization": `Bearer ${token}`,
                },
                body: formData,
            });

            const data = await response.json();

            update(data.token);
            showMessage(data.message, 'message');

            navigate('/home');
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">Become part of us!</h2>
            {isLoadingOnReceive ? <Loading type={'big'} /> :
                <article className="w-2/3 p-8 bg-zinc-400 bg-opacity-75 shadow-2xl shadow-gray-400 rounded-xl space-y-4 md:w-full sm:w-full">
                    <ProfilePhoto changePhoto={setProfilePhoto} />
                    <Formik
                        initialValues={{
                            firstName: userData.firstName,
                            lastName: userData.lastName,
                            contactEmail: userData.email || '',
                            contactPhoneNumber: userData.phoneNumber || '',
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
                                        <label className="font-medium">First name</label>
                                        <Field
                                            className="opacity-75 rounded w-full py-1 px-2 border-2 border-white cursor-default focus:outline-none"
                                            type="text"
                                            name="firstName"
                                            readOnly
                                        />
                                    </div>
                                    <div className="w-1/2 sm:w-full">
                                        <label className="font-medium">Last name</label>
                                        <Field
                                            className="opacity-75 rounded w-full py-1 px-2 border-2 border-white cursor-default focus:outline-none"
                                            type="text"
                                            name="lastName"
                                            readOnly
                                        />
                                    </div>
                                </div>
                                <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                    <div className="w-1/2 sm:w-full">
                                        <label className="font-medium">Contact email</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                            type="email"
                                            name="contactEmail"
                                        />
                                        <ErrorMessage name="contactEmail" component="div" className="text-red-500" />
                                    </div>
                                    <div className="w-1/2 sm:w-full">
                                        <label className="font-medium">Phone number</label>
                                        <Field
                                            className="rounded w-full py-1 px-2 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                            type="tel"
                                            name="contactPhoneNumber"
                                        />
                                        <ErrorMessage name="contactPhoneNumber" component="div" className="text-red-500" />
                                    </div>
                                </div>
                                <div>
                                    <label className="font-medium">Choose Hospital</label>
                                    <DropdownMenu
                                        options={hospitals}
                                        optionType="All Hospitals"
                                        setSelectedOption={(value) => setFieldValue('hospitalId', value)}
                                    />
                                    <ErrorMessage name="hospitalId" component="div" className="text-red-500" />
                                </div>
                                <div>
                                    <label className="font-medium">Choose Specialty</label>
                                    <DropdownMenu
                                        options={specialties}
                                        optionType="All Specialties"
                                        setSelectedOption={(value) => setFieldValue('specialtyId', value)}
                                    />
                                    <ErrorMessage name="specialtyId" component="div" className="text-red-500" />
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