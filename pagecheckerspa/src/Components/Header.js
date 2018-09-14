import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import "../Styles/Index.css";

export default class Header extends Component {

    render(){

        const AccountDropdown = (
            <React.Fragment>
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                 Account
                </a>
                <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                   <NavLink className="dropdown-item my-di" to="/">Logout</NavLink>
                </div>
            </li>
            </React.Fragment>
        );

        const LoginButton = (
            <React.Fragment>
                <li className="nav-item">
                    <NavLink className="nav-link" to="/login">Login</NavLink>
                </li>
            </React.Fragment>
        );

        const Register = (
            <React.Fragment>
                <li className="nav-item">
                    <NavLink className="nav-link" to="/register">Register</NavLink>
                </li>
            </React.Fragment>
        )

        return (

    <nav className="navbar navbar-expand-sm navbar-dark bg-dark fixed-top">
        <div className="container">
        <NavLink className="navbar-brand" to="/">PageChecker</NavLink>
        <div className="navbar-header">
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
              <span className="navbar-toggler-icon"></span>
          </button>
          </div>

          <div className="collapse navbar-collapse" id="navbarResponsive">
            <ul className="navbar-nav ml-auto">

              <li className="nav-item">
                <NavLink className="nav-link active" to="/">Home<span className="sr-only">(current)</span></NavLink>
              </li>

              <li className="nav-item">
                  <NavLink className="nav-link" to="/Pages">Your Pages</NavLink>
              </li>

              <li className="nav-item">
                  <NavLink className="nav-link" to="/AddPage">Add Page</NavLink>
              </li>

              {LoginButton}

              {AccountDropdown}

              {Register}

            </ul>
          </div>   
        </div>
    </nav>
        )
    }
}