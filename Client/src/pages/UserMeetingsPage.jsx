import React, { useEffect, useState } from 'react';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';
import MeetingCard from '../components/userMeetingsPage/MeetingCard';

function UserMeetingsPage() {
    const [isLoading, setIsLoading] = useState(true);
    const [isDeleted, setIsDeleted] = useState(false);
    const [meetings, setMeetings] = useState([]);

    useEffect(() => {
        const receiveData = async () => {
            try {
                setIsLoading(true);

                const response = await apiRequest('meetings', 'getUserMeetings', undefined, localStorage.getItem('accessToken'), 'GET', false);

                setMeetings(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
                setIsDeleted(false);
            }
        };

        receiveData();
    }, [isDeleted]);

    return (
        <>
            {isLoading ? <Loading type={'big'} /> :
                <section className="text-gray-700 flex flex-wrap justify-center">
                    {meetings.length == 0 ?
                        <div className="text-3xl text-center font-light md:text-2xl sm:text-xl">You have no active meetings.</div> :
                        <>
                            {meetings.map((meeting) => (
                                <MeetingCard
                                    key={meeting.id}
                                    meeting={meeting}
                                    setIsDeleted={setIsDeleted} />
                            ))}
                        </>}
                </section>}
        </>
    );
}

export default UserMeetingsPage;