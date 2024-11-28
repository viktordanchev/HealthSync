import apiRequest from '../services/apiRequest';

function useRefreshToken() {
    const refreshAccessToken = async () => {
        let isRefreshed = false;

        try {
            const response = await apiRequest('account', 'refreshToken', undefined, undefined, 'GET', true);

            if (response.token) {
                localStorage.setItem('accessToken', response.token);
                isRefreshed = true;
            }
        } catch (error) {
            console.error(error);
        }

        return isRefreshed;
    };

    return { refreshAccessToken };
}

export default useRefreshToken;