import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import apiRequest from '../services/apiRequest';
import DoctorCard from '../components/doctorsPage/DoctorCard';
import Loading from '../components/Loading';
import NavigationBar from '../components/doctorsPage/NavigationBar';

function DoctorsPage() {
    const [searchParams, setSearchParams] = useSearchParams();
    const [order, setOrder] = useState('None');
    const [search, setSearch] = useState('');
    const [filter, setFilter] = useState('');
    const [doctors, setDoctors] = useState([]);
    const [specialties, setSpecialties] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    
    {/* This hook receives data from the query string */ }
    useEffect(() => {
        const orderFromQuery = searchParams.get('order');
        const filterFromQuery = searchParams.get('filter');
        const searchFromQuery = searchParams.get('search');

        if (orderFromQuery) {
            setOrder(orderFromQuery);
        } else {
            setOrder('None');
        }

        if (filterFromQuery) {
            setFilter(filterFromQuery);
        } else {
            setFilter('');
        }

        if (searchFromQuery) {
            setSearch(searchFromQuery);
        } else {
            setSearch('');
        }
    }, [searchParams]);

    useEffect(() => {
        const receiveData = async () => {
            const dto = {
                index: 0,
                sorting: order,
                search: search,
                filter: filter
            };

            setIsLoading(true);

            try {
                const doctors = await apiRequest('doctors', 'getDoctors', dto, localStorage.getItem('accessToken'), 'POST', false);
                const specialties = await apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false);

                setDoctors(doctors);
                setSpecialties(specialties);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        if (searchParams.size === 0 ||
            (searchParams.get('order') === order ||
                searchParams.get('search') === search ||
                searchParams.get('filter') === filter)
        ) {
            receiveData();
        }
    }, [order, filter, search]);

    return (
        <section className="mx-32 flex flex-col items-center space-y-6 text-gray-700 lg:mx-16 md:mx-0 sm:mx-0">
            {isLoading ? <Loading type={'big'} /> :
                <>
                    <NavigationBar
                        searchParams={searchParams}
                        setSearchParams={setSearchParams}
                        specialties={specialties}
                    />
                    <article className="flex flex-wrap gap-4 justify-center w-full h-full">
                        {doctors.length == 0 ?
                            <div className="text-3xl">No doctors found!</div> :
                            <>
                                {doctors.map((doctor) => (
                                    <DoctorCard key={doctor.id} doctor={doctor} />
                                ))}
                            </>
                        }
                    </article>
                </>}
        </section>
    );
}

export default DoctorsPage;