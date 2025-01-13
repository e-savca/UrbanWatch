import Footer from './components/Footer'
import {BrowserRouter, Route, Routes} from "react-router";
import Home from "./pages/Home.jsx";
import NavBar from "./components/NavBar.jsx";
import RoutesPage from "./pages/RoutesPage.jsx";

function App() {
  return (
      <BrowserRouter>
          <NavBar />
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
