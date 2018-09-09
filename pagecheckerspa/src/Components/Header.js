import React, { Component } from "react";
import { Link } from "react-router-dom";
import "../Styles/Header.css";

export default class Header extends Component {


    render(){

        return (
    <nav className="navbar navbar-expand-sm navbar-light bg-light fixed-top">
        <div className="container">
        <div className="navbar-header">
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
          </button>
          </div>

          <div className="collapse navbar-collapse" id="navbarResponsive">
            <ul className="navbar-nav ml-auto">
              <li className="nav-item">
                <Link className="nav-link" to="/">Home<span className="sr-only">(current)</span></Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/ERRRORRRR">jakis przycisk<span className="sr-only">(current)</span></Link>
              </li>
              <li>
                  <Link className="nav-link" to="/Pages">Your Pages</Link>
              </li>
              <li className="nav_item">
                  <Link className="nav-link" to="/AddPage">Add Page</Link>
              </li>
            </ul>
          </div>   
        </div>
    </nav>
        )
    }
}