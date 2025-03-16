// Home.jsx
import { NavLink } from 'react-router';

function Home() {
  return (
    <div className="flex flex-col items-center bg-gray-100 p-4 h-full min-h-[80dvh] space-y-12">
      {/* Hero Section */}
      <section className="flex flex-col items-center justify-center text-center">
        <h1 className="text-5xl font-bold text-gray-800 mb-4">
          Welcome to UrbanWatch+
        </h1>
        <p className="text-lg text-gray-600 max-w-2xl mb-6">
          Discover the best routes and interactive maps to make your journey
          easier and more efficient. Start exploring today!
        </p>
        <div className="flex flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4">
          <NavLink to="/map">
            <button
              type="button"
              className="px-6 py-3 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600 transition-colors duration-300"
            >
              Explore the Map
            </button>
          </NavLink>

          <button
            type="button"
            className="px-6 py-3 bg-gray-200 text-gray-800 font-semibold rounded-lg hover:bg-gray-300 transition-colors duration-300"
            onClick={() => alert('Coming soon!')}
          >
            Learn More
          </button>
        </div>
      </section>

      {/* Features Section */}
      <section className="w-full max-w-6xl px-4">
        <h2 className="text-3xl font-semibold text-gray-800 mb-8 text-center">
          Key Features
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          {/* Feature 1 */}
          <div className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300">
            <div className="text-blue-500 mb-4">
              {/* Replace with an appropriate icon */}
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-12 w-12"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M3 7v10a2 2 0 002 2h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2z"
                />
              </svg>
            </div>
            <h3 className="text-xl font-semibold text-gray-700 mb-2">
              Interactive Maps
            </h3>
            <p className="text-gray-600">
              Navigate through detailed and interactive maps that provide
              real-time updates and insights.
            </p>
          </div>

          {/* Feature 2 */}
          <div className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300">
            <div className="text-blue-500 mb-4">
              {/* Replace with an appropriate icon */}
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-12 w-12"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M12 8c-2.21 0-4 1.79-4 4 0 .73.19 1.41.52 2.01A4.978 4.978 0 0112 18c1.31 0 2.5-.53 3.44-1.44A4.978 4.978 0 0116 12c0-2.21-1.79-4-4-4z"
                />
              </svg>
            </div>
            <h3 className="text-xl font-semibold text-gray-700 mb-2">
              Real-Time Traffic
            </h3>
            <p className="text-gray-600">
              Get up-to-the-minute traffic information to avoid delays and
              choose the best routes.
            </p>
          </div>

          {/* Feature 3 */}
          <div className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300">
            <div className="text-blue-500 mb-4">
              {/* Replace with an appropriate icon */}
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-12 w-12"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M9 5l7 7-7 7"
                />
              </svg>
            </div>
            <h3 className="text-xl font-semibold text-gray-700 mb-2">
              Route Planning
            </h3>
            <p className="text-gray-600">
              Plan your journeys efficiently with optimized routes tailored to
              your preferences.
            </p>
          </div>
        </div>
      </section>

      {/* Testimonials Section */}
      <section className="w-full max-w-6xl px-4">
        <h2 className="text-3xl font-semibold text-gray-800 mb-8 text-center">
          What Our Users Say
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {/* Testimonial 1 */}
          <div className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300">
            <p className="text-gray-600 mb-4">
              &quot;UrbanWatch+ has completely transformed the way I navigate
              the city. The real-time updates are a game-changer!&quot;
            </p>
            <div className="flex items-center">
              <img
                src="https://via.placeholder.com/48"
                alt="User Avatar"
                className="h-12 w-12 rounded-full mr-4"
              />
              <div>
                <p className="text-gray-800 font-semibold">Jane Doe</p>
                <p className="text-gray-500 text-sm">Travel Blogger</p>
              </div>
            </div>
          </div>

          {/* Testimonial 2 */}
          <div className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300">
            <p className="text-gray-600 mb-4">
              &quot;The interactive maps are incredibly detailed. I never get
              lost anymore!&quot;
            </p>
            <div className="flex items-center">
              <img
                src="https://via.placeholder.com/48"
                alt="User Avatar"
                className="h-12 w-12 rounded-full mr-4"
              />
              <div>
                <p className="text-gray-800 font-semibold">John Smith</p>
                <p className="text-gray-500 text-sm">Entrepreneur</p>
              </div>
            </div>
          </div>

          {/* Testimonial 3 */}
          <div className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300">
            <p className="text-gray-600 mb-4">
              &quot;With UrbanWatch+, planning my daily commute has never been
              easier. Highly recommend!&quot;
            </p>
            <div className="flex items-center">
              <img
                src="https://via.placeholder.com/48"
                alt="User Avatar"
                className="h-12 w-12 rounded-full mr-4"
              />
              <div>
                <p className="text-gray-800 font-semibold">Emily Clark</p>
                <p className="text-gray-500 text-sm">Software Engineer</p>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* CTA Section */}
      <section className="w-full max-w-4xl px-4 text-center">
        <h2 className="text-3xl font-semibold text-gray-800 mb-4">
          Ready to Start Your Journey?
        </h2>
        <p className="text-gray-600 mb-6">
          Join thousands of users who are making their travels smarter and more
          efficient with UrbanWatch+.
        </p>
        <NavLink to="/signup">
          <button
            type="button"
            className="px-8 py-3 bg-green-500 text-white font-semibold rounded-lg hover:bg-green-600 transition-colors duration-300"
          >
            Get Started
          </button>
        </NavLink>
      </section>
    </div>
  );
}

export default Home;
