import { BrowserRouter, Outlet, Route, Routes, useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';
import React, { useEffect } from 'react';
import jobseekerimg from './jobseekerbg.png'
const JobSeeker = () => {
  const navigate=useNavigate()
  const handleLogout = () => {
    // Clear session and redirect to login
    sessionStorage.removeItem("token");
    navigate("/Login");
};
  useEffect(() => {
    if (sessionStorage.getItem("token") === null) {
      navigate("/Login");
    }
  }, []);
    return (
      <div className="App">
     
      <header className="header">
        <nav>
          <ul className="nav-links">
            
          <li>
                <Link to="GetJobs">ViewJobs</Link>
              </li>
              <li>
              <Link to="AppliedJobs">Applied Jobs</Link> {/* Add this line */}
            </li>
              <li>
              <button className="btn btn-primary" onClick={handleLogout}>Logout </button>
           
              </li>
            
          </ul>
        </nav>
      </header>

      <main>
        <Outlet></Outlet>
      </main>
     
      </div>
      );
    };
export default JobSeeker