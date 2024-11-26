import React, { createContext, useContext, useState, useEffect } from 'react';
import jwtDecoder from '../services/jwtDecoder';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';

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

            setLoading(false);
        };

        checkAuth();
    }, []);

    const refreshAccessToken = async () => {
        setLoading(true);

        try {
            const response = await apiRequest('account', 'refreshToken', undefined, undefined, 'GET', true);

            return response.token ? response.token : undefined;
        } catch (error) {
            console.error(error);
            return undefined;
        } finally {
            setLoading(false);
        }
    };

    const login = (token) => {
        localStorage.setItem('accessToken', token);
        setIsAuthenticated(true);
    };

    return (
        <>
            {loading ? null :
                <AuthContext.Provider value={{ isAuthenticated, isSessionEnd, login }}>
                    {children}
                </AuthContext.Provider>}
        </>
    );
};

export const useAuthContext = () => {
    return useContext(AuthContext);
};