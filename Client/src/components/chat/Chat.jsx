import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMessage, faXmark, faMinus } from '@fortawesome/free-solid-svg-icons';
import { useChat } from '../../contexts/ChatContext';
import { useAuthContext } from '../../contexts/AuthContext';
import AllChats from './AllChats';
import OpenedChat from './OpenedChat';

function Chat() {
    const { isAuthenticated } = useAuthContext();
    const { isStarted, closeChat, getReceiverData } = useChat();
    const [isOpen, setIsOpen] = useState(false);
    
    useEffect(() => {
        if (isStarted) {
            setIsOpen(true);
        }
    }, [isStarted]);

    const minimuzeChat = () => {
        let existingChats = JSON.parse(sessionStorage.getItem('allChats')) || [];
        const newChat = getReceiverData();

        if (!existingChats.some(chat => chat.receiverId === newChat.receiverId)) {
            existingChats.push(newChat);
            sessionStorage.setItem('allChats', JSON.stringify(existingChats));
        }

        closeChat();
    };

    const removeChat = () => {
        if (sessionStorage.getItem('allChats')) {
            const newChat = getReceiverData();
            const existingChats = JSON.parse(sessionStorage.getItem('allChats'))
                .filter(chat => chat.receiverId !== newChat.receiverId);

            sessionStorage.setItem('allChats', JSON.stringify(existingChats));
        }

        closeChat();
    };

    return (
        <>
            {isAuthenticated && <>
                {!isOpen ? <button className="fixed bottom-16 right-16 border border-zinc-500 flex items-center justify-center bg-blue-500 h-16 w-16 rounded-full shadow-xl transition-opacity duration-300 hover:bg-blue-600 md:bottom-12 md:right-12 md:h-14 md:w-14 sm:bottom-8 sm:right-8 sm:h-12 sm:w-12"
                    onClick={() => setIsOpen(true)}>
                    <FontAwesomeIcon icon={faMessage} className="cursor-pointer text-white text-2xl sm:text-lg" />
                </button> :
                    <div className="fixed z-20 bottom-16 right-16 h-96 w-72 shadow-2xl shadow-gray-400 rounded-xl bg-gray-200 bg-opacity-85 border border-zinc-500 sm:bottom-0 sm:right-0 sm:h-full sm:w-full sm:bg-opacity-100 sm:rounded-none sm:z-50">
                        <div className="h-[10%] flex justify-between items-center bg-maincolor rounded-t-xl space-x-2 py-1 px-2 text-zinc-600 sm:rounded-t-none">
                            <p className="font-medium">{isStarted ? `To: ${getReceiverData().receiverName}` : 'Messages'}</p>
                            <div className="flex justify-end space-x-2">
                                {isStarted && (<FontAwesomeIcon
                                    className="text-2xl cursor-pointer"
                                    onClick={minimuzeChat}
                                    icon={faMinus} />)}
                                <FontAwesomeIcon
                                    className="text-2xl cursor-pointer"
                                    onClick={() => {
                                        if (isStarted) {
                                            removeChat();
                                        } else {
                                            setIsOpen(false);
                                        }
                                    }}
                                    icon={faXmark} />
                            </div>
                        </div>
                        <div className="relative h-[90%] flex flex-col justify-between space-y-2 p-2">
                            {isStarted ? <OpenedChat /> : <AllChats />}
                        </div>
                    </div>}
            </>}
        </>
    );
};

export default Chat;