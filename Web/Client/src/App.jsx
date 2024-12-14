import { faAngleUp } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import { Navigate, Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import Footer from './components/Footer';
import GuestOnly from './components/GuestOnly';
import Header from './components/Header';
import ParticlesBg from './components/ParticlesBg';
import ProtectedRoute from './components/ProtectedRoute';
import SessionMessage from './components/SessionMessage';
import DoctorDetails from './components/doctorsPage/DoctorDetails';
import { AuthProvider } from './contexts/AuthContext';
import { LoadingProvider } from './contexts/LoadingContext';
import { MessageProvider } from './contexts/MessageContext';
import DoctorsPage from './pages/DoctorsPage';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import NotFoundPage from './pages/NotFoundPage';
import RecoverPassPage from './pages/RecoverPassPage';
import RegisterPage from './pages/RegisterPage';
import UserMeetingsPage from './pages/UserMeetingsPage';
import UserSettingsPage from './pages/UserSettingsPage';
import BecomeDoctorPage from './pages/BecomeDoctorPage';

function App() {
    const [showButton, setShowButton] = useState(false);

    useEffect(() => {
        const handleScroll = () => {
            if (window.scrollY > 300) {
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

    const scrollToTop = () => {
        window.scrollTo({
            top: 0,
            behavior: "smooth"
        });
    };

    return (
        <LoadingProvider>
            <ParticlesBg />
            <Router>
                <AuthProvider>
                    <SessionMessage />
                    <Header />
                    <main className="h-full w-full flex flex-col justify-center items-center my-6 md:px-6 sm:px-6">
                        <MessageProvider>
                            <Routes>
                                <Route path="*" element={<NotFoundPage />} />

                                <Route path="/login" element={<GuestOnly><LoginPage /></GuestOnly>} />
                                <Route path="/register" element={<GuestOnly><RegisterPage /></GuestOnly>} />
                                <Route path="/account/recoverPassword" element={<GuestOnly><RecoverPassPage /></GuestOnly>} />
                                <Route path="/account/settings" element={<ProtectedRoute><UserSettingsPage /></ProtectedRoute>} />

                                <Route path="/" element={<Navigate to="/home" />} />
                                <Route path="/home" element={<HomePage />} />
                                <Route path="/doctors" element={<DoctorsPage />} />
                                <Route path="/doctors/:name/:specialty" element={<DoctorDetails />} />
                                <Route path="/meetings" element={<ProtectedRoute><UserMeetingsPage /></ProtectedRoute>} />
                                <Route path="/becomeDoctor" element={<ProtectedRoute><BecomeDoctorPage /></ProtectedRoute>} />
                            </Routes>
                        </MessageProvider>
                        <button
                            onClick={scrollToTop}
                            className={`fixed bottom-16 right-16 bg-zinc-600 h-16 w-16 rounded-full shadow-xl hover:bg-zinc-500 transition-opacity duration-300 md:bottom-12 md:right-12 md:h-14 md:w-14 sm:bottom-8 sm:right-8 sm:h-11 sm:w-11 ${showButton ? 'opacity-100' : 'opacity-0 pointer-events-none'}`}>
                            <FontAwesomeIcon icon={faAngleUp} className="text-white text-4xl md:text-3xl sm:text-xl" />
                        </button>
                    </main>
                    <Footer />
                </AuthProvider>
            </Router>
        </LoadingProvider>
    );
}

export default App;