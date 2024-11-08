import { fetchError } from '../constants/errors';

const url = 'https://localhost:7080';
const headers = { 'Content-Type': 'application/json' };

async function apiRequest(controller, action, values, jwtToken, method, credentials) {
    const requestOptions = {
        method: `${method}`,
        headers: headers
    };

    if (jwtToken) {
        headers['Authorization'] = `Bearer ${jwtToken}`;
    }

    if (credentials) {
        requestOptions.credentials = 'include';
    }

    if (values) {
        requestOptions.body = JSON.stringify(values);
    }

    try {
        const response = await fetch(`${url}/${controller}/${action}`, requestOptions);
        
        if (response.status >= 500) {
            throw new Error(fetchError);
        }

        const data = await response.json();
        
        if (data.serverError) {
            throw new Error(data.serverError);
        }

        return data;
    } catch (error) {
        console.error(error.message);
    }
}

export default apiRequest;