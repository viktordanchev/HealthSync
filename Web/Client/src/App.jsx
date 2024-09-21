import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import ProtectedRoute from './components/ProtectedRoute.jsx';
import Home from './components/Home.jsx';
import Loading from './components/Loading.jsx';
import Login from './components/authentication/Login.jsx';
import Register from './components/authentication/Register.jsx';
import Verification from './components/authentication/Verification.jsx';
import { isAuthenticated } from './services/account.js';

function App() {
    const [isUserAuthenticated, setIsUserAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const checkUserStatus = async () => {
            const response = await isAuthenticated();
            setIsUserAuthenticated(response);
            setLoading(false);
        }

        checkUserStatus();
    }, []);

    return (
        <>
            {loading ? <Loading /> :
                <Router>
                    <Header isAuthenticated={isUserAuthenticated} />
                    <Routes>
                        <Route path="/" element={<Navigate to="/home" />} />
                        <Route path="/home" element={
                            <ProtectedRoute isAuthenticated={isUserAuthenticated}>
                                <Home />
                            </ProtectedRoute>
                        } />
                        <Route path="/login" element={isUserAuthenticated ? <Navigate to="/home" /> : <Login />} />
                        <Route path="/register" element={isUserAuthenticated ? <Navigate to="/home" /> : <Register />} />
                        <Route path="/verifyAccount" element={isUserAuthenticated ? <Navigate to="/home" /> : <Verification />} />
                    </Routes>
                    <Footer />
                </Router >}
        </>
    );
}

export default App;