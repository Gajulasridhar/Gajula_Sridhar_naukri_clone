// JobSeekerApplications.js
import React, { useEffect, useState } from "react";
import axios from "axios";
import './JobApplications.css'; // Reuse the existing CSS for styling

const JobSeekerApplications = () => {
  const [applications, setApplications] = useState([]);
  const userId = sessionStorage.getItem('userid');
  
  useEffect(() => {
    const fetchApplications = async () => {
      try {
        const response = await axios.get(`http://localhost:5011/api/JobApplications/user/${userId}`);
        
        if (response.data && Array.isArray(response.data)) {
          setApplications(response.data);
        } else {
          setApplications([]);
        }
      } catch (error) {
        console.error("Error fetching job applications!", error);
      }
    };

    fetchApplications();
  }, [userId]);

  return (
    <div className="applications-container">
      <div className="applications-grid">
        {applications.length > 0 ? (
          applications.map((app) => (
            <div key={app.jobApplicationId} className="application-card">
              <h3>{app.job.jobTitle}</h3>
              <p><strong>Company:</strong> {app.job.company.companyName}</p>
              <p><strong>Category:</strong> {app.job.jobCategory.categoryName}</p>
              <p><strong>Status:</strong> {app.status}</p>
              
              <hr />
              
              
            </div>
          ))
        ) : (
          <p>No applications found.</p>
        )}
      </div>
    </div>
  );
};

export default JobSeekerApplications;
