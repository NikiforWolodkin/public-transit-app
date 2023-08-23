import { useEffect, useRef, useState } from "react";
import { TbSearch } from "react-icons/tb";
import { CgClose } from "react-icons/cg";
import { TbBus, TbBuilding } from "react-icons/tb";
import Route from "../models/route";
import searchRoutes from "../API/searchRoutes";
import BusStop from "../models/busStop";
import searchBusStops from "../API/searchBusStops";
import { useMap } from "react-leaflet";

interface ISearchProps {
    selectedBusStop: string
    setSelected: any
    setSelectedCoordinates: any
}

const Search: React.FC<ISearchProps> = ({ selectedBusStop, setSelected, setSelectedCoordinates }) => {
    const inputRef = useRef(null);  
    const [text, setText] = useState<string>("");
    const [routes, setRoutes] = useState<Route[] | null>(null);
    const [busStops, setBusStops] = useState<BusStop[] | null>(null);

    useEffect(() => {
        const fetchRoutes = async () => {
            try {
                const data = await searchRoutes(text);
                setRoutes(data);
            } catch (err) {
                console.error(err);
            }
        };

        const fetchBusStops = async () => {
            try {
                const data = await searchBusStops(text);
                setBusStops(data);
            } catch (err) {
                console.error(err);
            }
        };
    
        if (text !== "") {
            setBusStops(null);
            setRoutes(null);
            fetchBusStops();
            fetchRoutes();
        }
    }, [text]);

    return (
        <>
            <div className="absolute ml-[4.5rem] mt-2 w-80 z-[9999] rounded-lg bg-white shadow-lg shadow-gray-500/30">
                <div className="h-12 flex items-center">
                    <input 
                        ref={inputRef}
                        className="w-full outline-none ml-4 font-semibold placeholder:text-gray-500"
                        placeholder="Search..."
                        value={text}
                        onChange={ e => setText(e.target.value) }
                    />
                    <div 
                        className="text-2xl text-gray-500 cursor-pointer ml-4"
                        onClick={ () => inputRef.current?.focus() }
                    >
                        <TbSearch />
                    </div>

                    {selectedBusStop !== "none" ? 
                        <>
                            <div className="border-r-2 h-5 rounded-full mx-2"></div>
                            <div 
                                className="text-2xl text-gray-500 cursor-pointer mr-4"
                                onClick={ () => setSelected("none") }
                            >
                                <CgClose />
                            </div>
                        </>
                        : <div className="w-4"></div>
                    }
                </div>

                {text !== "" ?
                    <>
                        <div className="w-full border-t"></div>
                            
                        <div className="px-4 py-2 font-semibold">
                            {routes !== null || busStops !== null ?
                                <>
                                    {(routes?.length !== 0 || busStops?.length !== 0) ?
                                        <>
                                            {routes?.map((route, index) => (
                                                <div 
                                                    key={route.id}
                                                    className={"cursor-pointer flex items-center " + 
                                                    ((index === routes.length - 1) && (busStops?.length === 0 || busStops === null) ? "" : "mb-1")}
                                                >
                                                    <div className="mr-2 text-xl text-gray-500">
                                                        <TbBus />
                                                    </div>    
                                                    {route.name}
                                                </div>
                                            ))}

                                            {busStops?.map((busStop, index) => (
                                                <div 
                                                    key={busStop.id}
                                                    className={"cursor-pointer flex items-center " + (index === busStops.length - 1 ? "" : "mb-1")}
                                                    onClick={ () => {
                                                        setSelected(busStop.id);
                                                        setSelectedCoordinates([busStop.longitude, busStop.latitude]);
                                                        setText("");
                                                    }}
                                                >
                                                    <div className="mr-2 text-xl text-gray-500">
                                                        <TbBuilding />
                                                    </div>    
                                                    {busStop.name}
                                                </div>
                                            ))}
                                        </>
                                        : <div>No matches found</div>
                                    }   
                                </>
                                : <div>Loading...</div>
                            }
                        </div>
                    </>
                    : null
                }
            </div>
        </>
    );
};

export default Search;