import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../services/apiRequest';
import { useLoading } from '../contexts/LoadingContext';
import { useMessage } from '../contexts/MessageContext';

function MeetingCard({ meeting, isDeleted, setIsDeleted }) {
    const { setIsLoading } = useLoading();
    const { showMessage } = useMessage();

    const handleDelete = async (meetingId) => {
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
        <div className="bg-zinc-400 rounded-xl m-2 p-4 w-64 h-80 bg-opacity-75 shadow-xl shadow-gray-300 sm:m-0 sm:mb-4 sm:w-full">
            <div className="w-full text-end">
                <div>
                    {meeting.name}
                </div>
                <div>{meeting.dateAndTime}</div>
                <button onClick={() => handleDelete(meeting.id)}>
                    <FontAwesomeIcon icon={faTrash} className="text-xl" />
                </button>
            </div>
        </div>
    );
}

export default MeetingCard;