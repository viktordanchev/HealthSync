import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars, faXmark } from '@fortawesome/free-solid-svg-icons';
import jwtDecoder from '../services/jwtDecoder';
import { useAuthContext } from '../contexts/AuthContext';
import UserManage from '../components/UserManage';
import HeaderResponsive from '../components/HeaderResponsive';

const Header = () => {
    const { isAuthenticated } = useAuthContext();
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const [userName, setUserName] = useState('');
    const [isFixed, setIsFixed] = useState(false);
    const [userRoles, setUserRoles] = useState([]);
    
    useEffect(() => {
        if (isAuthenticated) {
            const { claimName, roles } = jwtDecoder();
            const splitedName = claimName.split(' ')
                .map((part, index) => {
                    if (index === 0) {
                        return `${part.charAt(0)}.`;
                    }
                    return part;
                }).join(' ');
            
            setUserName(splitedName);

            if (roles) {
                setUserRoles(roles);
            }
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
        <header className={`relative z-40 transition-all duration-300 transform flex bg-maincolor ${isFixed ? 'sticky top-0 h-20 w-4/5 rounded-xl p-5 shadow-2xl shadow-gray-500 translate-y-6 border border-zinc-500 sm:translate-y-3 sm:w-[calc(100%-24px)]' : 'h-24 w-full p-6 translate-y-0'}`}>
            <article className="flex basis-full justify-between items-center">
                <a href="/home" className={`${isFixed ? 'text-3xl' : 'text-4xl'} text-white font-bold hover:text-gray-200 transition duration-300 lg:text-2xl md:text-2xl sm:text-2xl`}>
                    HealthSync
                </a>
                <ul className="flex flex-row w-2/4 space-x-20 md:hidden sm:hidden">
                    <li>
                        <a href="/doctors" className="relative py-1 text-white font-bold text-xl transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Doctors
                        </a>
                    </li>
                    {isAuthenticated && (
                        <li>
                        <a href="/meetings" className="relative py-1 text-white font-bold text-xl transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                            Meetings
                        </a>
                        </li>)}
                    {(isAuthenticated && userRoles.includes('Doctor')) && (
                        <li>
                            <a href="/workSchedule" className="relative py-1 text-white font-bold text-xl transition duration-300 after:content-[''] after:absolute after:bottom-0 after:left-0 after:w-full after:h-[0.1em] after:bg-white after:opacity-0 after:transition-opacity after:transition-transform after:duration-300 after:scale-0 after:origin-center hover:after:opacity-100 hover:after:scale-100 focus:after:opacity-100 focus:after:scale-100">
                                Work Schedule
                            </a>
                        </li>)}
                </ul>
                <div className="flex justify-end space-x-4 w-52 md:hidden sm:hidden">
                    {isAuthenticated ? <UserManage userName={userName} userRoles={userRoles} /> :
                        <>
                            <a href="/login" className="bg-blue-500 border-2 border-blue-500 text-white font-medium py-2 px-3 rounded hover:bg-white hover:text-blue-500 md:text-base sm:text-base">
                                Login
                            </a>
                            <a href="/register" className="bg-blue-500 border-2 border-blue-500 text-white font-medium py-2 px-3 rounded hover:bg-white hover:text-blue-500 md:text-base sm:text-base">
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
            </article>

            <HeaderResponsive
                isMenuOpen={isMenuOpen}
                isFixed={isFixed}
                isAuthenticated={isAuthenticated}
                userName={userName}
                userRoles={userRoles}
            />
        </header>
    );
};

export default Header;
