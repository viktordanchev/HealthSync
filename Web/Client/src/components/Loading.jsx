import React from 'react';

function Loading() {
    return (
        <div className="flex items-center justify-center">
            <div>
                <p className="text-xl font-bold">Loading</p>
                <div className="mt-4 flex justify-center">
                    <div className="w-14 h-14 border-4 border-maincolor border-dotted rounded-full animate-spin"></div>
                </div>
            </div>
        </div>
    );
}

export default Loading;