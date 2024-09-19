import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Header from './components/Header.jsx';
import Login from './components/authentication/Login.jsx';
import Register from './components/authentication/Register.jsx';
import Verification from './components/authentication/Verification.jsx';
import Footer from './components/Footer.jsx';
import Section from './components/Section.jsx';

function App() {

    return (
        <Router>
            <Header />
            <Routes>
                <Route path="/" element={<Navigate to="/home" />} />
                <Route path="/home" element={< Section />} />
                <Route path="/account/login" element={<Login />} />
                <Route path="/account/register" element={<Register />} />
                <Route path="/account/confirmRegistration" element={<Verification />} />
            </Routes>
            <Footer />
        </Router >
    );
}

export default App;