import React from 'react';

function WeeklySchedule() {
    return (
        <div className="space-y-3">
            <h2 className="text-center text-3xl font-thin underline-thin">Weekly Schedule</h2>
            <div className="text-white">
                <div className="bg-zinc-700 bg-opacity-45 p-3 rounded-xl">
                    <label className="inline-flex items-center cursor-pointer">
                        <input type="checkbox" className="sr-only peer" />
                        <div className="relative w-11 h-6 bg-gray-200 peer-focus:outline-none rounded-full peer dark:bg-red-500 peer-checked:after:translate-x-full rtl:peer-checked:after:-translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:start-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600 peer-checked:bg-green-500"></div>
                    </label>
                    <p className="text-center text-xl">Monday</p>
                    <div>
                        <label className="text-base font-bold text-gray-800">Meeting minutes</label>
                        <input
                            className="rounded w-full py-1 px-2 text-gray-700 border-2 border-white focus:outline-none focus:shadow-lg focus:shadow-gray-400 focus:border-maincolor"
                            type="number"
                        />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default WeeklySchedule;