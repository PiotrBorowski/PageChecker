import React, { Component } from "react";
import { Link } from "react-router-dom";
import "../Styles/Header.css";

export default class Header extends Component {


    render(){
        
        const Links = (
            <React.Fragment>
                <li className="nav-link">
                    <Link className="nav-link">Przycisk jako const<span className="sr-only">(current)</span></Link>
                </li>
            </React.Fragment>
        )

        return (
    <nav className="navbar navbar-expand-sm navbar-light bg-light fixed-top">
        <div className="container">
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarResponsive">
            <ul className="navbar-nav ml-auto">
              <li className="nav-item">
                <Link className="nav-link" to="/">Home<span className="sr-only">(current)</span></Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/events">jakis przycisk<span className="sr-only">(current)</span></Link>
              </li>
            </ul>
          </div>   
        </div>
    </nav>
        )
    }
}