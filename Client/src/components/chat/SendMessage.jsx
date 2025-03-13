import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { useChat } from '../../contexts/ChatContext';
import { useAuthContext } from '../../contexts/AuthContext';
import jwtDecoder from '../../services/jwtDecoder';

function SendMessage({ connection, updateMessages }) {
    const { getReceiverData } = useChat();
    const { userId } = jwtDecoder();
    const [message, setMessage] = useState('');

    const sendMessage = async () => {
        const dto = {
            senderId: userId,
            receiverId: getReceiverData().receiverId,
            message: message,
            dateAndTime: new Date()
        };

        updateMessages(prevMessages => [...prevMessages, {
            senderId: dto.senderId,
            message: dto.message,
            dateAndTime: dto.dateAndTime
        }]);

        await connection.invoke("SendMessage", dto);
        setMessage('');
    };

    const sendTyping = async (messageLength) => {
        await connection.invoke("SenderTyping", getReceiverData().receiverId, messageLength > 0);
    };

    return (
        <div className="flex space-x-2 justify-between bg-white rounded-full px-3 py-1 border border-blue-500">
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
    );
};

export default SendMessage;