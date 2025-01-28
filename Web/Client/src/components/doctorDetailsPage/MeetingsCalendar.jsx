import React, { useState, useEffect } from 'react';
import apiRequest from '../../services/apiRequest';
import AddMeeting from './AddMeeting';
import Loading from '../Loading';

const MeetingsCalendar = ({ doctorId }) => {
    const [currentYear, setCurrentYear] = useState(new Date().getFullYear());
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [isLoading, setIsLoading] = useState(true);
    const [date, setDate] = useState(null);
    const [days, setDays] = useState([]);
    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

    useEffect(() => {
        const receiveData = async () => {
            const dto = {
                doctorId: doctorId,
                month: currentMonth + 1,
                year: currentYear
            }

            setIsLoading(true);

            try {
                const response = await apiRequest('doctors', 'getMonthSchedule', dto, undefined, 'POST', false);

                if (response.length > 0) {
                    const dates = response.map(item => ({
                        ...item,
                        date: new Date(item.date)
                    }));

                    setDays(dates);
                    generateCalendar(currentYear, currentMonth);
                }
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, [currentYear, currentMonth]);

    const generateCalendar = (year, month) => {
        const firstDayOfMonth = new Date(year, month, 1);
        const firstDayOfWeek = (firstDayOfMonth.getDay() + 6) % 7;
        let emptySpaces = [];

        for (let i = 0; i < firstDayOfWeek; i++) {
            emptySpaces.push(null);
        }

        setDays(prevDays => [...emptySpaces, ...prevDays]);
    };

    const handlePreviousMonth = () => {
        if (newMonth < 0) {
            setCurrentMonth(11);
            setCurrentYear(currentYear - 1);
        } else {
            setCurrentMonth(currentMonth - 1);
        }
    };

    const handleNextMonth = () => {
        if (newMonth > 11) {
            setCurrentMonth(0);
            setCurrentYear(currentYear + 1);
        } else {
            setCurrentMonth(currentMonth + 1);
        }
    };

    const handleDayClick = (day) => {
        if (day && day.date > new Date() && day.isAvailable) {
            setDate(day.date);
        }
    };

    return (
        <>
            {date ?
                <AddMeeting
                    doctorId={doctorId}
                    date={date}
                    setDate={setDate}
                /> :
                <div className="h-80">
                    {isLoading ? <Loading type={'small'} /> :
                        <>
                            {days.length == 0 ?
                                <div className="font-bold text-2xl text-center">
                                    No working schedule provided yet
                                </div> :
                                <>
                                    <div className="h-1/6 flex items-center rounded-t-xl justify-evenly bg-maincolor font-medium py-1">
                                        {new Date() <= new Date(currentYear, currentMonth) ?
                                            <button className="w-1/3" onClick={handlePreviousMonth}>
                                                Previous
                                            </button> :
                                            <div className="w-1/3"></div>}
                                        <div className="w-1/3 text-center flex justify-center space-x-1">
                                            <p>{monthNames[currentMonth]}</p>
                                            <p>{currentYear}</p>
                                        </div>
                                        <button className="w-1/3" onClick={handleNextMonth}>Next</button>
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
                                                ${day && new Date().getDate() === day.date.getDate() &&
                                                                new Date().getFullYear() === currentYear &&
                                                                new Date().getMonth() === currentMonth
                                                                ? 'bg-blue-500 text-white' : 'bg-gray-200'} 
                                                ${day && new Date() < day.date && day.isAvailable ? 'cursor-pointer hover:bg-gray-300' : 'opacity-35 cursor-default'}`}>
                                                        <p>{day ? day.date.getDate() : ''}</p>
                                                    </div>
                                                ))}
                                            </>}
                                    </div>
                                </>}
                        </>}
                </div>}
        </>
    );
};

export default MeetingsCalendar;
