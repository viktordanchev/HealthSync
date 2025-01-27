import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';

function DaysoffCalendar() {
    return (
        <article className="p-4 bg-zinc-400 bg-opacity-75 shadow-xl shadow-gray-300 rounded-xl flex flex-col space-y-4 text-center">
            <h2 className="text-3xl font-thin underline-thin text-gray-700 sm:text-2xl">Daysoff</h2>
            <div className="space-y-2">
            </div>
        </article>
    );
}

export default DaysoffCalendar;