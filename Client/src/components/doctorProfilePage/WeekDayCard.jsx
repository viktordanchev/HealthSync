import React, { useState, useEffect } from 'react';
import Timepicker from './Timepicker';

function WeekDayCard({ data, setChanges }) {
    const [isWorkDay, setIsWorkDay] = useState(data.isWorkDay);
    const [minutes, setMinutes] = useState(data.meetingTimeMinutes);
    const [start, setStart] = useState(data.workDayStart);
    const [end, setEnd] = useState(data.workDayEnd);

    useEffect(() => {
        if (isWorkDay !== data.isWorkDay || minutes != data.meetingTimeMinutes ||
            start != data.workDayStart || end != data.workDayEnd) {
            setChanges((prevDays) => {
                if (prevDays.some(day => day.id === data.id)) {
                    return prevDays.map((day) =>
                        day.id === data.id
                            ? {
                                id: data.id,
                                isWorkDay,
                                workDayStart: start,
                                workDayEnd: end,
                                meetingTimeMinutes: minutes
                            }
                            : day
                    );
                } else {
                    return [...prevDays,
                    {
                        id: data.id,
                        isWorkDay,
                        workDayStart: start,
                        workDayEnd: end,
                        meetingTimeMinutes: minutes
                    }
                    ];
                }
            });
        } else {
            setChanges((prevDays) => {
                return prevDays.filter(day => day.id !== data.id);
            });
        }
    }, [isWorkDay, minutes, start, end]);

    return (
        <div className={`p-2 bg-maincolor border border-zinc-500 rounded-xl flex justify-between space-x-3 text-gray-700 text-base font-medium sm:w-full sm:justify-evenly sm:text-sm ${!isWorkDay && 'opacity-65'}`}>
            <div className="flex items-center space-x-2">
                <input className="w-4 h-4 cursor-pointer"
                    type="checkbox"
                    checked={isWorkDay}
                    onChange={() => setIsWorkDay(!isWorkDay)} />
                <label className="text-base">{data.weekDay}</label>
            </div>
            <div className={`flex space-x-3 sm:flex-col sm:space-x-0 ${!isWorkDay && 'pointer-events-none'}`}>
                <div className="flex flex-col">
                    <p className="text-center">Start</p>
                    <Timepicker time={data.workDayStart} setTime={setStart} />
                </div>
                <div className="flex flex-col">
                    <p className="text-center">End</p>
                    <Timepicker time={data.workDayEnd} setTime={setEnd} />
                </div>
                <div className="flex flex-col">
                    <p className="text-center">Minutes</p>
                    <input className="rounded-xl px-2 w-24 h-full text-gray-700 font-bold border-2 border-white appearance-none focus:outline-none focus:border-2 focus:border-blue-500"
                        type="number"
                        value={minutes}
                        onChange={(e) => setMinutes(e.target.value)} />
                </div>
            </div>
        </div>
    );
}

export default WeekDayCard;