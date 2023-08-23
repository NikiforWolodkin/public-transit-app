import { MapContainer, TileLayer, ZoomControl } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import Sidebar from './components/sidebar';
import Search from './components/search';
import { useEffect, useState } from 'react';
import getBusStops from './API/getBusStops';
import BusStop from './models/busStop';
import BusStopMarker from './components/busStopMarker';
import BusStopTab from './components/busStopTab';
import MapFocuserComponent from './components/mapFocuserComponent';

function App() {
  const [busStops, setBusStops] = useState<BusStop[]>([]);
  const [selectedBusStopId, setSelectedBusStopId] = useState<string>("none");
  const [selectedBusStopCoordinates, setSelectedBusStopCoordinates] = useState<number[] | null>(null);

  useEffect(() => {
    const fetchBusStops = async () => {
      try {
        const data = await getBusStops();
        setBusStops(data);
      } catch (err) {
        console.error(err);
      }
    };

    fetchBusStops();
  }, []);

  return (
    <>
      <Sidebar 
        selectedBusStop={selectedBusStopId}
      />
      <Search 
        selectedBusStop={selectedBusStopId}
        setSelected={setSelectedBusStopId}
        setSelectedCoordinates={setSelectedBusStopCoordinates}
      />
      <BusStopTab
        selectedBusStopId={selectedBusStopId}
        selectedBusStop={busStops.filter(stop => stop.id === selectedBusStopId)?.[0]}
      />

      <MapContainer 
        center={[52.2297, 21.0122]}
        zoom={13}
        zoomControl={false}
      >
        <ZoomControl position='topright' />

        <TileLayer 
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />

        {busStops.map(stop => (
          <BusStopMarker 
            key={stop.id}
            busStop={stop}
            isSelected={stop.id === selectedBusStopId}
            setSelected={setSelectedBusStopId}
          />
        ))}

        <MapFocuserComponent 
          coordinates={selectedBusStopCoordinates}
        />
      </MapContainer>
    </>
  )
}

export default App;
