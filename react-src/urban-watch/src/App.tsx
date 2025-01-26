import { HashRouter, Route, Routes } from 'react-router';
import Home from './pages/Home';
import RoutesPageLeaflet from './pages/map/RoutesPage-Leaflet';
import RoutesPageMapLibre from './pages/map/RoutesPage-MapLibre';
import MainLayout from './layouts/MainLayout';
import MapLayout from './layouts/MapLayout';

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
        />
        <Route path="map" element={<MapLayout />}>
          <Route path="routes">
            <Route index element={<RoutesPageLeaflet />} />
            <Route path="leaflet" element={<RoutesPageLeaflet />} />
            <Route path="maplibre" element={<RoutesPageMapLibre />} />
          </Route>
        </Route>
      </Routes>
    </HashRouter>
  );
}

export default App;
