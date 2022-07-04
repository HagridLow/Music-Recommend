import { React, useState } from "react";
import DownloadAds from "./DownloadAds";
import VisibilitySensor from "react-visibility-sensor";
import { motion } from "framer-motion";


function Hero() {
  const buttonStyle = "border-[2px] rounded-[20px] border-white px-[84px] py-[3px]";
  const [elementIsVisible, setElementIsVisible] = useState(false);

   {/* Transition Settings */}
  const bg = {
    true: {
      left: "13.5rem",
    },
    false: {
      left: "30rem",
    },
  };
  const musicPlayer = {
    true: {
      left: "50px",
    },
    false: {
      left: "30px",
    },
  };
  const rect = {
    true: {
      left: "20rem",
    },
    false: {
      left: "38rem",
    },
  }
  const heart = {
    true: {
      left: "0.1rem",
    },
    false: {
      left: "1rem",
    },
  };
  return (
    <VisibilitySensor
      onChange={(isVisible) => setElementIsVisible(isVisible)}
      minTopValue={300}>

      <div className="wrapper bg-[#000000] flex items-center justify-between px-[5rem] rounded-b-[5rem] w-[100%] h-[40rem] relative z-[3]">

        {/* Left Side */}
        <div className="headings flex flex-col items-start justify-center h-[100%] text-[3rem]">
          <span className="place-self-center">Rate The</span>{" "}
          <span>
            <b className="place-self-center">Quality Music You Love!</b>
          </span>
          <span className="text-[15px] text-[#525D6E] place-self-center text-center">
            Search for your favorite artists and review their discography.
            <br />
            Create a list of your favorite Albums and share it with the people you love.
          </span>

          {/* download ads */}
          <div className=" place-self-center">
            <span className="text-[23px]">Start Your Experience Now!</span> 
             <br/>
            <button className={buttonStyle+` text-white 
               hover:bg-gradient-to-bl from-[#010100] to-[#654d00]`}>
               Start </button>
          </div>

        </div>
        
        {/* right side */}
        <div className="images relative w-[55%]">
          <motion.img
            variants={bg}
            animate={`${elementIsVisible}`}
            transition={{ duration: 4, type: "ease-out" }}
            src={require("../img/damna.jpg")}
            alt=""
            className="absolute top-[-14rem] left-[19rem]"
          />
         
         <motion.img
            variants={rect}
            animate={`${elementIsVisible}`}
            transition={{
              type: "ease-out",
              duration: 2,
            }}
            src={require("../img/Prodavnica_tajni.jpeg")}
            alt=""
            className="absolute left-[13rem] top-[-9rem] w-[350px] h-[325px]"
          />

          <motion.img
            variants={musicPlayer}
            animate={`${elementIsVisible}`}
            transition={{
              duration: 2,
              type: "ease-out",
            }}
            src={require("../img/TPAB.jpg")}
            alt=""
            className="absolute left-[35px] top-[-18rem] w-[400px] h-[375px] "
          />
        
          <motion.img
            variants={heart}
            animate={`${elementIsVisible}`}
            transition={{
              type: "ease-out",
              duration: 4,
            }}
            src={require("../img/Filosofem.jpg")}
            alt=""
            className="absolute left-[1px] top-[-2rem] w-[300px] h-[275px]"
          />
        </div>
      </div>
    </VisibilitySensor>
  );
}

export default Hero;
