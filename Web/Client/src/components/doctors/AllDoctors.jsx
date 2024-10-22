import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { getAllDoctors } from '../../services/apiRequests/doctors';
import DoctorCard from './DoctorCard';
import Loading from '../Loading';

function AllDoctors() {
    const [order, setOrder] = useState('NameAsc');
    const [searchField, setSearchField] = useState('');
    const [isSearching, setIsSearching] = useState(false);
    const [loading, setLoading] = useState(true);
    const [doctors, setDoctors] = useState([]);

    useEffect(() => {
        setLoading(true);

        const getDoctors = async () => {
            const dto = {
                index: 0,
                sorting: order,
                filter: '',
                search: searchField
            };

            const doctors = await getAllDoctors(dto);
            setDoctors(doctors);

            setLoading(false);
        };

        getDoctors();
        setIsSearching(false);
    }, [order, isSearching]);

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            setIsSearching(true);
        }
    };

    return (
        <section className="mx-32 flex flex-col h-full items-center lg:mx-16 md:mx-6 sm:mx-6">
            <article className="rounded-full bg-maincolor w-1/2 my-6 p-2 flex justify-between items-center lg:w-2/3 sm:w-full md:w-full sm:flex-col sm:py-2 sm:rounded-xl sm:space-y-2">
                <select className="bg-white h-8 rounded-full text-center focus:outline-none"
                    defaultValue=""
                    onChange={(e) => setOrder(e.target.value)}
                >
                    <option value="" disabled hidden>Order</option>
                    <option value="NameAsc">NameAsc</option>
                    <option value="NameDesc">NameDesc</option>
                    <option value="RatingAsc">RatingAsc</option>
                    <option value="RatingDesc">RatingDesc</option>
                </select>
                <div className="flex items-center sm:w-full">
                    <input
                        placeholder="Search..."
                        className="text-lg rounded-s-3xl bg-white h-8 p-4 focus:outline-none focus:ring-1 focus:ring-blue-500 sm:w-full"
                        onChange={(e) => setSearchField(e.target.value)}
                        onKeyDown={handleKeyDown}
                    >
                    </input>
                    <button className="bg-blue-500 h-8 w-9 rounded-e-3xl"
                        onClick={() => { setIsSearching(true) }}>
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
                                <DoctorCard key={index} data={doctor} />
                            ))}
                        </>
                    }
                </article>}
        </section>
    );
}

export default AllDoctors;