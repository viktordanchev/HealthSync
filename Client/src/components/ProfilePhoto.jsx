import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCamera, faXmark } from '@fortawesome/free-solid-svg-icons';
import doctorProfile from '../assets/images/doctor-profile.jpg';

function ProfilePhoto({ changePhoto, currentImage }) {
    const [image, setImage] = useState(currentImage ? currentImage : doctorProfile);
    const [isImageLoaded, setIsImageLoaded] = useState(false);

    const handleChangePhoto = (e) => {
        const file = e.target.files[0];

        if (file) {
            const reader = new FileReader();

            reader.onload = (event) => {
                setImage(event.target.result);
                changePhoto(file);
            };

            reader.readAsDataURL(file);
        }

        e.target.value = '';
    };

    const handleRemovePhoto = () => {
        changePhoto(null);
        setImage(doctorProfile);
    };

    return (
        <div className="relative group">
            <img className="w-32 h-32 object-cover rounded-full border-2 border-zinc-700 sm:w-28 sm:h-28"
                src={isImageLoaded ? image : doctorProfile}
                onLoad={() => setIsImageLoaded(true)} />
            <label className="absolute top-0 right-0 text-white bg-zinc-700 w-9 h-9 flex items-center justify-center rounded-full text-base cursor-pointer opacity-0 transition-opacity duration-200 group-hover:opacity-100 sm:opacity-100">
                <FontAwesomeIcon icon={faCamera} />
                <input
                    type="file"
                    accept="image/jpeg, image/png, image/jpg"
                    className="hidden"
                    onChange={handleChangePhoto}
                />
            </label>
            {image != doctorProfile &&
                <label
                    className="absolute top-0 left-0 text-white bg-zinc-700 w-9 h-9 flex items-center justify-center rounded-full text-xl cursor-pointer opacity-0 transition-opacity duration-200 group-hover:opacity-100 sm:opacity-100"
                    onClick={handleRemovePhoto}
                >
                    <FontAwesomeIcon icon={faXmark} />
                </label>}
        </div>
    );
}

export default ProfilePhoto;