import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthContext } from '../contexts/AuthContext';
import jwtDecoder from '../services/jwtDecoder';

function ProtectedRoute({ children }) {
    const { isAuthenticated } = useAuthContext();

    if (!isAuthenticated) {
        return <Navigate to="/home" replace />;
    }

    return React.cloneElement(children);
}

export default ProtectedRoute;