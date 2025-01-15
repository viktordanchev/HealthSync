import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import apiRequest from '../services/apiRequest';
import DoctorCard from '../components/doctorsPage/DoctorCard';
import Loading from '../components/Loading';
import DoctorsNavBar from '../components/doctorsPage/DoctorsNavBar';

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

        const receiveData = async () => {
            const dto = {
                index: 0,
                sorting: order,
                search: search,
                filter: filter
            };

            try {
                const response = await apiRequest('doctors', 'getDoctors', dto, localStorage.getItem('accessToken'), 'POST', false);

                setDoctors(response);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };

        receiveData();
    }, [order, search, filter]);

    return (
        <section className="mx-32 flex flex-col items-center space-y-6 text-gray-700 lg:mx-16 md:mx-0 sm:mx-0">
            <DoctorsNavBar
                order={order}
                setOrder={setOrder}
                filter={filter}
                setFilter={setFilter}
                setSearch={setSearch}
                setSearchParams={setSearchParams}
            />
            {loading ? <Loading type={'big'} /> :
                <article className="flex flex-wrap gap-4 justify-center w-full h-full">
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