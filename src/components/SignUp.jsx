import React from "react";

function SignUp() {

const buttonStyle = "border-[2px] rounded-[10px] border-white px-[25px] py-[7px]";
  return (
    <div className="buttons flex">
      <button className={` text-white  mr-[35px] hover:bg-gradient-to-bl from-[#563000] to-[#c9a120]  ` + buttonStyle}>
         Log in
      </button>
      <button className={buttonStyle+` text-black  bg-gradient-to-bl from-[#8c4d00] to-[#ffc400]`}>
         Sign up</button>
      </div>
    )
}

export default SignUp