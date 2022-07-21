import React from "react";
import Card from "react-bootstrap/Card";
import ListGroup from "react-bootstrap/ListGroup";
import "./Common.css";

const CardList = ({ games }) => {
  return (
    <React.Fragment>
      <div className="d-inline-flex p-2">
        {games.map((item) => (
          <Card border="dark" key={item["Id"]}>
            <div className="images">
              <Card.Img
                variant="left"
                src={item["HomeTeamLogo"]}
                className="card-image-home"
                alt="Image not available"
              />
              <Card.Img
                variant="right"
                src={item["VisitorTeamLogo"]}
                className="card-image-visitors"
                alt="Image not available"
              />
            </div>
            <Card.Body>
              <Card.Title>
                {item["HomeTeam"]} - {item["VisitorTeam"]}
              </Card.Title>
              <Card.Text></Card.Text>
            </Card.Body>
            <ListGroup className="list-group-flush">
              <ListGroup.Item>
                {item["Date"]} (Season {item["Season"]})
              </ListGroup.Item>
              <ListGroup.Item>
                {item["HomeTeamScore"]} : {item["VisitorTeamScore"]}
              </ListGroup.Item>
            </ListGroup>
          </Card>
        ))}
      </div>
    </React.Fragment>
  );
};

export default CardList;
