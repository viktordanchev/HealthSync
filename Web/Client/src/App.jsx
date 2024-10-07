import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './components/Home';
import Loading from './components/Loading';
import SessionMessage from './components/SessionMessage';
import Login from './components/authentication/Login';
import Register from './components/authentication/Register';
import Verification from './components/authentication/Verification';
import RecoverPassword from './components/authentication/RecoverPassword';
import useCheckAuth from './hooks/useCheckAuth';

function App() {
    const { error } = useCheckAuth();

    return (
        <>
            {error ? <SessionMessage /> : null}
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
                    </Routes>
                </main>
                <Footer />
            </Router>
        </>
    );
}

export default App;