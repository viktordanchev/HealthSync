﻿import React, { useState } from 'react';
import WeekDayCard from './WeekDayCard';
import apiRequest from '../../services/apiRequest';
import { useLoading } from '../../contexts/LoadingContext';
import { useMessage } from '../../contexts/MessageContext';

function WeeklySchedule({ weekDays }) {
    const [changedWeekDays, setChangedWeekDays] = useState([]);
    const { setIsLoading } = useLoading();
    const { showMessage } = useMessage();
    
    const handleSubmit = async () => {
        try {
            setIsLoading(true);

            const response = await apiRequest('doctors', 'updateWeeklySchedule', changedWeekDays, localStorage.getItem('accessToken'), 'POST', false);

            showMessage(response.message, 'message');
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <article className="border border-zinc-500 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl text-center flex flex-col space-y-4">
            <h2 className="text-3xl font-thin underline-thin text-gray-700 sm:text-2xl">Weekly Schedule</h2>
            <div className="flex justify-center flex-wrap gap-2">
                {weekDays.map((weekDay) => (
                    <WeekDayCard
                        key={weekDay.id}
                        data={weekDay}
                        setChanges={setChangedWeekDays} />
                ))}
            </div>
            <div className="text-sm">
                <p>Start - Set time working day start</p>
                <p>End - Set time working day end</p>
                <p>Minutes - Set a meeting length in minutes</p>
            </div>
            <div>
                <button
                    className={`bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded 
                    ${changedWeekDays.length > 0 ? 'hover:bg-white hover:text-blue-500' : 'opacity-75 cursor-default'}`}
                    type="submit"
                    onClick={(e) => {
                        if (changedWeekDays.length === 0) {
                            e.preventDefault();
                        } else {
                            handleSubmit();
                        }
                    }}>
                    Save changes
                </button>
            </div>
        </article>
    );
}

export default WeeklySchedule;