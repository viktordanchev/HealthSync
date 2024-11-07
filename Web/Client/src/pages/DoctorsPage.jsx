import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { getAllDoctors } from '../services/apiRequests/doctor';
import DoctorCard from '../components/doctor/DoctorCard';
import Loading from '../components/Loading';
import DoctorsNavBar from '../components/doctor/DoctorsNavBar';

function DoctorsPage() {
    const [searchParams, setSearchParams] = useSearchParams();
    const [order, setOrder] = useState('None');
    const [search, setSearch] = useState('');
    const [filter, setFilter] = useState('');
    const [doctors, setDoctors] = useState([]);
    const [loading, setLoading] = useState(true);

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
            queryParams.search = search.trim();
        }

        setSearchParams(queryParams);
    }, [order, search, filter]);

    useEffect(() => {
        setLoading(true);

        const receiveDoctors = async () => {
            const dto = {
                index: 0,
                sorting: order,
                search: search,
                filter: filter
            };

            const doctors = await getAllDoctors(dto);
            setDoctors(doctors);

            setLoading(false);
        };

        receiveDoctors();
    }, [order, search, filter]);

    return (
        <section className="mx-32 flex flex-col h-full items-center lg:mx-16 md:mx-6 sm:mx-6">
            <DoctorsNavBar
                order={order}
                setOrder={setOrder}
                filter={filter}
                setFilter={setFilter}
                search={search}
                setSearch={setSearch}
            />
            {loading ? <Loading type={'big'} /> :
                <article className="flex flex-wrap justify-center w-full h-full">
                    {doctors.length == 0 ?
                        <div className="text-3xl">No doctors found!</div> :
                        <>
                            {doctors.map((doctor) => (
                                <DoctorCard key={doctor.id} doctor={doctor} />
                            ))}
                        </>
                    }
                </article>}
        </section>
    );
}

export default DoctorsPage;