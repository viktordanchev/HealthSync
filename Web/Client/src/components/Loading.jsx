import React from 'react';

function Loading({ type }) {
    let width = '0';
    let height = '0';

    switch (type) {
        case 'small':
            width = '3';
            height = '6';
            break;
        case 'big':
            width = '5';
            height = '10';
            break;
    }

    return (
        <div className="h-full w-full flex items-center justify-center">
            <div className="flex space-x-3">
                <div className={`w-${width} h-${height} bg-blue-500 animate-grow`}></div>
                <div className={`w-${width} h-${height} bg-blue-500 animate-grow`} style={{ animationDelay: '0.4s' }}></div>
                <div className={`w-${width} h-${height} bg-blue-500 animate-grow`} style={{ animationDelay: '0.8s' }}></div>
            </div>
        </div>
    );
}

export default Loading;