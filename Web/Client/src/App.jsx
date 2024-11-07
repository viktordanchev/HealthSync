import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import useCheckAuth from './hooks/useCheckAuth';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './components/Home';
import SessionMessage from './components/SessionMessage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import VerificationPage from './pages/VerificationPage';
import RecoverPassPage from './pages/RecoverPassPage';
import DoctorsPage from './pages/DoctorsPage';

function App() {
    const { isSessionEnd } = useCheckAuth();
    const [showSessionMessage, setShowSessionMessage] = useState();

    useEffect(() => {
        setShowSessionMessage(isSessionEnd);
    }, [isSessionEnd]);

    return (
        <>
            {showSessionMessage && <SessionMessage close={() => setShowSessionMessage(false)} />}
            <Router>
                <Header />
                <main className="grow content-center my-6">
                    <Routes>
                        <Route path="/" element={<Navigate to="/home" />} />
                        <Route path="/home" element={<Home />} />
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/register" element={<RegisterPage />} />
                        <Route path="/account/verify" element={<VerificationPage />} />
                        <Route path="/account/recoverPassword" element={<RecoverPassPage />} />
                        <Route path="/doctors" element={<DoctorsPage />} />
                    </Routes>
                </main>
                <Footer />
            </Router>
        </>
    );
}

export default App;