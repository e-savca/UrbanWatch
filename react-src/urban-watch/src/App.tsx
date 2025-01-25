import { HashRouter, Route, Routes } from 'react-router'
import Home from './pages/Home.jsx'
import RoutesPageLeaflet from './pages/map/RoutesPage-Leaflet/index.jsx'
import RoutesPageMapLibre from './pages/map/RoutesPage-MapLibre/index'
import MainLayout from './layouts/MainLayout.jsx'
import MapLayout from './layouts/MapLayout'

function App() {
  return (
    <HashRouter>
      <Routes>
        <Route
          path="/"
          element={
            <MainLayout>
              <Home />
            </MainLayout>
          }
        ></Route>
        <Route path="map" element={<MapLayout />}>
          <Route path="routes">
            <Route index element={<RoutesPageLeaflet />} />
            <Route path="leaflet" element={<RoutesPageLeaflet />} />
            <Route path="maplibre" element={<RoutesPageMapLibre />} />
          </Route>
        </Route>
      </Routes>
    </HashRouter>
  )
}

export default App
