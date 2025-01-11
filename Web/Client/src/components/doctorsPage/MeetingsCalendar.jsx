import React, { useState, useEffect } from 'react';
import apiRequest from '../../services/apiRequest';
import AddMeeting from './AddMeeting';
import Loading from '../Loading';

const MeetingsCalendar = ({ doctorId }) => {
    const [currentYear, setCurrentYear] = useState(new Date().getFullYear());
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [isDateChoosed, setIsDateChoosed] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [date, setDate] = useState(null);
    const [days, setDays] = useState([]);
    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

    useEffect(() => {
        const getDaysOff = async () => {
            const dto = {
                doctorId: doctorId,
                month: currentMonth + 1,
                year: currentYear
            }

            try {
                const response = await apiRequest('doctors', 'getMonthDaysOff', dto, undefined, 'POST', false);
                
                if (response.length > 0) {
                    const daysOff = response.map(date => (new Date(date)));
                   
                    generateCalendar(currentYear, currentMonth, daysOff);
                }
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        getDaysOff();
    }, [currentYear, currentMonth]);

    const generateCalendar = (year, month, daysOff) => {
        const firstDayOfMonth = new Date(year, month, 1);
        const firstDayOfWeek = (firstDayOfMonth.getDay() + 6) % 7;
        let days = [];

        for (let i = 0; i < firstDayOfWeek; i++) {
            days.push(null);
        }

        const daysInMonth = new Date(year, month, 0);

        for (let day = 1; day <= daysInMonth; day++) {
            const date = new Date(year, month, day);

            if()
            days.push(date);
        }

        setDays(days);
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
        if (day && day.date > new Date() && day.isAvailable) {
            setDate(day.date);
            setIsDateChoosed(true);
        }
    };

    return (
        <>
            {!isDateChoosed ?
                <div className="h-80">
                    {isLoading ? <Loading type={'small'} /> :
                        <>
                            {days.length == 0 ?
                                <div className="font-bold text-2xl text-center">
                                    No working schedule provided yet
                                </div> :
                                <>
                                    <div className="h-1/6 flex items-center rounded-t-xl justify-evenly bg-maincolor py-1">
                                        {new Date() <= new Date(currentYear, currentMonth) ?
                                            <button onClick={handlePreviousMonth} className="w-1/3">
                                                Previous
                                            </button> :
                                            <div className="w-1/3">
                                            </div>}
                                        <div className="w-1/3 text-center flex justify-center space-x-1">
                                            <p>{monthNames[currentMonth]}</p>
                                            <p>{currentYear}</p>
                                        </div>
                                        <button onClick={handleNextMonth} className="w-1/3">Next</button>
                                    </div>
                                    <div className="h-5/6 rounded-b-xl bg-white grid grid-cols-7 gap-2 text-center text-sm p-2">
                                        {daysOfWeek.map((day, index) => (
                                            <div key={index} className="text-center">{day}</div>
                                        ))}
                                        {days.length == 0 ? <Loading type={'small'} /> :
                                            <>
                                                {days.map((day, index) => (
                                                    <div
                                                        key={index}
                                                        onClick={() => handleDayClick(day)}
                                                        className={`rounded-full flex items-center justify-center
                                                ${day && new Date().getDate() === day.getDate() &&
                                                                new Date().getFullYear() === currentYear &&
                                                                new Date().getMonth() === currentMonth
                                                                ? 'bg-blue-500 text-white' : 'bg-gray-200'} 
                                                ${day && new Date() < day ? 'cursor-pointer hover:bg-gray-300' : 'opacity-35 cursor-default'}`}>
                                                        <p>{day ? day.date.getDate() : ''}</p>
                                                    </div>
                                                ))}
                                            </>}
                                    </div>
                                </>}
                        </>}
                </div> :
                <AddMeeting
                    doctorId={doctorId}
                    date={date}
                    setIsDateChoosed={setIsDateChoosed}
                />}
        </>
    );
};

export default MeetingsCalendar;
