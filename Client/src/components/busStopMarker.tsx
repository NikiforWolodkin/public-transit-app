import BusStop from "../models/busStop";
import { Marker, Popup } from 'react-leaflet';
import { Icon } from 'leaflet';

interface IBusStopMarkerProps {
    busStop: BusStop
}

const selectedIcon = new Icon({
    iconUrl: '/marker.png',
    iconSize: [30, 30]
});
const busStopIcon = new Icon({
    iconUrl: '/bus-stop.png',
    iconSize: [20, 20]
});
const depoIcon = new Icon({
    iconUrl: '/depo.png',
    iconSize: [30, 30]
});


const BusStopMarker: React.FC<IBusStopMarkerProps> = ({ busStop }) => {
    return (
        <>
            <Marker 
                position={[busStop.longitude, busStop.latitude]}
                icon={busStop.type === 0 ? busStopIcon : depoIcon}
            >
                <Popup>{busStop.name}</Popup>
            </Marker>
        </>
    );
};

export default BusStopMarker;