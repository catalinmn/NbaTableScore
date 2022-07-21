import React, { useState, useEffect, useCallback } from "react";
import axios from "axios";
import Pagination from "./Pagination";
import CardList from "./CardList";
import Loading from "./Loading";
import "./Common.css";

function MainPage() {
  const [loading, setLoading] = useState(true);
  const [games, setGames] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize] = useState(4);

  const fetchData = useCallback(async () => {
    const response = await axios.get("http://localhost:5142/api/Main/GetGames", {
      headers: {
        "Access-Control-Allow-Origin": "*",
        //withCredentials: true,
        //allowCredentials: true,
      },
    });
    const json = await response.data;
    setGames(json);
  }, []);

  useEffect(() => {
    setLoading(true);

    fetchData()
      .then(() => setLoading(false))
      .catch(console.error);
  }, [fetchData]);

  // Get current items 
  const indexOfLastItem = currentPage * pageSize;
  const indexOfFirstItem = indexOfLastItem - pageSize;
  const currentGames = games.slice(indexOfFirstItem, indexOfLastItem);

  //Change page
  const handlePageChange = (page) => setCurrentPage(page);

  return (
    <React.Fragment>
      {loading === false && (
        <div>
          <div>
            <CardList games={currentGames} />
          </div>
          <div className="d-inline-flex p-2">
            <Pagination
              totalItems={games.length}
              itemsPerPage={pageSize}
              onPageChange={handlePageChange}
              currentPage={currentPage}
            />
          </div>
        </div>
      )}
      {loading === true && (
        <div className="loading">
          <Loading />
        </div>
      )}
    </React.Fragment>
  );

  //#region snippet
  //   useEffect(() => {
  //     const fetchData = async () => {
  //       const response = await axios.get("http://localhost:5142/api/Main/GetGames");
  //       console.log(response.data);

  //       const json = await response.data;
  //       console.log("JSON");
  //       console.log(json);

  //       setGames(json);
  //       console.log("GAMES");
  //       console.log(games);
  //     };

  //     fetchData().catch(console.error);
  //   }, []);
  //#endregion
}

export default MainPage;
