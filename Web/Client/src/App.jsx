import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import useCheckAuth from './hooks/useCheckAuth';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './components/Home';
import SessionMessage from './components/SessionMessage';
import Login from './components/account/Login';
import Register from './components/account/Register';
import Verification from './components/account/Verification';
import RecoverPassword from './components/account/RecoverPassword';
import AllDoctors from './components/doctor/AllDoctors';

function App() {
    const { isSessionEnd } = useCheckAuth();

    return (
        <>
            {isSessionEnd ? <SessionMessage /> : null}
            <Router>
                <Header />
                <main className="grow content-center my-6">
                    <Routes>
                        <Route path="/" element={<Navigate to="/home" />} />
                        <Route path="/home" element={<Home />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/account/verify" element={<Verification />} />
                        <Route path="/account/recoverPassword" element={<RecoverPassword />} />
                        <Route path="/doctors/all" element={<AllDoctors />} />
                    </Routes>
                </main>
                <Footer />
            </Router>
        </>
    );
}

export default App;