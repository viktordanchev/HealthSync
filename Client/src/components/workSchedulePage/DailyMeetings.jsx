import React from 'react';
import { format } from 'date-fns';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMessage } from '@fortawesome/free-solid-svg-icons';
import { useChat } from '../../contexts/ChatContext';

function DailySchedule({ data }) {
    const { openChat } = useChat();

    const handleTextMe = (patientId, patientName) => {
        openChat(patientId, patientName);
    };

    return (
        <div className="flex flex-wrap justify-center bg-maincolor rounded-xl p-4 bg-opacity-25 border border-zinc-500">
            {data.map((meeting) => (
                <div key={meeting.id} className="bg-zinc-400 rounded-xl m-2 p-4 bg-opacity-75 shadow-lg shadow-gray-400 space-y-2 text-lg sm:text-base sm:m-0 sm:mb-4 sm:w-full">
                    <div className="text-center flex justify-center items-center space-x-2">
                        <p className="font-bold">Time:</p>
                        <p>{format(meeting.dateAndTime, 'HH:mm')}</p>
                    </div>
                    <hr className="border-e border-white w-full" />
                    <div className="flex flex-col items-center">
                        <div className="flex space-x-2">
                            <p className="font-bold">Patient:</p>
                            <p>{meeting.patientName}</p>
                        </div>
                        <div className="flex space-x-2">
                            <p className="font-bold">Contact:</p>
                            <p>{meeting.patientPhoneNumber !== null ? meeting.patientPhoneNumber : 'Missing'}</p>
                        </div>
                        <button
                            className="mt-2 group flex justify-between items-center space-x-2 bg-blue-500 border-2 border-blue-500 text-white text-base font-base py-1 px-2 rounded hover:bg-white hover:text-blue-500"
                            onClick={() => handleTextMe(meeting.patientId, meeting.patientName)}>
                            <FontAwesomeIcon icon={faMessage} className="cursor-pointer text-white text-base group-hover:text-blue-500" />
                            <p className="group-hover:text-blue-500">Text me</p>
                        </button>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default DailySchedule;