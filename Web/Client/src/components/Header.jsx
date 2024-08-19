import React from 'react';

function Header() {

    return (
        <header class="bg-maincolor m-6 mx-32 rounded-lg">
            <div class="container mx-auto flex justify-between items-center p-5">
                <div class="text-white text-3xl font-bold">
                    <a href="#" class="hover:text-black">HealthSync</a>
                </div>
                <div class="flex space-x-4">
                    <button class="bg-blue-500 text-white font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                        Login
                    </button>
                    <button class="bg-blue-500 text-white font-bold py-2 px-4 rounded hover:bg-blue-700 transition duration-300">
                        Register
                    </button>
                </div>
            </div>
        </header>
    );
}

export default Header;