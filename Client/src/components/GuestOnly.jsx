import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthContext } from '../contexts/AuthContext';
import jwtDecoder from '../services/jwtDecoder';

function GuestOnly({ children, role = null }) {
    const { isAuthenticated } = useAuthContext();
    
    if (isAuthenticated) {
        if (role) {
            const { roles } = jwtDecoder();
            if (!roles || !roles.includes(role)) return React.cloneElement(children);
        }

        return <Navigate to="/home" replace />;
    }

    return React.cloneElement(children);
}

export default GuestOnly;