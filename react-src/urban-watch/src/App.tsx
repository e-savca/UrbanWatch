import { HashRouter, Route, Routes } from 'react-router';
import Home from './pages/Home';
import MainLayout from './components/layouts/MainLayout';
import MapPage from './pages/MapPage';

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
        <Route path="map" element={<MapPage />} />
      </Routes>
    </HashRouter>
  );
}

export default App;
