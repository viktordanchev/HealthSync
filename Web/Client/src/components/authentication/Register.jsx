import React, { useState } from 'react';

function Register() {
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: '',
    });

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const register = async (e) => {
        e.preventDefault();

        await fetch('https://localhost:7080/api/account/register', {
            method: 'POST',
            headers: {
                'Content-type': 'application/json'
            },
            body: JSON.stringify(formData)
        });
    };

    return (
        <section className="flex items-center justify-center">
            <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                <p className="text-3xl text-center text-white">Welcome</p>
                <hr className="my-4" />
                <form className="flex flex-col space-y-2" onSubmit={register}>
                    <div className="flex flex-row space-x-4">
                        <div>
                            <label className="text-md font-bold" htmlFor="firstName">
                                First name
                            </label>
                            <input
                                className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                type="text"
                                name="firstName"
                                id="firstName"
                                value={formData.firstName}
                                onChange={handleChange}
                                required />
                        </div>
                        <div>
                            <label className="text-md font-bold" htmlFor="lastName">
                                Last name
                            </label>
                            <input
                                className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                                type="text"
                                name="lastName"
                                id="lastName"
                                value={formData.lastName}
                                onChange={handleChange}
                                required />
                        </div>
                    </div>
                    <div>
                        <label className="text-md font-bold" htmlFor="email">
                            Email
                        </label>
                        <input
                            className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                            type="email"
                            name="email"
                            id="email"
                            value={formData.email}
                            onChange={handleChange}
                            required />
                    </div>
                    <div>
                        <label className="text-md font-bold" htmlFor="password">
                            Password
                        </label>
                        <input
                            className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                            type="password"
                            name="password"
                            id="password"
                            value={formData.password}
                            onChange={handleChange}
                            required />
                    </div>
                    <div>
                        <label className="text-md font-bold" htmlFor="confirmPassword">
                            Confirm Password
                        </label>
                        <input
                            className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none"
                            type="password"
                            name="confirmPassword"
                            id="confirmPassword"
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
    );
}

export default Register;
