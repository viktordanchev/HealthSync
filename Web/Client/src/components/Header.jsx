import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars, faXmark } from '@fortawesome/free-solid-svg-icons';
import jwtDecoder from '../services/jwtDecoder';
import { useAuthContext } from '../contexts/AuthContext';
import UserManage from '../components/UserManage';

const Header = () => {
    const { isAuthenticated } = useAuthContext();
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [userName, setUserName] = useState('');
    const [isFixed, setIsFixed] = useState(false);

    useEffect(() => {
        if (isAuthenticated) {
            const { claimName } = jwtDecoder();

            setUserName(claimName);
        }
    }, [isAuthenticated, localStorage.getItem('accessToken')]);

    useEffect(() => {
        const handleScroll = () => {
            if (window.scrollY > 100) {
                setIsFixed(true);
            } else {
                setIsFixed(false);
            }
        };

        window.addEventListener('scroll', handleScroll);

        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    }, []);

    const toggleMenu = () => {
        setIsMenuOpen(!isMenuOpen);
    };

    return (
        <header className={`transition-all duration-300 transform flex bg-maincolor ${isFixed ? 'sticky top-0 z-40 h-20 w-4/5 rounded-xl p-5 shadow-2xl shadow-gray-500 translate-y-6 sm:translate-y-3 sm:w-[calc(100%-24px)]' : 'h-24 w-full p-6 translate-y-0'}`}>
            <div className="flex basis-full justify-between items-center">
                <a href="/home" className={`${isFixed ? 'text-3xl' : 'text-4xl'} text-white font-bold hover:text-gray-200 transition duration-300 lg:text-2xl md:text-2xl sm:text-2xl`}>
                    HealthSync
                </a>
                <ul className="flex flex-row w-2/4 justify-between md:hidden sm:hidden">
                    <li>
                        <a href="/doctors" className="relative py-1 text-white font-bold text-xl transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Doctors
                        </a>
                    </li>
                    <li>
                        <a href="/meetings" className="relative py-1 text-white font-bold text-xl transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Meetings
                        </a>
                    </li>
                </ul>
                <div className="flex justify-end space-x-4 w-52 md:hidden sm:hidden">
                    {isAuthenticated ? <UserManage userName={userName} /> :
                        <>
                            <a href="/login" className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-2 px-3 rounded hover:bg-white hover:text-blue-500 md:text-base sm:text-base">
                                Login
                            </a>
                            <a href="/register" className="bg-blue-500 border-2 border-blue-500 text-white font-bold py-2 px-3 rounded hover:bg-white hover:text-blue-500 md:text-base sm:text-base">
                                Register
                            </a>
                        </>}
                </div>
                <button
                    onClick={toggleMenu}
                    className={`hidden ${isMenuOpen ? 'hidden' : 'md:block sm:block'}`}
                >
                    <FontAwesomeIcon icon={faBars} className="text-white text-3xl" />
                </button>

                <button
                    onClick={toggleMenu}
                    className={`hidden ${isMenuOpen ? 'md:block sm:block' : 'hidden'}`}
                >
                    <FontAwesomeIcon icon={faXmark} className="text-white text-3xl" />
                </button>
            </div>

            <div className={`absolute top-full left-0 right-0 bg-maincolor p-5 rounded-xl mt-2 z-40 hidden transition-all duration-500 ease-in-out transform md:block sm:block ${isMenuOpen ? 'opacity-100 translate-y-0' : 'opacity-0 translate-y-[-10px] hidden'} ${isFixed ? '' : 'mx-3'}`}>
                <ul className="flex flex-col items-center space-y-4 mb-4">
                    <li>
                        <a href="/doctors" className="relative py-1 text-white font-bold text-xl">
                            Doctors
                        </a>
                    </li>
                </ul>
                <hr className="border-e border-white w-full my-3" />
                {isAuthenticated ? <UserManage userName={userName} /> :
                    <div className="flex flex-row justify-center space-x-6">
                        <a href="/login" className="w-1/2 bg-blue-500 text-white text-center text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                            Login
                        </a>
                        <a href="/register" className="w-1/2 bg-blue-500 text-white text-center text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                            Register
                        </a>
                    </div>}
            </div>
        </header>
    );
};

export default Header;
