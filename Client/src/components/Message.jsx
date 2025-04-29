import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faXmark } from '@fortawesome/free-solid-svg-icons';

function Message({ message, type }) {
    return (
        <>
            {message &&
                (<article className="fixed top-6 left-6 text-xl bg-gray-200 bg-opacity-95 border border-zinc-500 rounded-xl p-3 text-gray-700 z-50 flex items-center justify-center space-x-2 animate-bounce-left-right sm:left-3 sm:top-3 sm:right-3">
                <FontAwesomeIcon className={`text-2xl ${type === "message" ? "text-green-500" : "text-red-500"}`} icon={type === "message" ? faCheck : faXmark} />
                    <p>{message}</p>
                </article>)}
        </>
    );
}

export default Message;