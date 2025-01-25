import { useState } from 'react'
import { NavLink } from 'react-router'

function NavBar() {
  const [isMenuOpen, setIsMenuOpen] = useState(false)

  return (
    <nav className="bg-white shadow-md p-4">
      <div className="container mx-auto flex items-center justify-between">
        <h1 className="text-xl font-bold text-gray-500">
          Urban<span className="text-green-500">Watch</span>
        </h1>

        {/* Hamburger button for mobile */}
        <button
          onClick={() => setIsMenuOpen(!isMenuOpen)}
          className="lg:hidden block text-gray-600 hover:text-gray-800"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-6 w-6"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="M4 6h16M4 12h16m-7 6h7"
            />
          </svg>
        </button>

        {/* Menu items */}
        <ul
          className={`lg:flex lg:space-x-6 lg:items-center ${
            isMenuOpen ? 'block' : 'hidden'
          } flex-col lg:flex-row absolute lg:static top-16 left-0 w-full lg:w-auto bg-white lg:bg-transparent shadow-lg lg:shadow-none z-10`}
        >
          <li className="lg:my-0 my-2">
            <NavLink
              to="/"
              className={({ isActive }) =>
                isActive
                  ? 'text-blue-500 font-semibold'
                  : 'text-gray-600 hover:text-gray-800'
              }
            >
              Home
            </NavLink>
          </li>
          <li className="lg:my-0 my-2">
            <NavLink
              to="/about"
              className={({ isActive }) =>
                isActive
                  ? 'text-blue-500 font-semibold'
                  : 'text-gray-600 hover:text-gray-800'
              }
            >
              About
            </NavLink>
          </li>
          <li className="lg:my-0 my-2">
            <NavLink
              to="/contact"
              className={({ isActive }) =>
                isActive
                  ? 'text-blue-500 font-semibold'
                  : 'text-gray-600 hover:text-gray-800'
              }
            >
              Contact
            </NavLink>
          </li>
        </ul>
      </div>
    </nav>
  )
}

export default NavBar
