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
                ${isOpen ? 'fixed bg-opacity-85 z-20 scale-150 sm:justify-normal sm:w-full md:relative sm:relative' : 'relative w-64 h-80 bg-opacity-35 sm:w-full'}`}
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
                        className={`rounded-full object-cover ${isOpen ? 'w-16 h-16 md:w-20 md:h-20 sm:w-24 sm:h-24' : 'w-24 h-24'}`}
                    />
                    <div className={`text-center ${isOpen ? 'text-base sm:text-xl' : 'text-xl'}`}>
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