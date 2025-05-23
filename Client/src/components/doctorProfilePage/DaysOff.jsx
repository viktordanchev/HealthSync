import React, { useState, useEffect } from 'react';
import DaysOffCalendar from './DaysOffCalendar';
import apiRequest from '../../services/apiRequest';
import { useMessage } from '../../contexts/MessageContext';
import { useAuthContext } from '../../contexts/AuthContext';
import { useLoading } from '../../contexts/LoadingContext';

function DaysOff({ data }) {
    const { setIsLoading } = useLoading();
    const { showMessage } = useMessage();
    const { isStillAuth } = useAuthContext();
    const [daysOff, setDaysOff] = useState(data);
    const [isChanged, setIsChanged] = useState(false);

    useEffect(() => {
        setIsChanged(JSON.stringify(daysOff) !== JSON.stringify(data));
    }, [daysOff]);

    const handleSubmitChanges = async () => {
        const isAuth = await isStillAuth();
        if (!isAuth) return;

        try {
            setIsLoading(true);

            const response = await apiRequest('doctors', 'updateDaysOff', daysOff, localStorage.getItem('accessToken'), 'POST', false);

            showMessage(response.message, 'message');
        } catch (error) {
            console.error(error);
        } finally {
            setIsChanged(false);
            setIsLoading(false);
        }
    };

    return (
        <article className="w-1/3 border border-zinc-500 p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col justify-between space-y-4 text-center lg:w-full md:w-full sm:w-full">
            <h2 className="text-3xl font-thin underline-thin text-gray-700 sm:text-2xl">Days off</h2>
            <div>
                <DaysOffCalendar daysOff={daysOff} setDaysOff={setDaysOff} />
                <p className="text-sm">Here you can set your days off.</p>
            </div>
            <div>
                <button className={`bg-blue-500 border-2 border-blue-500 text-white font-medium py-1 px-2 rounded
                    ${isChanged ? 'hover:bg-white hover:text-blue-500' : 'opacity-75 cursor-default pointer-events-none'}`}
                    type="submit"
                    onClick={handleSubmitChanges}>
                    Save changes
                </button>
            </div>
        </article>
    );
}

export default DaysOff;