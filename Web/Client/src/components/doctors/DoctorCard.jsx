import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import DoctorDetails from './DoctorDetails';

function DoctorCard({ data }) {
    const [isOpen, setIsOpen] = useState(false);

    const toggleDetails = () => {
        setIsOpen(!isOpen);
    };

    return (
        <>
            <div
                className={`bg-zinc-700 rounded-xl m-2 p-4 flex flex-col justify-between items-center shadow-md shadow-gray-400
                transition-all duration-700 ease-in-out
                ${isOpen ? 'fixed top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-opacity-85 z-20 sm:w-full sm:relative sm:top-0 sm:left-0 sm:translate-x-0 sm:translate-y-0' : 'relative w-64 h-80 bg-opacity-35 sm:w-full'}`}
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
                        src={data.imgUrl ? data.imgUrl : '/profile.jpg'}
                        className="rounded-full object-cover w-24 h-24"
                    />
                    <div className="text-center text-xl">
                        <p>{data.name}</p>
                        <p className="font-bold">{data.specialty}</p>
                    </div>
                </div>
                {!isOpen ?
                    <>
                        <div className="w-full flex justify-evenly text-gray-700">
                            <div className="flex flex-col items-center text-xl">
                                <p>Rating</p>
                                <p className="font-bold">{data.rating > 0 ? data.rating : 0} / 5</p>
                            </div>
                            <div className="flex flex-col items-center text-xl">
                                <p>Reviews</p>
                                <p className="font-bold">{data.totalReviews > 0 ? data.totalReviews : 0}</p>
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
                        doctorId={data.id}
                        rating={data.rating}
                        hospitalName={data.hospital}
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