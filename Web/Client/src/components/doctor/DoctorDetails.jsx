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
                <div className="h-full w-full flex flex-col items-center mt-6">
                    <div className="h-full w-full flex flex-col space-y-2 text-center text-white bg-zinc-700 p-3 rounded-xl md:w-full sm:w-full">
                        <p className="font-bold text-xl">Personal Information</p>
                        <div className="h-full w-full flex space-x-3">
                            <div className="h-full w-1/2 rounded-xl bg-blue-500 bg-opacity-55 flex items-center justify-center">
                                <p>{doctor.information ? doctor.information : 'There is no given information.'}</p>
                            </div>
                            <div className="h-full w-1/2 rounded-xl bg-blue-500 bg-opacity-55 flex items-center justify-center">
                                <p>Hospital</p>
                            </div>
                        </div>
                    </div>
                    <div className="w-full mt-4 flex items-end space-x-4 md:flex-col md:space-y-4 sm:flex-col sm:space-y-4">
                        <DoctorReviews doctorId={doctorId} />
                        <MeetingsCalendar doctorId={doctorId} />
                    </div>
                </div>}
        </>
    );
}

export default DoctorDetails;