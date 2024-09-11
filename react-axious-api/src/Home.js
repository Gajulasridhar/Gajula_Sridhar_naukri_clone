import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Home.css';


const HomePage = () => {
  const navigate = useNavigate();
  const [jobs, setJobs] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');

  useEffect(() => {
    // Fetch all jobs initially
    axios.get('http://localhost:5011/api/Jobs')
      .then((res) => {
        setJobs(res.data);
      })
      .catch((err) => console.log(err));
  }, []);

  const handleCardClick = (path) => {
    navigate(path);
  };

  const handleSearch = (e) => {
    setSearchQuery(e.target.value);
  };

  const filteredJobs = jobs.filter(job => 
    job.jobTitle.toLowerCase().includes(searchQuery.toLowerCase()) ||
    job.company.companyName.toLowerCase().includes(searchQuery.toLowerCase()) ||
    job.jobCategory.categoryName.toLowerCase().includes(searchQuery.toLowerCase())
  );

  return (
    <div className="home-container">
      <header className="home-header">
        <nav>
          <ul className="nav-links">
            <li><a href="/Home">Home</a></li>
            <li><a href="/Register">Register</a></li>
            <li><a href="/Login">Login</a></li>
          </ul>
        </nav>
      </header>
      <div className='main'>
      <h1><strong>Find Your Dream Job Now..!!</strong></h1>
        </div>
      <div className="home-search">
        
        <input 
          type="text" 
          placeholder="Search jobs..." 
          value={searchQuery} 
          onChange={handleSearch} 
          className="search-bar"
        />
      </div>
      

      <div className="home-content">
     
        <div className="card home-card" onClick={() => handleCardClick('/login')}>
          <h2>Find Jobs</h2>
          <p>Explore thousands of jobs across various categories.</p>
        </div>
        <div className="card home-card" onClick={() => handleCardClick('/login')}>
          <h2>Post Jobs</h2>
          <p>Are you an employer? Post jobs and find the best talent.</p>
        </div>
        <div className="card home-card" onClick={() => handleCardClick('/login')}>
          <h2>Career Advice</h2>
          <p>Get tips and guidance to help you advance in your career.</p>
        </div>
      </div>

      <div className="jobs-list">
        {filteredJobs.map(job => (
          <div key={job.jobId} className="job-card">
            <h2>{job.jobTitle}</h2>
            <p><strong>Company:</strong> {job.company.companyName}</p>
            <p><strong>Location:</strong> {job.company.location}</p>
            <p><strong>Description:</strong> {job.jobDescription}</p>
            <p><strong>Category:</strong> {job.jobCategory.categoryName}</p>
            <p><strong>Posted Date:</strong> {new Date(job.postedDate).toLocaleDateString()}</p>
            <button
              className="apply-button"
              onClick={() => handleCardClick('/login')}
            >
              Apply
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default HomePage;
