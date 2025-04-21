import React, { useState, useEffect } from "react";
import doctorProfile from '../../assets/images/doctor-profile.jpg';
import apiRequest from '../../services/apiRequest';
import Loading from '../Loading';

function TopRatedDoctors() {
    const [isImageLoaded, setIsImageLoaded] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [doctors, setDoctors] = useState([]);

    useEffect(() => {
        const receiveData = async () => {
            try {
                await new Promise((resolve) => setTimeout(resolve, 1500));
                const doctors = await apiRequest('doctors', 'getTopDoctors', undefined, undefined, 'GET', false);

                setDoctors(doctors);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, []);

    return (
        <section className="w-full h-full px-32 flex space-x-6 text-gray-700 lg:px-6 md:px-0 md:flex-col md:space-x-0 md:space-y-6 sm:px-0 sm:flex-col sm:space-x-0 sm:space-y-6">
            <article className="w-2/3 flex flex-col items-center min-h-[400px] md:w-full sm:w-full sm:space-y-3">
                <h2 className="text-2xl font-bold">Top Rated Doctors</h2>
                {isLoading ? <Loading type={'big'} /> :
                    <div className="flex gap-6 justify-center w-full sm:flex-col">
                        {doctors.map((doctor, index) => {
                            let translateYClass = "";

                            if (index === 0) translateYClass = "translate-y-12";
                            else if (index === 1) translateYClass = "translate-y-6";
                            else if (index === 2) translateYClass = "translate-y-16";

                            return (
                                <div key={index}
                                    className={`border border-zinc-500 bg-zinc-400 rounded-xl bg-opacity-75 shadow-xl shadow-gray-300 p-4 text-center w-48 transition-transform duration-300 transform ${translateYClass} sm:translate-y-0 sm:w-full`}>
                                    <img className="w-20 h-20 border-2 border-zinc-700 mx-auto rounded-full mb-3 object-cover"
                                        src={isImageLoaded && doctor.imgUrl ? doctor.imgUrl : doctorProfile}
                                        onLoad={() => setIsImageLoaded(true)} />
                                    <div className="flex flex-col space-y-3">
                                        <div className="text-center md:text-base sm:text-base">
                                            <p>{doctor.name}</p>
                                            <p className="font-bold">{doctor.specialty}</p>
                                        </div>
                                        <div className="w-full flex justify-evenly text-gray-700">
                                            <div className="flex flex-col items-center">
                                                <p>Rating</p>
                                                <p className="font-bold">{doctor.rating} / 5</p>
                                            </div>
                                            <div className="flex flex-col items-center">
                                                <p>Reviews</p>
                                                <p className="font-bold">{doctor.totalReviews > 0 ? doctor.totalReviews : 0}</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            );
                        })}
                    </div>}
            </article>
            <article className="w-1/3 p-6 border border-zinc-500 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-center text-center md:w-full sm:w-full">
                <h2 className="text-2xl font-bold mb-4">Are You a Doctor?</h2>
                <p>If you're a doctor looking to elevate your career and connect with more patients, our platform is the right place for you. Join us and grow with a trusted medical community.</p>
                <div className="text-center pt-6">
                    <button
                        className="bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                        type="submit">
                        Join Us
                    </button>
                </div>
            </article>
        </section>
    );
}

export default TopRatedDoctors;