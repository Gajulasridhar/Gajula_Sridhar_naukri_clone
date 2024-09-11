import axios from "axios";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import './Register.css'; // Import the CSS file

const Register = () => {
  const initialValues = {
    userName: "",
    email: "",
    dateOfBirth: "",
    password: "",
    role: "",
  };

  const validationSchema = Yup.object({
    userName: Yup.string().required("Username is required"),
    email: Yup.string().email("Invalid email format").required("Email is required"),
    dateOfBirth: Yup.date().required("Date of birth is required"),
    password: Yup.string().required("Password is required"),
    role: Yup.string().required("Role is required"),
  });

  const onSubmit = (values, { setSubmitting, resetForm }) => {
    axios
      .post("http://localhost:5011/api/Users/Register", values)
      .then((resp) => {
        setSubmitting(false);
        console.log("Registration successful:", resp.data);
        // Reset form after successful registration
        resetForm();
      })
      .catch((err) => {
        setSubmitting(false);
        console.error("Registration failed:", err);
      });
  };

  return (
    <div className="register-main">
      <div className="register-wrapper">
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={onSubmit}
        >
          {({ isSubmitting }) => (
            <Form className="register-form">
              <h1 className="form-title" style={{color:'blue'}}>Register</h1>
              
              <div className="input-container">
                <label htmlFor="userName" className="input-label" style={{color:'black'}}>UserName</label>
                <Field
                  type="text"
                  name="userName"
                  className="input-field"
                  placeholder="Enter your username"
                />
                <ErrorMessage name="userName" component="div" className="error-message" />
              </div>
              
              <div className="input-container">
                <label htmlFor="email" className="input-label" style={{color:'black'}}>Email</label>
                <Field
                  type="email"
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
                  name="password"
                  className="input-field"
                  placeholder="Enter your password"
                />
                <ErrorMessage name="password" component="div" className="error-message" />
              </div>
              
              <div className="input-container">
                <label htmlFor="dateOfBirth" className="input-label" style={{color:'black'}}>DOB</label>
                <Field
                  type="date"
                  name="dateOfBirth"
                  className="input-field"
                />
                <ErrorMessage name="dateOfBirth" component="div" className="error-message" />
              </div>
              
              <div className="input-container">
                <label htmlFor="role" className="input-label" style={{color:'black'}}>Role</label>
                <Field as="select" name="role" className="input-field">
                  <option value="">Select Role</option>
                  <option value="JobSeeker">JobSeeker</option>
                  <option value="Employer">Employer</option>
                </Field>
                <ErrorMessage name="role" component="div" className="error-message" />
              </div>
              
              <button type="submit" className="register-btn" disabled={isSubmitting}>
                {isSubmitting ? "Registering..." : "Register"}
              </button>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};

export default Register;
