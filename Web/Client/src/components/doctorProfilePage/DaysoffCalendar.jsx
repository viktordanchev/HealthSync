import React, { useState, useEffect } from 'react';

function DaysoffCalendar() {
    const [days, setDays] = useState([]);
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [currentYear, setCurrentYear] = useState(new Date().getFullYear());

    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

    useEffect(() => {
        generateDays(currentYear, currentMonth);
    }, [currentYear, currentMonth]);

    const generateDays = (year, month) => {
        const firstDayOfMonth = new Date(year, month, 1);
        const lastDayOfMonth = new Date(year, month + 1, 0);

        const daysInMonth = [];

        const startDay = (firstDayOfMonth.getDay() + 6) % 7;
        for (let i = 0; i < startDay; i++) {
            daysInMonth.push(null);
        }

        for (let date = 1; date <= lastDayOfMonth.getDate(); date++) {
            daysInMonth.push({ date: new Date(year, month, date) });
        }

        setDays(daysInMonth);
    };

    const handlePreviousMonth = () => {
        if (currentMonth === 0) {
            setCurrentMonth(11);
            setCurrentYear(currentYear - 1);
        } else {
            setCurrentMonth(currentMonth - 1);
        }
    };

    const handleNextMonth = () => {
        if (currentMonth === 11) {
            setCurrentMonth(0);
            setCurrentYear(currentYear + 1);
        } else {
            setCurrentMonth(currentMonth + 1);
        }
    };

    return (
        <div className="h-80">
            <div className="h-1/6 flex items-center rounded-t-xl justify-evenly bg-maincolor py-1">
                <button className="w-1/3" onClick={handlePreviousMonth}>Previous</button>
                <div className="w-1/3 text-center flex justify-center space-x-1">
                    <p>{monthNames[currentMonth]}</p>
                    <p>{currentYear}</p>
                </div>
                <button className="w-1/3" onClick={handleNextMonth}>Next</button>
            </div>
            <div className="h-5/6 rounded-b-xl bg-white grid grid-cols-7 gap-2 text-center text-sm p-2">
                {daysOfWeek.map((day, index) => (
                    <div key={index} className="font-bold text-gray-700">{day}</div>
                ))}
                {days.map((day, index) => (
                    <div key={index} className="rounded-full flex items-center justify-center bg-gray-200 cursor-pointer hover:bg-gray-300">
                        <p>{day ? day.date.getDate() : ''}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default DaysoffCalendar;
