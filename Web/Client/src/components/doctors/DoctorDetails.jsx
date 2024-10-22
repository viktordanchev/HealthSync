import React, { useRef, useState, useEffect } from 'react';
import { getReviews } from '../../services/apiRequests/doctors';
import Loading from '../Loading';
import ReviewCard from './ReviewCard';
import MeetingsCalendar from './MeetingsCalendar';
import AddReview from './AddReview';

function DoctorDetails({ doctorId, rating }) {
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
                setLoadingMore(false);
            }

            setLoading(false);
        };

        receiveReviews(doctorId);
    }, [index]);

    useEffect(() => {
        const container = scrollContainerRef.current;

        const handleScroll = () => {
            if (container.scrollTop + container.clientHeight >= container.scrollHeight && hasMore) {
                setIndex(prevIndex => prevIndex + 1);
            }
        };

        container.addEventListener('scroll', handleScroll);

        return () => container.removeEventListener('scroll', handleScroll);
    }, [hasMore]);

    return (
        <div className="p-2 flex items-end space-x-4 md:flex-col md:space-y-4 sm:flex-col sm:space-y-4 sm:w-full">
            <div className="w-1/2 text-xl flex flex-col justify-between md:w-full sm:w-full md:text-sm sm:text-sm">
                <p className="text-center mb-1 text-white text-xl">Reviews</p>
                <div
                    ref={scrollContainerRef}
                    className="h-60 flex flex-col bg-zinc-700 space-y-3 p-3 overflow-auto scrollbar scrollbar-thin scrollbar-thumb-rounded-full scrollbar-track-rounded-full scrollbar-thumb-blue-500 scrollbar-track-gray-200 rounded-xl md:h-40 sm:h-40">
                    <AddReview doctorId={doctorId} />
                    {loading ? <Loading type={'small'} /> :
                        <>
                            {reviews.length == 0 ?
                                <div className="flex justify-center text-md text-white px-10">
                                    <p>Be the first reviewer</p>
                                </div> :
                                <>
                                    {reviews.map((review, index) => (
                                        <ReviewCard key={index} data={review} />
                                    ))}
                                    <>
                                        {loadingMore ? <Loading type={'small'} /> :
                                            <div className="flex justify-center text-md text-white px-10">
                                                <p>No more found</p>
                                            </div>}
                                    </>
                                </>
                            }
                        </>}
                </div>
            </div>
            <div className="w-1/2 text-xl flex flex-col justify-end md:w-full sm:w-full md:text-sm sm:text-sm">
                <p className="text-center mb-1 text-white text-xl">Meetings</p>
                <MeetingsCalendar />
            </div>
        </div>
    );
}

export default DoctorDetails;