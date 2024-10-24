const url = 'https://localhost:7080/doctors';

export const getAllDoctors = async (values) => {
    try {
        const response =
            await fetch(`${url}/all`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values)
            });

        if (!response.ok) {
            throw new Error('Failed to fetch doctors');
        }

        return await response.json();
    } catch (error) {
        console.error(error.message);
        return [];
    }
};

export const getReviews = async (values) => {
    try {
        const response =
            await fetch(`${url}/getReviews`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(values)
            });

        if (!response.ok) {
            throw new Error('Failed to fetch reviews');
        }

        return await response.json();
    } catch (error) {
        console.error(error.message);
        return [];
    }
};

export const addReview = async (values, jwtToken) => {
    try {
        const response =
            await fetch(`${url}/addReview`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${jwtToken}`
                },
                body: JSON.stringify(values),
                credentials: 'include'
            });
        
        if (!response.ok) {
            const errorData = await response.json();
            let errorMessage = 'Failed to add review';

            if (Array.isArray(errorData)) {
                errorMessage = errorData.join('\n');
            }

            throw new Error(errorMessage);
        }
    } catch (error) {
        console.error(error.message);
    }
};

export const getSpecialties = async () => {
    try {
        const response =
            await fetch(`${url}/getSpecialties`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

        if (!response.ok) {
            throw new Error('Failed to fetch specialties');
        }

        return await response.json();
    } catch (error) {
        console.error(error.message);
    }
};