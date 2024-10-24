import React from 'react';

function Message({ message, type }) {
    return (
        <>
            {message &&
                (<article className="flex flex-col items-center">
                <div className={`w-80 text-center text-xl ${type == 'message' ? 'bg-green-500' : 'bg-red-500'} rounded-xl p-3 space-y-3`}>
                        <p className="text-white">{message}</p>
                    </div>
                </article>)}
        </>
    );
}

export default Message;