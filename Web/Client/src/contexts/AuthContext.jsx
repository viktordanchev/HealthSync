import React, { createContext, useContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useAuth from '../hooks/useAuth';
import useRefreshToken from '../hooks/useRefreshToken';
import { useLoading } from '../contexts/LoadingContext';
import Loading from '../components/Loading';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const navigate = useNavigate();
    const { setIsLoading } = useLoading();
    const isAuth = useAuth();
    const { refreshAccessToken } = useRefreshToken();
    const isTokenExist = !!localStorage.getItem('accessToken');
    const [isAuthenticated, setIsAuthenticated] = useState(isAuth);
    const [isSessionEnd, setIsSessionEnd] = useState(false);
    const [isAuthLoading, setIsAuthLoading] = useState(false);
    
    useEffect(() => {
        const tryRefreshToken = async () => {
            setIsAuthLoading(true);

            await new Promise(res => setTimeout(res, 1000));
            const isRefreshed = await refreshAccessToken();

            if (isRefreshed) {
                setIsAuthenticated(true);
            } else {
                setIsSessionEnd(isTokenExist);
                setIsAuthenticated(false);
                navigate('/home');
            }

            setIsAuthLoading(false);
        };
        
        if (!isAuth && isTokenExist) {
            tryRefreshToken();
        }
    }, []);

    const login = (token) => {
        localStorage.setItem('accessToken', token);
        setIsAuthenticated(true);
        setIsSessionEnd(false);
    };

    const logout = () => {
        localStorage.removeItem('accessToken');
        setIsAuthenticated(false);
    };

    const update = (token) => {
        localStorage.setItem('accessToken', token);
    };

    const isStillAuth = async () => {
        let isStill = false;
        const isAuth = useAuth();

        if (!isAuth && isTokenExist) {
            setIsLoading(true);

            const isRefreshed = await refreshAccessToken();

            if (isRefreshed) {
                isStill = true;
            }
            setIsLoading(false);
        } else {
            isStill = true;
        }

        setIsSessionEnd(!isStill && isTokenExist);
        setIsAuthenticated(isStill && isTokenExist);

        return isStill && isTokenExist;
    };

    return (
        <AuthContext.Provider value={{
            isAuthenticated,
            isSessionEnd,
            setIsSessionEnd,
            login,
            logout,
            update,
            isStillAuth
        }}>
            {isAuthLoading ? <div className="fixed h-full w-full"><Loading type={'big'} /></div> : children}
        </AuthContext.Provider>
    );
};

export const useAuthContext = () => {
    return useContext(AuthContext);
};