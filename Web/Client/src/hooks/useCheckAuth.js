import { useEffect, useState } from 'react';
import { jwtDecode } from 'jwt-decode';
import apiRequest from '../services/apiRequest';

const useCheckAuth = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);
    const [isSessionEnd, setIsSessionEnd] = useState(false);
    const [jwtToken, setJwtToken] = useState('');
    const [decodedJwtToken, setDecodedJwtToken] = useState('');

    useEffect(() => {
        const checkAuth = async () => {
            const token = sessionStorage.getItem('accessToken');
            setJwtToken(token);

            if (token) {
                const decodedToken = jwtDecode(token);
                const currentTime = Date.now() / 1000;

                setDecodedJwtToken(decodedToken);

                if (decodedToken.exp > currentTime) {
                    setIsAuthenticated(true);
                } else {
                    const newJwtToken = await apiRequest('account', 'refreshToken', undefined, undefined, 'GET', true);

                    if (newJwtToken) {
                        setNewJwtToken(newJwtToken);
                    } else {
                        setIsSessionEnd(true);
                    }
                }
            } else {
                const newJwtToken = await apiRequest('account', 'refreshToken', undefined, undefined, 'GET', true);
                
                if (newJwtToken) {                    
                    setNewJwtToken(newJwtToken);
                }
            }

            setLoading(false);
        };

        checkAuth();
    }, []);

    const setNewJwtToken = (jwtToken) => {
        sessionStorage.setItem('accessToken', jwtToken);
        setJwtToken(token);
        setIsAuthenticated(true);
    };

    return { isAuthenticated, loading, isSessionEnd, jwtToken, decodedJwtToken };
};

export default useCheckAuth;
