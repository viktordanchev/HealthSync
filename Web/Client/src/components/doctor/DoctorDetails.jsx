import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import apiRequest from '../../services/apiRequest';
import Loading from '../Loading';
import MeetingsCalendar from './MeetingsCalendar';
import DoctorReviews from './DoctorReviews';
import AddReview from './AddReview';

function DoctorDetails() {
    const navigate = useNavigate();
    const location = useLocation();
    const [loading, setLoading] = useState(true);
    const [doctor, setDoctor] = useState({});
    const doctorId = location.state?.doctorId;

    useEffect(() => {
        const receiveDoctorDetails = async () => {
            const response = await apiRequest('doctor', 'getDoctorDetails', doctorId, undefined, 'POST', false);

            if (response) {
                setDoctor(response);
                setLoading(false);
            }
        };

        if (!doctorId) {
            navigate('/doctors');
        } else {
            receiveDoctorDetails();
        }
    }, []);

    return (
        <>
            {loading ? <Loading type={'big'} /> :
                <section className="h-full flex space-x-6 mx-20 text-gray-700 lg:mx-16 lg:flex-col lg:space-x-0 lg:space-y-6 md:mx-6 md:flex-col md:space-x-0 md:space-y-6 sm:mx-6 sm:flex-col sm:space-x-0 sm:space-y-6">
                    <article className="w-1/4 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col items-center lg:w-full md:w-full sm:w-full">
                        <div className="flex flex-col items-center space-y-3">
                            <div className="w-36 h-36 flex justify-center items-center bg-white rounded-full border-2 border-maincolor">
                                <img
                                    src={doctor.imgUrl ? doctor.imgUrl : '/profile.jpg'}
                                    className="object-cover w-32 h-32 rounded-full"
                                />
                            </div>
                            <div className="flex flex-col items-center text-2xl">
                                <p>{doctor.name}</p>
                                <p>{doctor.specialty}</p>
                            </div>
                        </div>
                        <hr className="border-e border-white w-full my-3" />
                        <div className="h-full flex flex-col justify-evenly space-y-3 lg:flex-row lg:space-y-0 lg:space-x-3 lg:w-full lg:h-40 md:flex-row md:space-y-0 md:space-x-3 md:w-full md:h-52">
                            <div className="h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-65 rounded-xl lg:h-full md:h-full sm:h-full">
                                <p className="font-bold underline text-xl">Personal info</p>
                                <div className="flex flex-row text-sm">
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Phone number</p>
                                        <p>{doctor.phoneNumber ? doctor.phoneNumber : 'Missing'}</p>
                                    </div>
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Email</p>
                                        <p>{doctor.email ? doctor.email : 'Missing'}</p>
                                    </div>
                                </div>
                            </div>
                            <div className="h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-65 rounded-xl lg:h-full md:h-full sm:h-full">
                                <p className="font-bold underline text-xl">Hospital</p>
                                <div className="flex flex-row text-sm">
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
                        <div className="h-1/2 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col justify-evenly text-center space-y-3">
                            <p className="font-bold underline text-xl">Doctor information</p>
                            <p>{doctor.information ? doctor.information : 'No given information.'}</p>
                        </div>
                        <div className="h-1/2 w-full flex space-x-6 md:flex-col md:space-x-0 md:space-y-6 sm:flex-col sm:space-x-0 sm:space-y-6">
                            <div className="w-1/2 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col justify-between space-y-3 text-center md:w-full sm:w-full">
                                <p className="font-bold underline text-xl">Reviews</p>
                                <AddReview doctorId={doctorId} />
                                <DoctorReviews doctorId={doctorId} />
                            </div>
                            <div className="w-1/2 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col justify-evenly space-y-3 text-center md:w-full sm:w-full">
                                <p className="font-bold underline text-xl">Meetings</p>
                                <MeetingsCalendar doctorId={doctorId} />
                            </div>
                        </div>
                    </article>
                </section>}
        </>
    );
}

export default DoctorDetails;