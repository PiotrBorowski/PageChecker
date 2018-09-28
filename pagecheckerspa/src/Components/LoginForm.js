import React, { Component } from "react";
import "../Styles/Form.css"
import axios from 'axios';
import {BASE_URL} from "../constants"
import TokenHelper from '../helpers/tokenHelper'
import { CheckUserToken } from "../Actions/userActions";
import {connect} from 'react-redux'

class LoginForm extends Component {
    
    constructor(props)
    {
        super(props);
        this.state = {
            username: "",
            password: ""
        };
        console.log(this.props)
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
            TokenHelper.setTokenInHeader(response.data.token);
            TokenHelper.setTokenInLocalStorage(response.data.token);
            localStorage.setItem('username', response.data.username)
            this.props.dispatch(CheckUserToken());
            this.props.history.push('/Pages');
        }, (error) => {
            console.log(error);
            
            if(error.response.status === 401){
                    this.props.history.push('/unauthorized');
            }
        });
    }

    render(){

        return (
 <div className="form-center">
        <form onSubmit={this.handleLoginSubmit}t>
            <h2 className="title">Login</h2>
            <div className="form-group col-md-10 offset-md-1">
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="inputGroup-sizing-default">Username</span>
                    </div>
                                    <input 
                                    class="form-control" 
                                    id="UsernameInput" 
                                    value={this.username}
                                    onChange={this.handleChangeUsername}
                                    required
                                    />
                                    </div>
            </div>
            <div className="form-group col-md-10 offset-md-1">
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="inputGroup-sizing-default">Password</span>
                    </div>
                                        <input 
                                        type="password"
                                        class="form-control"
                                        id="PasswordInput"
                                        value={this.password}
                                        onChange={this.handleChangePassword}
                                        required
                                        />
                                        </div>
                </div>
            <div className="row">
                <div className="col-sm-4 button-form">
                    <button
                        type="submit"
                        id="submitButton"
                        className="btn btn-dark"
                    >
                        Login
                    </button>
                </div>
            </div>
            </form>
        </div>   
        )
    }
}

export default connect()(LoginForm);