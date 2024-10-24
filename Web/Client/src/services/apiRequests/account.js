const url = 'https://localhost:7080/account';

export const login = async (values) => {
    try {
        const response =
            await fetch(`${url}/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values)
            });

        const data = await response.json();

        if (Array.isArray(data)) {
            let errorMessage = 'Failed attempt to login';

            errorMessage = errorData.join('\n');

            throw new Error(errorMessage);
        }

        return data;
    } catch (error) {
        console.error(error.message);
    }
};

export const register = async (values) => {
    try {
        const response =
            await fetch(`${url}/register`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values)
            });

        if (response.ok) {
            return null;
        }

        const data = await response.json();

        if (Array.isArray(data)) {
            let errorMessage = 'Failed attempt to register';

            errorMessage = errorData.join('\n');

            throw new Error(errorMessage);
        }

        return data;
    } catch (error) {
        console.error(error.message);
    }
};

export const verifyAccount = async (values) => {
    try {
        const response =
            await fetch(`${url}/verifyAccount`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values),
                credentials: 'include'
            });

        if (response.ok) {
            return null;
        }

        const data = await response.json();

        if (Array.isArray(data)) {
            let errorMessage = 'Failed account verification';

            errorMessage = errorData.join('\n');

            throw new Error(errorMessage);
        }

        return data;
    } catch (error) {
        console.error(error.message);
    }
};

export const sendVrfCode = async (email) => {
    try {
        const response = 
            await fetch(`${url}/sendVrfCode`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(email)
            });

        if (!response.ok && response.status !== 400 && response.status !== 401) {
            throw new Error('Failed to send verification code');
        }

        return await response.json();
    } catch (error) {
        console.log(error.message);
    }
};

export const sendRecoverPasswordEmail = async (email) => {
    try {
        const response = 
            await fetch(`${url}/sendRecoverPasswordEmail`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(email)
            });

        if (!response.ok && response.status !== 400) {
            throw new Error('Failed to send recover password email');
        }

        return await response.json();
    } catch(error) {
        console.error(error.message);
    }
};

export const recoverPassword = async (values) => {
    try {
        const response =
            await fetch(`${url}/recoverPassword`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values)
            });

        if (response.ok) {
            return null;
        }

        const data = await response.json();

        if (Array.isArray(data)) {
            let errorMessage = 'Failed password recovering';

            errorMessage = errorData.join('\n');

            throw new Error(errorMessage);
        }

        return data;
    } catch (error) {
        console.error(error.message);
    }
};

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