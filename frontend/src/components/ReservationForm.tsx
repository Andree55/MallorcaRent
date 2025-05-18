import { useEffect, useState } from 'react';
import { createReservation, getCars, getLocations } from '../api/reservations';
import React from 'react';

interface Car {
    id: number;
    model: string;
    pricePerDay: number;
}

interface Location {
    id: number;
    name: string;
}

const ReservationForm = () => {
    const [cars, setCars] = useState<Car[]>([]);
    const [locations, setLocations] = useState<Location[]>([]);

    const [carId, setCarId] = useState<number>(0);
    const [pickupId, setPickupId] = useState<number>(0);
    const [returnId, setReturnId] = useState<number>(0);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');
    const [result, setResult] = useState<string>('');

    useEffect(() => {
        getCars().then(setCars);
        getLocations().then(setLocations);
    }, []);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (!carId || !pickupId || !returnId || !startDate || !endDate) {
            setResult('Please fill all fields.');
            return;
        }

        try {
            const response = await createReservation({
                carId,
                pickupLocationId: pickupId,
                returnLocationId: returnId,
                startDate,
                endDate,
            });

            setResult(`Reservation confirmed! Total cost: \u20AC${response.totalCost}`);

        } catch (err) {
            if (err instanceof Error) {
                setResult(`Error: ${err.message}`);
            } else {
                setResult('Unknown error occurred.');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Car Model:</label>
                <select value={carId} onChange={(e) => setCarId(Number(e.target.value))}>
                    <option value="">Select</option>
                    {cars.map((car) => (
                        <option key={car.id} value={car.id}>
                            {car.model}: &#8364;{car.pricePerDay}/day
                        </option>
                    ))}
                </select>
            </div>

            <div>
                <label>Pickup Location:</label>
                <select value={pickupId} onChange={(e) => setPickupId(Number(e.target.value))}>
                    <option value="">Select</option>
                    {locations.map((loc) => (
                        <option key={loc.id} value={loc.id}>{loc.name}</option>
                    ))}
                </select>
            </div>

            <div>
                <label>Return Location:</label>
                <select value={returnId} onChange={(e) => setReturnId(Number(e.target.value))}>
                    <option value="">Select</option>
                    {locations.map((loc) => (
                        <option key={loc.id} value={loc.id}>{loc.name}</option>
                    ))}
                </select>
            </div>

            <div>
                <label>Start Date:</label>
                <input type="date" value={startDate} onChange={(e) => setStartDate(e.target.value)} />
            </div>

            <div>
                <label>End Date:</label>
                <input type="date" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
            </div>

            <button type="submit">Reserve</button>

            <p>{result}</p>
        </form>
    );
};

export default ReservationForm;