import React, { useState } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { authErrors, maxLength } from "../../constants/errors";
import { doctorPersonalInfoMaxLength } from '../../constants/data';
import { useAuthContext } from '../../contexts/AuthContext';
import { useMessage } from '../../contexts/MessageContext';
import { useLoading } from '../../contexts/LoadingContext';
import ProfilePhoto from '../../components/ProfilePhoto';
import DropdownMenu from '../../components/DropdownMenu';
import apiRequest from '../../services/apiRequest';

function DoctorInfo({ doctorData, hospitals, specialties }) {
    const { setIsLoading } = useLoading();
    const { isStillAuth } = useAuthContext();
    const { showMessage } = useMessage();
    const [profilePhoto, setProfilePhoto] = useState(doctorData.imgUrl);
    const [isPhotoChanged, setIsPhotoChanged] = useState(false);
    const [personalInfoLenght, setPersonalInfoLenght] = useState(doctorData.personalInformation ? doctorData.personalInformation.length : 0);
    
    const validationSchema = Yup.object({
        email: Yup.string()
            .email(authErrors.InvalidEmail),
        personalInformation: Yup.string()
            .max(doctorPersonalInfoMaxLength, maxLength)
    });

    const handleSubmit = async (values, { resetForm }) => {
        const isAuth = await isStillAuth();
        if (!isAuth) return;

        try {
            setIsLoading(true);

            const updatedValues = {
                ...values,
                profileImage: ''
            };

            if (profilePhoto) {
                const formData = new FormData();
                formData.append('profileImage', profilePhoto);

                const formDataResponse = await fetch(`${import.meta.env.VITE_API_URL}/doctors/updateProfileImage`, {
                    method: 'POST',
                    headers: {
                        "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
                    },
                    body: formData,
                });

                const data = await formDataResponse.json();
                updatedValues.profileImage = data.imageUrl;
            }

            const response = await apiRequest('doctors', 'updateProfileInfo', values, localStorage.getItem('accessToken'), 'POST', false);

            showMessage(response.message, 'message');
        } catch (error) {
            console.error(error);
        } finally {
            resetForm();
            setIsPhotoChanged(false);
            setIsLoading(false);
        }
    };

    const handlePhotoChange = (photo) => {
        setProfilePhoto(photo);
        setIsPhotoChanged(true);
    };

    return (
        <article className="w-2/3 border border-zinc-500 space-y-3 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl lg:w-full md:w-full sm:w-full sm:text-sm">
            <div className="flex flex-col items-center">
                <ProfilePhoto
                    changePhoto={(photo) => handlePhotoChange(photo)}
                    currentImage={profilePhoto}
                />
                <p className="text-2xl sm:text-xl">{doctorData.name}</p>
            </div>
            <div>
                <Formik
                    initialValues={{
                        contactEmail: doctorData.email || '',
                        contactPhoneNumber: doctorData.phoneNumber || '',
                        hospitalId: doctorData.hospitalId || '',
                        specialtyId: doctorData.specialtyId || '',
                        personalInformation: doctorData.personalInformation || ''
                    }}
                    validationSchema={validationSchema}
                    onSubmit={handleSubmit}
                >
                    {({ dirty, setFieldValue }) => (
                        <Form className="flex flex-col space-y-2 text-gray-700">
                            <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                <div className="w-1/2 sm:w-full">
                                    <label className="font-medium">Contact email</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                        type="email"
                                        name="contactEmail"
                                    />
                                    <ErrorMessage name="contactEmail" component="div" className="text-red-500" />
                                </div>
                                <div className="w-1/2 sm:w-full">
                                    <label className="font-medium">Phone number</label>
                                    <Field
                                        className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                        type="tel"
                                        name="contactPhoneNumber"
                                    />
                                    <ErrorMessage name="contactPhoneNumber" component="div" className="text-red-500" />
                                </div>
                            </div>
                            <div className="flex flex-row space-x-4 sm:flex-col sm:space-x-0 sm:space-y-2">
                                <div className="w-1/2 sm:w-full">
                                    <label className="font-medium">Choose Hospital</label>
                                    <DropdownMenu
                                        classes={"rounded"}
                                        options={hospitals}
                                        optionType={doctorData.hospital}
                                        setSelectedOption={(value) => setFieldValue('hospitalId', value.id)}
                                    />
                                    <ErrorMessage name="hospitalId" component="div" className="text-red-500" />
                                </div>
                                <div className="w-1/2 sm:w-full">
                                    <label className="font-medium">Choose Specialty</label>
                                    <DropdownMenu
                                        classes={"rounded"}
                                        options={specialties}
                                        optionType={doctorData.specialty}
                                        setSelectedOption={(value) => setFieldValue('specialtyId', value.id)}
                                    />
                                    <ErrorMessage name="specialtyId" component="div" className="text-red-500" />
                                </div>
                            </div>
                            <div>
                                <label className="font-medium">Personal information</label>
                                <div>
                                    <Field
                                        className="h-32 rounded w-full py-1 px-2 text-gray-700 border-2 border-white overflow-hidden focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                                        as="textarea"
                                        name="personalInformation"
                                        maxLength={doctorPersonalInfoMaxLength}
                                        onChange={(e) => {
                                            setPersonalInfoLenght(e.target.value.length);
                                            setFieldValue('personalInformation', e.target.value);
                                        }}
                                    />
                                    <p className="text-right text-sm">{personalInfoLenght}/{doctorPersonalInfoMaxLength}</p>
                                </div>
                                <ErrorMessage name="personalInformation" component="div" className="text-red-500" />
                            </div>
                            <div className="text-center pt-6">
                                <button className={`bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded 
                                        ${dirty || isPhotoChanged ? 'hover:bg-white hover:text-blue-500' : 'opacity-75 cursor-default pointer-events-none'}`}
                                    type="submit">
                                    Save changes
                                </button>
                            </div>
                        </Form>)}
                </Formik>
            </div>
        </article>
    );
}

export default DoctorInfo;