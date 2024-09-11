import './App.css';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "./Login";
import Employer from "./Employer-Dashboard";
import JobSeeker from "./JobSeeker-dashboard";
import Register from './Register';
import Home from './Home';
import Layout from './Layout';
import PostJob from './Addjobs';
import ViewJobs from './GetJobs';
import JobApplications from './JobApplication';
import JobSeekerApplications from './JobSeekerApplications';
import EmployerApplications from './EmployerApplications';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route path="Login" element={<Login />} />
            <Route path="Register" element={<Register />} />
            <Route index element={<Home />} />
            <Route path="Home" element={<Home />} />
          </Route>
          {/* Admin Dashboard */}
          <Route path="Employer-Dashboard" element={<Employer />}>
            <Route path="Addjobs" element={<PostJob />} />
            <Route path="JobApplication" element={<EmployerApplications />} />
            <Route path="Home" element={<Home />} /> {/* Ensure this maps to Home */}
          </Route>
          {/* User Dashboard */}
          <Route path="JobSeeker-dashboard" element={<JobSeeker />}>
            <Route path="GetJobs" element={<ViewJobs />} />
            <Route path="AppliedJobs" element={<JobSeekerApplications />} />
            <Route path="Home" element={<Home />} /> {/* Ensure this maps to Home */}
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
