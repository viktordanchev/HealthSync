import jwtDecoder from '../services/jwtDecoder';

function useAuth() {
    let isAuthenticated = false;
    const token = localStorage.getItem("accessToken");

    if (token) {
        try {
            const { expTime } = jwtDecoder();

            if (expTime * 1000 > Date.now()) {
                isAuthenticated = true;
            }
        } catch (error) {
            console.error(error.message);
        }
    }

    return isAuthenticated;
}

export default useAuth;