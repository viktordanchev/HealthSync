import React from 'react';
import { format } from 'date-fns';

function ReviewCard({ data }) {
    return (
        <article className="px-4 py-2 text-white bg-blue-500 rounded-xl flex flex-col items-center shadow shadow-blue-500">
            <p>{format(data.date, 'dd.MM.yyyy HH:mm')}</p>
            <hr className="border-e border-white w-full my-1" />
            <div className="flex justify-between w-full space-x-3 sm:flex-col sm:space-y-2">
                <div className="flex flex-col justify-center items-center space-y-1">
                    <p>Rating</p>
                    <div className="flex items-center">
                        {Array.from({ length: 5 }, (_, i) => (
                            <svg key={i} className={`w-4 h-4 ${data.rating >= i + 1 ? 'text-yellow-300' : 'text-gray-300'} ms-1`} aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 22 20">
                                <path d="M20.924 7.625a1.523 1.523 0 0 0-1.238-1.044l-5.051-.734-2.259-4.577a1.534 1.534 0 0 0-2.752 0L7.365 5.847l-5.051.734A1.535 1.535 0 0 0 1.463 9.2l3.656 3.563-.863 5.031a1.532 1.532 0 0 0 2.226 1.616L11 17.033l4.518 2.375a1.534 1.534 0 0 0 2.226-1.617l-.863-5.03L20.537 9.2a1.523 1.523 0 0 0 .387-1.575Z" />
                            </svg>
                        ))}
                    </div>
                </div>
                <div className="flex flex-col justify-center items-center">
                    <p>Reviewer</p>
                    <p className="font-bold text-center">{data.reviewer}</p>
                </div>
            </div>
        </article>
    );
}

export default ReviewCard;
