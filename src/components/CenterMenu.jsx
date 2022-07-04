import React from 'react'

function CenterMenu() {
    const liStyle = "mr-[4rem] hover:cursor-pointer"
  return (
    <div className="menu flex">
        <ul className='flex w-[100%] justify-between'>
            <li className={liStyle}>Home</li>
            <li className={liStyle}>About</li>
            <li className={liStyle}>Latest Reviews</li>
            <li className={liStyle}>Charts</li>
        </ul>
    </div>
    )
}

export default CenterMenu