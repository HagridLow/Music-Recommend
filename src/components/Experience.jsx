import React from "react";
import Feature from "./Feature";

function Experience() {
  return (
    <div className="experience flex flex-col items-center justify-center  bg-[#1f1f1f] h-[60rem]  mt-[-10rem] relative z-[2] rounded-b-[5rem]">
      {/* Yellow Trail Pallete */}
      <img src={require("../img/zamn.jpg")} alt="" className=" w-[1550px] pt-[70px]" />
      
      {/* Heading */}
      <div className="headline mt-7 flex flex-col items-center text-[2rem]">
        <span>Rate the Music you love and Change your</span>
        <span>
          <b>Music Experience</b>
        </span>
      </div>

      {/* Featuing  */}
      <div className="feature flex items-center justify-around mt-[6rem] w-[100%]">
        <Feature icon="Yellow-Mic" title="Rate Solo Projects" />
        <Feature icon="music icon" title="Rate Daily Music" />
        <Feature icon="Group 4" title="Rate Discography's" />
      </div>
    </div>
  );
}

export default Experience;
