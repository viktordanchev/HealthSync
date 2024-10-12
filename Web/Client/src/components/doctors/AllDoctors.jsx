import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass, faArrowDown } from '@fortawesome/free-solid-svg-icons';
import DoctorCard from './DoctorCard';

function AllDoctors() {
    return (
        <section className="mx-64 flex flex-col h-full items-center">
            <article className="rounded-full bg-maincolor w-1/2 h-14 my-6 px-2 flex justify-between items-center">
                <button className="bg-white h-9 rounded-full flex items-center justify-evenly space-x-2">
                    <p className="ml-2">Order</p>
                    <hr className="h-4 border-l border-black" />
                    <FontAwesomeIcon icon={faArrowDown} className="pr-2" />
                </button>
                <div className="flex items-center">
                    <input
                        placeholder="Search..."
                        className="text-lg rounded-s-3xl bg-white h-9 w-56 pl-4 focus:outline-none focus:ring-1 focus:ring-blue-500"
                    >
                    </input>
                    <button className="bg-blue-500 h-9 w-9 rounded-e-3xl">
                        <FontAwesomeIcon icon={faMagnifyingGlass} className="text-white text-xl" />
                    </button>
                </div>
            </article>
            <article className="flex flex-wrap justify-center w-full h-full">
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
                <DoctorCard />
            </article>
        </section>
    );
}

export default AllDoctors;