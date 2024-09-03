import React from 'react';

function Login() {
    return (
        <section className="flex items-center justify-center">
            <div className="w-full max-w-xs bg-maincolor rounded-xl shadow-md px-8 py-8">
                <p className="text-3xl text-center text-white">Welcome back</p>
                <hr className="my-4" />
                <form className="flex flex-col space-y-2">
                    <div>
                        <label className="text-md font-bold" for="email">
                            Email
                        </label>
                        <input className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none" type="email" />
                    </div>
                    <div>
                        <label className="text-md font-bold" for="password">
                            Password
                        </label>
                        <input className="rounded w-full py-1 px-2 text-gray-700 focus:outline-none" type="password" />
                    </div>
                    <div className="flex items-center justify-between pt-6">
                        <a className="inline-block align-baseline font-bold text-sm text-blue-500 hover:text-blue-800" href="#">
                            Forgot Password?
                        </a>
                        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="submit">
                            Sign In
                        </button>
                    </div>
                </form>
            </div>
        </section>
    );
}

export default Login;