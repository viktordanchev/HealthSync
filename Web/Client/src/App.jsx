﻿import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleUp } from '@fortawesome/free-solid-svg-icons';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './components/Home';
import SessionMessage from './components/SessionMessage';
import NotFound from './components/NotFound';
import GuestOnly from './components/GuestOnly';
import ProtectedRoute from './components/ProtectedRoute';
import DoctorDetails from './components/doctor/DoctorDetails';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import VerificationPage from './pages/VerificationPage';
import RecoverPassPage from './pages/RecoverPassPage';
import DoctorsPage from './pages/DoctorsPage';
import UserSettingsPage from './pages/UserSettingsPage';
import { AuthProvider } from './contexts/AuthContext';
import { LoadingProvider } from './contexts/LoadingContext';
import { MessageProvider } from './contexts/MessageContext';

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
            <Router>
                <AuthProvider>
                    <SessionMessage />
                    <Header />
                    <main className="my-6 md:mx-6 sm:mx-6">
                        <MessageProvider>
                            <Routes>
                                <Route path="*" element={<NotFound />} />

                                <Route path="/login" element={<GuestOnly><LoginPage /></GuestOnly>} />
                                <Route path="/register" element={<GuestOnly><RegisterPage /></GuestOnly>} />
                                <Route path="/account/verify" element={<GuestOnly><VerificationPage /></GuestOnly>} />
                                <Route path="/account/recoverPassword" element={<GuestOnly><RecoverPassPage /></GuestOnly>} />
                                <Route path="/account/settings" element={<ProtectedRoute><UserSettingsPage /></ProtectedRoute>} />

                                <Route path="/" element={<Navigate to="/home" />} />
                                <Route path="/home" element={<Home />} />
                                <Route path="/doctors" element={<DoctorsPage />} />
                                <Route path="/doctors/:name/:specialty" element={<DoctorDetails />} />
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