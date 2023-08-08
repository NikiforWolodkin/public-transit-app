import { useState } from "react";
import { TbSearch } from "react-icons/tb";

const Search: React.FC = () => {
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
                <div className="text-2xl text-gray-500 cursor-pointer mx-4">
                    <TbSearch />
                </div>
            </div>
        </>
    );
};

export default Search;