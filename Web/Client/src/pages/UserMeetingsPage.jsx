import React, { useEffect, useState } from 'react';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';
import MeetingCard from '../components/MeetingCard';
import jwtDecoder from '../services/jwtDecoder';

function UserMeetingsPage() {
    const [isLoading, setIsLoading] = useState(false);
    const [isDeleted, setIsDeleted] = useState(false);
    const [meetings, setMeetings] = useState([]);

    useEffect(() => {
        const receiveMeetings = async () => {
            try {
                setIsLoading(true);

                const { userId } = jwtDecoder();
                
                const response = await apiRequest('meetings', 'getUserMeetings', userId, localStorage.getItem('accessToken'), 'POST', false);

                setMeetings(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveMeetings();
    }, [isDeleted]);

    return (
        <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">My meetings</h2>
            {isLoading ? <Loading type={'big'} /> :
                <article className="flex flex-wrap justify-center">
                    {meetings.length == 0 ?
                        <div className="text-3xl">You have no active meetings.</div> :
                        <>
                            {meetings.map((meeting) => (
                                <MeetingCard
                                    key={meeting.id}
                                    meeting={meeting}
                                    isDeleted={isDeleted}
                                    setIsDeleted={setIsDeleted} />
                            ))}
                        </>}
                </article>}
        </section>
    );
}

export default UserMeetingsPage;