import { useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import { refreshToken } from '../services/apiRequests/account';
import { authErrors } from '../constants/errors';

const useCheckAuth = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        const checkAuth = async () => {
            const token = sessionStorage.getItem('accessToken');

            if (token) {
                const decodedToken = jwtDecode(token);
                const currentTime = Date.now() / 1000;

                if (decodedToken.exp > currentTime) {
                    setIsAuthenticated(true);
                } else {
                    const token = await refreshToken();

                    if (token) {
                        sessionStorage.setItem('accessToken', token);
                        setIsAuthenticated(true);
                    } else {
                        setError(authErrors.SessionEnd);
                    }
                }
            } else {
                const token = await refreshToken();
                
                if (token) {                    
                    sessionStorage.setItem('accessToken', token);
                    setIsAuthenticated(true);
                }
            }

            setLoading(false);
        };

        checkAuth();
    }, []);

    return { isAuthenticated, loading, error };
};

export default useCheckAuth;
