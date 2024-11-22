import { useEffect, useState } from 'react';
import jwtDecoder from '../services/jwtDecoder';
import apiRequest from '../services/apiRequest';

const useAuth = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [isSessionEnd, setIsSessionEnd] = useState(false);
    const [loading, setLoading] = useState(true);
    const accessToken = localStorage.getItem('accessToken');

    useEffect(() => {
        const checkAuth = async () => {
            if (accessToken) {
                const { expTime } = jwtDecoder(accessToken);

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

    return { isAuthenticated, isSessionEnd, loading };
};

export default useAuth;

const refreshAccessToken = async () => {
    try {
        const response = await apiRequest('account', 'refreshToken', undefined, undefined, 'GET', true);

        return response.token ? response.token : undefined;
    } catch (error) {
        console.error(error);
        return undefined;
    }
};
