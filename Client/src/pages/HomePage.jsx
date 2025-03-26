import React from 'react';
import threeDoctors from '../assets/images/three-doctors.png';
import FAQsSection from '../components/homePage/FAQsSection';

function HomePage() {
    return (
        <div className="space-y-6">
            <section className="flex space-x-12 items-center">
                <div className="flex flex-col items-center text-maincolor ">
                    <p className="text-4xl">Welcome to</p>
                    <p className="text-5xl font-bold underline underline-offset-5">HealthSync</p>
                </div>
                <img src={threeDoctors} />
            </section>
            <FAQsSection />
        </div>
    );
}

export default HomePage;