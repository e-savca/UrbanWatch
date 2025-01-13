import { NavLink } from "react-router";

function NavBar() {
    return (
        <nav className="navbar">
            <div className="navbar-container">
                <h1 className="navbar-logo">MyApp</h1>
                <ul className="navbar-list">
                    <li className="navbar-item">
                        <NavLink className="navbar-link" to={"/"}>
                            Home
                        </NavLink>
                    </li>
                    <li className="navbar-item">
                        <NavLink className="navbar-link" to={"/map"}>
                            Map
                        </NavLink>
                    </li>
                </ul>
            </div>
        </nav>
    );
}

export default NavBar;
