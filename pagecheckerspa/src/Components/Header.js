import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import "../Styles/Index.css";

export default class Header extends Component {


    render(){

        return (
    <nav className="navbar navbar-expand-sm navbar-light bg-dark fixed-top">
        <div className="container">
        <div className="navbar-header">
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
          </button>
          </div>

          <div className="collapse navbar-collapse" id="navbarResponsive">
            <ul className="navbar-nav ml-auto">

              <li className="nav-item">
                <NavLink className="nav-link" to="/">Home<span className="sr-only">(current)</span></NavLink>
              </li>

              <li className="nav-item">
                <NavLink className="nav-link" className="nav-link" to="/ERRRORRRR">404 Not Found</NavLink>
              </li>

              <li className="nav-item">
                  <NavLink className="nav-link" to="/Pages">Your Pages</NavLink>
              </li>

              <li className="nav-item">
                  <NavLink className="nav-link" to="/AddPage">Add Page</NavLink>
              </li>
            </ul>
          </div>   
        </div>
    </nav>
        )
    }
}