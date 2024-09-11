import React, { useEffect, useState } from "react";
import axios from "axios";
import './JobApplications.css'; // Ensure this file contains the updated CSS

const JobApplications = () => {
  const [applications, setApplications] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState({});

  const userId = sessionStorage.getItem('userid')
   /* Retrieve the logged-in user's ID here */

  useEffect(() => {
    // Fetch job applications for the logged-in user
    axios.get(`http://localhost:5011/api/JobApplications/employer/${userId}`)
      .then(response => setApplications(response.data))
      .catch(error => console.error("There was an error fetching the job applications!", error));
  }, [userId]);

  const updateStatus = (applicationId) => {
    const newStatus = selectedStatus[applicationId] || "Pending";  // Default to "Pending" if no status selected
    
    axios.put(`http://localhost:5011/api/JobApplications/${applicationId}/status`, { status: newStatus }, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(() => {
        // Update the status locally after a successful API call
        setApplications(applications.map(app => 
          app.jobApplicationId === applicationId ? { ...app, status: newStatus } : app
        ));
      })
      .catch(error => console.error("There was an error updating the status!", error));
  };
  
  return (
    <div className="applications-container">
      <div className="applications-grid">
        {applications.map((app) => (
          <div key={app.jobApplicationId} className="application-card">
            <h3>{app.job.jobTitle}</h3>
            <p><strong>Company:</strong> {app.job.company.companyName}</p>
            <p><strong>Category:</strong> {app.job.jobCategory.categoryName}</p>
            <p><strong>Status:</strong> {app.status}</p>

            <hr />
            
            <h4>Applicant Details:</h4>
            <p><strong>Name:</strong> {app.user.userName}</p>
            <p><strong>Email:</strong> {app.user.email}</p>
            <p><strong>Date of Birth:</strong> {new Date(app.user.dateOfBirth).toLocaleDateString()}</p>

            <div className="status-update">
              <select
                value={selectedStatus[app.jobApplicationId] || app.status}
                onChange={(e) => setSelectedStatus({ ...selectedStatus, [app.jobApplicationId]: e.target.value })}
              >
                <option value="Pending">Pending</option>
                <option value="Accepted">Accept</option>
                <option value="Rejected">Reject</option>
              </select>
              <button
                className="status-update-btn"
                onClick={() => updateStatus(app.jobApplicationId)}
              >
                Update Status
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default JobApplications;