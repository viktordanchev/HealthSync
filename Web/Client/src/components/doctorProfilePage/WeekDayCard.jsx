import React from 'react';
import Timepicker from './Timepicker';

function WeekDayCard({ data }) {
    return (
        <div className="w-56 bg-zinc-700 bg-opacity-45 p-3 rounded-xl">
            <label className="inline-flex items-center cursor-pointer">
                <input
                    className="sr-only peer"
                    type="checkbox"
                    checked={data.isWorkDay}
                    onChange={(e) => setIsChecked(e.target.checked)}
                />
                <div className="relative w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer dark:bg-red-500 peer-checked:after:translate-x-full rtl:peer-checked:after:-translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:start-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-green-500"></div>
            </label>
            <p className="text-center text-xl">{data.weekDay}</p>
            <div>
                <label className="text-base font-bold text-gray-800">Meeting minutes</label>
                <input
                    className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                    type="number"
                />
            </div>
            <div>
                <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select time:</label>
                <div className="flex space-x-2">
                    <Timepicker />
                    <Timepicker />
                </div>
            </div>
        </div>
    );
}

export default WeekDayCard;