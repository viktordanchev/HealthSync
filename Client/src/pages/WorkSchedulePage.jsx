import React, { useState, useEffect } from 'react';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';

function WorkSchedulePage() {
    const [meetings, setMeetings] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const receiveData = async () => {
            try {
                const response = await apiRequest('meetings', 'getDoctorMeetings', undefined, localStorage.getItem('accessToken'), 'GET', false);
                
                setMeetings(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, []);

    return (
        <>
            {isLoading ? <Loading type={'big'} /> :
                <section className="text-gray-700 space-y-4 flex flex-col justify-center items-center">
                    {meetings.length === 0 ?
                        <div className="text-3xl text-center">There are no active meetings.</div> :
                        <>
                            {meetings.map((meeting) => (
                                <div className="border-b-2 border-maincolor">{meeting.date}</div>
                            ))}
                        </>}
                </section>}
        </>
    );
}

export default WorkSchedulePage;