import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthContext } from '../contexts/AuthContext';

function ProtectedRoute({ children }) {
    const { isAuthenticated } = useAuthContext();
    
    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    return React.cloneElement(children);
}

export default ProtectedRoute;