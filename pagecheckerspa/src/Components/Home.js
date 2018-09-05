import React, { Component } from "react";
import "../Styles/Header.css";

export default class Home extends Component {
  render() {
    return (
      <div>
        <div className="bgimage">
          <div className="container-fluid">
            <div className="hero-text">
              <h1>Sprawdź zmianę strony XD</h1>
            </div>
          </div>
        </div>
        <div className="container home-text">
          <p>
      strona startowa
          </p> 
          <p>
          By Piotr Borowski
          </p>
        </div>
      </div>
      
    );
  }
}