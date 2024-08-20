import React from 'react';

function Register() {
    return (
        <section className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
            <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">Register as a new member</h2>
            </div>

            <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-xs">
                <form className="space-y-4" action="#" method="POST">
                    <div className="flex gap-6">
                        <div>
                            <label className="block text-base font-medium text-gray-900">First name</label>
                            <div className="mt-2">
                                <input required className="px-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6" />
                            </div>
                        </div>
                        <div>
                            <label className="block text-base font-medium text-gray-900">Last name</label>
                            <div className="mt-2">
                                <input required className="px-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6" />
                            </div>
                        </div>
                    </div>

                    <div class="flex">
                        <div class="relative">
                            <select class="block appearance-none bg-white border border-gray-300 rounded-l-md py-2 pl-3 pr-8 leading-tight focus:outline-none focus:bg-white focus:border-blue-500">
                                <option value="+1">+1 (USA)</option>
                                <option value="+44">+44 (UK)</option>
                                <option value="+91">+91 (India)</option>
                            </select>
                            <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
                                <svg class="fill-current h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"><path d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 011.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z" /></svg>
                            </div>
                        </div>

                        <input type="tel" placeholder="123-456-7890" class="appearance-none block w-full bg-white text-gray-700 border border-gray-300 rounded-r-md py-2 px-4 leading-tight focus:outline-none focus:bg-white focus:border-blue-500"/>
                    </div>


                    <div>
                        <label for="email" className="block text-base font-medium text-gray-900">Email address</label>
                        <div className="mt-2">
                            <input id="email" name="email" type="email" autocomplete="email" required className="px-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset sm:text-sm sm:leading-6" />
                        </div>
                    </div>

                    <div>
                        <label for="password" className="block text-base font-medium text-gray-900">Password</label>
                        <div className="mt-2">
                            <input id="password" name="password" type="password" autocomplete="current-password" className="px-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                        </div>
                    </div>

                    <div>
                        <label for="password" className="block text-base font-medium text-gray-900">Confirm Password</label>
                        <div className="mt-2">
                            <input id="password" name="password" type="password" autocomplete="current-password" className="px-3 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6" />
                        </div>
                    </div>

                    <button type="submit" className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">Register</button>
                </form>
            </div>
        </section>
    );
}

export default Register;