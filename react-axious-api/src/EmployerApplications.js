import React, { useEffect, useState } from "react";
import axios from "axios";
import './JobApplications.css'; // Ensure this file contains the updated CSS

const EmployerApplications = () => {
  const [applications, setApplications] = useState([]);
  const [applicationCount, setApplicationCount] = useState(0);
  const [selectedStatus, setSelectedStatus] = useState({});
  const employerId = sessionStorage.getItem('userid'); // Assuming employerId is stored as userid in session
  const role = sessionStorage.getItem('role');

  // Fetch applications when component mounts
  useEffect(() => {
    const fetchApplications = async () => {
      try {
        if (role === "Employer") {
          const response = await axios.get(`http://localhost:5011/api/JobApplications/employer/${employerId}`);
          console.log('API Response:', response.data);

          // Check if data is returned and set it to state
          if (response.data && Array.isArray(response.data)) {
            setApplications(response.data);
            setApplicationCount(response.data.length);
          } else {
            setApplications([]);
            setApplicationCount(0);
          }
        }
      } catch (error) {
        console.error("Error fetching job applications!", error);
      }
    };

    fetchApplications();
  }, [employerId, role]);

  // Function to update the status of a job application
  const updateStatus = async (applicationId) => {
    const newStatus = selectedStatus[applicationId] || "Pending";  // Default to "Pending" if no status selected

    try {
        console.log('Payload:', newStatus);

        // Send the newStatus directly as the payload
        const response = await axios.put(`http://localhost:5011/api/JobApplications/${applicationId}/status`, newStatus, {
            headers: {
                'Content-Type': 'application/json'
            }
        });

        console.log('Response:', response.data);

        // Update the status locally after a successful API call
        setApplications(applications.map(app => 
            app.jobApplicationId === applicationId ? { ...app, status: newStatus } : app
        ));
    } catch (error) {
        console.error("Error updating the status!", error);

        // Log the error response from the server for better debugging
        if (error.response) {
            console.error('Server Response:', error.response.data);
        }
    }
};
  return (
    <div className="applications-container">
      <div>
        <h2 style={{ fontWeight: 'strong' }}>Total Applications: {applicationCount}</h2>
      </div>
      <div className="applications-grid">
        {applications.length > 0 ? (
          applications.map((app) => (
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
          ))
        ) : (
          <p>No applications found.</p>
        )}
      </div>
    </div>
  );
};

export default EmployerApplications;
