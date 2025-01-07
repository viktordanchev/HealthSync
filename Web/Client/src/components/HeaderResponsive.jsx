import React from 'react';
import { useAuthContext } from '../contexts/AuthContext';
import { useLoading } from '../contexts/LoadingContext';
import apiRequest from '../services/apiRequest';
import jwtDecoder from '../services/jwtDecoder';

function HeaderResponsive({ isMenuOpen, isFixed, isAuthenticated, userName }) {
    const { logout } = useAuthContext();
    const { setIsLoading } = useLoading();
    let userRoles;

    if (isAuthenticated) {
        const { roles } = jwtDecoder();
        userRoles = roles;
    }

    const handleLogout = async () => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'logout', undefined, undefined, 'GET', true);

            if (response) {
                logout();
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <article className={`absolute top-full left-0 right-0 m-3 p-3 bg-maincolor rounded-xl transition-all duration-500 ease-in-out transform space-y-3
        ${isMenuOpen ? 'opacity-100 translate-y-0 shadow-2xl shadow-gray-500' : 'opacity-0 translate-y-[-10px] pointer-events-none'} 
        ${isFixed && 'm-0 mt-3'}`}>
            {isAuthenticated ?
                <div className="text-white font-bold text-xl space-y-3">
                    <p className="text-center">{userName}</p>
                    <ul className="flex flex-col items-center space-y-3">
                        <li>
                            <a href={`${userRoles.includes('Doctor') ? '/home' : '/becomeDoctor'}`}>
                                {`${userRoles.includes('Doctor') ? 'Doctor profile' : 'Become a Doctor'}`}
                            </a>
                        </li>
                        <li>
                            <a href="/account/settings">Settings</a>
                        </li>
                        <li>
                            <button onClick={handleLogout}>Logout</button>
                        </li>
                    </ul>
                </div> :
                <div className="flex flex-row justify-evenly">
                    <a href="/login" className="w-1/3 bg-blue-500 text-white text-center text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                        Login
                    </a>
                    <a href="/register" className="w-1/3 bg-blue-500 text-white text-center text-lg font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                        Register
                    </a>
                </div>}
            <hr className="border-e border-white w-full" />
            <ul className="flex flex-col items-center space-y-3 text-white font-bold text-xl">
                <li>
                    <a href="/doctors">
                        Doctors
                    </a>
                </li>
                {isAuthenticated && (
                    <li>
                        <a href="/meetings">
                            Meetings
                        </a>
                    </li>
                )}
            </ul>
        </article>
    );
}

export default HeaderResponsive;