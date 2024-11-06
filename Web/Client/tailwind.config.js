﻿/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
    theme: {
        extend: {
            colors: {
                maincolor: '#01bfa5'
            },
            backgroundImage: {
                'doctors-img': "url('./assets/doctors.png')"
            },
            screens: {
                sm: { min: '0px', max: '500px' },
                md: { min: '501px', max: '900px' },
                lg: { min: '901px', max: '1300px' },
            },
            spacing: {
                'doctorCardSm': '70rem',
            },
            transitionProperty: {
                'opacity-transform': 'opacity, transform',
            },
            transformOrigin: {
                center: 'center',
            },
            textUnderlineOffset: {
                5: '12px',
            },
            animation: {
                grow: 'grow 1.2s ease-in-out infinite',
                'bounce-left-right': 'bounceLeftRight 0.2s ease-in-out 3'
            },
            keyframes: {
                grow: {
                    '0%, 100%': {
                        transform: 'scale(1)',
                        opacity: '0.8',
                    },
                    '50%': {
                        transform: 'scale(1.5)',
                        opacity: '1',
                    },
                },
                bounceLeftRight: {
                    '0%, 100%': { transform: 'translateX(0)' }, 
                    '50%': { transform: 'translateX(3%)' }
                }
            }
        }
    },
    plugins: [require('tailwind-scrollbar')({ nocompatible: true, preferredStrategy: 'pseudoelements' })],
}

