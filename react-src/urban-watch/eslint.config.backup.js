// eslint.config.js
import { FlatCompat } from '@eslint/eslintrc';
import globals from 'globals';
import tseslint from 'typescript-eslint';
import pluginReact from 'eslint-plugin-react';

// Initialize FlatCompat
const compat = new FlatCompat();

export default [
  {
    files: ['**/*.{js,mjs,cjs,ts,jsx,tsx}'],
    languageOptions: {
      globals: globals.browser,
    },
  },
  // Spread the configurations returned by compat.extends()
  ...compat.extends('airbnb'),
  ...compat.extends('airbnb/hooks'),
  // Add TypeScript and React plugin configurations as needed
  tseslint.configs.recommended,
  pluginReact.configs.recommended,
];
