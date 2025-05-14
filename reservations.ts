const API_URL = 'https://localhost:5001/api';

export async function getCars() {
    const res = await fetch(`${API_URL}/cars`);
    return res.json();
}

export async function getLocations() {
    const res = await fetch(`${API_URL}/locations`);
    return res.json();
}

export async function createReservation(data: any) {
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
