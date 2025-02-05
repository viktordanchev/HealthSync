import React from 'react';
import NotFound from '../assets/images/404NotFound.png';

function NotFoundPage() {
    return (
        <div className="flex justify-center items-center">
            <img src={NotFound} className="h-notFoundPng"/>
        </div>
    );
}

export default NotFoundPage;