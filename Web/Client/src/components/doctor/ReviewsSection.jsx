import React, { useRef, useState, useEffect } from 'react';
import apiRequest from '../../services/apiRequest';
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

            try {
                const response = await apiRequest('doctor', 'getReviews', dto, undefined, 'POST', false);
                
                if (response.length != 0) {
                    setReviews(prevReviews => [...prevReviews, ...response]);
                } else {
                    setHasMore(false);
                }

                setLoadingMore(false);
                setLoading(false);
            } catch (error) {
                console.error(error);
            }
        };

        receiveReviews(doctorId);
    }, [index]);

    useEffect(() => {
        const container = scrollContainerRef.current;

        const handleScroll = () => {
            if (container.scrollTop + 30 + container.clientHeight >= container.scrollHeight && hasMore) {
                setIndex(prevIndex => prevIndex + 1);
                setLoadingMore(true);
            }
        };

        container.addEventListener('scroll', handleScroll);

        return () => container.removeEventListener('scroll', handleScroll);
    }, [hasMore]);

    return (
        <div
            ref={scrollContainerRef}
            className="h-60 flex-col bg-zinc-700 space-y-2 p-2 overflow-auto scrollbar-thin scrollbar-thumb-rounded scrollbar-track-rounded scrollbar-thumb-zinc-500 scrollbar-track-gray-300 rounded md:h-80 sm:h-80">
            {loading ? <Loading type={'small'} /> :
                <>
                    {reviews.length == 0 ?
                        <p className="text-white text-xl">Be the first reviewer</p> :
                        <>
                            {reviews.map((review) => (
                                <ReviewCard key={review.id} data={review} />
                            ))}
                            <>
                                {loadingMore ? <Loading type={'small'} /> :
                                    <div className="flex justify-center text-white px-10">
                                        <p>No more found</p>
                                    </div>}
                            </>
                        </>
                    }
                </>}
        </div>
    );
}

export default DoctorReviews;