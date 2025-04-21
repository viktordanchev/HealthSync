import React from 'react';
import threeDoctors from '../assets/images/three-doctors.png';
import FAQsSection from '../components/homePage/FAQsSection';
import TopRatedDoctors from '../components/homePage/TopRatedDoctors';

function HomePage() {
    return (
        <div className="space-y-6 w-full h-full flex flex-col items-center">
            <section className="w-2/3 flex space-x-12 justify-center items-center lg:w-full md:flex-col md:space-y-6 md:space-x-0 sm:flex-col sm:space-y-6 sm:space-x-0">
                <div className="flex flex-col items-center text-maincolor">
                    <p className="text-4xl lg:text-3xl sm:text-2xl">Welcome to</p>
                    <p className="text-5xl font-bold underline underline-offset-5 lg:text-4xl sm:text-3xl">HealthSync</p>
                </div>
                <img src={threeDoctors} className="w-2/5 h-2/5 lg:w-1/4 lg:h-1/4 md:w-2/3 md:h-2/3 sm:w-full sm:h-full" />
            </section>
            <FAQsSection />
            <TopRatedDoctors />
        </div>
    );
}

export default HomePage;