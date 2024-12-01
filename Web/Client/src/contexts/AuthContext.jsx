import React, { createContext, useContext, useState, useEffect } from 'react';
import useAuth from '../hooks/useAuth';
import useRefreshToken from '../hooks/useRefreshToken';
import Loading from '../components/Loading';
import LoadingGlobal from '../components/LoadingGlobal';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const isAuth = useAuth();
    const { refreshAccessToken } = useRefreshToken();
    const isTokenExist = !!localStorage.getItem('accessToken');
    const [isAuthenticated, setIsAuthenticated] = useState(isAuth);
    const [isSessionEnd, setIsSessionEnd] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [isStillAuthLoading, setIsStillAuthLoading] = useState(false);

    useEffect(() => {
        const tryRefreshToken = async () => {
            setIsLoading(true);

            await new Promise(res => setTimeout(res, 3000));
            const isRefreshed = await refreshAccessToken();

            if (isRefreshed) {
                setIsAuthenticated(true);
            } else {
                setIsSessionEnd(isTokenExist);
                setIsAuthenticated(false);
            }

            setIsLoading(false);
        };

        if (!isAuth && isTokenExist) {
            tryRefreshToken();
        }
    }, []);

    const login = (token) => {
        localStorage.setItem('accessToken', token);
        setIsAuthenticated(true);
    };

    const logout = () => {
        localStorage.removeItem('accessToken');
        setIsAuthenticated(false);
    };

    const isStillAuth = async () => {
        let isStill = false;
        const isAuth = useAuth();

        if (!isAuth && isTokenExist) {
            setIsStillAuthLoading(true);

            await new Promise(res => setTimeout(res, 3000));
            const isRefreshed = await refreshAccessToken();

            if (isRefreshed) {
                isStill = true;
            }
            setIsStillAuthLoading(false);
        } else {
            isStill = true;
        }

        setIsSessionEnd(!isStill && isTokenExist);
        setIsAuthenticated(isStill && isTokenExist);

        return isStill && isTokenExist;
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, isSessionEnd, login, logout, isStillAuth }}>
            {isStillAuthLoading && <LoadingGlobal />}
            {isLoading ? <Loading type='big' /> : children}
        </AuthContext.Provider>
    );
};

export const useAuthContext = () => {
    return useContext(AuthContext);
};