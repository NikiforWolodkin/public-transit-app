import Route from "../models/route";

const searchRoutes = async (routeName: string): Promise<Route[]> => {
    const response = await fetch(`http://localhost:5000/api/routes/search?routeName=${routeName}`, {
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

export default searchRoutes;