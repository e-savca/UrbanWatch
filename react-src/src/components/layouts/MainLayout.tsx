import React from 'react';
import Footer from '../Footer';
import Header from '../Header/Header';

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
