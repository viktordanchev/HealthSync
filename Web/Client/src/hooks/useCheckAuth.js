import { useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import { refreshToken } from '../services/apiRequests/account';

const useCheckAuth = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [user, setUser] = useState(false);

    useEffect(() => {
        const checkAuth = async () => {
            const token = sessionStorage.getItem('accessToken');

            if (token) {
                const decodedToken = jwtDecode(token);
                const currentTime = Date.now() / 1000;

                if (decodedToken.exp > currentTime) {
                    setIsAuthenticated(true);
                } else {
                    const data = await refreshToken();

                    if (data.token) {
                        sessionStorage.setItem('accessToken', data.token);
                        setUser(true);
                        setIsAuthenticated(true);
                    } else {
                        setError(data.error);
                    }
                }
            } else {
                const data = await refreshToken();
                
                if (data.token) {
                    sessionStorage.setItem('accessToken', data.token);
                    setUser(true);
                    setIsAuthenticated(true);
                }
            }

            setLoading(false);
        };

        checkAuth();
    }, []);

    return { isAuthenticated, loading, error, user };
};

export default useCheckAuth;
