import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import SendEmail from './SendEmail.jsx';
import Verify from './Verify.jsx';

function EmailVerification() {
    const email = sessionStorage.getItem('email');

    return (
        <div>
            {!email ? (
                <SendEmail />
            ) : (
                <Verify />
            )}
        </div>
    );
}


export default EmailVerification;