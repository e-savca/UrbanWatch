import PropTypes from 'prop-types';
import Footer from '../components/Footer';
import Header from '../components/Header';
import React from 'react';

function MainLayout({ children }: { children: React.JSX.Element }) {
  return (
    <div className="flex flex-col min-h-dvh">
      <Header />
      <main className="grow">{children}</main>
      <Footer />
    </div>
  );
}

export default MainLayout;
