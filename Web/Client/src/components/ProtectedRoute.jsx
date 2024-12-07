import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthContext } from '../contexts/AuthContext';
import jwtDecoder from '../services/jwtDecoder';

function ProtectedRoute({ children }) {
    const { isAuthenticated } = useAuthContext();
    const { isEmailConfirmed } = jwtDecoder();

    if (!isAuthenticated) {
        return <Navigate to="/home" replace />;
    } else if (!isEmailConfirmed) {
        return <Navigate to="/account/verify" replace />;
    }

    return React.cloneElement(children);
}

export default ProtectedRoute;