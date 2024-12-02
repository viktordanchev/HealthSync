import Loading from '../components/Loading';
import { useLoading } from '../contexts/LoadingContext';

function LoadingGlobal() {
    const { isLoading } = useLoading();

    return (
        <>
            {isLoading && (
                <div className="fixed z-50 h-full w-full bg-black bg-opacity-45">
                    <Loading type={'big'} />
                </div>
            )}
        </>

    );
}

export default LoadingGlobal;