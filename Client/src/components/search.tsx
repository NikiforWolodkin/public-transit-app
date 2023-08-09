import { useState } from "react";
import { TbSearch } from "react-icons/tb";
import { CgClose } from "react-icons/cg";

interface ISearchProps {
    selectedBusStop: string
    setSelected: any
}

const Search: React.FC<ISearchProps> = ({ selectedBusStop, setSelected }) => {
    const [text, setText] = useState<string>("");

    return (
        <>
            <div className="flex items-center absolute ml-[4.5rem] mt-2 w-80 h-12 z-[9999] rounded-lg bg-white shadow-lg shadow-gray-500/30">
                <input 
                    className="w-full outline-none ml-4 font-semibold placeholder:text-gray-500"
                    placeholder="Search..."
                    value={text}
                    onChange={ e => setText(e.target.value) }
                />
                <div className="text-2xl text-gray-500 cursor-pointer ml-4">
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
        </>
    );
};

export default Search;