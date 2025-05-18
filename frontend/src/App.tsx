import { Routes, Route } from 'react-router-dom';
import ReservationPage from './pages/ReservationPage';
import AdminPage from './pages/AdminPage';

function App() {
    return (
        <Routes>
            <Route path="/" element={<ReservationPage />} />
            <Route path="/admin" element={<AdminPage />} />
        </Routes>
    );
}

export default App;
