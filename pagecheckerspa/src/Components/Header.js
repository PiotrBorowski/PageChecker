import React, { Component } from "react";
import { NavLink, withRouter } from "react-router-dom";
import "../Styles/Index.css";
import TokenHelper from '../helpers/tokenHelper'
import {connect} from 'react-redux'
import { CheckUserToken } from "../Actions/userActions";

class Header extends Component {
    constructor(props){
        super(props);
        console.log(this.props)
        window.onload = () => {
            this.props.dispatch(CheckUserToken())          
        }
    }

    handleLogout = () => {
        TokenHelper.setTokenInHeader(false);
        TokenHelper.setTokenInLocalStorage(false);
        localStorage.removeItem('username');
        this.props.history.push('/');
        this.props.dispatch(CheckUserToken());
    };

    render(){
        
        const AccountDropdown = (
            <React.Fragment>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    {this.props.user.username}
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                    <button className="dropdown-item my-di" onClick={this.handleLogout}>Logout</button>
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

        const YourPages = (
            <React.Fragment>
              <li className="nav-item">
                  <NavLink className="nav-link" to="/Pages">Your Pages</NavLink>
              </li>
            </React.Fragment>
        )

        const AddPage = (
            <React.Fragment>
              <li className="nav-item">
                  <NavLink className="nav-link" to="/AddPage">Add Page</NavLink>
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

              { this.props.user.isAuthenticated ? YourPages : null}  

              { this.props.user.isAuthenticated ? AddPage : null}

              { this.props.user.isAuthenticated ? null : LoginButton }

              { this.props.user.isAuthenticated ? AccountDropdown : null}

              { this.props.user.isAuthenticated ? null : Register}

            </ul>
          </div>   
        </div>
    </nav>
        )
    }
}

function mapStateToProps(state){
    return{
        user: state.user
    };
}

export default withRouter(connect(mapStateToProps)(Header));