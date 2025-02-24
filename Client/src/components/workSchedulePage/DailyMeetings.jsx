import React from 'react';
import { format } from 'date-fns';

function DailySchedule({ data }) {
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
                    </div>
                </div>
            ))}
        </div>
    );
}

export default DailySchedule;