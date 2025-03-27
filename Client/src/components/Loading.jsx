import React from 'react';

function Loading({ type }) {
    let width = '';
    let height = '';

    switch (type) {
        case 'small':
            width = 'w-3';
            height = 'h-6';
            break;
        case 'big':
            width = 'w-5';
            height = 'h-10';
            break;
    }
    
    return (
        <div className="h-full w-full flex items-center justify-center">
            <div className="flex space-x-3">
                <div className={`${width} ${height} bg-blue-600 animate-grow`}></div>
                <div className={`${width} ${height} bg-blue-600 animate-grow`} style={{ animationDelay: '0.4s' }}></div>
                <div className={`${width} ${height} bg-blue-600 animate-grow`} style={{ animationDelay: '0.8s' }}></div>
            </div>
        </div>
    );
}

export default Loading;