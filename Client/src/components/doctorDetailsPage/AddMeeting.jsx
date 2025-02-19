import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../../services/apiRequest';
import { useAuthContext } from '../../contexts/AuthContext';
import Loading from '../Loading';
import { useMessage } from '../../contexts/MessageContext';
import jwtDecoder from '../../services/jwtDecoder';
import { format } from 'date-fns';

function AddMeeting({ doctorId, date, setDate }) {
    const navigate = useNavigate();
    const { showMessage } = useMessage();
    const { isAuthenticated, isStillAuth } = useAuthContext();
    const [isTimeChoosed, setIsTimeChoosed] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [meetingDate, setMeetingDate] = useState('');
    const [meetingTimes, setMeetingTimes] = useState([]);
    
    useEffect(() => {
        const receiveData = async () => {
            const dto = {
                doctorId: doctorId,
                date: format(date, 'yyyy-MM-dd')
            };
            
            try {
                const response = await apiRequest('doctors', 'getAvailableMeetingHours', dto, undefined, 'POST', false);

                setMeetingTimes(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, []);

    const handleConfirmMeetingButton = () => {
        if (isAuthenticated) {
            confirmMeeting();
        } else {
            navigate('/login');
        }
    };

    const handleMeeting = (time) => {
        const [hour, minutes] = time.split(" : ").map(Number);

        date.setHours(hour);
        date.setMinutes(minutes);

        setIsTimeChoosed(true);
        setMeetingDate(`${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()} ${date.getHours()}:${date.getMinutes().toString().padStart(2, '0')}`);
    };

    const confirmMeeting = async () => {
        const isAuth = await isStillAuth();

        if (!isAuth) {
            navigate('/home');
            return;
        }

        const { userId } = jwtDecoder();

        const dto = {
            doctorId: doctorId,
            dateAndTime: format(date, 'yyyy-MM-dd HH:mm'),
            patientId: userId
        };
        
        try {
            const response = await apiRequest('meetings', 'addDoctorMeeting', dto, localStorage.getItem('accessToken'), 'POST', false);

            if (response.error) {
                showMessage(response.error, 'error');
                navigate('/meetings');
            } else {
                showMessage(response.message, 'message');
                setDate(null);
            }
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="min-h-80 bg-opacity-95 border border-white rounded-xl bg-zinc-700 p-4 flex flex-col sm:w-full">
            <div className="w-full text-right">
                <button onClick={() => setDate(null)}>
                    <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                </button>
            </div>
            {isTimeChoosed ?
                <div className="text-white font-bold flex flex-col items-center space-y-3">
                    <p className="text-2xl text-center">Please confirm the meeting date?</p>
                    <p className="text-xl">{meetingDate}</p>
                    <div className="flex justify-evenly space-x-4 w-40 text-xl">
                        <button
                            className="bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                            onClick={() => setIsTimeChoosed(false)}
                        >
                            No
                        </button>
                        <button
                            className="bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                            onClick={handleConfirmMeetingButton}
                        >
                            Yes
                        </button>
                    </div>
                </div>
                : <div className="h-full flex flex-col items-center space-y-2">
                    <p className="font-bold text-white text-xl">Choose meeting time</p>
                    <div className="h-full flex justify-center items-center text-white">
                        {isLoading ? <Loading type={'small'} /> :
                            <div className="flex justify-center flex-wrap">
                                {meetingTimes.map((meetingTime, index) => (
                                    <div
                                        key={index}
                                        onClick={() => handleMeeting(meetingTime)}
                                        className="flex justify-center items-center m-1 text-base font-bold border-2 border-maincolor rounded-xl p-2 cursor-pointer hover:bg-maincolor"
                                    >
                                        {meetingTime}
                                    </div>
                                ))}
                            </div>}
                    </div>
                </div>}
        </div>
    );
}

export default AddMeeting;