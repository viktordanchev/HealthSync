import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars, faXmark } from '@fortawesome/free-solid-svg-icons';
import { jwtDecode } from 'jwt-decode';
import useCheckAuth from '../hooks/useCheckAuth';

const Header = () => {
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [userName, setUserName] = useState('');
    const { isAuthenticated, loading } = useCheckAuth();

    useEffect(() => {
        if (isAuthenticated) {
            const token = sessionStorage.getItem('accessToken');
            const decodedToken = jwtDecode(token);
            setUserName(decodedToken['Name']);
        }
    }, [isAuthenticated]);

    const toggleMenu = () => {
        setIsMenuOpen(!isMenuOpen);
    };

    return (
        <header className="relative bg-maincolor mt-6 rounded-xl p-5 mx-32 md:mx-16 sm:mx-6">
            <div className="flex justify-between items-center">
                <a href="/home" className="text-white font-bold text-3xl hover:text-gray-200 transition duration-300 md:text-2xl sm:text-xl">
                    HealthSync
                </a>
                <ul className="flex flex-row w-2/4 justify-between sm:hidden">
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Doctors
                        </a>
                    </li>
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Home
                        </a>
                    </li>
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Products
                        </a>
                    </li>
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Products
                        </a>
                    </li>
                </ul>
                <div className="flex space-x-4 sm:hidden">
                    {loading ? <div>Loading...</div> :
                        <>
                            {isAuthenticated ? (
                                <p className="text-white text-lg font-bold py-2 px-4">
                                    {userName}
                                </p>
                            ) : (
                                <>
                                    <a href="/login" className="bg-blue-500 text-white text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300 sm:text-base">
                                        Login
                                    </a>
                                    <a href="/register" className="bg-blue-500 text-white text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300 sm:text-base">
                                        Register
                                    </a>
                                </>
                            )}
                        </>}
                </div>
                <button
                    onClick={toggleMenu}
                    className={`hidden ${isMenuOpen ? 'hidden' : 'sm:block'}`}
                >
                    <FontAwesomeIcon icon={faBars} className="text-white text-3xl" />
                </button>

                <button
                    onClick={toggleMenu}
                    className={`hidden ${isMenuOpen ? 'sm:block' : 'hidden'}`}
                >
                    <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                </button>
            </div>

            <div className={`absolute top-full left-0 right-0 bg-maincolor p-5 rounded-lg mt-2 z-10 hidden sm:block transition-all duration-500 ease-in-out transform ${isMenuOpen ? 'opacity-100 translate-y-0' : 'opacity-0 translate-y-[-10px] hidden'}`}>
                <ul className="flex flex-col items-center space-y-4 mb-4">
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Products
                        </a>
                    </li>
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Home
                        </a>
                    </li>
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Products
                        </a>
                    </li>
                    <li>
                        <a href="#0" className="relative py-1 text-white font-bold text-xl text-inherit hover:text-gray-200 transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Products
                        </a>
                    </li>
                </ul>
                <div className="flex flex-row justify-center space-x-6">
                    <button className="w-1/2 bg-blue-500 text-white text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                        Login
                    </button>
                    <button className="w-1/2 bg-blue-500 text-white text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                        Register
                    </button>
                </div>
            </div>
        </header >
    );
};

export default Header;
