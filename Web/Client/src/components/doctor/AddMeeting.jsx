import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../../services/apiRequest';
import useCheckAuth from '../../hooks/useCheckAuth';

function AddMeeting({ doctorId, date, setIsDateChoosed, setMessage }) {
    const navigate = useNavigate();
    const { isAuthenticated, jwtToken, loading } = useCheckAuth();
    const [isTimeChoosed, setIsTimeChoosed] = useState(false);
    const [meetingDate, setMeetingDate] = useState('');
    const [meetingTimes, setMeetingTimes] = useState([]);

    if (!isAuthenticated && !loading) {
        navigate('/login');
    }

    useEffect(() => {
        const getMeetingTimes = async () => {
            const dto = {
                doctorId: doctorId,
                date: date.toISOString()
            };

            try {
                const response = await apiRequest('doctor', 'getAvailableMeetTimes', dto, jwtToken, 'POST', true);

                setMeetingTimes(response);
            } catch (error) {
                console.error(error);
            }
        };

        if (jwtToken) {
            getMeetingTimes();
        }
    }, [loading]);

    const handleMeeting = (time) => {
        const [hour, minutes] = time.split(" : ").map(Number);

        date.setHours(hour);
        date.setMinutes(minutes);

        setIsTimeChoosed(true);
        setMeetingDate(`${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes().toString().padStart(2, '0')}`);
    };

    const confirmMeeting = async () => {
        const dto = {
            doctorId: doctorId,
            date: date
        };

        try {
            const response = await apiRequest('doctor', 'addMeeting', dto, jwtToken, 'POST', true);

            setMessage(response.message);
            setIsDateChoosed(false);

            setTimeout(() => { setMessage(''); }, 3000);
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="min-h-80 bg-opacity-95 border border-white rounded-xl bg-zinc-700 p-4 flex flex-col space-y-4 sm:w-full">
            <div className="w-full text-right">
                <button onClick={() => setIsDateChoosed(false)}>
                    <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                </button>
            </div>
            {isTimeChoosed ?
                <div className="text-white font-bold flex flex-col items-center space-y-3">
                    <p className="text-2xl text-center">Are you sure for this date?</p>
                    <p className="text-xl">{meetingDate}</p>
                    <div className="flex space-x-4 w-40 text-xl">
                        <button
                            onClick={() => setIsTimeChoosed(false)}
                            className="w-1/2 p-1 bg-blue-500 hover:bg-blue-700 rounded focus:outline-none focus:shadow-outline"
                        >
                            No
                        </button>
                        <button
                            onClick={confirmMeeting}
                            className="w-1/2 p-1 bg-blue-500 hover:bg-blue-700 rounded focus:outline-none focus:shadow-outline"
                        >
                            Yes
                        </button>
                    </div>
                </div>
                : <div className="flex flex-col items-center space-y-2">
                    <p className="font-bold text-white text-xl">Choose meeting time</p>
                    <div className="flex justify-center items-center flex-wrap text-white">
                        {meetingTimes.map((meetingTime, index) => (
                            <div
                                key={index}
                                onClick={() => handleMeeting(meetingTime)}
                                className="flex justify-center items-center m-1 text-base font-bold border-2 border-maincolor rounded-xl p-2 cursor-pointer hover:bg-maincolor"
                            >
                                {meetingTime}
                            </div>
                        ))}
                    </div>
                </div>}
        </div>
    );
}

export default AddMeeting;