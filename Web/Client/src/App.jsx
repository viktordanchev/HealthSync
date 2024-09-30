import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import Home from './components/Home.jsx';
import Loading from './components/Loading.jsx';
import Login from './components/authentication/Login.jsx';
import Register from './components/authentication/Register.jsx';
import EmailVerification from './components/authentication/EmailVerification.jsx';
import RecoverPassword from './components/authentication/RecoverPassword.jsx';
import { jwtDecode } from 'jwt-decode';
import useCheckAuth from './hooks/useCheckAuth.js'

function App() {
    const { isAuthenticated, loading } = useCheckAuth();

    return (
        <>
            {loading ? <Loading /> :
                <Router>
                    <Header isAuthenticated={isAuthenticated} />
                    <main className="grow content-center">
                        <Routes>
                            <Route path="/" element={<Navigate to="/home" />} />
                            <Route path="/home" element={isAuthenticated ? <Home /> : <Navigate to="/login" />} />
                            <Route path="/login" element={isAuthenticated ? <Navigate to="/home" /> : <Login />} />
                            <Route path="/register" element={isAuthenticated ? <Navigate to="/home" /> : <Register />} />
                            <Route path="/account/verify" element={isAuthenticated ? <Navigate to="/home" /> : <EmailVerification />} />
                            <Route path="/account/recoverPassword" element={isAuthenticated ? <Navigate to="/home" /> : <RecoverPassword />} />
                        </Routes>
                    </main>
                    <Footer />
                </Router >}
        </>
    );
}

export default App;