import React, { useState, useEffect } from 'react';

function Messages({ data, type }) {
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        setMessages(Object.values(data));
    }, [data]);

    return (
        <>
            {messages.length > 0 ?
                <article className="flex flex-col items-center">
                    <ul className={`max-w-xs min-w-80 text-center text-xl ${type == 'message' ? 'bg-green-500' : 'bg-red-500'} rounded-xl p-3 space-y-3`}>
                        {messages.map((message, index) => (
                            <li key={index} className="text-white">
                                {message}
                            </li>
                        ))}
                    </ul>
                </article> : null}
        </>
    );
}

export default Messages;