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
                <section className="h-full flex space-x-6 mx-44 text-gray-700">
                    <article className="h-full w-1/4 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col items-center">
                        <div className="flex flex-col items-center space-y-3">
                            <img
                                src={doctor.imgUrl ? doctor.imgUrl : '/profile.jpg'}
                                className="object-cover w-32 h-32 rounded-full border-4 border-maincolor"
                            />
                            <div className="flex flex-col items-center text-2xl">
                                <p>{doctor.name}</p>
                                <p>{doctor.specialty}</p>
                            </div>
                        </div>
                        <hr class="my-3 w-full bg-white border-1" />
                        <div className="h-full flex flex-col justify-evenly space-y-3">
                            <div className="h-1/2 w-full flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-65 rounded-xl">
                                <p className="font-bold underline text-xl">Personal info</p>
                                <div className="flex flex-row justify-between text-sm">
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
                            <div className="h-1/2 w-full flex flex-col justify-evenly space-y-3 text-center bg-maincolor bg-opacity-65 rounded-xl">
                                <p className="font-bold underline text-xl">Hospital</p>
                                <div className="flex flex-row justify-between text-sm">
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
                    <article className="h-full w-3/4 flex flex-col space-y-6">
                        <div className="h-1/2 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col justify-evenly text-center">
                            <p className="font-bold underline text-xl">Doctor information</p>
                            <p>{doctor.information ? doctor.information : 'No given information.'}</p>
                        </div>
                        <div className="h-1/2 w-full flex space-x-6">
                            <div className="w-1/2 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col justify-evenly space-y-3 text-center">
                                <p className="font-bold underline text-xl">Reviews</p>
                                <AddReview doctorId={doctorId} />
                                <DoctorReviews doctorId={doctorId} />
                            </div>
                            <div className="w-1/2 p-4 bg-zinc-700 bg-opacity-35 shadow-md shadow-gray-400 rounded-xl flex flex-col justify-evenly space-y-3 text-center">
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