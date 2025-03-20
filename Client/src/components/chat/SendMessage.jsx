import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPaperPlane, faPaperclip, faXmark } from '@fortawesome/free-solid-svg-icons';
import { useChat } from '../../contexts/ChatContext';
import { useAuthContext } from '../../contexts/AuthContext';
import jwtDecoder from '../../services/jwtDecoder';
import { useLoading } from '../../contexts/LoadingContext';

function SendMessage({ connection, updateMessages }) {
    const { getReceiverData } = useChat();
    const { setIsLoading } = useLoading();
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
        let imgUrls;

        try {
            const formData = new FormData();

            images.forEach((img) => {
                formData.append('images', img.file);
            });
            
            setIsLoading(true);

            const response = await fetch('https://localhost:7080/account/uploadImage', {
                method: 'POST',
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem('accessToken')}`,
                },
                body: formData,
            });

            imgUrls = await response.json();
        } catch (error) {
            console.error(error);
        } finally {
            setIsLoading(false);
        }

        const dto = {
            senderId: userId,
            receiverId: getReceiverData().receiverId,
            message: message,
            dateAndTime: new Date(),
            images: imgUrls
        };
        
        updateMessages(prevMessages => [...prevMessages, {
            senderId: dto.senderId,
            message: dto.message,
            dateAndTime: dto.dateAndTime,
            images: images
        }]);

        await connection.invoke("SendMessage", dto);
        setMessage('');
    };

    const sendTyping = async (messageLength) => {
        await connection.invoke("SenderTyping", getReceiverData().receiverId, messageLength > 0);
    };

    const removeImage = (img) => {
        setImages((prev) => prev.filter(currImg => currImg.imgUrl !== img));
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
                    <input
                        type="file"
                        accept="image/png, image/jpg, image/jpeg"
                        className="hidden"
                        onChange={addImage}
                    />
                </label>
                <input
                    className="w-full focus:outline-none"
                    placeholder="Message..."
                    type="text"
                    value={message}
                    onChange={(e) => {
                        setMessage(e.target.value);
                        sendTyping(e.target.value.length);
                    }} />
                <button
                    className="cursor-pointer flex justify-center items-center"
                    onClick={sendMessage}>
                    <FontAwesomeIcon className="text-zinc-600" icon={faPaperPlane} />
                </button>
            </div>
        </div>
    );
};

export default SendMessage;