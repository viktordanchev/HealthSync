import React, { useState, useEffect } from 'react';

function DaysoffCalendar({ daysOff, setDaysOff }) {
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [days, setDays] = useState([]);
    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    
    useEffect(() => {
        const firstDayOfMonth = new Date(new Date().getFullYear(), currentMonth, 1);
        const lastDayOfMonth = new Date(new Date().getFullYear(), currentMonth + 1, 0);
        const startDay = (firstDayOfMonth.getDay() + 6) % 7;
        const daysInMonth = [];

        for (let i = 0; i < startDay; i++) {
            daysInMonth.push(null);
        }

        for (let day = 1; day <= lastDayOfMonth.getDate(); day++) {
            daysInMonth.push(day);
        }

        setDays(daysInMonth);
    }, [currentMonth]);
    
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
    
    const handleSelectDay = (day) => {
        const dayOff = {
            month: currentMonth + 1,
            day: day,
        };
        
        setDaysOff(prev =>
            prev.some(doff => doff.month === currentMonth + 1 && doff.day === day)
                ? prev.filter(doff => doff.month !== currentMonth + 1 || doff.day !== day)
                : [...prev, dayOff]
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
                    <p className={`rounded-full flex items-center justify-center 
                    ${day ? 'cursor-pointer' : 'opacity-45 cursor-default'}
                    ${day && daysOff.some(doff => doff.month === currentMonth + 1 && doff.day === day) ? 'bg-blue-500 hover:bg-blue-400 text-white' : 'bg-gray-200 hover:bg-gray-300'}`}
                        key={index}
                        onClick={() => handleSelectDay(day)}
                    >
                        {day && day}
                    </p>
                ))}
            </div>
        </div>
    );
}

export default DaysoffCalendar;
