import React, { useEffect, useState } from 'react';
import * as signalR from "@microsoft/signalr";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMessage, faXmark, faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { useAuthContext } from '../contexts/AuthContext';

function Chat() {
    const { isAuthenticated } = useAuthContext();
    const [messages, setMessages] = useState([]);
    const [message, setMessage] = useState("");
    const [isOpen, setIsOpen] = useState(false);

    useEffect(() => {
        // Създаване на SignalR връзка
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7080/chathub") // Адресът на твоя SignalR Hub
            .withAutomaticReconnect()
            .build();

        // Стартиране на връзката
        connection.start()
            .then(() => console.log("Connected to SignalR"))
            .catch(err => console.error("SignalR error:", err));

        // Слушане за съобщения
        connection.on("ReceiveMessage", (user, message) => {
            setMessages(prevMessages => [...prevMessages, { user, message }]);
        });

        return () => {
            connection.stop();
        };
    }, []);

    const sendMessage = async () => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7080/chathub")
            .withAutomaticReconnect()
            .build();

        await connection.start();
        await connection.invoke("SendMessage", "User1", message);
        setMessage("");
    };

    return (
        <>
            {isAuthenticated && (<>{!isOpen ?
                <button
                    onClick={() => setIsOpen(!isOpen)}
                    className={`fixed bottom-16 right-16 border-2 border-zinc-500 flex items-center justify-center bg-blue-500 h-16 w-16 rounded-full shadow-xl transition-opacity duration-300 hover:bg-blue-600 md:bottom-12 md:right-12 md:h-14 md:w-14 sm:bottom-8 sm:right-8 sm:h-12 sm:w-12`}>
                    <FontAwesomeIcon icon={faMessage} className="cursor-pointer text-white text-2xl sm:text-lg" />
                </button> :
                <div className="fixed bottom-16 right-16 h-96 w-72 shadow-2xl shadow-gray-400 rounded-xl bg-gray-200 bg-opacity-85 border border-zinc-500">
                    <div className="h-[10%] flex justify-end items-center bg-maincolor rounded-t-xl py-1 px-2">
                        <FontAwesomeIcon
                            className="text-2xl text-zinc-600 cursor-pointer"
                            onClick={() => setIsOpen(!isOpen)}
                            icon={faXmark} />
                    </div>
                    <div className="h-[90%] flex flex-col justify-between space-y-2 p-2">
                        <div className="h-full border border-zinc-500 rounded-xl p-2">
                            {messages.map((msg, index) => (
                                <p key={index}><b>{msg.user}:</b> {msg.text}</p>
                            ))}
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
                </div>}</>)}
        </>
    );
};

export default Chat;