import React from "react";
import DownloadAds from "./DownloadAds";

function Download() {
  return (
    <div className="flex flex-col items-center justify-start px-[5rem] bg-[#1f1f1f] h-[48rem] pt-[18rem] mt-[-10rem] relative z-[0] rounded-b-[5rem]">
      
     
      {/* Header Information */}
      <div className="headline mt-7 flex flex-col items-center text-[2rem]">
        <span>Try Out The Best Music</span>
        <span>
          <b>App Now!</b>
        </span>
        <span className="text-[1rem] text-gray-400 px-[15rem] text-center mt-[1rem]">
          For any further notice, we will be at your display!
        </span>
      </div>
      
      {/* Ads for App */}
      <div className="mt-14">
        <DownloadAds />
      </div>
    </div>
  );
}

export default Download;
