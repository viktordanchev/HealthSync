import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';
import DailyMeetings from '../components/workSchedulePage/DailyMeetings';

function WorkSchedulePage() {
    const [meetings, setMeetings] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [openDates, setOpenDates] = useState({});

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

    const handleOpen = (date) => {
        setOpenDates((prevState) => ({
            ...prevState,
            [date]: !prevState[date]
        }));
    };

    return (
        <>
            {isLoading ? <Loading type={'big'} /> :
                <section className="px-64 w-full text-gray-700 space-y-4 flex flex-col justify-center items-center lg:px-32 md:px-8 sm:px-0">
                    {meetings.length === 0 ?
                        <div className="text-3xl text-center md:text-2xl sm:text-xl">There are no active meetings.</div> :
                        <>
                            {meetings.map((meeting, index) => (
                                <div key={index} className="w-full flex flex-col space-y-2">
                                    <div className="cursor-pointer flex justify-between border-b-2 border-zinc-700 text-2xl sm:text-xl"
                                        onClick={() => handleOpen(meeting.date)}>
                                        <p>{meeting.date}</p>
                                        <p><FontAwesomeIcon icon={openDates[meeting.date] ? faCaretUp : faCaretDown} /></p>
                                    </div>
                                    {openDates[meeting.date] && <DailyMeetings key={meeting.date} data={meeting.dailyMeetings} />}
                                </div>
                            ))}
                        </>}
                </section>}
        </>
    );
}

export default WorkSchedulePage;