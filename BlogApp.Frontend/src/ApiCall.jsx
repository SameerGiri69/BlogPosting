import React, { useEffect, useState } from "react";

const ApiCall = () => {
  const [data, setData] = useState();
  useEffect(() => {
    debugger;
    const fetchData = async () => {
      try {
        const res = await fetch("http://localhost:5137/api/blog/get-image");
        const responsedata = await res.json();
        setData(responsedata);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  console.log(data);
  return (
    <div>
      {data.map((item, index) => (
        <div
          key={index}
          style={{ border: "1px solid #ccc", padding: "10px", margin: "10px" }}
        >
          <h2>{item.title}</h2>
          <h4>Author: {item.author}</h4>
          <p>{item.description}</p>
          {item.imageBase64 && (
            <img
              src={`data:image/jpeg;base64,${item.imageBase64}`}
              alt={item.title}
              style={{ width: "200px", height: "auto" }}
            />
          )}
        </div>
      ))}
    </div>
  );
};

export default ApiCall;
