import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { addReview } from '../../services/apiRequests/doctors';
import useCheckAuth from '../../hooks/useCheckAuth';
import { jwtDecode } from 'jwt-decode';

function AddReview({ doctorId }) {
    const navigate = useNavigate();
    const [isVisible, setIsVisible] = useState(false);
    const [isAdded, setIsAdded] = useState(false);
    const [rating, setRating] = useState(1);
    const { isAuthenticated } = useCheckAuth();

    const add = async () => {
        const token = sessionStorage.getItem('accessToken');
        const decodedToken = jwtDecode(token);
        const obj = {
            doctorId: doctorId,
            rating: rating,
            reviewer: decodedToken['Name']
        };

        await addReview(obj, token);

        setIsAdded(true);
        setIsVisible(false);
        setTimeout(() => { setIsAdded(false); }, 3000);
    };

    return (
        <>
            {isAdded && (
                <p className="absolute top-6 left-1/2 transform -translate-x-1/2 rounded-xl bg-maincolor text-white text-2xl p-4 flex flex-col justify-between">
                    Added new rating
                </p>
            )}
            {isVisible ?
                <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 rounded-xl bg-maincolor p-4 flex flex-col justify-between space-y-3 sm:w-1/2">
                    <div className="w-full text-right">
                        <button
                            onClick={() => { setIsVisible(false) }}>
                            <FontAwesomeIcon icon={faXmark} className="text-white text-xl" />
                        </button>
                    </div>
                    <div className="flex flex-col items-center">
                        <p className="font-bold text-white text-base mb-1">Rating: {rating}</p>
                        <div className="flex w-full justify-between space-x-3 sm:space-x-2">
                            {Array.from({ length: 5 }, (_, i) => (
                                <svg
                                    key={i}
                                    className={`w-5 h-5 cursor-pointer ${i === 0 ? 'text-yellow-400' : (i < rating ? 'text-yellow-400' : 'text-white')}`}
                                    aria-hidden="true"
                                    xmlns="http://www.w3.org/2000/svg"
                                    fill="currentColor"
                                    viewBox="0 0 22 20"
                                    onClick={() => setRating(i + 1)}
                                >
                                    <path d="M20.924 7.625a1.523 1.523 0 0 0-1.238-1.044l-5.051-.734-2.259-4.577a1.534 1.534 0 0 0-2.752 0L7.365 5.847l-5.051.734A1.535 1.535 0 0 0 1.463 9.2l3.656 3.563-.863 5.031a1.532 1.532 0 0 0 2.226 1.616L11 17.033l4.518 2.375a1.534 1.534 0 0 0 2.226-1.617l-.863-5.03L20.537 9.2a1.523 1.523 0 0 0 .387-1.575Z" />
                                </svg>
                            ))}
                        </div>
                    </div>
                    <button
                        onClick={add}
                        className="bg-blue-500 hover:bg-blue-700 text-white text-xs font-bold py-1 rounded focus:outline-none focus:shadow-outline"
                    >
                        Add
                    </button>
                </div> :
                <button
                    className="text-xs bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded-xl focus:outline-none focus:shadow-outline md:text-base sm:text-base"
                    onClick={() => {
                        if (!isAuthenticated) {
                            navigate('/login');
                        } else {
                            setIsVisible(true);
                        }
                    }}
                >
                    Add
                </button>}
        </>
    );
}

export default AddReview;