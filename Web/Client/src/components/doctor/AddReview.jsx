﻿import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import apiRequest from '../../services/apiRequest';
import useCheckAuth from '../../hooks/useCheckAuth';
import { reviewCommentLength } from '../../constants/constants';

function AddReview({ doctorId }) {
    const navigate = useNavigate();
    const { isAuthenticated, jwtToken } = useCheckAuth();
    const [rating, setRating] = useState(1);
    const [isOpen, setIsOpen] = useState(false);

    const add = async () => {
        const dto = {
            doctorId: doctorId,
            rating: rating
        };

        const response = await apiRequest('doctor', 'addReview', dto, jwtToken, 'POST', true);

        setIsOpen(false);
    };

    return (
        <div className="flex justify-center items-center">
            <div
                onClick={() => {
                    if (isAuthenticated) {
                        setIsOpen(true);
                    } else {
                        navigate('/login');
                    }
                }}
                className={`${isOpen ? 'w-full h-80 rounded-xl bg-maincolor' : 'w-1/2 h-10 cursor-pointer rounded bg-blue-500 text-white text-lg font-bold py-1 text-center border-2 border-blue-500 hover:bg-white hover:text-blue-500'} transition-[width,height] duration-500 ease-in-out`}>
                {!isOpen ? 'Add Review' :
                    <div className="h-full flex flex-col justify-between items-center p-4">
                        <div className="w-full">
                            <p className="text-lg">Add Review</p>
                            <hr className="border-e border-white w-full my-2" />
                        </div>
                        <div className="w-full flex flex-col items-center md:space-y-3 sm:space-y-3">
                            <div className="w-1/3">
                                <p className="font-bold">Rating: {rating}</p>
                                <div className="flex justify-between space-x-3 sm:space-x-2">
                                    {Array.from({ length: 5 }, (_, i) => (
                                        <svg
                                            key={i}
                                            className={`w-6 h-6 cursor-pointer ${i === 0 ? 'text-yellow-400' : (i < rating ? 'text-yellow-400' : 'text-gray-300')}`}
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
                            <div className="w-full">
                                <p className="font-bold">Comment</p>
                                <textarea maxLength={reviewCommentLength} className="h-24 w-full px-1 resize-none bg-white text-black rounded-xl border-2 focus:outline-none focus:border-blue-500 scrollbar scrollbar-thin scrollbar-thumb-rounded-full scrollbar-track-rounded-full scrollbar-thumb-blue-500 scrollbar-track-gray-200"></textarea>
                            </div>
                        </div>
                        <div className="w-full flex justify-evenly">
                            <button
                                onClick={(e) => {
                                    e.stopPropagation();
                                    add();
                                }}
                                className="w-1/4 bg-blue-500 border-2 border-blue-500 hover:bg-white hover:text-blue-500 text-white font-bold py-1 px-2 rounded">
                                Add
                            </button>
                            <button
                                onClick={(e) => {
                                    e.stopPropagation();
                                    setIsOpen(false);
                                }}
                                className="w-1/4 bg-blue-500 border-2 border-blue-500 hover:bg-white hover:text-blue-500 text-white font-bold py-1 px-2 rounded">
                                Close
                            </button>
                        </div>
                    </div>}
            </div>
        </div>
    );
}

export default AddReview;