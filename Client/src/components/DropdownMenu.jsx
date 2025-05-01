import React, { useState, useEffect, useRef } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleUp, faAngleDown } from '@fortawesome/free-solid-svg-icons';

function DropdownMenu({ classes, options, optionType, setSelectedOption }) {
    const [isOpen, setIsOpen] = useState(false);
    const [selected, setSelected] = useState(optionType);
    const dropdownRef = useRef(null);

    {/* This hook handles clicks outside the dropdown menu. */ }
    useEffect(() => {
        const handleClickOutside = (event) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setIsOpen(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);

        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    const handleOptionClick = (option) => {
        setSelectedOption(option);
        setSelected(option.name);
        setIsOpen(false);
    };

    return (
        <div className="relative text-gray-700" ref={dropdownRef}>
            <div
                className={`flex justify-between items-center space-x-2 py-1 px-2 border-2 border-white bg-white cursor-pointer
                ${classes}`}
                onClick={() => setIsOpen(!isOpen)}
            >
                <span className="truncate">{selected}</span>
                <FontAwesomeIcon icon={isOpen ? faAngleUp : faAngleDown} />
            </div>
            {isOpen && (
                <ul
                    className="absolute top-10 z-10 w-full bg-white border border-zinc-500 rounded shadow-2xl shadow-gray-500 max-h-60 overflow-auto"
                >
                    {options.map((option) => (
                        <li
                            key={option.id}
                            title={option.name}
                            className="p-2 hover:bg-gray-200 cursor-pointer truncate"
                            onClick={() => handleOptionClick(option)}
                        >
                            {option.name}
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}

export default DropdownMenu;