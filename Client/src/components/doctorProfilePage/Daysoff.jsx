import React from 'react';
import DaysoffCalendar from './DaysoffCalendar';

function Daysoff() {
    return (
        <article className="w-1/3 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-between space-y-4 text-center">
            <h2 className="text-3xl font-thin underline-thin text-gray-700 sm:text-2xl">Daysoff</h2>
            <div>
                <DaysoffCalendar />
                <p className="text-sm">Here you can set your days off or adjust your working hours.</p>
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

export default Daysoff;