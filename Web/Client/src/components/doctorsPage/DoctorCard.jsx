import React from 'react';
import { useNavigate } from 'react-router-dom';

function DoctorCard({ doctor }) {
    const navigate = useNavigate();
    const pathDoctorName = doctor.name.toLowerCase().replace(/\s+/g, '-');
    const pathDoctorSpecialty = doctor.specialty.toLowerCase().replace(/\s+/g, '-');

    return (
        <article
            className="bg-zinc-400 rounded-xl p-4 flex flex-col justify-between items-center space-y-2 text-gray-700 w-64 h-80 bg-opacity-75 shadow-xl shadow-gray-300 text-lg sm:m-0 sm:w-full"
        >
            <img
                src={doctor.imgUrl ? doctor.imgUrl : '/profile.jpg'}
                className="rounded-full object-cover w-20 h-20"
            />
            <div className="text-center text-lg md:text-base sm:text-base">
                <p>{doctor.name}</p>
                <p className="font-bold">{doctor.specialty}</p>
            </div>
            <div className="w-full flex justify-evenly text-gray-700">
                <div className="flex flex-col items-center">
                    <p>Rating</p>
                    <p className="font-bold">{doctor.rating > 0 ? doctor.rating : 0} / 5</p>
                </div>
                <div className="flex flex-col items-center">
                    <p>Reviews</p>
                    <p className="font-bold">{doctor.totalReviews > 0 ? doctor.totalReviews : 0}</p>
                </div>
            </div>
            <button
                className="bg-blue-500 border-2 border-blue-500 hover:bg-white hover:text-blue-500 text-white font-bold py-1 px-2 rounded"
                onClick={() => navigate(`/doctors/${pathDoctorName}/${pathDoctorSpecialty}`, { state: { doctorId: doctor.id } })}
            >
                Details
            </button>
        </article>
    );
}

export default DoctorCard;