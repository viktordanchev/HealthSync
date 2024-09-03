import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './components/Header.jsx';
import Login from './components/authentication/Login.jsx';
import Register from './components/authentication/Register.jsx';
import Footer from './components/Footer.jsx';

function App() {

    return (
        <>
            <Header />
            <Router>
                <Routes>
                    <Route path="/account/login" element={<Login />} />
                    <Route path="/account/Register" element={<Register />} />
                </Routes>
            </Router >
            <Footer />
        </>
    );
}

export default App;