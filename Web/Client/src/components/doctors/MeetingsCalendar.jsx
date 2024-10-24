import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';

const MeetingsCalendar = () => {
    const [currentYear, setCurrentYear] = useState(new Date().getFullYear());
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [selectedDate, setSelectedDate] = useState(null);
    const [days, setDays] = useState([]);

    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const daysOfWeek = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];

    useEffect(() => {
        generateCalendar(currentYear, currentMonth);
    }, [currentYear, currentMonth]);

    const generateCalendar = (year, month) => {
        const firstDayOfMonth = new Date(year, month, 1);
        const daysInMonth = new Date(year, month + 1, 0).getDate();
        const firstDayOfWeek = firstDayOfMonth.getDay();

        let tempDays = [];

        for (let i = 0; i < firstDayOfWeek; i++) {
            tempDays.push(null);
        }

        for (let day = 1; day <= daysInMonth; day++) {
            tempDays.push(day);
        }

        setDays(tempDays);
    };

    const handlePreviousMonth = () => {
        let newMonth = currentMonth - 1;
        if (newMonth < 0) {
            setCurrentMonth(11);
            setCurrentYear(currentYear - 1);
        } else {
            setCurrentMonth(newMonth);
        }
    };

    const handleNextMonth = () => {
        let newMonth = currentMonth + 1;
        if (newMonth > 11) {
            setCurrentMonth(0);
            setCurrentYear(currentYear + 1);
        } else {
            setCurrentMonth(newMonth);
        }
    };

    const handleDayClick = (day) => {
        if (day) {
            const selectedDate = new Date(currentYear, currentMonth, day);
            const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
            setSelectedDate(selectedDate.toLocaleDateString(undefined, options));
        }
    };

    const closeModal = () => {
        setSelectedDate(null);
    };

    return (
        <>
            <div className="bg-white rounded-xl text-xs md:text-base sm:text-base">
                <div className="flex items-center rounded-t-xl justify-evenly bg-zinc-700 py-1">
                    <button onClick={handlePreviousMonth} className="text-white">Previous</button>
                    <h2 className="text-white">{monthNames[currentMonth]} {currentYear}</h2>
                    <button onClick={handleNextMonth} className="text-white">Next</button>
                </div>
                <div className="grid grid-cols-7 gap-1 p-2 text-center sm:gap-2" id="calendar">
                    {daysOfWeek.map((day, index) => (
                        <div key={index} className="text-center">{day}</div>
                    ))}
                    {days.map((day, index) => (
                        <div
                            key={index}
                            onClick={() => handleDayClick(day)}
                            className={`text-center rounded-full cursor-pointer ${day && new Date().getDate() === day && new Date().getFullYear() === currentYear && new Date().getMonth() === currentMonth ? 'bg-blue-500 text-white' : 'bg-gray-200'}`}
                        >
                            {day || ''}
                        </div>
                    ))}
                </div>
            </div>
            {selectedDate && (
                <div className="absolute inset-0 bg-opacity-85 rounded-xl bg-blue-500 p-4 m-10">
                    <div className="w-full text-right">
                        <button onClick={closeModal}>
                            <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                        </button>
                    </div>
                    <p className="text-2xl font-bold">Selected Date</p>
                    <div className="text-xl font-semibold">{selectedDate}</div>
                </div>
            )}
        </>
    );
};

export default MeetingsCalendar;
