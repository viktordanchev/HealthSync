import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import noImage from '../../assets/images/no-image.png';

function ImageMessage({ image }) {
    const [isImageLoaded, setIsImageLoaded] = useState(false);
    const [isImageZoomed, setIsImageZoomed] = useState(false);

    return (
        <>
            {isImageZoomed && (
                <div
                    className="z-30 fixed inset-0 bg-black bg-opacity-45"
                    onClick={() => setIsImageZoomed(false)}
                ></div>
            )}
            <button className={`group relative ${isImageZoomed && 'cursor-default'}`}
                onClick={() => setIsImageZoomed(true)}>
                <img className={`rounded transform transition-transform duration-300 ${isImageZoomed ? 'z-40 w-1/4 h-auto fixed top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 lg:w-1/2 md:w-1/2 sm:w-1/2' : 'w-16 h-16 group-hover:opacity-35'}`}
                    src={isImageLoaded ? image : noImage}
                    onLoad={() => setIsImageLoaded(true)} />
                {!isImageZoomed && (
                    <div className="absolute inset-0 flex items-center justify-center">
                        <FontAwesomeIcon icon={faMagnifyingGlass} className="text-zinc-700 text-3xl opacity-0 transition-opacity duration-300 group-hover:opacity-100" />
                    </div>
                )}
            </button>
        </>
    );
};

export default ImageMessage;