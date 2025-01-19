import { NavLink } from 'react-router'

function Home() {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100 p-4">
      <h1 className="text-4xl font-bold text-gray-800 mb-4">
        Welcome to MyApp
      </h1>
      <p className="text-lg text-gray-600 text-center max-w-md mb-6">
        Discover the best routes and interactive maps to make your journey
        easier and more efficient. Start exploring today!
      </p>
      <div className="flex space-x-4">
        <NavLink to={'/map/routes'}>
          <button className="px-6 py-2 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600">
            Explore the Map
          </button>
        </NavLink>

        <button
          className="px-6 py-2 bg-gray-200 text-gray-800 font-semibold rounded-lg hover:bg-gray-300"
          onClick={() => alert('Coming soon!')}
        >
          Learn More
        </button>
      </div>
    </div>
  )
}

export default Home
