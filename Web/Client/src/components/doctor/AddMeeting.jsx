import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import apiRequest from '../../services/apiRequest';
import useCheckAuth from '../../hooks/useCheckAuth';

function AddMeeting({ doctorId, date }) {
    const navigate = useNavigate();
    const { isAuthenticated, jwtToken, loading } = useCheckAuth();
    const [isChoosed, setIsChoosed] = useState(false);
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

            const response = await apiRequest('doctor', 'getAvailableMeetTimes', dto, jwtToken, 'POST', true);
            setMeetingTimes(response);
        };

        if (jwtToken) {
            getMeetingTimes();
        }
    }, [loading]);

    const handleMeeting = (time) => {
        const [hour, minutes] = time.split(" : ").map(Number);

        date.setHours(hour);
        date.setMinutes(minutes);

        setIsChoosed(true);
        setMeetingDate(`${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes().toString().padStart(2, '0')}`);
    };

    return (
        <>
            {isChoosed ?
                <div className="text-white font-bold flex flex-col items-center space-y-3">
                    <p className="text-2xl text-center">Are you sure for this date?</p>
                    <p className="text-xl">{meetingDate}</p>
                    <div className="flex space-x-4 w-40 text-xl">
                        <button
                            onClick={() => setIsChoosed(false)}
                            className="w-1/2 p-1 bg-blue-500 hover:bg-blue-700 rounded focus:outline-none focus:shadow-outline"
                        >
                            No
                        </button>
                        <button
                            className="w-1/2 p-1 bg-blue-500 hover:bg-blue-700 rounded focus:outline-none focus:shadow-outline"
                        >
                            Yes
                        </button>
                    </div>
                </div>
                : <div className="flex flex-col items-center space-y-4">
                    <p className="font-bold text-white text-xl">Choose meeting time</p>
                    <div className="grid grid-cols-3 gap-2">
                        {meetingTimes && (
                            <>
                                {meetingTimes.map((meetingTime, index) => (
                                    <div
                                        key={index}
                                        onClick={() => handleMeeting(meetingTime)}
                                        className="flex justify-center items-center text-white text-lg font-bold border-2 border-maincolor rounded-xl p-2 cursor-pointer hover:bg-maincolor"
                                    >
                                        {meetingTime}
                                    </div>
                                ))}
                            </>
                        )}
                    </div>
                </div>}
        </>
    );
}

export default AddMeeting;