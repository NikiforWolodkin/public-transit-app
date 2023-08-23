import Route from "../models/route";

const getBusStopRoutes = async (busStopId: string): Promise<Route[]> => {
    const response = await fetch(`http://localhost:5000/api/routes/search?busStopId=${busStopId}`, {
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

export default getBusStopRoutes;