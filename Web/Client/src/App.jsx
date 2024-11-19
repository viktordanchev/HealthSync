import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleUp } from '@fortawesome/free-solid-svg-icons';
import useCheckAuth from './hooks/useCheckAuth';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './components/Home';
import SessionMessage from './components/SessionMessage';
import NotFound from './components/NotFound';
import DoctorDetails from './components/doctor/DoctorDetails';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import VerificationPage from './pages/VerificationPage';
import RecoverPassPage from './pages/RecoverPassPage';
import DoctorsPage from './pages/DoctorsPage';

function App() {
    const { isSessionEnd } = useCheckAuth();
    const [showSessionMessage, setShowSessionMessage] = useState();
    const [showButton, setShowButton] = useState(false);

    useEffect(() => {
        const handleScroll = () => {
            if (window.scrollY > 100) {
                setShowButton(true);
            } else {
                setShowButton(false);
            }
        };

        window.addEventListener('scroll', handleScroll);

        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    }, []);

    useEffect(() => {
        setShowSessionMessage(isSessionEnd);
    }, [isSessionEnd]);

    const scrollToTop = () => {
        window.scrollTo({
            top: 0,
            behavior: "smooth"
        });
    };

    return (
        <>
            {showSessionMessage && <SessionMessage close={() => setShowSessionMessage(false)} />}
            <Router>
                <Header />
                <main className="my-6">
                    <Routes>
                        <Route path="/" element={<Navigate to="/home" />} />
                        <Route path="/home" element={<Home />} />
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/register" element={<RegisterPage />} />
                        <Route path="/account/verify" element={<VerificationPage />} />
                        <Route path="/account/recoverPassword" element={<RecoverPassPage />} />
                        <Route path="/doctors" element={<DoctorsPage />} />
                        <Route path="/doctors/:name/:specialty" element={<DoctorDetails />} />
                        <Route path="*" element={<NotFound />} />
                    </Routes>
                    {showButton && (
                        <button
                            onClick={scrollToTop}
                            className="fixed bottom-16 right-16 bg-zinc-700 h-16 w-16 rounded-full shadow-xl hover:bg-zinc-600 md:bottom-12 md:right-12 md:h-14 md:w-14 sm:bottom-8 sm:right-8 sm:h-11 sm:w-11">
                            <FontAwesomeIcon icon={faAngleUp} className="text-white text-4xl md:text-3xl sm:text-xl"/>
                        </button>
                    )}
                </main>
                <Footer />
            </Router>
        </>
    );
}

export default App;