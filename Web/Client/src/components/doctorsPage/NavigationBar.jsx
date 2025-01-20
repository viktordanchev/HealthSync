import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import DropdownMenu from './DropdownMenu';

function NavigationBar({ searchParams, setSearchParams, specialties }) {
    const [isCleared, setIsCleared] = useState(false);
    const [searchOnChange, setSearchOnChange] = useState('');
    const orderTypes = [{ id: '1', name: 'NameAsc' },
        { id: '2', name: 'NameDesc' },
        { id: '3', name: 'RatingAsc' },
        { id: '4', name: 'RatingDesc' }];
    
    const handleEnterPress = (event) => {
        if (event.key === 'Enter') {
            handleSearch();
        }
    };

    const handleSearch = () => {
        setSearchParams({ search: searchOnChange });
        setSearchOnChange('');
    };

    const handleClear = () => {
        setSearchParams({});
        setIsCleared(!isCleared);
    };

    return (
        <article className="rounded-full bg-maincolor p-2 flex justify-between items-center space-x-6 shadow-lg shadow-gray-300 md:flex-col md:py-2 md:rounded-xl md:space-y-2 md:space-x-0 sm:w-full sm:flex-col sm:py-2 sm:rounded-xl sm:space-y-2 sm:space-x-0">
            <div className="flex justify-between space-x-2 md:justify-between sm:w-full sm:flex-col sm:space-x-0 sm:space-y-2">
                <button
                    className="bg-white h-9 px-4 rounded-full text-center hover:bg-gray-200"
                    onClick={handleClear}>Clear</button>
                <DropdownMenu
                    options={orderTypes}
                    optionType={searchParams.get('order') ? searchParams.get('order') : 'Order'}
                    setSelectedOption={(value) => setSearchParams({ order: value})}
                    isCleared={isCleared}
                />
                <DropdownMenu
                    options={specialties}
                    optionType={searchParams.get('filter') ? searchParams.get('filter') : 'Filter'}
                    setSelectedOption={(value) => setSearchParams({ filter: value })}
                    isCleared={isCleared}
                />
            </div>
            <div className="flex items-center sm:w-full">
                <input
                    placeholder="Search doctor..."
                    className="text-lg rounded-s-3xl bg-white h-9 p-4 focus:outline-none focus:ring-1 focus:ring-blue-500 sm:w-full"
                    value={searchOnChange}
                    onChange={(e) => setSearchOnChange(e.target.value)}
                    onKeyDown={handleEnterPress}
                >
                </input>
                <button className="bg-blue-500 h-9 w-9 rounded-e-3xl hover:scale-110"
                    onClick={handleSearch}>
                    <FontAwesomeIcon icon={faMagnifyingGlass} className="text-white text-xl" />
                </button>
            </div>
        </article>
    );
}

export default NavigationBar;