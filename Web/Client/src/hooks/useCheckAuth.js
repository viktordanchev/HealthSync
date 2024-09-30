import { useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import { refreshToken } from '../services/account';

const useAuth = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const checkAuth = async () => {
            const token = localStorage.getItem('accessToken');

            if (token) {
                const decodedToken = jwtDecode(token);
                const currentTime = Date.now() / 1000;

                if (decodedToken.exp > currentTime) {
                    setIsAuthenticated(true);
                } else {
                    localStorage.removeItem('accessToken');
                    const data = await refreshToken();

                    if (data.token) {
                        localStorage.setItem('accessToken', data.token);
                        setIsAuthenticated(true);
                    } else {
                        console.log(data.error);
                    }
                }
            }

            setLoading(false);
        };

        checkAuth();
    }, []);

    return { isAuthenticated, loading };
};

export default useAuth;
