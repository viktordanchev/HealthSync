import React, { useState, useEffect } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import apiRequest from '../services/apiRequest';
import { useAuthContext } from '../contexts/AuthContext';
import { useLoading } from '../contexts/LoadingContext';
import { useMessage } from '../contexts/MessageContext';
import Loading from '../components/Loading';
import ProfilePhoto from '../components/ProfilePhoto';
import DropdownMenu from '../components/DropdownMenu';
import { authErrors, maxLength } from "../constants/errors";
import { doctorPersonalInfoMaxLength } from '../constants/data';
import WeeklySchedule from '../components/doctorProfilePage/WeeklySchedule';
import DaysoffCalendar from '../components/doctorProfilePage/DaysoffCalendar';

function DoctorProfilePage() {
    const { isStillAuth } = useAuthContext();
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const [profilePhoto, setProfilePhoto] = useState(null);
    const [isPhotoChanged, setIsPhotoChanged] = useState(false);
    const [doctorData, setDoctorData] = useState(null);
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);
    const [hospitals, setHospitals] = useState([]);
    const [specialties, setSpecialties] = useState([]);
    const [personalInfoLenght, setPersonalInfoLenght] = useState(0);

    const validationSchema = Yup.object({
        email: Yup.string()
            .email(authErrors.InvalidEmail),
        personalInformation: Yup.string()
            .max(doctorPersonalInfoMaxLength, maxLength)
    });

    useEffect(() => {
        const receiveData = async () => {
            try {
                const doctorData = await apiRequest('doctors', 'getDoctorInfo', undefined, localStorage.getItem('accessToken'), 'GET', false);
                let hospitals = await apiRequest('hospitals', 'getHospitals', undefined, undefined, 'GET', false);
                let specialties = await apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false);
                
                hospitals = hospitals.filter(h => h.id !== doctorData.hospitalId);
                specialties = specialties.filter(s => s.id !== doctorData.specialtyId);

                setDoctorData(doctorData);
                setProfilePhoto(doctorData.imgUrl);
                setHospitals(hospitals);
                setSpecialties(specialties);

                if (doctorData.personalInformation) {
                    setPersonalInfoLenght(doctorData.personalInformation.length);
                }
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
            setIsLoading(true);

            await new Promise(res => setTimeout(res, 2000));
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    const handlePhotoChange = (photo) => {
        setProfilePhoto(photo);
        setIsPhotoChanged(true);
    };

    return (
        <section className="mx-20 text-gray-700 space-y-4">
            <h1 className="text-center text-4xl font-thin underline-thin">Doctor Profile</h1>
            {isLoadingOnReceive ? <Loading type={'big'} /> :
                <div className="flex space-x-6">
                    <article className="flex lg:w-full md:w-full sm:w-full">
                        <div className="space-y-3 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl">
                            <div className="flex flex-col items-center space-y-3">
                                <ProfilePhoto
                                    changePhoto={(photo) => handlePhotoChange(photo)}
                                    currentImage={doctorData.imgUrl}
                                />
                                <p className="text-2xl">{doctorData.name}</p>
                            </div>
                            <div>
                                <Formik
                                    initialValues={{
                                        contactEmail: doctorData.email || '',
                                        contactPhoneNumber: doctorData.phoneNumber || '',
                                        hospitalId: '',
                                        specialtyId: '',
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
                                            <div>
                                                <label className="font-medium">Choose Hospital</label>
                                                <DropdownMenu
                                                    options={hospitals}
                                                    optionType={doctorData.hospital}
                                                    setSelectedOption={(value) => setFieldValue('hospitalId', value)}
                                                />
                                                <ErrorMessage name="hospitalId" component="div" className="text-red-500" />
                                            </div>
                                            <div>
                                                <label className="font-medium">Choose Specialty</label>
                                                <DropdownMenu
                                                    options={specialties}
                                                    optionType={doctorData.specialty}
                                                    setSelectedOption={(value) => setFieldValue('specialtyId', value)}
                                                />
                                                <ErrorMessage name="specialtyId" component="div" className="text-red-500" />
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
                                                <button
                                                    className={`bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded 
                                        ${dirty || isPhotoChanged ? 'hover:bg-white hover:text-blue-500' : 'opacity-75 cursor-default'}`}
                                                    type="submit"
                                                    onClick={(e) => {
                                                        if (!dirty && !isPhotoChanged) {
                                                            e.preventDefault();
                                                        }
                                                    }}>
                                                    Save changes
                                                </button>
                                            </div>
                                        </Form>)}
                                </Formik>
                            </div>
                        </div>
                        <hr className="border-l border-white h-full mx-3" />
                        <WeeklySchedule weekDays={doctorData.weeklySchedule} />
                        <hr className="border-l border-white h-full mx-3" />
                        <DaysoffCalendar />
                    </article>
                </div>}
        </section>
    );
}

export default DoctorProfilePage;