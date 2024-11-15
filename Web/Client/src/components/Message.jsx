import React from 'react';

function Message({ message, type }) {
    return (
        <>
            {message &&
                (<article className="flex flex-col items-center my-6 mx-6">
                <div className={`text-center text-xl ${type == 'message' ? 'bg-green-500' : 'bg-red-500'} rounded-xl px-6 py-3 space-y-3`}>
                        <p className="text-white">{message}</p>
                    </div>
                </article>)}
        </>
    );
}

export default Message;