import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../../services/apiRequest';

function DoctorsNavBar({ order, setOrder, filter, setFilter, setSearch, setSearchParams }) {
    const [specialties, setSpecialties] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [searchOnChange, setSearchOnChange] = useState('');

    useEffect(() => {
        const receiveSpecialties = async () => {
            try {
                const response = await apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false);

                setSpecialties(response);
                setIsLoading(false);
            } catch (error) {
                console.error(error);
            }
        };

        receiveSpecialties();
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
    };

    return (
        <article className="rounded-full bg-maincolor p-2 flex justify-between items-center space-x-6 shadow-lg shadow-gray-300 md:flex-col md:py-2 md:rounded-xl md:space-y-2 md:space-x-0 sm:w-full sm:flex-col sm:py-2 sm:rounded-xl sm:space-y-2 sm:space-x-0">
            <div className="flex space-x-2 md:justify-between sm:flex-col sm:space-x-0 sm:space-y-2">
                <button
                    className="bg-white h-8 px-4 rounded-full text-center hover:bg-gray-200"
                    onClick={handleClear}
                >
                    Clear
                </button>
                <select className="bg-white h-8 rounded-full text-center hover:bg-gray-200 hover:cursor-pointer focus:outline-none"
                    value={order}
                    onChange={(e) => setOrder(e.target.value)}
                >
                    <option value="None" disabled hidden>Order</option>
                    <option value="NameAsc">NameAsc</option>
                    <option value="NameDesc">NameDesc</option>
                    <option value="RatingAsc">RatingAsc</option>
                    <option value="RatingDesc">RatingDesc</option>
                </select>
                <select className="bg-white h-8 rounded-full text-center hover:bg-gray-200 hover:cursor-pointer focus:outline-none"
                    value={filter}
                    onChange={(e) => setFilter(e.target.value)}
                >
                    <option value="" disabled hidden>Filter</option>
                    {isLoading ?
                        <option disabled value="">
                            Loading...
                        </option> :
                        <>
                            {specialties.map((specialty) => (
                                <option
                                    key={specialty.id}
                                    value={specialty.name}
                                >
                                    {specialty.name}
                                </option>
                            ))}
                        </>}
                </select>
            </div>
            <div className="flex items-center sm:w-full">
                <input
                    placeholder="Search doctor..."
                    className="text-lg rounded-s-3xl bg-white h-8 p-4 focus:outline-none focus:ring-1 focus:ring-blue-500 sm:w-full"
                    value={searchOnChange}
                    onChange={(e) => setSearchOnChange(e.target.value)}
                    onKeyDown={handleEnterPress}
                >
                </input>
                <button className="bg-blue-500 h-8 w-9 rounded-e-3xl hover:scale-110"
                    onClick={handleSearch}>
                    <FontAwesomeIcon icon={faMagnifyingGlass} className="text-white text-xl" />
                </button>
            </div>
        </article>
    );
}

export default DoctorsNavBar;