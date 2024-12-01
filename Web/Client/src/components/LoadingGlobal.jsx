import Loading from '../components/Loading';

function LoadingGlobal() {
    return (
        <div className="fixed z-50 h-full w-full bg-black bg-opacity-45">
            <Loading type={'big'} />
        </div>
    );
}

export default LoadingGlobal;