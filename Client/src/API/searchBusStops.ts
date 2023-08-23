import BusStop from "../models/busStop";

const searchBusStops = async (busStopName: string): Promise<BusStop[]> => {
    const response = await fetch(`http://localhost:5000/api/bus-stops/search?busStopName=${busStopName}`, {
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

export default searchBusStops;