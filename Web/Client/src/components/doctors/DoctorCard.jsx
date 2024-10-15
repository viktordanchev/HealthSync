import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { getReviews } from '../../services/apiRequests/doctors';
import Loading from '../Loading';
import ReviewCard from './ReviewCard';

function DoctorCard({ data }) {
    const [isOpen, setIsOpen] = useState(false);
    const [loading, setLoading] = useState(true);
    const [reviews, setReviews] = useState([]);

    const toggleMoreInfo = async (doctorId) => {
        setLoading(true);
        setIsOpen(!isOpen);

        if (doctorId) {
            var response = await getReviews(doctorId);

            if (response.ok) {
                var data = await response.json();
                console.log(data);
                setReviews(data);
            }

            setLoading(false);
        }
    };

    return (
        <>
            <div
                className={`bg-zinc-700 bg-opacity-35 w-64 h-80 rounded-xl m-2 p-4 flex flex-col justify-between items-center shadow-xl transition-all duration-500 ease-in-out transform 
                ${isOpen ? 'bg-opacity-85 fixed top-0 left-0 right-0 bottom-0 m-auto w-1/3 h-96 z-50' : 'relative'}`}
                style={{
                    transition: 'all 0.7s ease-in-out',
                    transform: isOpen ? 'scale(1.5)' : 'scale(1)',
                    transitionTimingFunction: 'ease-in-out',
                }}
            >
                {isOpen && (
                    <button
                        onClick={() => toggleMoreInfo('')}
                        className="absolute top-4 right-4 focus:outline-none"
                    >
                        <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                    </button>
                )}
                <div className="flex flex-col items-center space-y-2 text-white">
                    <img
                        src={data.imgUrl ? data.imgUrl : '/profile.jpg'}
                        className={`rounded-full object-cover ${isOpen ? 'w-14 h-14' : 'w-24 h-24'}`}
                    />
                    <div className={`text-center ${isOpen ? 'text-sm' : 'text-xl'}`}>
                        <p>{data.name}</p>
                        <p className="font-bold">{data.specialty}</p>
                    </div>
                </div>
                <div className={`${isOpen ? 'flex flex-col' : 'w-full'}`}>
                    <div className="w-full flex justify-evenly text-white">
                        <div className="flex flex-col items-center">
                            <p className={`${isOpen ? 'text-sm' : 'text-xl'}`}>Rating</p>
                            <p className={`font-bold ${isOpen ? 'text-md' : 'text-xl'}`}>{data.raiting > 0 ? data.raiting : 0} / 5</p>
                        </div>
                        <div className="flex flex-col items-center">
                            <p className={`${isOpen ? 'text-sm' : 'text-xl'}`}>Reviews</p>
                            <p className={`font-bold ${isOpen ? 'text-md' : 'text-xl'}`}>{data.totalReviews > 0 ? data.totalReviews : 0}</p>
                        </div>
                    </div>
                    {isOpen && (<div className="mt-3">
                        <div className="text-center text-white">Reviews</div>
                        <div className="flex flex-col bg-zinc-700 w-60 h-32 overflow-y-auto rounded-xl">
                            {loading ?
                                <div className="basis-full flex items-center justify-center">
                                    <div className="flex space-x-2">
                                        <div className="w-3 h-6 bg-blue-500 animate-grow"></div>
                                        <div className="w-3 h-6 bg-blue-500 animate-grow" style={{ animationDelay: '0.4s' }}></div>
                                        <div className="w-3 h-6 bg-blue-500 animate-grow" style={{ animationDelay: '0.8s' }}></div>
                                    </div>
                                </div> :
                                <>
                                    {reviews.length == 0 ?
                                        <div className="flex-grow flex justify-center items-center text-xl text-white">
                                            There are no reviews!
                                        </div> :
                                        <>
                                            {reviews.map((review, index) => (
                                                <ReviewCard key={index} data={review} />
                                            ))}
                                        </>
                                    }
                                </>}
                        </div>
                    </div>)}
                </div>
                {!isOpen && (
                    <button
                        onClick={() => toggleMoreInfo(data.id)}
                        className="w-24 bg-blue-500 hover:bg-blue-700 text-white font-bold p-1 rounded focus:outline-none focus:shadow-outline"
                    >
                        More info
                    </button>
                )}
            </div>

            {isOpen && (
                <div
                    className="fixed inset-0 bg-black bg-opacity-50 transition-opacity z-40"
                    onClick={() => toggleMoreInfo('')}
                ></div>
            )}
        </>
    );
}

export default DoctorCard;