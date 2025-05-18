const API_URL = import.meta.env.VITE_API_URL;

export async function getCars() {
    const res = await fetch(`${API_URL}/cars`);
    return res.json();
}

export async function getLocations() {
    const res = await fetch(`${API_URL}/locations`);
    return res.json();
}

export async function createReservation(data: ReservationRequest) {
    const res = await fetch(`${API_URL}/reservations`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    });

    if (!res.ok) {
        const text = await res.text();
        throw new Error(text);
    }

    return res.json();
}
export interface ReservationRequest {
    carId: number;
    pickupLocationId: number;
    returnLocationId: number;
    startDate: string;
    endDate: string;
}

export interface Reservation {
    id: number;
    car: { model: string };
    pickupLocation: { name: string };
    returnLocation: { name: string };
    startDate: string;
    endDate: string;
    totalCost: number;
}

export async function getReservations(): Promise<Reservation[]> {
    const res = await fetch(`${API_URL}/reservations`);
    if (!res.ok) throw new Error('Failed to fetch reservations');
    return res.json();
}
