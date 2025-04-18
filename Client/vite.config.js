import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import tailwindcss from 'tailwindcss';

export default defineConfig({
    plugins: [plugin(), tailwindcss()],
    server: {
        host: '0.0.0.0',
        port: 5173,
        allowedHosts: [
            'healthsync-client.up.railway.app',
            'localhost',
            '0.0.0.0'
        ]
    }
})
