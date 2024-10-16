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
                className={`bg-zinc-700 rounded-xl m-2 p-4 flex flex-col justify-between items-center shadow-xl
                ${isOpen ? 'bg-opacity-85 fixed top-0 left-0 right-0 bottom-0 m-auto w-1/3 h-96 z-50' : 'w-64 h-80 bg-opacity-35'}`}
                style={{
                    transition: 'all 0.7s ease-in-out',
                    transform: isOpen ? 'scale(1.5)' : 'scale(1)'
                }}
            >
                {isOpen && (
                    <button
                        onClick={toggleDetails}
                        className="absolute top-4 right-4 focus:outline-none"
                    >
                        <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                    </button>
                )}
                <div className="flex flex-col items-center space-y-2 text-white">
                    <img
                        src={data.imgUrl ? data.imgUrl : '/profile.jpg'}
                        className={`rounded-full object-cover ${isOpen ? 'w-16 h-16' : 'w-24 h-24'}`}
                    />
                    <div className={`text-center ${isOpen ? 'text-md' : 'text-xl'}`}>
                        <p>{data.name}</p>
                        <p className="font-bold">{data.specialty}</p>
                    </div>
                </div>
                {!isOpen ?
                    <>
                        <div className="w-full flex justify-evenly text-white">
                            <div className="flex flex-col items-center text-xl">
                                <p>Rating</p>
                                <p className="font-bold">{data.raiting > 0 ? data.raiting : 0} / 5</p>
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
                    </> : <DoctorDetails data={{ raiting: data.raiting, doctorId: data.id }} />}
            </div>

            {isOpen && (
                <div
                    className="fixed inset-0 z-40"
                    onClick={toggleDetails}
                ></div>
            )}
        </>
    );
}

export default DoctorCard;