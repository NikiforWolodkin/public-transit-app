import { MapContainer, TileLayer, Marker, Popup, ZoomControl } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import Sidebar from './components/sidebar';
import Search from './components/search';
import { useEffect, useState } from 'react';
import getBusStops from './API/getBusStops';
import BusStop from './models/busStop';
import BusStopMarker from './components/busStopMarker';

function App() {
  const [busStops, setBusStops] = useState<BusStop[]>([]);

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

  console.log(busStops);

  return (
    <>
      <Sidebar />
      <Search />

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
          <BusStopMarker busStop={stop} />
        ))}
      </MapContainer>
    </>
  )
}

export default App;
