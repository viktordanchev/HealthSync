import React, { useState, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClock } from "@fortawesome/free-solid-svg-icons";

const TimePicker = ({ time, setTime }) => {
    const [hours, setHours] = useState(time.split(':')[0]);
    const [minutes, setMinutes] = useState(time.split(':')[1]);
    const [isOpen, setIsOpen] = useState(false);
    const hoursList = Array.from({ length: 24 }, (_, i) => String(i).padStart(2, "0"));
    const minutesList = Array.from({ length: 60 }, (_, i) => String(i).padStart(2, "0"));

    useEffect(() => {
        setTime(`${hours}:${minutes}:00`);
    }, [hours, minutes]);

    return (
        <div className="relative bg-zinc-700 w-24 rounded-xl cursor-pointer text-white text-sm">
            <div className="py-2 px-3 flex justify-between items-center"
                onClick={() => setIsOpen(!isOpen)}>
                <p className="font-medium">
                    {hours}:{minutes}
                </p>
                <FontAwesomeIcon icon={faClock} />
            </div>
            <div className={`absolute top-11 left-0 h-32 w-full border bg-zinc-700 rounded-xl z-10 flex transition-all duration-300 transform 
            ${isOpen ? 'opacity-100 translate-y-0' : 'opacity-0 translate-y-[-10px] pointer-events-none'}`}>
                <div className="w-1/2 overflow-y-scroll scrollbar-hide text-center">
                    <ul>
                        {hoursList.map((hour) => (
                            <li
                                key={hour}
                                onClick={() => setHours(hour)}
                                className={`py-1 cursor-pointer 
                                ${hour === hours ? "text-blue-400 text-lg font-bold" : "text-white"}`}>
                                {hour}
                            </li>
                        ))}
                    </ul>
                </div>
                <hr className="border-l border-white h-full" />
                <div className="w-1/2 overflow-y-scroll scrollbar-hide text-center">
                    <ul>
                        {minutesList.map((minute) => (
                            <li
                                key={minute}
                                onClick={() => setMinutes(minute)}
                                className={`py-1 cursor-pointer ${minute === minutes ? "text-blue-400 text-lg font-bold" : "text-white"
                                    }`}
                            >
                                {minute}
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    );
};

export default TimePicker;
