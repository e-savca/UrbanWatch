module.exports = {
  extends: ['airbnb', 'airbnb/hooks', 'plugin:@typescript-eslint/recommended'],
  parser: '@typescript-eslint/parser',
  parserOptions: {
    ecmaVersion: 2020,
    sourceType: 'module',
  },
  plugins: ['react', '@typescript-eslint'],
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
    'brace-style': 'off',

    // Allow JSX in .tsx files
    'react/jsx-filename-extension': ['error', { extensions: ['.tsx', '.jsx'] }],

    // Disable React in scope rule if using React 17+
    'react/react-in-jsx-scope': 'off',

    // Disable the requirement for specific file extensions
    'import/extensions': [
      'error',
      'ignorePackages',
      {
        js: 'never',
        jsx: 'never',
        ts: 'never',
        tsx: 'never',
      },
    ],
  },
};
