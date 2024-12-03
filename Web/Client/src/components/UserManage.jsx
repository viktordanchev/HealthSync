import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown } from '@fortawesome/free-solid-svg-icons';
import { useAuthContext } from '../contexts/AuthContext';
import { useLoading } from '../contexts/LoadingContext';
import apiRequest from '../services/apiRequest';

function UserManage({ userName }) {
    const navigate = useNavigate();
    const { logout } = useAuthContext();
    const { setIsLoading } = useLoading();
    const [isOpen, setIsOpen] = useState(false);

    const handleLogout = async () => {
        try {
            setIsLoading(true);

            const response = await apiRequest('account', 'logout', undefined, localStorage.getItem('accessToken'), 'GET', true);
            
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
        <article className="relative">
            <div
                onClick={() => setIsOpen(!isOpen)}
                className="text-xl flex justify-between items-center text-white hover:cursor-pointer group"
            >
                <p className="basis-full text-center font-bold py-2 px-4">
                    {userName}
                </p>
                <button className="group-hover:text-blue-500">
                    <FontAwesomeIcon icon={isOpen ? faCaretUp : faCaretDown} />
                </button>
            </div>
            <div
                className={`absolute right-0 z-40 w-52 rounded-xl shadow-xl bg-white bg-opacity-100 border border-zinc-500 transition-all duration-300 transform ${isOpen ? 'translate-y-0 opacity-100' : 'translate-y-4 opacity-0 pointer-events-none'
                    }`}
            >
                <ul className="text-center text-gray-700 text-xl">
                    <li className="py-1 border-b border-zinc-500 cursor-pointer">info</li>
                    <li className="py-1 border-b border-zinc-500 cursor-pointer">Settings</li>
                    <li
                        onClick={handleLogout}
                        className="py-1 font-bold cursor-pointer"
                    >
                        Logout
                    </li>
                </ul>
            </div>
        </article>
    );
}

export default UserManage;
