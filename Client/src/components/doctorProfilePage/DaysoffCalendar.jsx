import React, { useState, useEffect } from 'react';

function DaysoffCalendar() {
    const [days, setDays] = useState([]);
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [daysOff, setDaysOff] = useState([]);
    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    
    useEffect(() => {
        generateDays(currentMonth, new Date().getFullYear());
    }, [currentMonth]);

    const generateDays = (month, year) => {
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
        } else {
            setCurrentMonth(currentMonth - 1);
        }
    };

    const handleNextMonth = () => {
        if (currentMonth === 11) {
            setCurrentMonth(0);
        } else {
            setCurrentMonth(currentMonth + 1);
        }
    };

    const handleSelectDate = (date) => {
        setDaysOff(prev =>
            prev.includes(date)
                ? prev.filter(d => d !== date)
                : [...prev, date]
        );     
    };

    return (
        <div className="h-80">
            <div className="h-1/6 flex items-center rounded-t-xl justify-evenly bg-maincolor font-medium py-1">
                <button className="w-1/3" onClick={handlePreviousMonth}>Previous</button>
                <p className="w-1/3 text-lg">{monthNames[currentMonth]}</p>
                <button className="w-1/3" onClick={handleNextMonth}>Next</button>
            </div>
            <div className="h-5/6 rounded-b-xl bg-white grid grid-cols-7 gap-2 text-center text-sm p-2">
                {daysOfWeek.map((day, index) => (
                    <div key={index}>{day}</div>
                ))}
                {days.map((day, index) => (
                    <p
                        className={`rounded-full flex items-center justify-center 
                    ${day ? 'cursor-pointer' : 'opacity-45 cursor-default'}
                    ${day && daysOff.includes(day.date.getTime()) ? 'bg-blue-500 hover:bg-blue-300 text-white' : 'bg-gray-200 hover:bg-gray-300'}`}
                        key={index}
                        onClick={() => handleSelectDate(day.date.getTime())}
                    >
                        {day ? day.date.getDate() : ''}
                    </p>
                ))}
            </div>
        </div>
    );
}

export default DaysoffCalendar;
