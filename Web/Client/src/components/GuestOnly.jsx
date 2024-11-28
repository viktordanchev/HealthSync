import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthContext } from '../contexts/AuthContext';

function GuestOnly({ children }) {
    const { isAuthenticated } = useAuthContext();

    if (isAuthenticated) {
        return <Navigate to="/home" replace />;
    }

    return children;
}

export default GuestOnly;