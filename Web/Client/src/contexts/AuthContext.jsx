import React, { createContext, useContext, useState, useEffect } from 'react';
import jwtDecoder from '../services/jwtDecoder';
import apiRequest from '../services/apiRequest';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isSessionEnd, setIsSessionEnd] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const checkAuth = async () => {
            const accessToken = localStorage.getItem('accessToken');

            if (accessToken) {
                const { expTime } = jwtDecoder();

                if (expTime * 1000 > Date.now()) {
                    setIsAuthenticated(true);
                } else {
                    const newToken = await refreshAccessToken();

                    if (newToken) {
                        localStorage.setItem('accessToken', newToken);
                        setIsAuthenticated(true);
                    } else {
                        setIsSessionEnd(true);
                    }
                }
            }
        };

        checkAuth();
        setLoading(false);
    }, []);

    const refreshAccessToken = async () => {
        try {
            const response = await apiRequest('account', 'refreshToken', undefined, undefined, 'GET', true);

            return response.token ? response.token : undefined;
        } catch (error) {
            console.error(error);
            return undefined;
        }
    };

    return (
        <>
            {loading ? null :
                <AuthContext.Provider value={{ isAuthenticated, isSessionEnd }}>
                    {children}
                </AuthContext.Provider>}
        </>
    );
};

export const useAuthContext = () => {
    return useContext(AuthContext);
};