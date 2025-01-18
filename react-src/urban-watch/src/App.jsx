import Footer from './components/Footer'
import { BrowserRouter, Route, Routes } from 'react-router'
import Home from './pages/Home.jsx'
import RoutesPage from './pages/RoutesPage.jsx'
import Header from './components/Header.jsx'

function App() {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route index element={<Home />} />
        <Route path="/" element={<Home />} />
        <Route path="/map" element={<RoutesPage />} />
      </Routes>
      <Footer />
    </BrowserRouter>
  )
}

export default App
