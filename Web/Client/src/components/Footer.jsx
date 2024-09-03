import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSquareFacebook, faInstagram, faTiktok } from '@fortawesome/free-brands-svg-icons';

function Footer() {
    return (
        <footer className="bg-maincolor rounded-t-xl p-8 text-white sm:text-center">
            <div className="flex flex-row justify-evenly sm:flex-col sm:space-y-6">
                <div>
                    <p className="font-bold text-xl mb-3 underline underline-offset-4">Contact us</p>
                    <ul>
                        <li>+359888888888</li>
                        <li>Sofia, str. Strahil Voivoda 23</li>
                        <li>info@healthsync.com</li>
                    </ul>
                </div>
                <div>
                    <p className="font-bold text-xl mb-3 text-center underline underline-offset-4">Socials</p>
                    <ul className="flex felx-row space-x-4 sm:justify-center">
                        <li><FontAwesomeIcon icon={faSquareFacebook} className="text-white text-3xl" /></li>
                        <li><FontAwesomeIcon icon={faInstagram} className="text-white text-3xl" /></li>
                        <li><FontAwesomeIcon icon={faTiktok} className="text-white text-3xl" /></li>
                    </ul>
                </div>
            </div>
            <div className="text-center mt-8">
                &copy; 2024 HealthSync. All rights reserved.
            </div>
        </footer>
    );
}

export default Footer;