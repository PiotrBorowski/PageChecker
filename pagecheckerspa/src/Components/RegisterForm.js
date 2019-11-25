import React, { Component } from "react";
import "../Styles/Form.css"
import axios from 'axios';
import {BASE_URL} from "../constants"
import EmailHelper from "../helpers/emailHelper";
import { withRouter, NavLink } from "react-router-dom";

export default class RegisterForm extends Component {
    
    constructor(props)
    {
        super(props);
        this.state = {
            email: "",
            password: "",
            confirmPassword: ""
        };
    }

    handleChangeUsername = e => {
        this.setState({email: e.target.value});
        if(!EmailHelper.Validate(e.target.value))
        {
            this.refs.UsernameInput.style.borderColor = "red";
        }
        else{
            this.refs.UsernameInput.style.borderColor = "green";
        }
    }

    checkUsernameValidity() {
        return EmailHelper.Validate(this.state.email);
    }

    handleChangePassword = e => {
        this.setState({password: e.target.value});
    }

    handleChangeConfirmPassword = e => {
        this.setState({confirmPassword: e.target.value});
    }

    passwordConfirmed(){
        return this.state.password === this.state.confirmPassword && this.state.confirmPassword !== ""
    }

    handleLoginSubmit = e => {
        e.preventDefault();
        if(!this.passwordConfirmed()){
            this.refs.ConfirmPasswordInput.style.borderColor = "red";
            return;
        }

        this.sendRequest();
    }

    sendRequest = () => {
         axios.post(BASE_URL + "/user/register", {
            Username: this.state.email,
            Email: this.state.email,
            Password: this.state.password 
        }).then(response => {
            console.log(response);
            this.props.history.push('/login');
        }, (error) => {
            console.log(error);
            
            if(error.response.status === 401){
                this.props.history.push('/unauthorized');
            }

            if(error.response.status === 400){
                this.refs.UsernameInput.style.borderColor = "red";
            }
        });
    }

    render(){
        return (
 <div className="form-center">
        <form onSubmit={this.handleLoginSubmit}>
            <h2 className="title">Register</h2>
            <div className="form-group col-md-10 offset-md-1">
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="inputGroup-sizing-default">Email</span>
                    </div>
                                    <input 
                                    className="form-control" 
                                    id="UsernameInput" 
                                    ref="UsernameInput"
                                    value={this.username}
                                    onChange={this.handleChangeUsername}
                                    required
                                    />
                                    </div>
            </div>
            <div className="form-group col-md-10 offset-md-1">
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="inputGroup-sizing-default">Password</span>
                    </div>
                                        <input 
                                        type="password"
                                        className="form-control"
                                        id="PasswordInput"
                                        value={this.password}
                                        onChange={this.handleChangePassword}
                                        required
                                        />
                                        </div>
                </div>
                <div className="form-group col-md-10 offset-md-1">
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="inputGroup-sizing-default">Confirm password</span>
                    </div>
                                        <input 
                                        type="password"
                                        className="form-control"
                                        id="ConfirmPasswordInput"
                                        ref="ConfirmPasswordInput"
                                        value={this.confirmPassword}
                                        onChange={this.handleChangeConfirmPassword}
                                        required
                                        />
                                        </div>
                </div>

            <div className="row">
                <div className="col-sm-4 button-form">
                    <button
                        id="submitButton"
                        className="btn btn-dark"
                        ref="submitButton"
                        disabled={!this.checkUsernameValidity()}
                    >
                        Register
                    </button>
                </div>
            </div>
            <div className="row justify-content-center"  style={{marginTop: '15px'}}><NavLink to="/login">Login to PageChecker</NavLink> </div>
            </form>
        </div>   
        )
    }
}

