import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import doctorProfile from '../../assets/images/doctor-profile.jpg';

function DoctorCard({ doctor }) {
    const navigate = useNavigate();
    const [isImageLoaded, setIsImageLoaded] = useState(false);
    const pathDoctorName = doctor.name.toLowerCase().replace(/\s+/g, '-');
    const pathDoctorSpecialty = doctor.specialty.toLowerCase().replace(/\s+/g, '-');

    return (
        <article className="border border-zinc-500 bg-zinc-400 rounded-xl p-4 flex flex-col justify-between items-center space-y-2 text-gray-700 w-64 h-80 bg-opacity-75 shadow-xl shadow-gray-300 text-lg sm:m-0 sm:w-full">
            <img
                className="rounded-full object-cover w-20 h-20 border-2 border-zinc-700"
                src={isImageLoaded && doctor.imgUrl ? doctor.imgUrl : doctorProfile}
                onLoad={() => setIsImageLoaded(true)}/>
            <div className="text-center md:text-base sm:text-base">
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
                className="bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded text-base hover:bg-white hover:text-blue-500"
                onClick={() => navigate(`/doctors/${pathDoctorName}/${pathDoctorSpecialty}`, { state: { doctorId: doctor.id } })}
            >
                Details
            </button>
        </article>
    );
}

export default DoctorCard;