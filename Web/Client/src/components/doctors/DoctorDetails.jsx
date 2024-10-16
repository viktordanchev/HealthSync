import React, { useState, useEffect } from 'react';
import { getReviews } from '../../services/apiRequests/doctors';
import Loading from '../Loading';
import ReviewCard from './ReviewCard';

function DoctorDetails({ data }) {
    const [loading, setLoading] = useState(true);
    const [reviews, setReviews] = useState([]);

    useEffect(() => {
        const receiveReviews = async (doctorId) => {
            var response = await getReviews(doctorId);

            if (response.ok) {
                var data = await response.json();
                setReviews(data);
            }

            setLoading(false);
        };

        receiveReviews(data.doctorId);
    }, []);

    return (
        <div className="w-full h-full p-2 flex space-x-4">
            <div className="w-full h-full flex flex-col justify-end">
                <p className="w-full text-center mb-1 text-white">Reviews</p>
                <div className="w-full flex flex-col bg-zinc-700 h-40 space-y-2 overflow-auto rounded-xl">
                    {loading ? <Loading type={'small'} /> :
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
            </div>
            <div className="bg-blue-500 w-full h-full">

            </div>
        </div>
    );
}

export default DoctorDetails;