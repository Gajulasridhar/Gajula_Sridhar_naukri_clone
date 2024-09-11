import { useState, useEffect } from "react";
import axios from "axios";
import "./GetCompanies.css"; // Assuming you store the CSS in this file

const GetCompanies = () => {
  const [items, GetItems] = useState([]);
  

  useEffect(() => {
    axios
      .get("http://localhost:5011/api/Companies")
      .then((response) => {
        console.log(response.data);
        GetItems(response.data);
      })
      .catch((error) => console.log(error));
  }, []);
  const update= (companyId) => {
    console.log(items);
    axios
      .put("http://localhost:5011/api/Companies/" + companyId)
      .then((res) => {
        console.log(res.data);
      })
      .catch((err) => console.log(err));
  };
  const Remove = (companyId) => {
    console.log(companyId);
    axios
      .delete("http://localhost:5011/api/Companies/" + companyId)
      .then((res) => {})
      .catch((err) => console.log(err));
  };
  

  return (
    <div className="container">
      <form>
        <table className="custom-table">
          <thead>
            <tr>
              <th>Company ID</th>
              <th>Company Name</th>
              <th>Location</th>
            </tr>
          </thead>
          <tbody>
            {items.map((i) => (
              <tr key={i.companyId}>
                <td>{i.companyId}</td>
                <td>{i.companyName}</td>
                <td>{i.location}</td>
                <td>
                  <button onClick={() => Remove(i.companyId)}>Delete</button>
                  <button onClick={() => update(i.companyId)}>Edit</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </form>
    </div>
  );
};

export default GetCompanies;
