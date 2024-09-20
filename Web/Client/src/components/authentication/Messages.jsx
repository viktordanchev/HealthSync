import React from 'react';

function Messages({ values, type }) {
    return (
        <article className="flex flex-col items-center space-y-4">
            <div className={`max-w-xs text-center text-xl ${type == 'message' ? 'bg-green-500' : 'bg-red-500'} rounded-xl p-4`}>
                {values.map((message, index) => (
                    <div key={index} className="text-white">
                        {message}
                    </div>
                ))}
            </div>
        </article>
    );
}

export default Messages;