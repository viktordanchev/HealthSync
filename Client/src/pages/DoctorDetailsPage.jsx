﻿import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMessage } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';
import MeetingsCalendar from '../components/doctorDetailsPage/MeetingsCalendar';
import ReviewsSection from '../components/doctorDetailsPage/ReviewsSection';
import AddReview from '../components/doctorDetailsPage/AddReview';
import doctorProfile from '../assets/images/doctor-profile.jpg';
import { useChat } from '../contexts/ChatContext';
import { useAuthContext } from '../contexts/AuthContext';

function DoctorDetailsPage() {
    const navigate = useNavigate();
    const location = useLocation();
    const { openChat } = useChat();
    const { isAuthenticated } = useAuthContext();
    const [isLoading, setIsLoading] = useState(true);
    const [isImageLoaded, setIsImageLoaded] = useState(false);
    const [doctor, setDoctor] = useState({});
    const doctorId = location.state?.doctorId;

    useEffect(() => {
        const receiveData = async () => {
            try {
                const response = await apiRequest('doctors', 'getDetails', doctorId, undefined, 'POST', false);

                setDoctor(response);
                setIsLoading(false);
            } catch (error) {
                console.error(error);
            }
        };

        if (!doctorId) {
            navigate('/doctors');
        } else {
            receiveData();
        }
    }, []);

    const handleTextMe = () => {
        if (isAuthenticated) {
            openChat(location.state?.doctorIdentityId, doctor.name);
        } else {
            navigate('/login');
        }
    };

    return (
        <>
            {isLoading ? <Loading type={'big'} /> :
                <section className="flex space-x-6 mx-20 text-gray-700 lg:mx-16 lg:flex-col lg:space-x-0 lg:space-y-6 md:mx-0 md:flex-col md:space-x-0 md:space-y-6 sm:mx-0 sm:flex-col sm:space-x-0 sm:space-y-6">
                    <article className="w-1/4 border border-zinc-500 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col items-center lg:w-full md:w-full sm:w-full">
                        <div className="flex flex-col items-center space-y-3">
                            <img
                                className="object-cover w-28 h-28 border-2 border-zinc-700 rounded-full"
                                src={isImageLoaded && doctor.imgUrl ? doctor.imgUrl : doctorProfile}
                                onLoad={() => setIsImageLoaded(true)}
                            />
                            <div className="flex flex-col items-center text-2xl">
                                <p>{doctor.name}</p>
                                <p>{doctor.specialty}</p>
                            </div>
                            <button
                                className="group flex justify-between items-center space-x-2 bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                                onClick={handleTextMe}>
                                <FontAwesomeIcon icon={faMessage} className="cursor-pointer text-white text-base group-hover:text-blue-500" />
                                <p className="group-hover:text-blue-500">Text me</p>
                            </button>
                        </div>
                        <hr className="border-e border-white w-full my-3" />
                        <div className="h-full flex flex-col justify-evenly space-y-3 lg:flex-row lg:space-y-0 lg:space-x-3 lg:w-full lg:h-40 md:flex-row md:space-y-0 md:space-x-3 md:w-full md:h-64 sm:h-96">
                            <div className="min-h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center border border-zinc-500 bg-opacity-65 bg-maincolor rounded-xl lg:h-full md:h-full sm:h-full">
                                <p className="font-bold underline text-xl">Personal info</p>
                                <div className="flex flex-row text-sm space-x-3">
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Phone number</p>
                                        <p>{doctor.contactPhoneNumber ? doctor.contactPhoneNumber : 'Missing'}</p>
                                    </div>
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Email</p>
                                        <p className="break-words">{doctor.contactEmail ? doctor.contactEmail : 'Missing'}</p>
                                    </div>
                                </div>
                            </div>
                            <div className="min-h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center bg-maincolor border border-zinc-500 bg-opacity-65 rounded-xl lg:h-full md:h-full sm:h-full">
                                <p className="font-bold underline text-xl">Hospital</p>
                                <div className="flex flex-row text-sm space-x-3">
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Name</p>
                                        <p>{doctor.hospitalName}</p>
                                    </div>
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Address</p>
                                        <p>{doctor.hospitalAddress}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </article>
                    <article className="h-full w-3/4 flex flex-col space-y-6 lg:w-full md:w-full sm:w-full">
                        <div className="h-1/2 border border-zinc-500 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-evenly text-center space-y-3">
                            <p className="font-bold underline text-xl">Doctor information</p>
                            <p>{doctor.information ? doctor.information : 'No given information.'}</p>
                        </div>
                        <div className="h-1/2 w-full flex space-x-6 md:flex-col md:space-x-0 md:space-y-6 sm:flex-col sm:space-x-0 sm:space-y-6">
                            <div className="w-1/2 border border-zinc-500 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-between space-y-3 text-center md:w-full sm:w-full">
                                <p className="font-bold underline text-xl">Reviews</p>
                                <AddReview doctorId={doctorId} />
                                <ReviewsSection doctorId={doctorId} />
                            </div>
                            <div className="w-1/2 border border-zinc-500 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-evenly space-y-3 text-center md:w-full sm:w-full">
                                <p className="font-bold underline text-xl">Meetings</p>
                                <div>
                                    <MeetingsCalendar doctorId={doctorId} />
                                    <p className="text-sm">You can schedule an appointment with this doctor on a day that is convenient for you.</p>
                                </div>
                            </div>
                        </div>
                    </article>
                </section>}
        </>
    );
}

export default DoctorDetailsPage;