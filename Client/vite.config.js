import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import tailwindcss from 'tailwindcss';

export default defineConfig({
    plugins: [plugin(), tailwindcss()],
    build: {
        outDir: 'dist',
    },
    base: '/'
})
