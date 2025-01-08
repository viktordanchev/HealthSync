import React from 'react';
import threeDoctors from '../assets/images/three-doctors.png';

function HomePage() {
    return (
        <section className="flex flex-col items-center text-maincolor">
            <p className="text-4xl">Welcome to</p>
            <p className="text-5xl font-bold underline underline-offset-5">HealthSync</p>
            <img
                src={threeDoctors}
            />
        </section>
    );
}

export default HomePage;