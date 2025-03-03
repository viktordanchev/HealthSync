import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthContext } from '../contexts/AuthContext';
import jwtDecoder from '../services/jwtDecoder';
import RestrictedPage from '../pages/RestrictedPage';

function ProtectedRoute({ children, roleNeeded = null }) {
    const { isAuthenticated, isSessionEnd } = useAuthContext();
    
    if (!isAuthenticated && !isSessionEnd) {
        return <Navigate to="/login" replace />;
    }
    
    const { roles } = jwtDecoder();
    if (roleNeeded && !roles.includes(roleNeeded)) {
        return <RestrictedPage />;
    }

    return React.cloneElement(children);
}

export default ProtectedRoute;