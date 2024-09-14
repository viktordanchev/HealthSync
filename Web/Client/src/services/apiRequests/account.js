const url = 'https://localhost:7080/api/account';

export const login = async (values) =>
    await fetch(`${url}/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(values),
        credentials: 'include'
    });

export const register = async (values) =>
    await fetch(`${url}/register`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(values)
    });

export const isAccessTokenExpired = async () =>
    await fetch(`${url}/isAccessTokenExpired`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'include'
    });