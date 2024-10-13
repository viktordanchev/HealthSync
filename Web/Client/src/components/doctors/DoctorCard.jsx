import React from 'react';

function DoctorCard({ data }) {
    return (
        <div className="bg-gray-700 bg-opacity-35 w-64 h-80 rounded-xl m-2 p-4 flex flex-col justify-between items-center shadow-xl">
            <div className="flex flex-col items-center space-y-2 text-white">
                <img src="/profile.jpg" className="w-24 h-24 rounded-full" />
                <div className="text-center">
                    <p className="text-xl">{data.name}</p>
                    <p className="font-bold text-lg">{data.specialty}</p>
                </div>
            </div>
            <div className="w-full flex justify-evenly text-white">
                <div className="flex flex-col items-center">
                    <p className="text-lg">Raiting</p>
                    <p className="text-xl font-bold">{data.raiting > 0 ? data.raiting : 0} / 5</p>
                </div>
                <div className="flex flex-col items-center">
                    <p className="text-lg">Reviews</p>
                    <p className="text-xl font-bold">{data.reviews > 0 ? data.reviews : 0}</p>
                </div>
            </div>
            <button className="w-24 bg-blue-500 hover:bg-blue-700 text-white font-bold p-1 rounded focus:outline-none focus:shadow-outline">
                More info
            </button>
        </div>
    );
}

export default DoctorCard;