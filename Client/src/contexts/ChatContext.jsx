import { createContext, useContext, useState } from 'react';

const ChatContext = createContext();

export function ChatProvider({ children }) {
    const [isStarted, setIsStarted] = useState(false);
    
    const closeChat = () => {
        setIsStarted(false);
        sessionStorage.removeItem('chatData');
    };

    const openChat = (receiverId, receiverName) => {
        setIsStarted(true);
       
        const chatData = {
            receiverId: receiverId,
            receiverName: receiverName
        };

        sessionStorage.setItem('chatData', JSON.stringify(chatData));
    };

    const getReceiverData = () => {
        return JSON.parse(sessionStorage.getItem('chatData'));
    };

    return (
        <ChatContext.Provider value={{ isStarted, openChat, closeChat, getReceiverData }}>
            {children}
        </ChatContext.Provider>
    );
}

export function useChat() {
    return useContext(ChatContext);
}