import { HashRouter, Route, Routes } from 'react-router';
import { Home, MapPage } from './pages';
import MainLayout from './components/layouts/MainLayout';

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
