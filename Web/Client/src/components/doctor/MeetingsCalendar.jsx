import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { getDaysInMonth } from '../../services/apiRequests/doctor';
import AddMeeting from './AddMeeting';
import Loading from '../Loading';

const MeetingsCalendar = ({ doctorId }) => {
    const [currentYear, setCurrentYear] = useState(new Date().getFullYear());
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [isVisible, setIsVisible] = useState(false);
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

            const data = await getDaysInMonth(dto);

            const transformedData = data.map(item => ({
                ...item,
                date: new Date(item.date)
            }));

            setDays(transformedData);

            generateCalendar(currentYear, currentMonth);
        };

        getDaysOff();
    }, [currentYear, currentMonth]);

    const generateCalendar = (year, month) => {
        const firstDayOfMonth = new Date(year, month, 1);
        const daysInMonth = new Date(year, month + 1, 0).getDate();
        const firstDayOfWeek = (firstDayOfMonth.getDay() + 6) % 7;
        
        let emptySpaces = [];

        for (let i = 0; i < firstDayOfWeek; i++) {
            emptySpaces.push(null);
        }
        
        setDays(prevDays => [...emptySpaces, ...prevDays]);
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
        if (day && day.date > new Date() && day.isWorkDay) {
            setDate(day.date);
            setIsVisible(true);
        }
    };

    return (
        <>
            <div className="bg-white rounded-xl text-base">
                <div className="flex items-center rounded-t-xl justify-evenly bg-zinc-700 py-1">
                    <button onClick={handlePreviousMonth} className="text-white">Previous</button>
                    <h2 className="text-white">{monthNames[currentMonth]} {currentYear}</h2>
                    <button onClick={handleNextMonth} className="text-white">Next</button>
                </div>
                <div className="grid grid-cols-7 gap-1 p-2 text-center sm:gap-2" id="calendar">
                    {daysOfWeek.map((day, index) => (
                        <div key={index} className="text-center">{day}</div>
                    ))}
                    {days.length == 0 ? <Loading type={'small'} /> :
                    <>
                        {days.map((day, index) => (
                            <div
                                key={index}
                                onClick={() => handleDayClick(day)}
                                className={`text-center rounded-full ${day && new Date().getDate() === day.date.getDate() && new Date().getFullYear() === currentYear && new Date().getMonth() === currentMonth ? 'bg-blue-500 text-white' : 'bg-gray-200'} ${day && new Date() < day.date && day.isWorkDay ? 'cursor-pointer' : 'opacity-35 cursor-default'}`}
                            >
                                {day ? day.date.getDate() : ''}
                            </div>
                        ))}
                    </>}
                </div>
            </div>
            {isVisible && (
                <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-opacity-95 border border-white rounded-xl bg-zinc-700 p-4 flex flex-col space-y-4 sm:w-full">
                    <div className="w-full text-right">
                        <button onClick={() => setIsVisible(false)}>
                            <FontAwesomeIcon icon={faXmark} className="text-white text-2xl" />
                        </button>
                    </div>
                    <AddMeeting
                        doctorId={doctorId}
                        date={date}
                    />
                </div>
            )}
        </>
    );
};

export default MeetingsCalendar;
