import React from 'react';

function Messages({ values, type }) {
    return (
        <article className="flex flex-col items-center">
            <ul className={`max-w-xs text-center text-xl ${type == 'message' ? 'bg-green-500' : 'bg-red-500'} rounded-xl p-4 space-y-3`}>
                {values.map((message, index) => (
                    <li key={index} className="text-white">
                        {message}
                    </li>
                ))}
            </ul>
        </article>
    );
}

export default Messages;