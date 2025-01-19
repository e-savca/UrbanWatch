import Footer from '../components/Footer'
import Header from '../components/Header'
import PropTypes from 'prop-types'

MainLayout.propTypes = {
  children: PropTypes.element,
}

function MainLayout({ children }) {
  return (
    <>
      <Header />
      {children}
      <Footer />
    </>
  )
}

export default MainLayout
