﻿import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function Register() {
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            navigate('/home/da');
        }
    }, [navigate]);

    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: '',
    });
    const [message, setMessage] = useState('');

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const register = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('https://localhost:7080/api/account/register', {
                method: 'POST',
                headers: {
                    'Content-type': 'application/json'
                },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                setFormData({
                    firstName: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: '',
                });

                setMessage('Registered user');
            } else {
                setMessage('Failed');
            }
        } catch (error) {
            console.error('Error:', error);
            setMessage('Failed');
        }

        setTimeout(() => { setMessage(''); }, 3000);
    };

    return (
        <>
            {message !== '' ? (
                <div className="flex justify-center">
                    <div className="max-w-xs text-center text-white text-xl bg-green-500 rounded-xl p-4">
                        {message}
                    </div>
                </div>
            ) : null}

            <section className="flex items-center justify-center">
                <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                    <p className="text-3xl text-center text-white">Welcome</p>
                    <hr className="my-4" />
                    <form className="flex flex-col space-y-2" onSubmit={register}>
                        <div className="flex flex-row space-x-4">
                            <div>
                                <label className="text-md font-bold">
                                    First name
                                </label>
                                <input
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                    type="text"
                                    name="firstName"
                                    value={formData.firstName}
                                    onChange={handleChange}
                                    required />
                            </div>
                            <div>
                                <label className="text-md font-bold">
                                    Last name
                                </label>
                                <input
                                    className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                    type="text"
                                    name="lastName"
                                    value={formData.lastName}
                                    onChange={handleChange}
                                    required />
                            </div>
                        </div>
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
                        <div>
                            <label className="text-md font-bold">
                                Confirm Password
                            </label>
                            <input
                                className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                type="password"
                                name="confirmPassword"
                                value={formData.confirmPassword}
                                onChange={handleChange}
                                required />
                        </div>
                        <div className="text-center pt-6">
                            <button
                                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                                type="submit">
                                Register
                            </button>
                        </div>
                    </form>
                </div>
            </section>
        </>
    );
}

export default Register;
