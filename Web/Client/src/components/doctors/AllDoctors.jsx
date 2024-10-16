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
        const getDoctors = async () => {
            const object = {
                sorting: order,
                filter: '',
                search: searchField
            };

            const response = await getAllDoctors(object);

            if (response.ok) {
                var data = await response.json();
                setDoctors(data);
            }

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
        <section className="mx-64 flex flex-col h-full items-center">
            <article className="rounded-full bg-maincolor w-1/2 h-14 my-6 px-2 flex justify-between items-center">
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
                <div className="flex items-center">
                    <input
                        placeholder="Search..."
                        className="text-lg rounded-s-3xl bg-white h-8 w-56 pl-4 focus:outline-none focus:ring-1 focus:ring-blue-500"
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