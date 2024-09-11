import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import './Login.css';  // Import the CSS file

const Login = () => {
  const [err, setErr] = useState("");
  const navigate = useNavigate();

  const initialValues = {
    email: "",
    password: "",
  };

  const validationSchema = Yup.object({
    email: Yup.string()
      .email("Invalid email format")
      .required("Email is required"),
    password: Yup.string().required("Password is required"),
  });

  const onSubmit = (values, { setSubmitting }) => {
    let user = { email: values.email, password: values.password };

    axios
      .post("http://localhost:5011/api/Users/Validate", user)
      .then((response) => {
        setSubmitting(false);
        console.log(response);
        if (response.status === 204) {
          setErr("Invalid User Credentials");
        } else {
          let user = response.data;
          console.log(user.token);
          // Store token data
          sessionStorage.setItem("token", user.token);
          sessionStorage.setItem("userid", user.userId);
          sessionStorage.setItem("role", user.role);  // Ensure role is stored

          if (user.role === "Employer") {
            navigate("/Employer-Dashboard");
          } else if (user.role === "JobSeeker") {
            navigate("/JobSeeker-dashboard");
          }
        }
      })
      .catch((error) => {
        setSubmitting(false);
        console.error("Login failed:", error);
        setErr("An error occurred during login.");
      });
  };

  const navigateToRegister = () => {
    navigate("/register");  // Replace "/register" with the actual registration route
  };

  return (
    <div className="login-wrapper">
      <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={onSubmit}
      >
        {({ isSubmitting }) => (
          <Form className="login-form">
            <h1 className="form-title" style={{color:'blue'}}>Welcome Back</h1>
            <p className="form-subtitle">Please login to continue</p>
            
            <div className="input-container">
              <label htmlFor="email" className="input-label" style={{color:'black'}}>Email</label>
              <Field
                type="text"
                id="email"
                name="email"
                className="input-field"
                placeholder="Enter your email"
              />
              <ErrorMessage name="email" component="div" className="error-message" />
            </div>

            <div className="input-container">
              <label htmlFor="password" className="input-label" style={{color:'black'}}>Password</label>
              <Field
                type="password"
                id="password"
                name="password"
                className="input-field"
                placeholder="Enter your password"
              />
              <ErrorMessage name="password" component="div" className="error-message" />
            </div>

            <button type="submit" className="login-btn" disabled={isSubmitting}>
              {isSubmitting ? "Logging in..." : "Login"}
            </button>
            {err && <span className="error-message">{err}</span>}
            <h4>or</h4>
            <div className="reg">
              
              <button
                type="button"
                className="register-btn"
                onClick={navigateToRegister} style={{background:'green'}}
              >
                Register
              </button>
            </div>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default Login;
