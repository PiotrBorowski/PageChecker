import React, { Component } from "react";
import "../Styles/Form.css"
import axios from 'axios';
import {BASE_URL} from "../constants"
import TokenHelper from '../helpers/tokenHelper'
import { checkUserToken } from "../Actions/userActions";
import {connect} from 'react-redux'
import FacebookLogin from 'react-facebook-login'

class LoginForm extends Component {
    
    constructor(props)
    {
        super(props);
        this.state = {
            username: "",
            password: "",
            notVerified: false,
            incorrectCredentials: false
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

    resetErrorState = () => {
        this.setState({incorrectCredentials: false});
        this.setState({notVerified: false});
    }

    handleLoginResponse = (response) => {
        console.log(response);
        TokenHelper.setTokenInHeader(response.data.token);
        TokenHelper.setTokenInLocalStorage(response.data.token);
        localStorage.setItem('username', response.data.username)
        localStorage.setItem('role', response.data.role)
        this.props.dispatch(checkUserToken());
    }

    sendRequest = () => {
         axios.post(BASE_URL + "/user/login", {
            Email: this.state.username,
            Password: this.state.password 
        }).then(response => {
            console.log(response);
            this.handleLoginResponse(response);
            this.props.history.push('/Pages');
        }, (error) => {
            console.log(error);
            
            this.resetErrorState();

            switch(error.response.status){
                case 401:
                    this.setState({incorrectCredentials: true});
                break;

                case 403:
                    this.setState({notVerified: true});
                break;
            }
        });
    }

    sendFacebookAccessToken = (token) =>{
        axios.post(BASE_URL + "/facebook/login?userAccessToken="+token, {
            userAccessToken: token
        }).then(response => {
            console.log(response);
            this.handleLoginResponse(response);
            this.props.history.push('/Pages');
        })
    }

    render(){

        const AccountNotVerifiedAlert = (
            <div class="alert alert-warning">
                <strong>Warning!</strong> Your account is not verified, please check your mailbox.
            </div>      
        )

        const IncorrectCredentialsAlert = (
            <div class="alert alert-danger">
                <strong>Warning!</strong> Password or Email is incorrect.
            </div>      
        )
        
        const responseFacebook = (response) => {
            console.log(response);
            var token = response.accessToken;
            this.sendFacebookAccessToken(token);
          }

        return (
 <div className="form-center">
        <form onSubmit={this.handleLoginSubmit}>
            <h2 className="title">Login</h2>
            <div className="form-group col-md-10 offset-md-1">
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="inputGroup-sizing-default">Email</span>
                    </div>
                                    <input 
                                    className="form-control" 
                                    id="UsernameInput" 
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

            {this.state.notVerified ? AccountNotVerifiedAlert : null}
            {this.state.incorrectCredentials ? IncorrectCredentialsAlert : null}
            </form>
        <br/>

        {/* <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-">
                    <h2 className="center">Login with Facebook</h2>                
                </div>
            </div>       
        </div>
        
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-">
                    <FacebookLogin
                    appId="255549251999650"
                    fields="name,email,picture"
                    callback={responseFacebook}
                    icon="fa-facebook"   
                    cssClass="btn btn-primary"          
                    redirectUri="https://pagecheckersite.azurewebsites.net/login"           
                    />
                </div>       
            </div>
        </div> */}

            
        </div>   
        )
    }
}

export default connect()(LoginForm);