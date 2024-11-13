import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import apiRequest from '../../services/apiRequest';
import Loading from '../Loading';
import MeetingsCalendar from './MeetingsCalendar';
import DoctorReviews from './DoctorReviews';

function DoctorDetails() {
    const navigate = useNavigate();
    const location = useLocation();
    const [loading, setLoading] = useState(true);
    const [doctor, setDoctor] = useState({});
    const doctorId = location.state?.doctorId;

    useEffect(() => {
        const receiveDoctorDetails = async () => {
            const response = await apiRequest('doctor', 'getDoctorDetails', doctorId, undefined, 'POST', false);

            if (response) {
                setDoctor(response);
                setLoading(false);
            }
        };

        if (!doctorId) {
            navigate('/doctors');
        } else {
            receiveDoctorDetails();
        }
    }, []);

    return (
        <>
            {loading ? <Loading type={'big'} /> :
                <section className="h-full m-20">
                    <div>
                        <img
                            src={doctor.imgUrl ? doctor.imgUrl : '/profile.jpg'}
                            className="object-cover w-40 h-40 rounded-xl"
                        />
                    </div>
                </section>}
        </>
    );
}

export default DoctorDetails;