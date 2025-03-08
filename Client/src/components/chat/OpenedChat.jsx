import React, { useEffect, useState, useRef, useCallback } from 'react';
import * as signalR from "@microsoft/signalr";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faXmark, faPaperPlane, faMinus } from '@fortawesome/free-solid-svg-icons';
import { useAuthContext } from '../../contexts/AuthContext';
import Loading from '../Loading';
import apiRequest from '../../services/apiRequest';
import jwtDecoder from '../../services/jwtDecoder';
import { useChat } from '../../contexts/ChatContext';
import { format } from 'date-fns';
import { debounce } from "lodash";

function OpenedChat() {
    const containerRef = useRef(null);
    const { isStarted, closeChat, getReceiverData } = useChat();
    const { isStillAuth } = useAuthContext();
    const { userId } = jwtDecoder();
    const [messageHistory, setMessageHistory] = useState([]);
    const [message, setMessage] = useState("");
    const [connection, setConnection] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [isUserTyping, setIsUserTyping] = useState(false);

    useEffect(() => {
        if (containerRef.current) {
            containerRef.current.scrollTop = containerRef.current.scrollHeight;
        }
    }, [messageHistory]);

    useEffect(() => {
        const startConnection = async () => {
            const newConnection = new signalR.HubConnectionBuilder()
                .withUrl('https://localhost:7080/chat', {
                    accessTokenFactory: () => localStorage.getItem('accessToken')
                })
                .build();

            newConnection.on("ReceiveMessage", (senderId, message, dateAndTime) => {
                setMessageHistory(prevMessages => [...prevMessages, { senderId, message, dateAndTime: dateAndTime }]);
                setIsUserTyping(false);
            });

            newConnection.on("UserTyping", (isTyping) => {
                setIsUserTyping(isTyping);
            });

            await newConnection.start();
            setConnection(newConnection);
        };

        startConnection();

        return () => {
            if (connection != null) {
                connection.stop();
            }
        };
    }, []);

    useEffect(() => {
        const receiveData = async () => {
            const dto = {
                senderId: userId,
                receiverId: getReceiverData().receiverId
            };

            try {
                const response = await apiRequest('account', 'getChatHistory', dto, localStorage.getItem('accessToken'), 'POST', false);

                setMessageHistory(response);
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
            receiverId: getReceiverData().receiverId,
            message: message,
            dateAndTime: new Date()
        };

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
        if (sessionStorage.getItem('allChats')) {
            const newChat = getReceiverData();
            const existingChats = JSON.parse(sessionStorage.getItem('allChats'))
                .filter(chat => chat.receiverId !== newChat.receiverId);

            sessionStorage.setItem('allChats', JSON.stringify(existingChats));
        }

        closeChat();
    };

    const sendTypingNotification = async (messageLength) => {
        await connection.invoke("TypingNotification", getReceiverData().receiverId, messageLength > 0 ? true : false);
    };

    return (
        <>
            {isStarted &&
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
                        <div className="h-full space-y-3 flex flex-col overflow-y-auto scrollbar-thin scrollbar-thumb-rounded scrollbar-track-rounded scrollbar-thumb-zinc-500 scrollbar-track-transparent border border-zinc-500 rounded p-2"
                            ref={containerRef}>
                            {isLoading ? <Loading type={'small'} /> :
                                <>
                                    {messageHistory.map((msg, index) => (
                                        <React.Fragment key={index}>
                                            {(index === 0 || format(msg.dateAndTime, 'dd.MM.yyyy') !== format(messageHistory[index - 1].dateAndTime, 'dd.MM.yyyy')) && (<p className="text-center border-b border-black">{format(msg.dateAndTime, 'dd.MM.yyyy')}</p>)}
                                            <div className={`max-w-[70%] px-2 py-1 rounded-xl break-words whitespace-pre-wrap flex flex-col ${userId === msg.senderId ? 'self-end bg-blue-500' : 'self-start bg-gray-400'}`}>
                                                <p>{msg.message}</p>
                                                <p className="text-xs self-end">{format(msg.dateAndTime, 'HH:mm')}</p>
                                            </div>
                                        </React.Fragment>
                                    ))}
                                </>}
                            {isUserTyping &&
                                <div className="flex items-center space-x-1 rounded-xl bg-gray-400 p-3 self-start">
                                    <div className="w-2 h-2 bg-gray-600 rounded-full animate-bounce"></div>
                                    <div className="w-2 h-2 bg-gray-600 rounded-full animate-bounce" style={{ animationDelay: '0.3s' }}></div>
                                    <div className="w-2 h-2 bg-gray-600 rounded-full animate-bounce" style={{ animationDelay: '0.6s' }}></div>
                                </div>}
                        </div>
                        <div className="flex space-x-2 justify-between bg-white rounded-full px-3 py-1 border border-blue-500">
                            <input
                                className="w-full focus:outline-none"
                                placeholder="Message..."
                                type="text"
                                value={message}
                                onChange={(e) => {
                                    setMessage(e.target.value);
                                    sendTypingNotification(e.target.value.length);
                                }} />
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