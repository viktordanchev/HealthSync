import React from 'react';

function Logout() {
    const logout = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('https://localhost:7080/api/account/login', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                var data = await response.json();

                if (formData.rememberMe) {
                    localStorage.setItem('jwtToken', data.jwtToken);
                } else {
                    sessionStorage.setItem('jwtToken', data.jwtToken);
                }

                if (data.redirectTo) {
                    navigate(data.redirectTo);
                }
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };
}

export default Logout;