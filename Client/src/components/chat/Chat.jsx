import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMessage, faXmark, faAngleRight } from '@fortawesome/free-solid-svg-icons';
import OpenedChat from './OpenedChat';
import { useChat } from '../../contexts/ChatContext';
import { useAuthContext } from '../../contexts/AuthContext';

function Chat() {
    const { isAuthenticated } = useAuthContext();
    const { isStart, openChat } = useChat();
    const [isOpen, setIsOpen] = useState(false);
    const [chats, setChats] = useState([]);

    useEffect(() => {
        if (isOpen) {
            const storedChats = JSON.parse(sessionStorage.getItem('allChats')) || [];
            setChats(storedChats);
        }
    }, [isOpen]);

    const openNewChat = (receiverData) => {
        openChat(receiverData.receiverId, receiverData.receiverName);
    };

    return (
        <>
            {isAuthenticated &&
                <>
                    {!isStart &&
                        <>{!isOpen ? <button className="fixed bottom-16 right-16 border-2 border-zinc-500 flex items-center justify-center bg-blue-500 h-16 w-16 rounded-full shadow-xl transition-opacity duration-300 hover:bg-blue-600 md:bottom-12 md:right-12 md:h-14 md:w-14 sm:bottom-8 sm:right-8 sm:h-12 sm:w-12"
                            onClick={() => setIsOpen(true)}>
                            <FontAwesomeIcon icon={faMessage} className="cursor-pointer text-white text-2xl sm:text-lg" />
                        </button> :
                            <div className="fixed bottom-16 right-16 h-96 w-72 shadow-2xl shadow-gray-400 rounded-xl bg-gray-200 bg-opacity-85 border border-zinc-500">
                                <div className="h-[10%] flex justify-end items-center bg-maincolor rounded-t-xl space-x-2 py-1 px-2">
                                    <FontAwesomeIcon
                                        className="text-2xl text-zinc-600 cursor-pointer"
                                        onClick={() => setIsOpen(false)}
                                        icon={faXmark} />
                                </div>
                                {chats.length > 0 ?
                            <ul className="max-h-[90%] overflow-y-auto scrollbar-thin scrollbar-thumb-rounded scrollbar-track-rounded scrollbar-thumb-zinc-500 scrollbar-track-transparent">
                                        {chats.map((chat) => (
                                            <li className="cursor-pointer shadow-md shadow-gray-400 h-12 flex justify-between items-center p-4"
                                                onClick={() => openNewChat(chat)}>
                                                <p className="text-zinc-700">{chat.receiverName}</p>
                                                <FontAwesomeIcon className="text-2xl text-zinc-600" icon={faAngleRight} />
                                            </li>
                                        ))}
                                    </ul> : <p className="text-xl text-center">No active chats.</p>}
                            </div>}
                        </>}
                    <OpenedChat />
                </>}
        </>
    );
};

export default Chat;