import { useEffect, useState, useCallback } from 'react';

const useTimer = () => {
    const [seconds, setSeconds] = useState(() => {
        const savedSeconds = localStorage.getItem('timerSeconds');
        return savedSeconds ? parseInt(savedSeconds, 10) : 60;
    });
    const [isButtonDisabled, setIsButtonDisabled] = useState(true);

    useEffect(() => {
        localStorage.setItem('timerSeconds', seconds);

        if (seconds > 0) {
            const timer = setTimeout(() => {
                setSeconds(seconds - 1);
            }, 1000);

            return () => clearTimeout(timer);
        } else {
            setIsButtonDisabled(false);
            localStorage.removeItem('timerSeconds');
        }
    }, [seconds]);

    const resetTimer = useCallback(() => {
        setSeconds(60);
        setIsButtonDisabled(true);
        localStorage.removeItem('timerSeconds');
    }, []);

    return { isButtonDisabled, seconds, resetTimer };
};

export default useTimer;
