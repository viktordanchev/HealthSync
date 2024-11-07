import React, { useRef, useState, useEffect } from 'react';
import { getReviews } from '../../services/apiRequests/doctor';
import Loading from '../Loading';
import ReviewCard from './ReviewCard';
import MeetingsCalendar from './MeetingsCalendar';
import AddReview from './AddReview';

function DoctorDetails({ doctorId }) {
    const scrollContainerRef = useRef(null);
    const [loading, setLoading] = useState(true);
    const [loadingMore, setLoadingMore] = useState(true);
    const [reviews, setReviews] = useState([]);
    const [index, setIndex] = useState(0);
    const [hasMore, setHasMore] = useState(true);

    useEffect(() => {
        const receiveReviews = async (doctorId) => {
            const dto = {
                index: index,
                doctorId: doctorId
            };

            var reviews = await getReviews(dto);

            if (reviews.length != 0) {
                setReviews(prevReviews => [...prevReviews, ...reviews]);
            } else {
                setHasMore(false);
            }

            setLoadingMore(false);
            setLoading(false);
        };

        receiveReviews(doctorId);
    }, [index]);

    useEffect(() => {
        const container = scrollContainerRef.current;

        const handleScroll = () => {
            if (container.scrollTop + container.clientHeight >= container.scrollHeight && hasMore) {
                setIndex(prevIndex => prevIndex + 1);
                setLoadingMore(true);
            }
        };

        container.addEventListener('scroll', handleScroll);

        return () => container.removeEventListener('scroll', handleScroll);
    }, [hasMore]);

    return (
        <div className="h-full w-full flex flex-col items-center mt-6">
            <div className="h-full w-full flex flex-col space-y-2 text-center text-white bg-zinc-700 p-3 rounded-xl md:w-full sm:w-full">
                <p className="font-bold text-xl">Personal Information</p>
                <div className="h-full w-full flex space-x-3">
                    <div className="h-full w-1/2 rounded-xl bg-blue-500 bg-opacity-55 flex items-center justify-center">
                        <p>{doctorInfo ? doctorInfo : 'There is no given information.'}</p>
                    </div>
                    <div className="h-full w-1/2 rounded-xl bg-blue-500 bg-opacity-55 flex items-center justify-center">
                        <p>Hospital</p>
                    </div>
                </div>
            </div>
            <div className="w-full mt-4 flex items-end space-x-4 md:flex-col md:space-y-4 sm:flex-col sm:space-y-4">
                <div className="w-full flex flex-col justify-between md:w-full sm:w-full">
                    <p className="text-center mb-1 text-white text-xl font-bold">Reviews</p>
                    <div
                        ref={scrollContainerRef}
                        className="h-52 flex flex-col bg-zinc-700 space-y-2 p-2 overflow-auto scrollbar scrollbar-thin scrollbar-thumb-rounded-full scrollbar-track-rounded-full scrollbar-thumb-blue-500 scrollbar-track-gray-200 rounded-xl">
                        <AddReview doctorId={doctorId} />
                        {loading ? <Loading type={'small'} /> :
                            <>
                                {reviews.length == 0 ?
                                    <div className="flex justify-center text-base text-white">
                                        <p>Be the first reviewer</p>
                                    </div> :
                                    <>
                                        {reviews.map((review, index) => (
                                            <ReviewCard key={index} data={review} />
                                        ))}
                                        <>
                                            {loadingMore ? <Loading type={'small'} /> :
                                                <div className="flex justify-center text-base text-white px-10">
                                                    <p>No more found</p>
                                                </div>}
                                        </>
                                    </>
                                }
                            </>}
                    </div>
                </div>
                <div className="w-full flex flex-col justify-end md:w-full sm:w-full md:text-sm sm:text-sm">
                    <p className="text-center mb-1 text-white text-xl font-bold">Meetings</p>
                    <MeetingsCalendar doctorId={doctorId} />
                </div>
            </div>
        </div>
    );
}

export default DoctorDetails;