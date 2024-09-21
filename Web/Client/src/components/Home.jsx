import React, { useState, useEffect } from 'react';

function Section() {
    const [data, setData] = useState(null);

    useEffect(() => {
        fetch('https://localhost:7080/home', {
            method: 'GET',
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

export default Section;