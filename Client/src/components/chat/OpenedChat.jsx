import React, { useEffect, useState, useRef } from 'react';
import * as signalR from "@microsoft/signalr";
import { useAuthContext } from '../../contexts/AuthContext';
import SendMessage from './SendMessage';
import MessageHistory from './MessageHistory';

function OpenedChat() {
    const { isStillAuth } = useAuthContext();
    const [messageHistory, setMessageHistory] = useState([]);
    const [connection, setConnection] = useState(null);
    const [isSenderTyping, setIsSenderTyping] = useState(false);
    const [isMessageReceived, setIsMessageReceived] = useState(false);
    const messageHistoryRef = useRef(null);

    useEffect(() => {
        const startConnection = async () => {
            const newConnection = new signalR.HubConnectionBuilder()
                .withUrl('https://localhost:7080/chat', {
                    accessTokenFactory: () => localStorage.getItem('accessToken')
                })
                .build();

            newConnection.on("ReceiveMessage", (senderId, message, dateAndTime, images) => {
                setMessageHistory(prevMessages => [...prevMessages, { senderId, message, dateAndTime, images }]);
                setIsSenderTyping(false);
                
                const { scrollTop, clientHeight, scrollHeight } = messageHistoryRef.current;

                if (scrollTop + clientHeight < scrollHeight - 10) {
                    setIsMessageReceived(true);
                }
            });

            newConnection.on("UserTyping", (isTyping) => {
                setIsSenderTyping(isTyping);
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

    return (
        <>
            <MessageHistory
                messages={messageHistory}
                updateMessages={setMessageHistory}
                isSenderTyping={isSenderTyping}
                containerRef={messageHistoryRef}
                isMessageReceived={isMessageReceived}
                setIsMessageReceived={setIsMessageReceived}
            />
            <SendMessage
                connection={connection}
                updateMessages={setMessageHistory}
            />
        </>
    );
};

export default OpenedChat;