import React, { useState, useEffect } from 'react';
import doctorProfile from '../assets/images/doctor-profile.jpg';
import ProfilePhoto from '../components/becomeDoctorPage/ProfilePhoto';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';

function DoctorProfile() {
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

    return (
        <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">Doctor Profile</h2>
            {isLoading ? <Loading type={'big'} /> :
                <div className="flex space-x-6">
                    <article className="p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col items-center lg:w-full md:w-full sm:w-full">
                        <div className="flex flex-col items-center space-y-3">
                            <ProfilePhoto setProfilePhoto={setProfilePhoto} />
                            <div className="flex flex-col items-center text-2xl">
                                <p>ivanaaaaaaaaaaaaaaaaaaaaaaaaaaaaa</p>
                                <p>ivan</p>
                            </div>
                        </div>
                        <hr className="border-e border-white w-full my-3" />
                        <div className="h-full flex flex-col justify-evenly space-y-3 lg:flex-row lg:space-y-0 lg:space-x-3 lg:w-full lg:h-40 md:flex-row md:space-y-0 md:space-x-3 md:w-full md:h-64 sm:h-96">
                            <div className="h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-100 rounded-xl lg:h-full md:h-full sm:h-full">
                                <p className="font-bold underline text-xl">Personal info</p>
                                <div className="flex flex-row text-sm space-x-3">
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Phone number</p>
                                        <p>Missing</p>
                                    </div>
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Email</p>
                                        <p>Missing</p>
                                    </div>
                                </div>
                            </div>
                            <div className="h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-65 rounded-xl lg:h-full md:h-full sm:h-full">
                                <p className="font-bold underline text-xl">Hospital</p>
                                <div className="flex flex-row text-sm space-x-3">
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Name</p>
                                        <p>Hospital 2</p>
                                    </div>
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Address</p>
                                        <p>Address 2</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </article>
                </div>}
        </section>
    );
}

export default DoctorProfile;