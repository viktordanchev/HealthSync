import React from 'react';
import WeekDayCard from './WeekDayCard';

function WeeklySchedule({ weekDays }) {
    return (
        <div className="flex justify-between items-center space-x-6">
            <div className="space-y-4">
                <h2 className="text-center text-3xl font-thin underline-thin text-gray-700">Weekly Schedule</h2>
                <div className="space-y-2">
                    {weekDays.map((weekDay) => (
                        <WeekDayCard key={weekDay.id} data={weekDay} />
                    ))}
                </div>
            </div>
        </div>
    );
}

export default WeeklySchedule;