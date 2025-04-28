import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPaperPlane, faPaperclip, faXmark } from '@fortawesome/free-solid-svg-icons';
import { useChat } from '../../contexts/ChatContext';
import jwtDecoder from '../../services/jwtDecoder';

function SendMessage({ connection, updateMessages }) {
    const { getReceiverData } = useChat();
    const { userId } = jwtDecoder();
    const [message, setMessage] = useState('');
    const [images, setImages] = useState([]);

    const addImage = (e) => {
        const file = e.target.files[0];

        if (file) {
            const reader = new FileReader();

            reader.onload = (event) => {
                setImages(prev => [...prev, { imgUrl: event.target.result, file: file }]);
            };

            reader.readAsDataURL(file);
        }

        e.target.value = '';
    };

    const sendMessage = async () => {
        let imgUrls = [];

        if (images.length > 0) {
            try {
                const formData = new FormData();

                images.forEach((img) => {
                    formData.append('images', img.file);
                });

                const response = await fetch(`${import.meta.env.VITE_API_URL}/account/uploadChatImages`, {
                    method: 'POST',
                    headers: {
                        "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
                    },
                    body: formData,
                });

                imgUrls = await response.json();
            } catch (error) {
                console.error(error);
            }
        }
        
        const dto = {
            senderId: userId,
            receiverId: getReceiverData().receiverId,
            message: message,
            dateAndTime: new Date(),
            imageUrls: imgUrls
        };

        updateMessages(prevMessages => [...prevMessages, {
            senderId: dto.senderId,
            message: dto.message,
            dateAndTime: dto.dateAndTime,
            images: dto.imageUrls
        }]);

        await connection.invoke("SendMessage", dto);
        setMessage('');
        setImages([]);
    };

    const sendTyping = async (messageLength) => {
        await connection.invoke("SenderTyping", getReceiverData().receiverId, messageLength > 0);
    };

    const removeImage = (img) => {
        setImages((prev) => prev.filter(currImg => currImg.imgUrl !== img));
    }

    const handleEnterPress = (event) => {
        if (event.key === 'Enter' && (message || images.length > 0)) {
            sendMessage();
        }
    }

    return (
        <div className="flex flex-col bg-white rounded-full px-4 py-1 border border-blue-500">
            {images.length > 0 &&
                <div className="flex flex-wrap">
                    {images.map((img, index) => (
                        <div key={index} className="relative group m-0.5">
                            <img className="w-6 h-6 rounded shadow shadow-gray-500 group-hover:opacity-45" src={img.imgUrl} />
                            <button className="absolute top-0 w-6 h-6 rounded opacity-0 group-hover:opacity-100 transition-opacity"
                                onClick={() => removeImage(img.imgUrl)}>
                                <FontAwesomeIcon icon={faXmark} className="text-lg text-zinc-700" />
                            </button>
                        </div>
                    ))}
                </div>}
            <div className="flex space-x-2 justify-between">
                <label className="text-zinc-600 flex items-center justify-center text-base cursor-pointer opacity-100 transition-opacity duration-200 group-hover:opacity-100 sm:opacity-100">
                    <FontAwesomeIcon icon={faPaperclip} />
                    <input type="file"
                        accept="image/png, image/jpg, image/jpeg"
                        className="hidden"
                        onChange={addImage}
                        onKeyDown={handleEnterPress}
                    />
                </label>
                <input className="w-full focus:outline-none"
                    placeholder="Message..."
                    type="text"
                    value={message}
                    onKeyDown={handleEnterPress}
                    onChange={(e) => {
                        setMessage(e.target.value);
                        sendTyping(e.target.value.length);
                    }} />
                <button className="cursor-pointer flex justify-center items-center"
                    onClick={sendMessage}
                    disabled={!message && images.length === 0}>
                    <FontAwesomeIcon className="text-zinc-600" icon={faPaperPlane} />
                </button>
            </div>
        </div>
    );
};

export default SendMessage;