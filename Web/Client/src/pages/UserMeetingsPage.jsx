import React, { useEffect, useState } from 'react';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';

function UserMeetingsPage() {
    const [isLoading, setIsLoading] = useState(false);
    const [meetings, setMeetings] = useState([]);

    useEffect(() => {
        const receiveMeetings = async () => {
            try {
                setIsLoading(true);

                const response = await apiRequest('account', 'getUserData', undefined, localStorage.getItem('accessToken'), 'GET', false);

                setMeetings(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveMeetings();
    }, []);

    return (
        <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
            <h2 className="text-center text-4xl font-thin underline-thin">My meetings</h2>
            {isLoading ? <Loading type={'big'} /> :
                <article>
                    {meetings.length == 0 ?
                        <div className="text-3xl">You have no active meetings.</div> :
                        <>
                            {meetings.map((meeting) => (
                                <DoctorCard key={meeting.id} doctor={doctor} />
                            ))}
                        </>}
                </article>}
        </section>
    );
}

export default UserMeetingsPage;