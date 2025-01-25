// .eslintrc.js
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
  rules: {
    // Add any project-specific rules or overrides here
  },
};
