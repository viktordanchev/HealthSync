import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import DropdownMenu from '../DropdownMenu';

function NavigationBar({ searchParams, setSearchParams, specialties }) {
    const [searchOnChange, setSearchOnChange] = useState('');
    const orderTypes = [{ id: '1', name: 'NameAsc' },
    { id: '2', name: 'NameDesc' },
    { id: '3', name: 'RatingAsc' },
    { id: '4', name: 'RatingDesc' }];

    const setNewSearchParams = (key, value) => {
        const currentParams = Object.fromEntries(searchParams);
        setSearchParams({ ...currentParams, [key]: value });
    };

    const handleEnterPress = (event) => {
        if (event.key === 'Enter') {
            handleSearch();
        }
    };

    const handleSearch = () => {
        setNewSearchParams('search', searchOnChange);
        setSearchOnChange('');
    };

    const handleClear = () => {
        setSearchParams({});
    };

    return (
        <article className="rounded-full bg-maincolor p-2 flex justify-between items-center space-x-6 shadow-lg shadow-gray-300 md:flex-col md:py-2 md:rounded-xl md:space-y-2 md:space-x-0 sm:w-full sm:flex-col sm:py-2 sm:rounded-xl sm:space-y-2 sm:space-x-0">
            <div className="min-w-72 flex justify-between space-x-2 md:justify-between sm:w-full sm:flex-col sm:space-x-0 sm:space-y-2">
                <button className="bg-white h-9 px-4 rounded-full text-center hover:bg-gray-200" onClick={handleClear}>Clear</button>
                <DropdownMenu
                    classes={"rounded-full"}
                    options={orderTypes}
                    optionType={searchParams.get('order') ? searchParams.get('order') : 'Order'}
                    setSelectedOption={(value) => setNewSearchParams('order', value.name)}
                />
                <DropdownMenu
                    classes={"rounded-full"}
                    options={specialties}
                    optionType={searchParams.get('filter') ? searchParams.get('filter') : 'Filter'}
                    setSelectedOption={(value) => setNewSearchParams('filter', value.name)}
                />
            </div>
            <div className="flex items-center sm:w-full">
                <input
                    placeholder="Search doctor..."
                    className="text-lg border border-r-0 border-white rounded-s-3xl bg-white h-9 p-4 focus:outline-none focus:border-blue-500 sm:w-full"
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