import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleDown } from '@fortawesome/free-solid-svg-icons';
import { useChat } from '../../contexts/ChatContext';
import Loading from '../Loading';
import apiRequest from '../../services/apiRequest';
import { format } from 'date-fns';
import jwtDecoder from '../../services/jwtDecoder';
import ImageMessage from './ImageMessage';

function MessageHistory({ messages, updateMessages, isSenderTyping, containerRef, isMessageReceived, setIsMessageReceived }) {
    const { getReceiverData } = useChat();
    const { userId } = jwtDecoder();
    const [isLoading, setIsLoading] = useState(true);
    const [isAtBottom, setIsAtBottom] = useState(true);

    {/* This hook handles receiving the message history. */ }
    useEffect(() => {
        const receiveData = async () => {
            const dto = {
                senderId: userId,
                receiverId: getReceiverData().receiverId
            };

            try {
                const response = await apiRequest('account', 'getChatHistory', dto, localStorage.getItem('accessToken'), 'POST', false);

                updateMessages(response);
            } catch (error) {
                console.error(error);
            } finally {
                setIsLoading(false);
            }
        };

        receiveData();
    }, []);

    useEffect(() => {
        const handleScroll = () => {
            const { scrollTop, clientHeight, scrollHeight } = containerRef.current;
            setIsAtBottom(scrollTop + clientHeight >= scrollHeight - 10);
        };

        const container = containerRef.current;

        container.addEventListener("scroll", handleScroll);

        return () => {
            container.removeEventListener("scroll", handleScroll);
        };
    }, []);

    useEffect(() => {
        if (isAtBottom) {
            containerRef.current.scrollTop = containerRef.current.scrollHeight;
        }
    }, [messages, isSenderTyping]);

    const scrollToBottom = () => {
        containerRef.current.scrollTo({
            top: containerRef.current.scrollHeight,
            behavior: "smooth"
        });

        setIsMessageReceived(false);
    };

    return (
        <>
            <button className="z-20 absolute bottom-14 left-1/2 transform -translate-x-1/2 text-sm flex flex-col items-center cursor-pointer"
                onClick={scrollToBottom}>
                {isMessageReceived && <p>New Message</p>}
                <FontAwesomeIcon icon={faAngleDown} className={`text-white bg-zinc-600 h-4 w-4 p-1 text-sm rounded-full shadow-xl hover:bg-zinc-500 ${!isAtBottom ? 'opacity-100' : 'opacity-0 pointer-events-none'}`} />
            </button>
            <div className="h-full space-y-3 flex flex-col overflow-y-auto scrollbar-none border border-zinc-500 rounded p-2"
                ref={containerRef}>
                {isLoading ? <Loading type={'small'} /> :
                    <>
                        {messages.map((msg, index) => (
                            <React.Fragment key={index}>
                                {(index === 0 || format(msg.dateAndTime, 'dd.MM.yyyy') !== format(messages[index - 1].dateAndTime, 'dd.MM.yyyy')) && (<p className="text-xs text-zinc-600 text-center">{format(msg.dateAndTime, 'dd.MM.yyyy')}</p>)}
                                <div className={`max-w-[70%] p-2 rounded-xl break-words whitespace-pre-wrap flex flex-col space-y-1 ${userId === msg.senderId ? 'self-end bg-blue-500' : 'self-start bg-gray-400'}`}>
                                    <div className="flex flex-wrap justify-center gap-2">
                                        {msg.images.map((img, index) => (
                                            <ImageMessage key={index} image={img} />
                                        ))}
                                    </div>
                                    <p>{msg.message}</p>
                                    <p className="text-xs self-end">{format(msg.dateAndTime, 'HH:mm')}</p>
                                </div>
                            </React.Fragment>
                        ))}
                    </>}
                {isSenderTyping &&
                    <div className="flex items-center space-x-1 rounded-xl bg-gray-400 p-3 self-start">
                        <div className="w-1.5 h-1.5 bg-gray-600 rounded-full animate-bounce"></div>
                        <div className="w-1.5 h-1.5 bg-gray-600 rounded-full animate-bounce" style={{ animationDelay: '0.3s' }}></div>
                        <div className="w-1.5 h-1.5 bg-gray-600 rounded-full animate-bounce" style={{ animationDelay: '0.6s' }}></div>
                    </div>}
            </div>
        </>
    );
};

export default MessageHistory;