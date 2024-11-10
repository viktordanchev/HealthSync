import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import apiRequest from '../../services/apiRequest';
import useCheckAuth from '../../hooks/useCheckAuth';
import { reviewCommentLength } from '../../constants/constants';

function AddReview({ doctorId }) {
    const navigate = useNavigate();
    const { isAuthenticated, jwtToken } = useCheckAuth();
    const [isVisible, setIsVisible] = useState(false);
    const [message, setMessage] = useState('');
    const [rating, setRating] = useState(1);

    const add = async () => {
        const dto = {
            doctorId: doctorId,
            rating: rating
        };

        const response = await apiRequest('doctor', 'addReview', dto, jwtToken, 'POST', true);

        setMessage(response);
        setIsVisible(false);
        setTimeout(() => { setMessage('') }, 3000);
    };

    return (
        <>
            {message && (
                <p className="absolute top-6 left-1/2 transform -translate-x-1/2 rounded-xl bg-green-500 border border-white text-white text-2xl p-4 flex flex-col justify-between">
                    {message}
                </p>
            )}
            <div>
                <div className="h-full flex flex-col items-center">
                    <p className="font-bold text-xl">Add new rating</p>
                    <div className="h-full w-full flex flex-col justify-center space-y-2">
                        <div className="w-1/2">
                            <p className="font-bold">Rating: {rating}</p>
                            <div className="flex justify-between space-x-3 sm:space-x-2">
                                {Array.from({ length: 5 }, (_, i) => (
                                    <svg
                                        key={i}
                                        className={`w-5 h-5 cursor-pointer ${i === 0 ? 'text-yellow-400' : (i < rating ? 'text-yellow-400' : 'text-gray-300')}`}
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
                        <div className="h-1/2 w-full">
                            <p className="font-bold">Comment</p>
                            <textarea maxLength={reviewCommentLength} className="w-full h-full px-1 resize-none bg-white text-black rounded-xl border-2 focus:outline-none focus:border-blue-500 scrollbar scrollbar-thin scrollbar-thumb-rounded-full scrollbar-track-rounded-full scrollbar-thumb-blue-500 scrollbar-track-gray-200"></textarea>
                        </div>
                    </div>
                </div>
                <button
                    onClick={add}
                    className="bg-blue-500 hover:bg-blue-700 font-bold py-1 rounded focus:outline-none focus:shadow-outline"
                >
                    Add
                </button>
            </div>
        </>
}

export default AddReview;