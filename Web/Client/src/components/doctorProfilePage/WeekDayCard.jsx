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
            <div className="space-y-3">
                <p className="text-center text-xl">{data.weekDay}</p>
                <div className="flex justify-between">
                    <label className="text-base font-medium text-gray-800">Meeting minutes:</label>
                    <input
                        className="w-1/3 px-1 rounded text-gray-800 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                        type="number"
                    />
                </div>
                <div className="text-center font-medium">
                    <label className="text-base text-gray-800">Working time</label>
                    <div className="flex space-x-2">
                        <div className="w-full">
                            <p>Start</p>
                            <Timepicker />
                        </div>
                        <div className="w-full">
                            <p>End</p>
                            <Timepicker />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default WeekDayCard;