import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import apiRequest from '../../services/apiRequest';
import useCheckAuth from '../../hooks/useCheckAuth';
import { reviewCommentLength } from '../../constants/constants';

function AddReview({ doctorId }) {
    const navigate = useNavigate();
    const { isAuthenticated, jwtToken } = useCheckAuth();
    const [rating, setRating] = useState(1);

    const add = async () => {
        const dto = {
            doctorId: doctorId,
            rating: rating
        };

        const response = await apiRequest('doctor', 'addReview', dto, jwtToken, 'POST', true);

        setMessage(response);

        setTimeout(() => { setMessage('') }, 3000);
    };

    return (
        <div>
            <button className="w-1/2 bg-blue-500 text-white text-center text-lg font-bold py-1 rounded border-2 border-blue-500 hover:bg-white hover:text-blue-500">
                Add
            </button>
        </div>
    );
}

export default AddReview;