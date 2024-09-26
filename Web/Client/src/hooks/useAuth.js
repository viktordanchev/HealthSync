import { useState, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';
import { refreshToken } from '../services/account';

function useAuth() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

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
                    } else {
                        console.log(data.error);
                    }
                }
            }
        };

        checkAuth();
    }, []);

    return isAuthenticated;
}

export default useAuth;