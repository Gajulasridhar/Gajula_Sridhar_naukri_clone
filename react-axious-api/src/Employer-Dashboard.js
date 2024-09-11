import { BrowserRouter, Outlet, Route, Routes, useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';
import React, { useEffect } from 'react';

const Employer = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Clear session and redirect to Home
    sessionStorage.removeItem("token");
    navigate("/"); // Redirect to Home page
  };

  useEffect(() => {
    if (sessionStorage.getItem("token") === null) {
      navigate("/Login");
    }
  }, [navigate]);

  return (
    <div className="App">
      <header className="header">
        <nav>
          <ul className="nav-links">
            <li>
              <Link to="Addjobs">Post Job</Link>
            </li>
            <li>
              <Link to="JobApplication">View Job Applications</Link>
            </li>
            <li>
              <button className="btn btn-primary" onClick={handleLogout}>Logout </button>
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

export default Employer;
