import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import Home from './components/Home.jsx';
import Loading from './components/Loading.jsx';
import Login from './components/authentication/Login.jsx';
import Register from './components/authentication/Register.jsx';
import Verification from './components/authentication/Verification.jsx';
import RecoverPassword from './components/authentication/RecoverPassword.jsx';
import { jwtDecode } from 'jwt-decode';
import useAuth from './hooks/useAuth.js'

function App() {
    const isAuthenticated = useAuth();

    return (
        <Router>
            <Header isAuthenticated={isAuthenticated} />
            <Routes>
                <Route path="/" element={<Navigate to="/home" />} />
                <Route path="/home" element={<Home />} />
                <Route path="/login" element={isAuthenticated ? <Navigate to="/home" /> : <Login />} />
                <Route path="/register" element={isAuthenticated ? <Navigate to="/home" /> : <Register />} />
                <Route path="/account/verify" element={isAuthenticated ? <Navigate to="/home" /> : <Verification />} />
                <Route path="/account/recoverPassword" element={isAuthenticated ? <Navigate to="/home" /> : <RecoverPassword />} />
            </Routes>
            <Footer />
        </Router >
    );
}

export default App;