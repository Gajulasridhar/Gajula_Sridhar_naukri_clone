import { useState, useEffect } from "react";
import axios from "axios";
import "./AddCompanies.css"; // Assuming you store the CSS in this file

const AddCompanies = () => {
  const [company, setCompany] = useState({
    companyId: 0,
    companyName: "",
    location: "",
  });

  const save = (e) => {
    e.preventDefault();
    console.log(company);
    axios
      .post("http://localhost:5011/api/Companies", company)
      .then((res) => {
        console.log(res.data);
      })
      .catch((err) => console.log(err));
  };

  return (
    <div className="container">
      <form onSubmit={save}>
        <table border={1} className="custom-table">
          <tr>
            <td className="label-cell">Company Name</td>
            <td>
              <input
                type="text"
                className="input-field"
                value={company.companyName}
                onChange={(e) =>
                  setCompany((prevobj) => ({
                    ...prevobj,
                    companyName: e.target.value,
                  }))
                }
              ></input>
            </td>
          </tr>
          <tr>
            <td className="label-cell">Location</td>
            <td>
              <input
                type="text"
                className="input-field"
                value={company.location}
                onChange={(e) =>
                  setCompany((prevobj) => ({
                    ...prevobj,
                    location: e.target.value,
                  }))
                }
              ></input>
            </td>
          </tr>
          <tr>
            <td colSpan={2} className="submit-cell">
              <button type="submit" className="submit-btn">
                Save
              </button>
            </td>
          </tr>
        </table>
      </form>
    </div>
  );
};

export default AddCompanies;
