const url = 'https://localhost:7080/account';

export const login = async (values) =>
    await fetch(`${url}/login1`, {
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

export const resendVrfCode = async (email) =>
    await fetch(`${url}/resendVrfCode`, {
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

export const refreshToken = async () =>
    await fetch(`${url}/refreshToken`, {
        method: 'GET',
        credentials: 'include'
    });