import React, { useState, useEffect } from 'react';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';
import WeeklySchedule from '../components/doctorProfilePage/WeeklySchedule';
import DaysoffCalendar from '../components/doctorProfilePage/DaysoffCalendar';
import DoctorInfo from '../components/doctorProfilePage/DoctorInfo';

function DoctorProfilePage() {
    const [doctorData, setDoctorData] = useState(null);
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);
    const [hospitals, setHospitals] = useState([]);
    const [specialties, setSpecialties] = useState([]);

    useEffect(() => {
        const receiveData = async () => {
            try {
                const doctorData = await apiRequest('doctors', 'getDoctorInfo', undefined, localStorage.getItem('accessToken'), 'GET', false);
                let hospitals = await apiRequest('hospitals', 'getHospitals', undefined, undefined, 'GET', false);
                let specialties = await apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false);

                hospitals = hospitals.filter(h => h.id !== doctorData.hospitalId);
                specialties = specialties.filter(s => s.id !== doctorData.specialtyId);

                setDoctorData(doctorData);
                setHospitals(hospitals);
                setSpecialties(specialties);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoadingOnReceive(false);
            }
        };

        receiveData();
    }, []);

    return (
        <>
            {isLoadingOnReceive ? <Loading type={'big'} /> :
                <section className="flex space-x-6 text-gray-700 md:flex-col md:space-x-0 md:space-y-6 sm:flex-col sm:space-x-0 sm:space-y-6">
                    <DoctorInfo
                        doctorData={doctorData}
                        hospitals={hospitals}
                        specialties={specialties} />
                    <WeeklySchedule weekDays={doctorData.weeklySchedule} />
                    <DaysoffCalendar />
                </section>}
        </>
    );
}

export default DoctorProfilePage;