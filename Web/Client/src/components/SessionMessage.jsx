import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faClock } from '@fortawesome/free-regular-svg-icons';
import { useAuthContext } from '../contexts/AuthContext';

function SessionMessage() {
    const { isSessionEnd } = useAuthContext();
    const [showSessionMessage, setShowSessionMessage] = useState(false);

    useEffect(() => {
        if (isSessionEnd) {
            setShowSessionMessage(true);
        }
    }, [isSessionEnd]);

    const handleClose = () => {
        localStorage.removeItem('accessToken');
        setShowSessionMessage(false);
    };

    return (
        <>
            {showSessionMessage && (
                <div className="fixed left-6 bottom-6 flex flex-col items-center justify-between p-3 text-gray-700 shadow-xl animate-bounce-left-right z-50 rounded-xl h-36 w-72 bg-gray-200 border-2 border-zinc-500 sm:w-auto sm:left-3 sm:bottom-3 sm:right-3">
                    <div className="h-full w-full flex items-center justify-center space-x-3">
                        <FontAwesomeIcon icon={faClock} className="text-3xl" />
                        <div>
                            <p className="text-lg text-center">Session has ended</p>
                            <p className="text-base font-thin">Login to create new one.</p>
                        </div>
                    </div>
                    <div className="w-full flex flex-row justify-evenly">
                        <a href="/login"
                            className="bg-blue-500 border-2 border-blue-500 hover:bg-white hover:text-blue-500 text-white font-bold py-1 px-2 rounded"
                            onClick={handleClose}>
                            Login
                        </a>
                        <button className="bg-blue-500 border-2 border-blue-500 hover:bg-white hover:text-blue-500 text-white font-bold py-1 px-2 rounded"
                            onClick={handleClose}>
                            Close
                        </button>
                    </div>
                </div>
            )}
        </>
    );
}

export default SessionMessage;