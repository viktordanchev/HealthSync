import React, { useState } from 'react';

function SessionMessage({ message }) {
    const [isVisible, setIsVisible] = useState(true);

    const handleClose = () => {
        sessionStorage.removeItem('accessToken');
        setIsVisible(false);
    };

    return (
        <div className={`${!isVisible ? 'hidden' : 'animate-bounce-left-right'} z-50 rounded-xl h-32 w-64 bg-white border-2 border-red-500 shadow-md shadow-red-500 fixed left-6 bottom-6 flex flex-col items-center justify-center space-y-3 sm:left-0 sm:bottom-0 sm:w-full sm:top-0`}>
            <span className="text-lg font-bold text-black">{message}</span>
            <div className="w-1/2 flex flex-row justify-evenly">
                <a href="/login" className="bg-blue-500 text-white text-md font-bold py-1 px-2 rounded hover:bg-blue-700 transition duration-300 sm:text-base"
                    onClick={handleClose}>
                    Login
                </a>
                <button className="bg-blue-500 text-white text-md font-bold py-1 px-2 rounded hover:bg-blue-700 transition duration-300 sm:text-base"
                    onClick={handleClose}>
                    Close
                </button>
            </div>
        </div>
    );
}

export default SessionMessage;