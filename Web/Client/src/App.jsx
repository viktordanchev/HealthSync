import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import useCheckAuth from './hooks/useCheckAuth';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './components/Home';
import SessionMessage from './components/SessionMessage';
import Login from './components/authentication/Login';
import Register from './components/authentication/Register';
import Verification from './components/authentication/Verification';
import RecoverPassword from './components/authentication/RecoverPassword';
import AllDoctors from './components/doctors/AllDoctors';

function App() {
    const { error } = useCheckAuth();

    return (
        <>
            {error ? <SessionMessage error={error} /> : null}
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