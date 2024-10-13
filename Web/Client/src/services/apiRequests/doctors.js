const url = 'https://localhost:7080/doctors';

export const getAllDoctors = async (values) =>
    await fetch(`${url}/all`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(values)
    });