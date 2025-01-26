import { HashRouter, Route, Routes } from 'react-router';
import Home from './pages/Home';
import RoutesPageMapLibre from './pages/map/RoutesPage-MapLibre';
import MainLayout from './components/layouts/MainLayout';
import MapLayout from './components/layouts/MapLayout';

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
            <Route index element={<RoutesPageMapLibre />} />
            <Route path="maplibre" element={<RoutesPageMapLibre />} />
          </Route>
        </Route>
      </Routes>
    </HashRouter>
  );
}

export default App;
