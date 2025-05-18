import { useEffect, useState } from 'react';
import { getReservations, Reservation } from '../api/reservations';
import * as React from 'react';

const AdminPage = () => {
    const [reservations, setReservations] = useState<Reservation[]>([]);
    const [error, setError] = useState('');

    useEffect(() => {
        getReservations()
            .then(setReservations)
            .catch((err) => setError(err.message));
    }, []);

    return (
        <div style={{ padding: '2rem' }}>
            <h1>All Reservations</h1>

            {error && <p style={{ color: 'red' }}>{error}</p>}
            {reservations.length === 0 && !error && <p>No reservations found.</p>}

            <ul>
                {reservations.map((r) => (
                    <li key={r.id} style={{ marginBottom: '1rem' }}>
                        <strong>{r.car.model}</strong> – €{r.totalCost}<br />
                        {r.pickupLocation.name} → {r.returnLocation.name}<br />
                        {r.startDate} → {r.endDate}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default AdminPage;
