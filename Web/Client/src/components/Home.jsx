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
        <section className="h-96">
            <p className="bg-doctors-img h-full bg-no-repeat bg-right mx-32">{data} text</p>
        </section>
    );
}

export default Home;