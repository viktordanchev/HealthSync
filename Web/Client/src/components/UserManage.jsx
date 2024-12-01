import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretUp, faCaretDown } from '@fortawesome/free-solid-svg-icons';
import { useAuthContext } from '../contexts/AuthContext';

function UserManage({ userName }) {
    const { logout } = useAuthContext();
    const [isOpen, setIsOpen] = useState(false);

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
                className={`absolute right-0 z-40 w-52 rounded-xl bg-gray-200 bg-opacity-85 border-2 border-zinc-500 transition-all duration-300 transform ${isOpen ? 'translate-y-0 opacity-100' : '-translate-y-4 opacity-0 pointer-events-none'
                    }`}
            >
                <ul className="text-center text-gray-700 text-xl">
                    <li className="py-1 border-b border-zinc-500 cursor-pointer">info</li>
                    <li className="py-1 border-b border-zinc-500 cursor-pointer">neshto</li>
                    <li
                        onClick={() => logout()}
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
