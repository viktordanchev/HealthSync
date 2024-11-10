import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import apiRequest from '../../services/apiRequest';

function DoctorsNavBar({ order, setOrder, filter, setFilter, search, setSearch }) {
    const [specialties, setSpecialties] = useState([]);
    const [loading, setLoading] = useState(true);
    const [searchOnChange, setSearchOnChange] = useState('');

    useEffect(() => {
        const receiveSpecialties = async () => {
            const response = await apiRequest('doctor', 'getSpecialties', undefined, undefined, 'GET', false);

            setSpecialties(response);
            setLoading(false);
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

    return (
        <article className="rounded-full bg-maincolor my-6 p-2 flex justify-between items-center space-x-6 sm:w-full sm:flex-col sm:py-2 sm:rounded-xl sm:space-y-2 sm:space-x-0">
            <div className="flex space-x-3 sm:justify-between">
                <select className="bg-white h-8 rounded-full text-center focus:outline-none"
                    value={order}
                    onChange={(e) => setOrder(e.target.value)}
                >
                    <option value="None" disabled hidden>Order</option>
                    <option value="NameAsc">NameAsc</option>
                    <option value="NameDesc">NameDesc</option>
                    <option value="RatingAsc">RatingAsc</option>
                    <option value="RatingDesc">RatingDesc</option>
                </select>
                <select className="bg-white h-8 rounded-full text-center focus:outline-none"
                    value={filter}
                    onChange={(e) => setFilter(e.target.value)}
                >
                    <option value="" disabled hidden>Filter</option>
                    {loading ? 
                        <option disabled value="">
                            Loading...
                        </option> :
                        <>
                            {specialties.map((specialty, index) => (
                                <option key={index} value={specialty}>{specialty}</option>
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
                <button className="bg-blue-500 h-8 w-9 rounded-e-3xl"
                    onClick={handleSearch}>
                    <FontAwesomeIcon icon={faMagnifyingGlass} className="text-white text-xl" />
                </button>
            </div>
        </article>
    );
}

export default DoctorsNavBar;