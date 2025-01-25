import PropTypes from 'prop-types';
import Footer from '../components/Footer';
import Header from '../components/Header';

MainLayout.propTypes = {
  children: PropTypes.element,
};

function MainLayout({ children }) {
  return (
    <div className="flex flex-col min-h-dvh">
      <Header />
      <main className="grow">{children}</main>
      <Footer />
    </div>
  );
}

export default MainLayout;
