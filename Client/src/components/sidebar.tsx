import { TbBusStop, TbRoute, TbStar } from 'react-icons/tb';

const Sidebar: React.FC = () => {
    return (
        <>
            <div className='flex flex-col items-center absolute w-16 h-screen z-[9999] bg-white shadow-lg shadow-gray-500 font-bold text-gray-500'>
                <div className='flex flex-col items-center mt-4 cursor-pointer'>
                    <div className='text-3xl'>
                        <TbRoute />
                    </div>
                    <div className='mt-1 text-xs'>
                        Routes
                    </div>
                </div>
                <div className='flex flex-col items-center mt-4 cursor-pointer'>
                    <div className='text-3xl'>
                        <TbBusStop />
                    </div>
                    <div className='mt-1 text-xs'>
                        Stops
                    </div>
                </div>
                <div className='flex flex-col items-center mt-4 cursor-pointer'>
                    <div className='text-3xl'>
                        <TbStar />
                    </div>
                    <div className='mt-1 text-xs'>
                        Saved
                    </div>
                </div>
            </div>
        </>
    );
};

export default Sidebar;