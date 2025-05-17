import { Routes, Route } from 'react-router-dom';
import ReservationPage from './pages/ReservationPage';

function App() {
    return (
        <Routes>
            <Route path="/" element={<ReservationPage />} />
        </Routes>
    );
}

export default App;
