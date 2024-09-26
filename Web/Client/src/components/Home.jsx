import React, { useState, useEffect } from 'react';

function Home() {
    const [data, setData] = useState(null);
    const token = localStorage.getItem('accessToken');

    useEffect(() => {
        fetch('https://localhost:7080/home', {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            },
            credentials: 'include'
        })
            .then(response => response.json())
            .then(data => setData(data.data));
    });

    return (
        <section className="flex items-center h-full bg-doctors-img h-full bg-no-repeat bg-right mx-96">
            <div className="text-center text-maincolor w-1/2">
                <p className="text-4xl">Welcome to</p>
                <p className="text-5xl font-bold underline underline-offset-5">HealthSync</p>
            </div>
        </section>
    );
}

export default Home;