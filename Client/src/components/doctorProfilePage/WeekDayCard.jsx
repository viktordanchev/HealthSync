import React, { useState } from 'react';
import Timepicker from './Timepicker';

function WeekDayCard({ data }) {
    const [isWorkDay, setIsWorkDay] = useState(data.isWorkDay);
    const [minutes, setMinutes] = useState(data.meetingTimeMinutes);

    return (
        <div className="p-2 bg-maincolor border border-zinc-500 rounded-xl flex justify-between space-x-3 text-gray-700 text-base font-medium sm:text-sm">
                <div className="flex items-center space-x-2">
                    <input className="w-4 h-4 cursor-pointer"
                        type="checkbox"
                        checked={isWorkDay}
                        onChange={() => setIsWorkDay(!isWorkDay)} />
                <label className="text-base">{data.weekDay}</label>
                </div>
                <div className="flex space-x-3">
                    <div className="flex flex-col">
                        <p className="text-center">Start</p>
                        <Timepicker time={data.workDayStart} />
                    </div>
                    <div className="flex flex-col">
                        <p className="text-center">End</p>
                        <Timepicker time={data.workDayEnd} />
                    </div>
                    <div className="flex flex-col">
                        <p className="text-center">Minutes</p>
                    <input className="rounded-xl px-2 w-20 h-full text-gray-700 font-bold border-2 border-white appearance-none focus:outline-none focus:border-2 focus:border-blue-500"
                            type="number"
                            value={minutes}
                            onChange={(e) => setMinutes(e.target.value)} />
                    </div>
            </div>
        </div>
    );
}

export default WeekDayCard;