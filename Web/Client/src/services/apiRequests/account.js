const url = 'https://localhost:7080/account';

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

export const verifyAccount = async (values) =>
    await fetch(`${url}/verifyAccount`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(values),
        credentials: 'include'
    });

export const sendVrfCode = async (email) =>
    await fetch(`${url}/sendVrfCode`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(email),
        credentials: 'include'
    });

export const sendRecoverPasswordEmail = async (email) =>
    await fetch(`${url}/sendRecoverPasswordEmail`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(email),
        credentials: 'include'
    });

export const recoverPassword = async (values) =>
    await fetch(`${url}/recoverPassword`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(values),
        credentials: 'include'
    });

export const refreshToken = async () => {
    try {
        const response =
            await fetch(`${url}/refreshToken`, {
                method: 'GET',
                credentials: 'include'
            });

        if (!response.ok) {
            throw new Error('Failed to fetch');
        }
        
        const data = await response.json();
        return data.token;
    } catch (error) {
        console.error(error.message);
    }
};