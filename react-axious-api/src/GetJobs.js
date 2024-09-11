import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './ViewJobs.css';

const ViewJobs = () => {
  const [jobs, setJobs] = useState([]);
  const [appliedJobs, setAppliedJobs] = useState({}); // Track applied jobs
  const [user, setUser] = useState({}); // User details to be fetched from API or session storage

  useEffect(() => {
    // Fetch jobs from the API
    axios.get('http://localhost:5011/api/Jobs')
      .then((res) => {
        setJobs(res.data);
      })
      .catch((err) => console.log(err));
    
    // Fetch user details from API or session storage
    const userId = parseInt(sessionStorage.getItem('userid'));
    axios.get(`http://localhost:5011/api/Users/${userId}`)
      .then((res) => {
        setUser(res.data);
      })
      .catch((err) => console.log(err));
  }, []);

  const handleApply = (job) => {
    const jobApplication = {
      jobApplicationId: 0,
      applicationDate: new Date().toISOString(),
      userId: user.userId,
      user: {
        userId: user.userId,
        userName: user.userName,
        email: user.email,
        dateOfBirth: user.dateOfBirth,
        password: user.password,
        role: user.role
      },
      jobId: job.jobId,
      job: {
        jobId: job.jobId,
        jobTitle: job.jobTitle,
        jobDescription: job.jobDescription,
        postedDate: job.postedDate,
        companyId: job.company.companyId,
        company: {
          companyId: job.company.companyId,
          companyName: job.company.companyName,
          location: job.company.location
        },
        jobCategoryId: job.jobCategory.jobCategoryId,
        jobCategory: {
          jobCategoryId: job.jobCategory.jobCategoryId,
          categoryName: job.jobCategory.categoryName
        }
      },
      status: 'Pending'
    };

    console.log(jobApplication);

    axios.post('http://localhost:5011/api/JobApplications', jobApplication)
      .then(() => {
        setAppliedJobs((prevState) => ({
          ...prevState,
          [job.jobId]: true
        }));
      })
      .catch((err) => console.log(err));
  };

  return (
    <div className="jobs-container">
      {jobs.map((job) => (
        <div key={job.jobId} className="job-card">
          <h2>{job.jobTitle}</h2>
          <p><strong>Company:</strong> {job.company.companyName}</p>
          <p><strong>Location:</strong> {job.company.location}</p>
          <p><strong>Description:</strong> {job.jobDescription}</p>
          <p><strong>Category:</strong> {job.jobCategory.categoryName}</p>
          <p><strong>Posted Date:</strong> {new Date(job.postedDate).toLocaleDateString()}</p>
          <button
            className={`apply-button ${appliedJobs[job.jobId] ? 'applied' : ''}`}
            onClick={() => handleApply(job)}
            disabled={appliedJobs[job.jobId]} // Disable the button if already applied
          >
            {appliedJobs[job.jobId] ? 'Applied' : 'Apply'}
          </button>
        </div>
      ))}
    </div>
  );
};

export default ViewJobs;
