import React from 'react';
import WeekDayCard from './WeekDayCard';

function WeeklySchedule({ weekDays }) {
    return (
        <div className="space-y-6 w-2/3">
            <h2 className="text-center text-3xl font-thin underline-thin">Weekly Schedule</h2>
            <div className="text-white flex justify-center items-center flex-wrap gap-3">
                <WeekDayCard />
            </div>
        </div>
    );
}

export default WeeklySchedule;