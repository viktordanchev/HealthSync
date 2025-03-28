import React from 'react';

function RestrictedPage() {
    return (
        <section className="space-y-6 flex text-center items-center flex-col">
            <article className="flex space-x-3 text-red-500 text-3xl font-thin md:text-2xl sm:text-lg">
                <p className="font-bold">Oops!</p>
                <p>You don't have access to this page.</p>
            </article>
            <button className="bg-blue-500 border-2 border-blue-500 text-white text-xl font-medium py-1 px-2 rounded hover:bg-white hover:text-blue-500 md:text-lg sm:text-base">
                <a href="/home">Get back</a>
            </button>
        </section>
    );
}

export default RestrictedPage;