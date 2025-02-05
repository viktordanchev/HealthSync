import { createContext, useContext, useState } from 'react';
import Loading from '../components/Loading';

const LoadingContext = createContext();

export const LoadingProvider = ({ children }) => {
    const [isLoading, setIsLoading] = useState(false);

    return (
        <LoadingContext.Provider value={{ setIsLoading }}>
            {isLoading && (
                <div className="fixed z-50 h-full w-full bg-black bg-opacity-45">
                    <Loading type={'big'} />
                </div>
            )}
            {children}
        </LoadingContext.Provider>
    );
};

export const useLoading = () => useContext(LoadingContext);