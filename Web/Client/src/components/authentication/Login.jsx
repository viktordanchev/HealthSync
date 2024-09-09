import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import * as Cookies from 'js-cookie';

function Login() {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        email: '',
        password: '',
        rememberMe: false
    });

    const handleChange = (e) => {
        const { name, type, checked, value } = e.target;
        setFormData({
            ...formData,
            [name]: type === 'checkbox' ? checked : value,
        });
    };

    const login = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('https://localhost:7080/api/account/login', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },
                body: JSON.stringify(formData),
                credentials: 'include'
            });

            if (response.ok) {
                var data = await response.json();

                if (data.redirectTo) {
                    navigate(data.redirectTo);
                }
            }
        } catch (error) {e
            console.error('Error:', error);
        }
    };

    return (
        <section className="flex items-center justify-center">
            <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                <p className="text-3xl text-center text-white">Welcome back</p>
                <hr className="my-4" />
                <form className="flex flex-col space-y-2" onSubmit={login}>
                    <div>
                        <label className="text-md font-bold">
                            Email
                        </label>
                        <input
                            className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            required />
                    </div>
                    <div>
                        <label className="text-md font-bold">
                            Password
                        </label>
                        <input
                            className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                            type="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            required />
                    </div>
                    <div className="flex items-center justify-between">
                        <label className="inline-flex items-center">
                            <input
                                type="checkbox"
                                name="rememberMe"
                                checked={formData.rememberMe}
                                onChange={handleChange}
                                className="form-checkbox text-blue-600" />
                            <span className="ml-1 text-md text-white">Remember me</span>
                        </label>
                        <a className="inline-block align-baseline text-sm text-blue-500 hover:text-blue-800">
                            Forgot Password?
                        </a>
                    </div>
                    <div className="text-center pt-6">
                        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit">
                            Sign In
                        </button>
                    </div>
                </form>
            </div>
        </section>
    );
}

export default Login;