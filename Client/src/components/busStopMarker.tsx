import BusStop from "../models/busStop";
import { Marker } from 'react-leaflet';
import { Icon } from 'leaflet';

interface IBusStopMarkerProps {
    busStop: BusStop,
    isSelected: boolean,
    setSelected: any
}

const selectedIcon = new Icon({
    iconUrl: '/marker.png',
    iconSize: [30, 30],
    iconAnchor: [15, 27]
});
const busStopIcon = new Icon({
    iconUrl: '/bus-stop.png',
    iconSize: [20, 20]
});
const depoIcon = new Icon({
    iconUrl: '/depo.png',
    iconSize: [30, 30]
});

const BusStopMarker: React.FC<IBusStopMarkerProps> = ({ busStop, isSelected, setSelected }) => {
    return (
        <>
            <Marker 
                position={[busStop.longitude, busStop.latitude]}
                icon={isSelected === true ? selectedIcon : (busStop.type === 0 ? busStopIcon : depoIcon)}
                eventHandlers={{
                    click: (e) => setSelected(busStop.id)
                }}
            />
        </>
    );
};

export default BusStopMarker;