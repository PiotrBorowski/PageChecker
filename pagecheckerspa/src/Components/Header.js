import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import "../Styles/Index.css";
import axios from 'axios';
import {BASE_URL} from "../constants"

export default class Header extends Component {
    
    constructor(props)
    {
        super(props);
        this.state = {
            username: "",
            password: ""
        };
    }

    handleChangeUsername = e => {
        this.setState({username: e.target.value});
    }

    handleChangePassword = e => {
        this.setState({password: e.target.value});
    }

    handleLoginSubmit = e => {
        e.preventDefault();
        this.sendRequest();
    }

    sendRequest = () => {
         axios.post(BASE_URL + "/user/login", {
            Username: this.state.username,
            Password: this.state.password 
        }).then(response => {
            console.log(response);
        }).then(() => this.props.history.push('/Pages'))
        .catch(error => {
            console.log(error);
            if(error.status === 401)
            {() => this.props.history.push("/unauthorized")}
        });
    }

    render(){

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

               <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                     Account
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <div className="drop-form">
                            <div class="form-group">
                                <label for="exampleDropdownFormUsernamel2">Email address</label>
                                <input 
                                class="form-control" 
                                id="exampleDropdownFormUsernamel2" 
                                placeholder="username"
                                value={this.username}
                                onChange={this.handleChangeUsername}
                                />
                            </div>
                            <div class="form-group">
                                <label for="exampleDropdownFormPassword2">Password</label>
                                <input 
                                type="password"
                                class="form-control"
                                id="exampleDropdownFormPassword2"
                                placeholder="Password"
                                value={this.password}
                                onChange={this.handleChangePassword}
                                />
                            </div>
                            <button type="submit" onClick={this.handleLoginSubmit} class="btn btn-primary">Sign in</button>
                        </div>
                    </div>
                </li>

            </ul>
          </div>   
        </div>
    </nav>
        )
    }
}