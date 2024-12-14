import { jwtDecode } from 'jwt-decode';

function jwtDecoder() {
    try {
        const decodedToken = jwtDecode(localStorage.getItem('accessToken'));

        const userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
        const claimName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
        const expTime = decodedToken.exp;
        const roles = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

        return { userId, claimName, expTime, roles };
    } catch (error) {
        console.error(error);
        return null;
    }
};

export default jwtDecoder;