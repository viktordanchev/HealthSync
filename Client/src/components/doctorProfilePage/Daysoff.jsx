import React, { useState } from 'react';
import DaysOffCalendar from './DaysOffCalendar';
import apiRequest from '../../services/apiRequest';
import { useMessage } from '../../contexts/MessageContext';
import { useLoading } from '../../contexts/LoadingContext';

function DaysOff({ data }) {
    const { showMessage } = useMessage();
    const { setIsLoading } = useLoading();
    const [daysOff, setDaysOff] = useState(data);

    const handleSubmitChanges = async () => {
        try {
            setIsLoading(true);

            const response = await apiRequest('doctors', 'updateDoctorDaysOff', daysOff, localStorage.getItem('accessToken'), 'POST', false);

            showMessage(response.message, 'message');
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <article className="w-1/3 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-between space-y-4 text-center">
            <h2 className="text-3xl font-thin underline-thin text-gray-700 sm:text-2xl">Daysoff</h2>
            <div>
                <DaysOffCalendar daysOff={daysOff} setDaysOff={setDaysOff} />
                <p className="text-sm">Here you can set your days off or adjust your working hours.</p>
            </div>
            <div>
                <button className={`bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded`}
                    type="submit"
                    onClick={handleSubmitChanges}>
                    Save changes
                </button>
            </div>
        </article>
    );
}

export default DaysOff;