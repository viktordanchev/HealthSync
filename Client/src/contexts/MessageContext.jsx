import React, { createContext, useContext, useState } from 'react';
import Message from '../components/Message';

const MessageContext = createContext();

export function MessageProvider({ children }) {
    const [message, setMessage] = useState('');
    const [type, setType] = useState('');

    const showMessage = (msg, msgType) => {
        setMessage(msg);
        setType(msgType);
        
        setTimeout(() => {
            setMessage('');
        }, 5000);
    };

    return (
        <MessageContext.Provider value={{ showMessage }}>
            {message && <Message message={message} type={type} />}
            {children}
        </MessageContext.Provider>
    );
}

export const useMessage = () => useContext(MessageContext);