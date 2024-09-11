import React, { useState, useEffect } from "react";
import axios from "axios";
import "./PostJob.css";

const PostJob = () => {
  const [job, setJob] = useState({
  
    jobTitle: "",
    jobDescription: "",
    postedDate: new Date().toISOString(),
    companyId: 0,
    jobCategoryId: 1,
    userId: sessionStorage.getItem('userid')
  });

  const [categories, setCategories] = useState([]);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  

  useEffect(() => {
    axios
      .get("http://localhost:5011/api/JobCategories")
      .then((res) => {
        setCategories(res.data);
      })
      .catch((err) => console.log("Error fetching categories: ", err));
  }, []);

  const save = (e) => {
    e.preventDefault();
  
    if (!job.jobTitle || !job.jobDescription || !job.companyId) {
      setError("Please fill in all required fields.");
      return;
    }
    console.log(job)
    axios
      .post("http://localhost:5011/api/Jobs", job)
      .then((res) => {
        setSuccess("Job posted successfully!");
        setError("");
        setJob({
          
          jobTitle: "",
          jobDescription: "",
          postedDate: new Date().toISOString(),
          companyId: 0,
          jobCategoryId: 1,
         
        }); // Clear the form after successful submission
      })
      .catch((err) => {
        const errorMessage = err.response?.data?.message || "Error posting job. Please try again.";
        setError(errorMessage);
        console.log("Error posting job: ", err);
      });
      
  };

  

  return (
    <div className="container">
      <form onSubmit={save}>
        <table border="1" className="custom-table">
          <tbody>
            <tr>
              <td className="label-cell">Job Title</td>
              <td>
                <input
                  type="text"
                  className="input-field"
                  value={job.jobTitle}
                  onChange={(e) =>
                    setJob((prevobj) => ({
                      ...prevobj,
                      jobTitle: e.target.value,
                    }))
                  }
                />
              </td>
            </tr>
            <tr>
              <td className="label-cell">Job Description</td>
              <td>
                <input
                  type="text"
                  className="input-field"
                  value={job.jobDescription}
                  onChange={(e) =>
                    setJob((prevobj) => ({
                      ...prevobj,
                      jobDescription: e.target.value,
                    }))
                  }
                />
              </td>
            </tr>
            <tr>
              <td className="label-cell">Company ID</td>
              <td>
                <input
                  type="number"
                  className="input-field"
                  value={job.companyId}
                  onChange={(e) =>
                    setJob((prevobj) => ({
                      ...prevobj,
                      companyId: parseInt(e.target.value, 10),
                    }))
                  }
                />
              </td>
            </tr>
            <tr>
              <td className="label-cell">Job Category</td>
              <td>
                <select
                  className="input-field"
                  value={job.jobCategoryId}
                  onChange={(e) =>
                    setJob((prevobj) => ({
                      ...prevobj,
                      jobCategoryId: parseInt(e.target.value, 10),
                    }))
                  }
                >
                  {categories.map((category) => (
                    <option key={category.jobCategoryId} value={category.jobCategoryId}>
                      {category.categoryName}
                    </option>
                  ))}
                </select>
              </td>
            </tr>
            <tr>
              <td colSpan="2" className="submit-cell">
                <button type="submit" className="submit-btn">
                  Post Job
                </button>
              </td>
            </tr>
          </tbody>
        </table>
        {error && <p className="error-message">{error}</p>}
        {success && <p className="success-message">{success}</p>}
      </form>
    </div>
  );
};

export default PostJob;
