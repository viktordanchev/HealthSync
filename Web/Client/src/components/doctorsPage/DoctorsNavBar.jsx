import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../../services/apiRequest';
import DropdownMenu from '../doctorsPage/DropdownMenu';

function DoctorsNavBar({ setOrder, setFilter, setSearch, setSearchParams }) {
    const [specialties, setSpecialties] = useState([]);
    const [isCleared, setIsCleared] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [searchOnChange, setSearchOnChange] = useState('');
    const orderTypes = [{ id: '1', name: 'NameAsc' },
        { id: '2', name: 'NameDesc' },
        { id: '3', name: 'RatingAsc' },
        { id: '4', name: 'RatingDesc' }];

    useEffect(() => {
        const receiveData = async () => {
            try {
                const response = await apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false);

                setSpecialties(response);
            } catch (error) {
                console.error(error);
            }
            finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, []);

    const handleEnterPress = (event) => {
        if (event.key === 'Enter') {
            handleSearch();
        }
    };

    const handleSearch = () => {
        setSearch(searchOnChange);
        setSearchOnChange('');
    };

    const handleClear = () => {
        setSearchParams({});
        setOrder('None');
        setFilter('');
        setSearch('');
        setIsCleared(!isCleared);
    };

    return (
        <article className="rounded-full bg-maincolor p-2 flex justify-between items-center space-x-6 shadow-lg shadow-gray-300 md:flex-col md:py-2 md:rounded-xl md:space-y-2 md:space-x-0 sm:w-full sm:flex-col sm:py-2 sm:rounded-xl sm:space-y-2 sm:space-x-0">
            <div className="flex justify-between space-x-2 md:justify-between sm:w-full sm:flex-col sm:space-x-0 sm:space-y-2">
                <button
                    className="bg-white h-9 px-4 rounded-full text-center hover:bg-gray-200"
                    onClick={handleClear}
                >
                    Clear
                </button>
                <DropdownMenu
                    options={orderTypes}
                    optionType="Order"
                    setSelectedOption={(value) => setOrder(value)}
                    clear={isCleared}
                    isLoading={isLoading}
                />
                <DropdownMenu
                    options={specialties}
                    optionType="Filter"
                    setSelectedOption={(value) => setFilter(value)}
                    clear={isCleared}
                    isLoading={isLoading}
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

export default DoctorsNavBar;