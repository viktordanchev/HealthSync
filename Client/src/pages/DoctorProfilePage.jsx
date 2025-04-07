import React, { useState, useEffect } from 'react';
import apiRequest from '../services/apiRequest';
import Loading from '../components/Loading';
import WeeklySchedule from '../components/doctorProfilePage/WeeklySchedule';
import DaysOff from '../components/doctorProfilePage/Daysoff';
import DoctorInfo from '../components/doctorProfilePage/DoctorInfo';

function DoctorProfilePage() {
    const [doctorData, setDoctorData] = useState(null);
    const [isLoadingOnReceive, setIsLoadingOnReceive] = useState(true);
    const [hospitals, setHospitals] = useState([]);
    const [specialties, setSpecialties] = useState([]);
    const [daysOff, setDaysOff] = useState([]);

    useEffect(() => {
        const receiveData = async () => {
            try {
                const [doctorData, hospitals, specialties, daysOff] = await Promise.all([
                    apiRequest('doctors', 'getProfileInfo', undefined, localStorage.getItem('accessToken'), 'GET', false),
                    apiRequest('hospitals', 'getHospitals', undefined, undefined, 'GET', false),
                    apiRequest('doctors', 'getSpecialties', undefined, undefined, 'GET', false),
                    apiRequest('doctors', 'getDaysOff', undefined, localStorage.getItem('accessToken'), 'GET', false),
                ]);
                
                let filteredHospitals = hospitals.filter(h => h.id !== doctorData.hospitalId);
                let filteredSpecialties = specialties.filter(s => s.id !== doctorData.specialtyId);

                setDoctorData(doctorData);
                setHospitals(filteredHospitals);
                setSpecialties(filteredSpecialties);
                setDaysOff(daysOff);
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
                <section className="space-y-6 text-gray-700 mx-20 md:mx-8 md:flex-col md:space-x-0 md:space-y-6 sm:mx-0 sm:flex-col sm:space-x-0 sm:space-y-6">
                    <div className="flex space-x-6 lg:flex-col lg:space-x-0 lg:space-y-6 md:flex-col md:space-x-0 md:space-y-6 sm:flex-col sm:space-x-0 sm:space-y-6">
                        <DoctorInfo
                            doctorData={doctorData}
                            hospitals={hospitals}
                            specialties={specialties} />
                        <DaysOff data={daysOff} />
                    </div>
                    <WeeklySchedule weekDays={doctorData.weeklySchedule} />
                </section>}
        </>
    );
}

export default DoctorProfilePage;