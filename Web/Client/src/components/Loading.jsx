import React from 'react';

function Loading({ type }) {
    const widthClass = type === "small" ? "w-3" : "w-5";
    const heightClass = type === "small" ? "h-6" : "h-10";

    return (
        <div className="basis-full flex items-center justify-center">
            <div className="flex space-x-3">
                <div className={`${widthClass} ${heightClass} bg-blue-500 animate-grow`}></div>
                <div className={`${widthClass} ${heightClass} bg-blue-500 animate-grow`} style={{ animationDelay: '0.4s' }}></div>
                <div className={`${widthClass} ${heightClass} bg-blue-500 animate-grow`} style={{ animationDelay: '0.8s' }}></div>
            </div>
        </div>
    );
}

export default Loading;