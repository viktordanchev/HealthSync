import React from 'react';

function ComponentLoading() {
    return (
        <div className="fixed top-0 left-0 right-0 bottom-0 flex items-center justify-center">
            <div className="flex space-x-3">
                <div className="w-5 h-10 bg-blue-500 animate-grow"></div>
                <div className="w-5 h-10 bg-blue-500 animate-grow" style={{ animationDelay: '0.4s' }}></div>
                <div className="w-5 h-10 bg-blue-500 animate-grow" style={{ animationDelay: '0.8s' }}></div>
            </div>
        </div>
    );
}

export default ComponentLoading;