import {NavLink} from "react-router";

function Home() {
    return (
        <div className="home-container">
            <h1 className="home-title">Welcome to MyApp</h1>
            <p className="home-description">
                Discover the best routes and interactive maps to make your journey easier and more efficient. Start exploring today!
            </p>
            <div className="home-actions">
                <NavLink to={"/map"}>
                    <button className="home-button" >
                        Explore the Map
                    </button>
                </NavLink>

                <button className="home-button secondary" onClick={() => alert('Coming soon!')}>
                    Learn More
                </button>
            </div>
        </div>
    );
}

export default Home;
