import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import apiRequest from '../../services/apiRequest';
import Loading from '../Loading';
import MeetingsCalendar from './MeetingsCalendar';
import ReviewsSection from './ReviewsSection';
import AddReview from './AddReview';

function DoctorDetails() {
    const navigate = useNavigate();
    const location = useLocation();
    const [isLoading, setIsLoading] = useState(true);
    const [doctor, setDoctor] = useState({});
    const doctorId = location.state?.doctorId;

    useEffect(() => {
        const receiveDoctorDetails = async () => {
            try {
                const response = await apiRequest('doctors', 'getDoctorDetails', doctorId, undefined, 'POST', false);

                setDoctor(response);
                setIsLoading(false);
            } catch (error) {
                console.error(error);
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
            {isLoading ? <Loading type={'big'} /> :
                <section className="h-full flex space-x-6 mx-20 text-gray-700 lg:mx-16 lg:flex-col lg:space-x-0 lg:space-y-6 md:mx-0 md:flex-col md:space-x-0 md:space-y-6 sm:mx-0 sm:flex-col sm:space-x-0 sm:space-y-6">
                    <article className="w-1/4 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col items-center lg:w-full md:w-full sm:w-full">
                        <div className="flex flex-col items-center space-y-3">
                            <div className="w-32 h-32 flex justify-center items-center bg-zinc-700 rounded-full border-2 border-maincolor">
                                <img
                                    src={doctor.imgUrl ? doctor.imgUrl : '/profile.jpg'}
                                    className="object-cover w-28 h-28 rounded-full"
                                />
                            </div>
                            <div className="flex flex-col items-center text-2xl">
                                <p>{doctor.name}</p>
                                <p>{doctor.specialty}</p>
                            </div>
                        </div>
                        <hr className="border-e border-white w-full my-3" />
                        <div className="h-full flex flex-col justify-evenly space-y-3 lg:flex-row lg:space-y-0 lg:space-x-3 lg:w-full lg:h-40 md:flex-row md:space-y-0 md:space-x-3 md:w-full md:h-64 sm:h-96">
                            <div className="h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-100 rounded-xl lg:h-full md:h-full sm:h-full">
                                <p className="font-bold underline text-xl">Personal info</p>
                                <div className="flex flex-row text-sm space-x-3">
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Phone number</p>
                                        <p>{doctor.contactPhoneNumber ? doctor.contactPhoneNumber : 'Missing'}</p>
                                    </div>
                                    <div className="w-1/2">
                                        <p className="text-base font-bold">Email</p>
                                        <p>{doctor.contactEmail ? doctor.contactEmail : 'Missing'}</p>
                                    </div>
                                </div>
                            </div>
                            <div className="h-1/2 w-full p-2 flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-65 rounded-xl lg:h-full md:h-full sm:h-full">
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
                        <div className="h-1/2 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-evenly text-center space-y-3">
                            <p className="font-bold underline text-xl">Doctor information</p>
                            <p>{doctor.information ? doctor.information : 'No given information.'}</p>
                        </div>
                        <div className="h-1/2 w-full flex space-x-6 md:flex-col md:space-x-0 md:space-y-6 sm:flex-col sm:space-x-0 sm:space-y-6">
                            <div className="w-1/2 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-between space-y-3 text-center md:w-full sm:w-full">
                                <p className="font-bold underline text-xl">Reviews</p>
                                <AddReview doctorId={doctorId} />
                                <ReviewsSection doctorId={doctorId} />
                            </div>
                            <div className="w-1/2 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-evenly space-y-3 text-center md:w-full sm:w-full">
                                <p className="font-bold underline text-xl">Meetings</p>
                                <p className="font-thin">You can schedule an appointment with this doctor on a day that is convenient for you.</p>
                                <MeetingsCalendar doctorId={doctorId} />
                            </div>
                        </div>
                    </article>
                </section>}
        </>
    );
}

export default DoctorDetails;