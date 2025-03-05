import React, { useEffect, useState } from 'react';
import * as signalR from "@microsoft/signalr";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark, faPaperPlane, faMinus } from '@fortawesome/free-solid-svg-icons';
import { useAuthContext } from '../../contexts/AuthContext';
import Loading from '../Loading';
import apiRequest from '../../services/apiRequest';
import jwtDecoder from '../../services/jwtDecoder';
import { useChat } from '../../contexts/ChatContext';

function OpenedChat() {
    const { isStart, closeChat, getReceiverData } = useChat();
    const { isStillAuth } = useAuthContext();
    const { userId } = jwtDecoder();
    const [messages, setMessages] = useState([]);
    const [message, setMessage] = useState("");
    const [isLoading, setIsLoading] = useState(true);
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7080/chat")
        .withAutomaticReconnect()
        .build();

    useEffect(() => {
        const receiveData = async () => {
            const dto = {
                senderId: userId,
                receiverId: JSON.parse(sessionStorage.getItem('chatData')).receiverId
            };

            try {
                const response = await apiRequest('account', 'getChatHistory', dto, localStorage.getItem('accessToken'), 'POST', false);

                setMessages(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, []);

    const sendMessage = async () => {
        const dto = {
            senderId: userId,
            receiverId: JSON.parse(sessionStorage.getItem('chatData')).receiverId,
            message: message
        };

        await connection.start();
        await connection.invoke("SendMessage", dto);
        setMessage("");
    };

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
        const newChat = getReceiverData();
        const existingChats = JSON.parse(sessionStorage.getItem('allChats'))
            .filter(chat => chat.receiverId !== newChat.receiverId);
        
        sessionStorage.setItem('allChats', JSON.stringify(existingChats));

        closeChat();
    };

    return (
        <>
            {isStart &&
                <div className="fixed bottom-16 right-16 h-96 w-72 shadow-2xl shadow-gray-400 rounded-xl bg-gray-200 bg-opacity-85 border border-zinc-500 sm:bottom-0 sm:right-0 sm:h-full sm:w-full sm:bg-opacity-100 sm:rounded-none sm:z-50">
                    <div className="h-[10%] flex justify-between items-center text-zinc-600 bg-maincolor rounded-t-xl py-1 px-2 sm:rounded-t-none">
                        <p className="font-medium">To: {getReceiverData().receiverName}</p>
                        <div className="flex justify-end space-x-2">
                            <FontAwesomeIcon
                                className="text-2xl cursor-pointer"
                                onClick={minimuzeChat}
                                icon={faMinus} />
                            <FontAwesomeIcon
                                className="text-2xl cursor-pointer"
                                onClick={removeChat}
                                icon={faXmark} />
                        </div>
                    </div>
                    <div className="h-[90%] flex flex-col justify-between space-y-2 p-2">
                        <div className="h-full border border-zinc-500 rounded-xl p-2">
                            {isLoading ? <Loading type={'small'} /> :
                                <>
                                    {messages.map((msg, index) => (
                                        <p key={index}><b>{msg.user}:</b> {msg.text}</p>
                                    ))}
                                </>}
                        </div>
                        <div className="flex space-x-2 justify-between bg-white rounded-full px-3 py-1 border border-blue-500">
                            <input
                                className="w-full focus:outline-none"
                                placeholder="Message..."
                                type="text"
                                value={message}
                                onChange={(e) => setMessage(e.target.value)} />
                            <button
                                className="cursor-pointer flex justify-center items-center"
                                onClick={sendMessage}>
                                <FontAwesomeIcon className="text-zinc-600" icon={faPaperPlane} />
                            </button>
                        </div>
                    </div>
                </div>}
        </>
    );
};

export default OpenedChat;