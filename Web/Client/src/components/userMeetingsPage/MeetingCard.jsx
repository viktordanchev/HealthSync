import React from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../../services/apiRequest';
import { useLoading } from '../../contexts/LoadingContext';
import { useMessage } from '../../contexts/MessageContext';
import { useAuthContext } from '../../contexts/AuthContext';

function MeetingCard({ meeting, isDeleted, setIsDeleted }) {
    const navigate = useNavigate();
    const { setIsLoading } = useLoading();
    const { showMessage } = useMessage();
    const { isStillAuth } = useAuthContext();

    const handleDelete = async (meetingId) => {
        const isAuth = await isStillAuth();

        if (!isAuth) {
            navigate('/home');
            return;
        }

        try {
            setIsLoading(true);

            const response = await apiRequest('meetings', 'deleteMeeting', meetingId, localStorage.getItem('accessToken'), 'DELETE', false);

            if (response.message) {
                showMessage(response.message, 'message');
                setIsDeleted(!isDeleted);
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="bg-zinc-400 rounded-xl m-2 p-4 bg-opacity-75 shadow-xl shadow-gray-300 space-y-3 sm:m-0 sm:mb-4 sm:w-full">
            <div className="text-center text-xl flex justify-center items-center space-x-2">
                <p className="font-bold">Meeting:</p>
                <p>{meeting.dateAndTime}</p>
            </div>
            <hr className="border-e border-white w-full" />
            <div className="flex space-x-4 sm:flex-col sm:space-x-0 sm:space-y-4">
                <div className="flex flex-col items-center">
                    <img
                        src={meeting.imgUrl ? meeting.imgUrl : '/profile.jpg'}
                        className="object-cover w-24 h-24 rounded-full"
                    />
                    <p className="text-center text-xl">{meeting.name}</p>
                </div>
                <div className="space-y-2 text-center w-64 sm:w-full">
                    <div className="flex flex-col items-center">
                        <p className="font-bold">Hospital</p>
                        <p>{meeting.hospital}</p>
                    </div>
                    <div className="flex flex-col items-center">
                        <p className="font-bold">Hospital Address</p>
                        <p>{meeting.hospitalAddress}</p>
                    </div>
                </div>
            </div>
            <div className="w-full text-end">
                <button onClick={() => handleDelete(meeting.id)}>
                    <FontAwesomeIcon icon={faTrash} className="text-xl" />
                </button>
            </div>
        </div>
    );
}

export default MeetingCard;