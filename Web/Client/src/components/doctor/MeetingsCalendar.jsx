import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../../services/apiRequest';
import AddMeeting from './AddMeeting';
import Loading from '../Loading';

const MeetingsCalendar = ({ doctorId }) => {
    const [currentYear, setCurrentYear] = useState(new Date().getFullYear());
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [isVisible, setIsVisible] = useState(false);
    const [loading, setLoading] = useState(true);
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

            const response = await apiRequest('doctor', 'getDaysInMonth', dto, undefined, 'POST', false);

            const transformedData = response.map(item => ({
                ...item,
                date: new Date(item.date)
            }));

            setDays(transformedData);
            setLoading(false);

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
        <article className="w-1/2 flex flex-col justify-end md:w-full sm:w-full">
            <p className="text-center mb-1 text-white text-lg font-bold md:text-base sm:text-base">Meetings</p>
            {loading ?
                <div className="h-52 flex items-center">
                    <Loading type={'small'} />
                </div> :
                <div className="h-60">
                    <div className="h-1/6 flex items-center rounded-t-xl justify-evenly bg-zinc-700 py-1">
                        {new Date() <= new Date(currentYear, currentMonth) ?
                            <button onClick={handlePreviousMonth} className="w-1/3 text-white">
                                Previous
                            </button> :
                            <div className="w-1/3">
                            </div>}
                        <div className="w-1/3 text-white text-center flex justify-center space-x-1">
                            <p>{monthNames[currentMonth]}</p>
                            <p>{currentYear}</p>
                        </div>
                        <button onClick={handleNextMonth} className="w-1/3 text-white">Next</button>
                    </div>
                    <div className="h-5/6 rounded-b-xl bg-white grid grid-cols-7 gap-1 text-center text-sm p-2 sm:gap-2" id="calendar">
                        {daysOfWeek.map((day, index) => (
                            <div key={index} className="text-center">{day}</div>
                        ))}
                        {days.length == 0 ? <Loading type={'small'} /> :
                            <>
                                {days.map((day, index) => (
                                    <div
                                        key={index}
                                        onClick={() => handleDayClick(day)}
                                        className={`rounded-full flex items-center justify-center ${day && new Date().getDate() === day.date.getDate() && new Date().getFullYear() === currentYear && new Date().getMonth() === currentMonth ? 'bg-blue-500 text-white' : 'bg-gray-200'} ${day && new Date() < day.date && day.isWorkDay ? 'cursor-pointer' : 'opacity-35 cursor-default'}`}
                                    >
                                        <p>{day ? day.date.getDate() : ''}</p>
                                    </div>
                                ))}
                            </>}
                    </div>
                </div>}
            {isVisible && (
                <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-opacity-95 border border-white rounded-xl bg-zinc-700 p-4 flex flex-col space-y-4 sm:w-full">
                    <div className="w-full text-right">
                        <button onClick={() => setIsVisible(false)}>
                            <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                        </button>
                    </div>
                    <AddMeeting
                        doctorId={doctorId}
                        date={date}
                    />
                </div>
            )}
        </article>
    );
};

export default MeetingsCalendar;
