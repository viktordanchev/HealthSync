import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleRight } from '@fortawesome/free-solid-svg-icons';
import { useChat } from '../../contexts/ChatContext';

function AllChats() {
    const { openChat } = useChat();
    const [chats, setChats] = useState([]);

    useEffect(() => {
        const storedChats = JSON.parse(sessionStorage.getItem('allChats')) || [];
        setChats(storedChats);
    }, [sessionStorage.getItem('allChats')]);

    const openCurrentChat = (receiverData) => {
        openChat(receiverData.receiverId, receiverData.receiverName);
    };

    return (
        <>
            {chats.length > 0 ?
                <ul className="max-h-[90%] space-y-2 p-2 overflow-y-auto scrollbar-thin scrollbar-thumb-rounded scrollbar-track-rounded scrollbar-thumb-zinc-500 scrollbar-track-transparent">
                    {chats.map((chat, index) => (
                        <li key={index} className="cursor-pointer border border-zinc-500 rounded h-12 flex justify-between items-center p-4"
                            onClick={() => openCurrentChat(chat)}>
                            <p className="text-zinc-700">{chat.receiverName}</p>
                            <FontAwesomeIcon className="text-2xl text-zinc-600" icon={faAngleRight} />
                        </li>
                    ))}
                </ul> : <p className="text-xl font-light text-center">No active chats.</p>}
        </>
    );
};

export default AllChats;