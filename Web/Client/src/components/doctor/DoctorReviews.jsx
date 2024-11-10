import React, { useRef, useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
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
                setReviews(prevReviews => [...prevReviews, ...response]);
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
        <article className="w-1/2 flex flex-col justify-between md:w-full sm:w-full">
            <p className="text-center mb-1 text-white text-lg font-bold md:text-base sm:text-base">Reviews</p>
            <div
                ref={scrollContainerRef}
                className="h-60 flex flex-col bg-zinc-700 space-y-2 p-2 overflow-auto scrollbar scrollbar-thin scrollbar-thumb-rounded-full scrollbar-track-rounded-full scrollbar-thumb-blue-500 scrollbar-track-gray-200 rounded-xl">
                {isVisible && (
                    <div className="fixed inset-0 m-auto z-30 h-2/5 w-1/3 rounded-xl bg-zinc-700 bg-opacity-95 border border-white p-4 flex flex-col justify-between text-white lg:w-2/5 md:w-3/5 sm:h-1/3 sm:w-11/12">
                        <div className="w-full text-right">
                            <button onClick={() => setIsVisible(false)}>
                                <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                            </button>
                        </div>
                        <AddMeeting
                            doctorId={doctorId}
                            date={date}
                        />
                    </div>
                )}
                {loading ? <Loading type={'small'} /> :
                    <>
                        {reviews.length == 0 ?
                            <div className="flex justify-center text-white">
                                <p>Be the first reviewer</p>
                            </div> :
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
        </article>
    );
}

export default DoctorReviews;