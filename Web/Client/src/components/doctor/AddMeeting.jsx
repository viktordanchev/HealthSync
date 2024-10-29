import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark } from '@fortawesome/free-solid-svg-icons';

function AddMeeting({ dayOfWeek }) {

    return (
        <>
            <div>
                <p className="text-2xl font-bold">Selected Date</p>
                <div className="text-xl font-semibold">{dayOfWeek}</div>
            </div>
        </>
    );
}

export default AddMeeting;