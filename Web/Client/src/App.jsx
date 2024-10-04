import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import Home from './components/Home';
import Loading from './components/Loading';
import Login from './components/authentication/Login';
import Register from './components/authentication/Register';
import Verification from './components/authentication/Verification';
import RecoverPassword from './components/authentication/RecoverPassword';
import { jwtDecode } from 'jwt-decode';
import useCheckAuth from './hooks/useCheckAuth'

function App() {
    const { isAuthenticated, loading, error } = useCheckAuth();

    return (
        <>
            {loading ? <Loading /> :
                <>
                    {error ?
                        <div class="bg-red-500 absolute inset-0 flex items-center justify-center">
                            <span class="text-xl font-bold text-white">Центриран текст</span>
                        </div> : <div class="bg-red-500 absolute inset-0 flex items-center justify-center">
                            <span class="text-xl font-bold text-white">Центриран текст</span>
                        </div>}
                    <Router>
                        <Header isAuthenticated={isAuthenticated} />
                        <main className="grow content-center my-6">
                            <Routes>
                                <Route path="/" element={<Navigate to="/home" />} />
                                <Route path="/home" element={isAuthenticated ? <Home /> : <Navigate to="/login" />} />
                                <Route path="/login" element={isAuthenticated ? <Navigate to="/home" /> : <Login />} />
                                <Route path="/register" element={isAuthenticated ? <Navigate to="/home" /> : <Register />} />
                                <Route path="/account/verify" element={isAuthenticated ? <Navigate to="/home" /> : <Verification />} />
                                <Route path="/account/recoverPassword" element={isAuthenticated ? <Navigate to="/home" /> : <RecoverPassword />} />
                            </Routes>
                        </main>
                        <Footer />
                    </Router >
                </>}
        </>
    );
}

export default App;