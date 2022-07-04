import React from "react";
import CenterMenu from "./CenterMenu";
import SignUp from "./SignUp";

function Header() {

  return (
    <div className="header bg-[#000000] flex items-center justify-between px-[5rem] pt-[2.4rem] text-[0.8rem]">

      {/* App Logo */}
      <img
        src={require("../img/Logo.png")}
        alt=""
        className="logo  w-[42px] h-[42px]"
      />

      {/* Navigation Bar */}
      <CenterMenu />

      {/* Sign-up & Login */}
      <SignUp />

    </div>
  );
}

export default Header;
