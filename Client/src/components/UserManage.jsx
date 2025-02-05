import React, { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown } from '@fortawesome/free-solid-svg-icons';
import { useAuthContext } from '../contexts/AuthContext';
import { useLoading } from '../contexts/LoadingContext';
import apiRequest from '../services/apiRequest';

function UserManage({ userName, userRoles }) {
    const navigate = useNavigate();
    const { logout } = useAuthContext();
    const { setIsLoading } = useLoading();
    const [isOpen, setIsOpen] = useState(false);
    const menuRef = useRef(null);

    {/* This hook handles clicks outside the dropdown menu. */ }
    useEffect(() => {
        const handleClickOutside = (event) => {
            if (menuRef.current && !menuRef.current.contains(event.target)) {
                setIsOpen(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);

        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    const handleLogout = async () => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'logout', undefined, undefined, 'GET', true);

            if (response) {
                logout();
                navigate('/home');
            }
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <article className="relative" ref={menuRef}>
            <div
                className="text-xl flex justify-between items-center text-white hover:cursor-pointer group sm:justify-center"
                onClick={() => setIsOpen(!isOpen)}
            >
                <p className="text-center font-bold py-2 px-4 hover:text-gray-200">{userName}</p>
                <button className="group-hover:text-blue-500">
                    <FontAwesomeIcon icon={isOpen ? faCaretUp : faCaretDown} />
                </button>
            </div>
            <div className={`absolute right-0 z-40 w-52 rounded-xl shadow-xl shadow-gray-300 bg-white bg-opacity-100 transition-all duration-300 transform sm:w-full ${isOpen ? 'opacity-100 translate-y-0' : 'opacity-0 translate-y-[-10px] pointer-events-none'
                    }`}>
                <ul className="text-center text-gray-700 text-xl">
                    <li className="py-3 rounded-t-xl border-b border-zinc-500 cursor-pointer hover:bg-gray-200">
                        <a
                            className="block"
                            href={`${userRoles.includes('Doctor') ? '/doctorProfile' : '/becomeDoctor'}`}
                        >
                            {`${userRoles.includes('Doctor') ? 'Doctor profile' : 'Become a Doctor'}`}
                        </a>
                    </li>
                    <li className="py-3 border-b border-zinc-500 cursor-pointer hover:bg-gray-200">
                        <a
                            className="block"
                            href="/account/settings"
                        >
                            Settings
                        </a>
                    </li>
                    <li
                        onClick={handleLogout}
                        className="py-3 rounded-b-xl font-bold cursor-pointer hover:bg-gray-200"
                    >
                        Logout
                    </li>
                </ul>
            </div>
        </article>
    );
}

export default UserManage;
