import React, { createContext, useContext, useState, useEffect } from 'react';
import useAuth from '../hooks/useAuth';
import useRefreshToken from '../hooks/useRefreshToken';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const isAuth = useAuth();
    const { refreshAccessToken } = useRefreshToken();
    const [isAuthenticated, setIsAuthenticated] = useState(isAuth);
    const [isSessionEnd, setIsSessionEnd] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    
    useEffect(() => {
        const tryRefreshToken = async () => {
            setIsLoading(true);
            await new Promise(res => setTimeout(res, 1000));
            const isRefreshed = await refreshAccessToken();

            if (isRefreshed) {
                setIsAuthenticated(true);
            } else {
                setIsSessionEnd(localStorage.getItem('accessToken'));
                setIsAuthenticated(false);
            }

            setIsLoading(false);
        };

        if (!isAuth) {
            tryRefreshToken();
        }
    }, []);

    const login = (token) => {
        localStorage.setItem('accessToken', token);
        setIsAuthenticated(true);
    };

    const logout = () => {
        localStorage.removeItem('accessToken');
    };

    const isStillAuth = async () => {
        let isStill = false;
        const isAuth = useAuth();

        if (!isAuth) {
            setIsLoading(true);
            await new Promise(res => setTimeout(res, 1000));
            const isRefreshed = await refreshAccessToken();

            if (isRefreshed) {
                isStill = true;
            }
        } else {
            isStill = true;
        }

        setIsLoading(false);
        return isStill;
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, isSessionEnd, login, logout, isStillAuth }}>
            {isLoading && (<div className="fixed z-50 w-full h-1 bg-red-500"></div>)}
            {children}
        </AuthContext.Provider>
    );
};

export const useAuthContext = () => {
    return useContext(AuthContext);
};