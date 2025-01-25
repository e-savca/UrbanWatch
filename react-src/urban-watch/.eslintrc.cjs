module.exports = {
  extends: [
    'airbnb',
    'airbnb/hooks',
    'plugin:@typescript-eslint/recommended',
    'plugin:prettier/recommended',
  ],
  parser: '@typescript-eslint/parser',
  parserOptions: {
    ecmaVersion: 2020,
    sourceType: 'module',
  },
  plugins: ['react', '@typescript-eslint', 'prettier'],
  env: {
    browser: true,
    es2020: true,
  },
  settings: {
    'import/resolver': {
      node: {
        extensions: ['.js', '.jsx', '.ts', '.tsx'],
      },
    },
  },
  rules: {
    // Enable Prettier as an error to enforce code formatting
    'prettier/prettier': ['error'],

    // Turn off brace-style rule since it's handled by Prettier
    'brace-style': 'off',

    // Allow JSX syntax in .tsx and .jsx files only
    'react/jsx-filename-extension': ['error', { extensions: ['.tsx', '.jsx'] }],

    // Disable the requirement to import React in scope (React 17+ with JSX transform)
    'react/react-in-jsx-scope': 'off',

    // Ignore file extensions when importing modules
    'import/extensions': [
      'error', // Enforce errors for incorrect extensions
      'ignorePackages', // Ignore node_modules and package imports
      {
        js: 'never', // Don't require .js extensions
        jsx: 'never', // Don't require .jsx extensions
        ts: 'never', // Don't require .ts extensions
        tsx: 'never', // Don't require .tsx extensions
      },
    ],

    // Example of additional commonly used rules
    'no-console': ['warn', { allow: ['warn', 'error'] }], // Allow only console.warn and console.error
    'react/prop-types': 'off', // Turn off prop-types rule for TypeScript usage
    'arrow-body-style': ['error', 'as-needed'], // Enforce concise arrow functions when possible
  },
};
