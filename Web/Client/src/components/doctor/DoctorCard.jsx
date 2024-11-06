import React, { useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import DoctorDetails from './DoctorDetails';

function DoctorCard({ doctor }) {
    const [searchParams, setSearchParams] = useSearchParams();
    const [isOpen, setIsOpen] = useState(false);

    const toggleDetails = () => {
        const newSearchParams = new URLSearchParams(searchParams);

        if (newSearchParams.has('doctor')) {
            newSearchParams.delete('doctor');
        } else {
            newSearchParams.set('doctor', doctor.name);
        }

        setSearchParams(newSearchParams);
        setIsOpen(!isOpen);
    };

    return (
        <>
            <div
                className={`bg-zinc-700 rounded-xl m-2 p-4 flex flex-col justify-between items-center transition-all duration-700 ease-in-out ${isOpen ? 'h-3/4 w-3/4 fixed inset-0 m-auto bg-opacity-85 z-20 sm:relative sm:h-doctorCardSm sm:w-full sm:m-2' : 'relative w-64 h-80 bg-opacity-35 shadow-md shadow-gray-400 sm:w-full'}`}
            >
                {isOpen && (
                    <button
                        onClick={toggleDetails}
                        className="w-full text-right focus:outline-none"
                    >
                        <FontAwesomeIcon icon={faXmark} className="text-white text-2xl" />
                    </button>
                )}
                <div className={`flex flex-col items-center space-y-2 ${isOpen ? 'text-white' : 'text-gray-700'}`}>
                    <img
                        src={doctor.imgUrl ? doctor.imgUrl : '/profile.jpg'}
                        className="rounded-full object-cover w-24 h-24"
                    />
                    <div className="text-center text-xl">
                        <p>{doctor.name}</p>
                        <p className="font-bold">{doctor.specialty}</p>
                    </div>
                </div>
                {!isOpen ?
                    <>
                        <div className="w-full flex justify-evenly text-gray-700">
                            <div className="flex flex-col items-center text-xl">
                                <p>Rating</p>
                                <p className="font-bold">{doctor.rating > 0 ? doctor.rating : 0} / 5</p>
                            </div>
                            <div className="flex flex-col items-center text-xl">
                                <p>Reviews</p>
                                <p className="font-bold">{doctor.totalReviews > 0 ? doctor.totalReviews : 0}</p>
                            </div>
                        </div>
                        <button
                            onClick={toggleDetails}
                            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded focus:outline-none focus:shadow-outline"
                        >
                            Details
                        </button>
                    </> :
                    <DoctorDetails
                        doctorId={doctor.id}
                        doctorName={doctor.name}
                        rating={doctor.rating}
                        hospitalName={doctor.hospital}
                        hospitalAddress={doctor.hospitalAddress}
                    />}
            </div>

            {isOpen && (
                <div
                    className="fixed inset-0 z-10"
                    onClick={toggleDetails}
                ></div>
            )}
        </>
    );
}

export default DoctorCard;