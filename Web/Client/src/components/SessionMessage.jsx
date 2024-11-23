import React from 'react';

function SessionMessage({ close }) {
    const handleClose = () => {
        localStorage.removeItem('accessToken');
        close();
    };

    return (
        <div className="animate-bounce-left-right z-50 rounded-xl h-32 w-64 bg-white border-2 border-red-500 fixed left-6 bottom-6 flex flex-col items-center justify-between p-3 sm:left-0 sm:bottom-0 sm:w-full sm:top-0">
            <div>
                <p className="text-lg text-black text-center">Session has ended</p>
                <p className="text-base text-black">Login to create new session.</p>
            </div>
            <div className="w-full flex flex-row justify-evenly">
                <a href="/login" className="bg-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-blue-700 transition duration-300 sm:text-base"
                    onClick={handleClose}>
                    Login
                </a>
                <button className="bg-blue-500 text-white font-bold py-1 px-2 rounded hover:bg-blue-700 transition duration-300 sm:text-base"
                    onClick={handleClose}>
                    Close
                </button>
            </div>
        </div>
    );
}

export default SessionMessage;