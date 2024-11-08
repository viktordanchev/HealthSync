import React, { useRef, useState, useEffect } from 'react';
import apiRequest from '../../services/apiRequest';
import AddReview from './AddReview';
import ReviewCard from './ReviewCard';
import Loading from '../Loading';

function DoctorReviews({ doctorId }) {
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
            
            const response = await apiRequest('doctor', 'getReviews', dto, undefined, 'POST', false);

            if (response.length != 0) {
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
        <article className="w-full flex flex-col justify-between md:w-full sm:w-full">
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
        </article>
    );
}

export default DoctorReviews;