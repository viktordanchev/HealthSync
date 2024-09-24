import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import SendEmail from './SendEmail.jsx';
import ChangePassword from './ChangePassword.jsx';

function RecoverPassword() {
    const query = new URLSearchParams(useLocation().search);
    const token = query.get('token') ? query.get('token').replace(/ /g, '+') : null;

    return (
        <div>
            {!token ? (
                <SendEmail />
            ) : (
                <ChangePassword token={token} />
            )}
        </div>
    );
}


export default RecoverPassword;