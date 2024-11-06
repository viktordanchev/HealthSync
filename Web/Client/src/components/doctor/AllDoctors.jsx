import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { getAllDoctors, getSpecialties } from '../../services/apiRequests/doctor';
import DoctorCard from './DoctorCard';
import Loading from '../Loading';

function AllDoctors() {
    const [searchParams, setSearchParams] = useSearchParams();
    const [order, setOrder] = useState('None');
    const [search, setSearch] = useState('');
    const [filter, setFilter] = useState('');
    const [searchOnChange, setSearchOnChange] = useState('');
    const [loading, setLoading] = useState(true);
    const [doctors, setDoctors] = useState([]);
    const [specialties, setSpecialties] = useState([]);

    {/* This hook receives data from the query string */ }
    useEffect(() => {
        const orderFromQuery = searchParams.get('order');
        const filterFromQuery = searchParams.get('filter');
        const searchFromQuery = searchParams.get('search');

        if (orderFromQuery) {
            setOrder(orderFromQuery);
        }

        if (filterFromQuery) {
            setFilter(filterFromQuery);
        }

        if (searchFromQuery) {
            setSearch(searchFromQuery);
        }
    }, [searchParams]);

    {/* This hook sets data in the query string */ }
    useEffect(() => {
        const queryParams = {};

        if (order !== 'None') {
            queryParams.order = order;
        }

        if (filter) {
            queryParams.filter = filter;
        }

        if (search) {
            queryParams.search = search;
        }

        setSearchParams(queryParams);
    }, [order, search, filter]);

    useEffect(() => {
        setLoading(true);

        const getDoctors = async () => {
            const dto = {
                index: 0,
                sorting: order,
                filter: filter,
                search: search
            };

            const specialtiesData = await getSpecialties();
            setSpecialties(specialtiesData);

            const doctors = await getAllDoctors(dto);
            setDoctors(doctors);

            setLoading(false);
        };

        getDoctors();
    }, [order, search, filter]);

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
        <section className="mx-32 flex flex-col h-full items-center lg:mx-16 md:mx-6 sm:mx-6">
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
                        <>
                            {specialties.map((specialty, index) => (
                                <option key={index} value={specialty}>{specialty}</option>
                            ))}
                        </>
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
            {loading ? <Loading type={'big'} /> :
                <article className="flex flex-wrap justify-center w-full h-full">
                    {doctors.length == 0 ?
                        <div className="text-3xl font-bold">No doctors found!</div> :
                        <>
                            {doctors.map((doctor, index) => (
                                <DoctorCard key={index} doctor={doctor} />
                            ))}
                        </>
                    }
                </article>}
        </section>
    );
}

export default AllDoctors;