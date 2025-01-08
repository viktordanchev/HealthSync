import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCamera, faXmark } from '@fortawesome/free-solid-svg-icons';
import doctorProfile from '../../assets/images/doctor-profile.jpg';

function ProfilePhoto({ setProfilePhoto }) {
    const [imageSrc, setImageSrc] = useState(doctorProfile);

    const handleFileChange = (e) => {
        const file = e.target.files[0];

        if (file) {
            const reader = new FileReader();

            reader.onload = (event) => {
                setImageSrc(event.target.result);
                setProfilePhoto(file);
            };

            reader.readAsDataURL(file);
        }

        e.target.value = '';
    };

    return (
        <div className="flex flex-col items-center">
            <h2 className="text-lg font-bold">Profile Photo</h2>
            <div className="relative group">
                <div className="w-36 h-36 flex justify-center items-center bg-zinc-700 rounded-full sm:w-28 sm:h-28">
                    <img
                        src={imageSrc}
                        className="w-32 h-32 object-cover rounded-full sm:w-24 sm:h-24"
                    />
                </div>
                <label
                    className="absolute top-0 right-0 text-white bg-zinc-700 w-9 h-9 flex items-center justify-center rounded-full text-base cursor-pointer opacity-0 transition-opacity duration-200 group-hover:opacity-100 sm:opacity-100"
                >
                    <FontAwesomeIcon icon={faCamera} />
                    <input
                        type="file"
                        accept="image/jpeg, image/png, image/jpg"
                        className="hidden"
                        onChange={handleFileChange}
                    />
                </label>
                <label
                    className="absolute top-0 left-0 text-white bg-zinc-700 w-9 h-9 flex items-center justify-center rounded-full text-xl cursor-pointer opacity-0 transition-opacity duration-200 group-hover:opacity-100 sm:opacity-100"
                    onClick={() => setImageSrc(doctorProfile)}
                >
                    <FontAwesomeIcon icon={faXmark} />
                </label>
            </div>
        </div>
    );
}

export default ProfilePhoto;