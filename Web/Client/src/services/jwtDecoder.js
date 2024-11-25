import { jwtDecode } from 'jwt-decode';

function jwtDecoder() {
    try {
        const decodedToken = jwtDecode(localStorage.getItem('accessToken'));

        const claimName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
        const expTime = decodedToken.exp;
        return { claimName, expTime };
    } catch (error) {
        console.error(error);
        return null;
    }
};

export default jwtDecoder;