import React from 'react';
import WeekDayCard from './WeekDayCard';

function WeeklySchedule({ weekDays }) {
    return (
        <article className="p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl text-center flex flex-col space-y-4">
            <h2 className="text-3xl font-thin underline-thin text-gray-700 sm:text-2xl">Weekly Schedule</h2>
            <div className="space-y-2">
                {weekDays.map((weekDay) => (
                    <WeekDayCard key={weekDay.id} data={weekDay} />
                ))}
            </div>
            <div>
                <p className="text-xs">Start - Set time working day start</p>
                <p className="text-xs">End - Set time working day end</p>
                <p className="text-xs">Minutes - Set a meeting length in minutes</p>
            </div>
            <div>
                <button
                    className={`bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded`}
                    type="submit">
                    Save changes
                </button>
            </div>
        </article>
    );
}

export default WeeklySchedule;