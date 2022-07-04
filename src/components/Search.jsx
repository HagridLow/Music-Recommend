import { React, useState } from "react";
import { motion } from "framer-motion";


function Search() {
  const [elementIsVisible, setElementIsVisible] = useState(false);

{/* Motion Setting */}
  const aa = {
    true: {
      left: "-12rem",
    },
    false: {
      left: "-17rem",
    },
  };

  {/* Left side */}
  return (
    <div className="search relative h-[65rem] px-[5rem] bg-[#000000] pt-[22rem] pb-[10rem] mt-[-15rem] z-[1] flex items-center justify-between rounded-b-[5rem]">
      
      <div className="left flex-1">
      <motion.img
          variants={aa}
          animate={`${elementIsVisible}`}
          transition={{
            duration: 1,
            type: "ease-in",
          }}
          src={require("../img/nirvana.jpg")}
          alt=""
          className="absolute top-[21rem] right-[-2rem] h-[44rem]"
        />
      </div>

      {/* Right side */}
      <div className="right flex items-start flex-col justify-start flex-1 h-[100%] pt-[9rem]">

        {/* Search */}
        <div className="searchbar flex justify-start w-[100%]">
          <input
            type="text"
            placeholder="Enter the Song or Artist"
            className="flex-[19] outline-none bg-[#020917] rounded-xl p-3 h-[3rem]"
          />

          {/* SearchIcon */}
          <div className="searchIcon flex flex-1 items-center rounded-xl ml-4 bg-gradient-to-bl from-[#683900] to-[#ffc400] p-4 h-[3rem]">
            <img
              
              src={require("../img/search.png")}
              alt=""
              className="w-[1.5rem]"
            />
          </div>
        </div>
  
       < br/>< br/>

        {/* Music Search Details */}
        <div className="detail flex flex-col mt-5 text-4xl place-self-center">
          <span className="place-self-center">Search Music by</span>
          <span>
            <b>Name or Song / Album / Artist</b>
          </span>
          <span className="text-sm mt-3 text-[#4D586A] place-self-center text-center">

            Find the Adequate band or artist and search them on our App. < br/> 
            Select the Ep / Single / Album or Track you wish to rate < br/> 
            Check up on your list and view your ratings< br/> 
            
          </span>
        </div>     
      </div>
    </div>
  );
}

export default Search;
