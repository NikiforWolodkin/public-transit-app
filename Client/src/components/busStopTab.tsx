import { useEffect, useState } from "react";
import { TbBuilding, TbBus } from "react-icons/tb";
import { FaMapMarkerAlt } from "react-icons/fa";
import { FaCircleInfo } from "react-icons/fa6";
import getImageNames from "../API/getImageNames";
import BusStop from "../models/busStop";
import getBusStopRoutes from "../API/getBusStopRoutes";
import Route from "../models/route";

interface IBusStopTabProps {
    selectedBusStopId: string
    selectedBusStop: BusStop
}

const BusStopTab: React.FC<IBusStopTabProps> = ({ selectedBusStopId, selectedBusStop }) => {
    const [routes, setRoutes] = useState<Route[] | null>(null);
    const [imageURL, setImageURL] = useState<string>("none");

    useEffect(() => {
        const fetchImages = async () => {
            try {
                const data = await getImageNames(selectedBusStopId);
                if (data.length >= 1) {
                    setImageURL(`http://localhost:5000/api/storage/${data[0]}`);
                } else {
                    setImageURL("placeholder.webp");
                }
            } catch (err) {
                console.error(err);
            }
        };

        const fetchRoutes = async () => {
            try {
                const data = await getBusStopRoutes(selectedBusStopId);
                setRoutes(data);
            } catch (err) {
                console.error(err);
            }
        };
    
        if (selectedBusStopId !== "none") {
            setRoutes(null);
            setImageURL("none");
            fetchRoutes();
            fetchImages();
        }
    }, [selectedBusStopId]);

    if (selectedBusStopId === "none") {
        return null;
    }

    return (
        <>
            <div className="absolute z-[8999] ml-16 w-[21rem] h-screen bg-white shadow-lg shadow-gray-500   ">
                {imageURL !== "none" ? 
                    <img 
                        className="w-full aspect-[4/3] object-cover"
                        src={imageURL} 
                    />
                    : <div className="w-full aspect-[4/3] flex justify-center items-center text-bold text-2xl">
                        Loading...
                    </div>
                }

                <div className="w-full border-t-2"></div>
                
                <div className="px-4 py-3">
                    <div className="text-2xl font-semibold">
                        {selectedBusStop.name}
                    </div>
                    <div className="mt-2 text-xl font-semibold">
                        <div className="flex items-center text-base">
                            <div className="mr-2 text-xl">
                                <TbBuilding />
                            </div>
                            {selectedBusStop.type === 0 ? "Bus stop" : "Depo"} 
                        </div>
                    </div>
                    <div className="mt-2 text-xl font-semibold">
                        <div className="flex items-start text-base">
                            <div className="mr-2 text-xl mt-0.5">
                                <FaMapMarkerAlt />
                            </div>
                            {selectedBusStop.longitude}, {selectedBusStop.latitude}
                        </div>
                    </div>
                    <div className="mt-2 text-xl font-semibold">
                        <div className="flex items-start text-base">
                            <div className="mr-2 text-xl mt-0.5">
                                <FaCircleInfo />
                            </div>
                            {selectedBusStop.id}
                        </div>
                    </div>
                </div>

                <div className="w-full border-t-2"></div>

                <div className="px-4 py-3">
                    <div className="text-2xl font-semibold">
                        Routes
                    </div>
                    <div>
                        {routes !== null ?
                            <>
                                {routes.map(route => (
                                    <div 
                                        className="cursor-pointer flex mt-2 text-large font-semibold items-center"
                                        key={route.id}
                                    >
                                        <div className="text-2xl mr-2">
                                            <TbBus />
                                        </div>
                                        {route.name}
                                    </div>
                                ))}
                            </>
                            : <div className="text-xl font-semibold">Loading...</div>
                        }
                    </div>
                </div>
            </div>
        </>
    );
};

export default BusStopTab;