import { useEffect } from "react";
import { useMap } from "react-leaflet"

interface IMapFocuserComponent {
    coordinates: number[] | null
}

const MapFocuserComponent: React.FC<IMapFocuserComponent> = ({ coordinates }) => {
    const map = useMap();

    useEffect(() => {
        if (coordinates !== null) {
            map.setView(coordinates, map.getZoom(), {
                animate: true
            });
        }
    }, [coordinates]);

    return null;
};

export default MapFocuserComponent;