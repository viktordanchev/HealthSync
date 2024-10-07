import React, { useState } from 'react';

function SessionMessage({ error }) {
    const [message, setMessage] = useState(error);

    const handleClose = () => {
        setMessage('');
        sessionStorage.removeItem('accessToken');
        window.location.reload();
    };

    return (
        <>
            <div className="rounded-xl h-32 w-64 bg-white border-4 border-red-500 absolute bottom-3 left-3 flex flex-col items-center justify-center space-y-3">
                <span className="text-lg font-bold text-black">Session has ended.</span>
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
        </>
    );
}

export default SessionMessage;