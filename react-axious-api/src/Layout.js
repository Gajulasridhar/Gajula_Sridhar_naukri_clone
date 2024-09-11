import React from 'react';
import { BrowserRouter, Outlet, Route, Routes, Link } from 'react-router-dom';
import './Layout.css'; // Import the CSS file for Layout

const Layout = () => {
  return (
    <div className="App">
      <header className="header">
        <nav>
          <ul className="nav-links">
            <li>
              <Link to="/Home">Home</Link>
            </li>
            <li>
              <Link to="/Register">Register</Link>
            </li>
            <li>
              <Link to="/Login">Login</Link>
            </li>
          </ul>
        </nav>
      </header>
      <main>
        <Outlet />
      </main>
    </div>
  );
};

export default Layout;
