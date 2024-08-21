/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
    theme: {
        extend: {
            colors: {
                maincolor: '#01bfa5',
            },
            backgroundImage: {
                'doctors-img': "url('./assets/doctors.png')"
            },
            screens: {
                sm: { min: '0px', max: '600px' },
                md: { min: '601px', max: '1280px' }
            },
            transitionProperty: {
                'opacity-transform': 'opacity, transform',
            },
            transformOrigin: {
                center: 'center',
            }
        }
    },
    plugins: [],
}

