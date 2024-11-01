import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { getAvailableMeetTimes, getDaysOffByMonth } from '../../services/apiRequests/doctor';
import AddMeeting from './AddMeeting';
import Loading from '../Loading';

const MeetingsCalendar = ({ doctorId }) => {
    const [currentYear, setCurrentYear] = useState(new Date().getFullYear());
    const [currentMonth, setCurrentMonth] = useState(new Date().getMonth());
    const [meetingTimes, setMeetingTimes] = useState(null);
    const [date, setDate] = useState(null);
    const [days, setDays] = useState([]);
    const [daysOff, setDaysOff] = useState([]);

    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

    useEffect(() => {
        const getDaysOff = async () => {
            const dto = {
                doctorId: doctorId,
                month: currentMonth + 1,
                year: currentYear
            }

            //await new Promise(res => setTimeout(res, 3000));

            const data = await getDaysOffByMonth(dto);
            setDaysOff(data);

            generateCalendar(currentYear, currentMonth);
        };

        getDaysOff();
    }, [currentYear, currentMonth]);

    const generateCalendar = (year, month) => {
        const firstDayOfMonth = new Date(year, month, 1);
        const daysInMonth = new Date(year, month + 1, 0).getDate();
        const firstDayOfWeek = (firstDayOfMonth.getDay() + 6) % 7;
        
        let tempDays = [];

        for (let i = 0; i < firstDayOfWeek; i++) {
            tempDays.push(null);
        }

        for (let day = 1; day <= daysInMonth; day++) {
            tempDays.push(new Date(Date.UTC(year, month, day)));
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

    const handleDayClick = async (date) => {
        if (date > new Date() && !daysOff.some((d) => d === date.toISOString().split('T')[0])) {
            const dto = {
                doctorId: doctorId,
                date: date.toISOString()
            };

            const response = await getAvailableMeetTimes(dto);

            setMeetingTimes(response);
            setDate(date);
        }
    };

    const closeModal = () => {
        setMeetingTimes(null);
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
                        {days.map((date, index) => (
                            <div
                                key={index}
                                onClick={() => handleDayClick(date)}
                                className={`text-center rounded-full ${date && new Date().getDate() === date.getDate() && new Date().getFullYear() === currentYear && new Date().getMonth() === currentMonth ? 'bg-blue-500 text-white' : 'bg-gray-200'} ${new Date() >= date || daysOff.some((d) => d === date.toISOString().split('T')[0]) ? 'opacity-55 cursor-default' : 'cursor-pointer'}`}
                            >
                                {date ? date.getDate() : ''}
                            </div>
                        ))}
                    </>}
                </div>
            </div>
            {meetingTimes && (
                <div className="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 bg-opacity-95 border border-white rounded-xl bg-zinc-700 p-4 flex flex-col space-y-4 sm:w-full">
                    <div className="w-full text-right">
                        <button onClick={closeModal}>
                            <FontAwesomeIcon icon={faXmark} className="text-white text-2xl" />
                        </button>
                    </div>
                    <AddMeeting
                        meetingTimes={meetingTimes}
                        date={date}
                    />
                </div>
            )}
        </>
    );
};

export default MeetingsCalendar;
