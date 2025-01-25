import { createRoot } from 'react-dom/client';

/**
 * Renders a React component to an HTML element and returns it.
 * @param {JSX.Element} Component - The React component to render.
 * @returns {HTMLElement} The resulting HTML element containing the rendered component.
 */
export const renderComponentToElement = (
  Component: JSX.Element
): HTMLElement => {
  const container = document.createElement('div'); // Create a container div
  createRoot(container).render(Component); // Render React component inside the div
  return container; // Return the rendered HTML element
};

export default renderComponentToElement;
