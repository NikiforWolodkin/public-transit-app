const getBusStops = async () => {
    const response = await fetch("http://localhost:5000/api/bus-stops", {
        method: 'GET',
        headers: {'Content-Type': 'application/json'},
        cache: 'no-store'
    });

    if (!response.ok) {
        const result = await response.text();
        console.log(result);
        throw new Error(response.status.toString());
    }

    const result = await response.json();

    return result;
}

export default getBusStops;