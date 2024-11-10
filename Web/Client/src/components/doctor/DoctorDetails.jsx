import React, { useState, useEffect } from 'react';
import apiRequest from '../../services/apiRequest';
import Loading from '../Loading';
import MeetingsCalendar from './MeetingsCalendar';
import DoctorReviews from './DoctorReviews';

function DoctorDetails({ doctorId }) {
    const [loading, setLoading] = useState(true);
    const [doctor, setDoctor] = useState({});

    useEffect(() => {
        const receiveDoctorDetails = async () => {
            const response = await apiRequest('doctor', 'getDoctorDetails', doctorId, undefined, 'POST', false);

            setDoctor(response);
            setLoading(false);
        };

        receiveDoctorDetails();
    }, []);

    return (
        <>
            {loading ? <Loading type={'big'} /> :
                <div className="h-full w-full flex flex-col items-center space-y-2">
                    <div className="h-full w-full flex flex-col space-y-2 text-center text-white bg-zinc-700 p-3 rounded-xl md:w-full sm:w-full">
                        <p className="font-bold text-lg md:text-base sm:text-base">Personal Information</p>
                        <div className="h-full w-full flex space-x-3 md:flex-col md:space-x-0 md:space-y-3 sm:flex-col sm:space-x-0 sm:space-y-3">
                            <div className="h-full w-1/2 p-2 rounded-xl bg-blue-500 bg-opacity-45 flex items-center justify-center md:w-full sm:w-full">
                                <p>{doctor.information ? doctor.information : 'There is no given information.'}</p>
                            </div>
                            <div className="h-full w-1/4 p-2 rounded-xl bg-blue-500 bg-opacity-45 flex flex-col justify-between md:w-full sm:w-full">
                                <p className="font-bold text-lg md:text-base sm:text-base">Hospital</p>
                                <div className="h-full flex flex-col space-y-2 justify-center md:flex-row md:space-y-0 md:justify-evenly">
                                    <div>
                                        <p className="font-bold underline">Name</p>
                                        <p>{doctor.hospitalName}</p>
                                    </div>
                                    <div>
                                        <p className="font-bold underline">Address</p>
                                        <p>{doctor.hospitalAddress}</p>
                                    </div>
                                </div>
                            </div>
                            <div className="h-full w-1/4 p-2 rounded-xl bg-blue-500 bg-opacity-45 flex flex-col justify-between md:w-full sm:w-full">
                                <p className="font-bold text-lg md:text-base sm:text-base">Contacts</p>
                                <div className="h-full flex flex-col space-y-2 justify-center md:flex-row md:space-y-0 md:justify-evenly">
                                    <div>
                                        <p className="font-bold underline">Phone number</p>
                                        <p>{doctor.phoneNumber ? doctor.phoneNumber : 'Missing'}</p>
                                    </div>
                                    <div>
                                        <p className="font-bold underline">Email</p>
                                        <p>{doctor.workEmail ? doctor.workEmail : 'Missing'}</p>
                                    </div>
                                    <div>
                                        <p className="font-bold underline">Start conversation</p>
                                        <button className="w-1/2 bg-blue-500 hover:bg-blue-700 text-white font-bold py-0 rounded focus:outline-none focus:shadow-outline">
                                            Start
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="w-3/4 flex items-end space-x-4 lg:w-full md:w-full sm:w-full sm:flex-col sm:space-y-4">
                        <DoctorReviews doctorId={doctorId} />
                        <MeetingsCalendar doctorId={doctorId} />
                    </div>
                </div>}
        </>
    );
}

export default DoctorDetails;