import React from 'react';

function Message({ message, type }) {
    return (
        <>
            {message &&
                (<article className="fixed top-6 left-6 flex z-50 flex-col items-center sm:left-3 sm:top-3 sm:right-3">
                <div className={`text-center text-xl border border-zinc-500 ${type == 'message' ? 'bg-green-500' : 'bg-red-500'} rounded-xl px-6 py-3 space-y-3`}>
                        <p className="text-white">{message}</p>
                    </div>
                </article>)}
        </>
    );
}

export default Message;