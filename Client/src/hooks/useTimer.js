import { useEffect, useState } from 'react';

const useTimer = () => {
    const [secondsLeft, setSecondsLeft] = useState(0);
    
    useEffect(() => {
        if (secondsLeft <= 0) {
            return;
        }

        const timeout = setTimeout(() => {
            setSecondsLeft(secondsLeft - 1);
        }, 1000);

        return () => clearTimeout(timeout);
    }, [secondsLeft]);

    const start = (sec) => {
        setSecondsLeft(sec);
    };

    return { secondsLeft, start };
};

export default useTimer;
