import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleUp, faAngleDown } from '@fortawesome/free-solid-svg-icons';

function DropdownMenu({ options, optionType, setSelectedOption }) {
    const [isOpen, setIsOpen] = useState(false);
    const [selected, setSelected] = useState('');
    
    const handleOptionClick = (option) => {
        setSelectedOption(option.id);
        setSelected(option.name);
        setIsOpen(false);
    };

    return (
        <div className="relative text-gray-700">
            <div
                className="w-full flex items-center justify-between space-x-2 rounded py-1 px-2 border-2 border-white bg-white cursor-pointer"
                onClick={() => setIsOpen(!isOpen)}
            >
                <span className="overflow-hidden text-ellipsis whitespace-nowrap md:max-w-80 sm:max-w-44">{selected || optionType}</span>
                <FontAwesomeIcon icon={isOpen ? faAngleUp : faAngleDown} />
            </div>
            {isOpen && (
                <ul
                    className="absolute top-10 z-10 w-full bg-white border border-gray-300 rounded shadow-lg max-h-60 overflow-auto"
                >
                    {options.map((option) => (
                        <li
                            key={option.id}
                            title={option.name}
                            className="p-2 hover:bg-gray-200 cursor-pointer text-ellipsis overflow-hidden whitespace-nowrap"
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