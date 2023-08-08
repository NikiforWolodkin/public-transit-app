const getImageNames = async (busStopId: string): Promise<string[]> => {
    const response = await fetch(`http://localhost:5000/api/images/${busStopId}`, {
        method: 'GET',
        headers: {'Content-Type': 'application/json'},
        cache: 'no-store'
    });

    if (!response.ok) {
        const result = await response.text();
        console.error(result);
        throw new Error(response.status.toString());
    }

    const result = await response.json();

    return result;
}

export default getImageNames;