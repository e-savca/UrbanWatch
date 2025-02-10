import React from 'react';
import { Header, Footer } from '../index';

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
