import React, { Component } from "react";
import "../Styles/Index.css";

export default class Home extends Component {
  render() {
    return (
      <div>
        <div className="bgimage">
          <div className="container-fluid">
            <div className="hero-text">
              <h1>PageChecker</h1>
            </div>
          </div>
        </div>
        <div className="container home-text">
          <p>
          By Piotr Borowski
          </p>
        </div>
      </div>
      
    );
  }
}