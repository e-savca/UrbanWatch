import { BrowserRouter, Route, Routes } from 'react-router'
import Home from './pages/Home.jsx'
import RoutesPage from './pages/RoutesPage'
import MainLayout from './layouts/MainLayout.jsx'
import MapLayout from './layouts/MapLayout'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={
            <MainLayout>
              <Home />
            </MainLayout>
          }
        ></Route>
        <Route path="/map" element={<MapLayout />}>
          <Route index element={<RoutesPage />} />
          <Route path="routes" element={<RoutesPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  )
}

export default App
