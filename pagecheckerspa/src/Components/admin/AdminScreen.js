import React, { Component } from "react";
import "../../Styles/Page.css";
import TokenHelper from '../../helpers/tokenHelper';
import {connect} from 'react-redux';
import {BASE_URL} from "../../constants"
import axios from "axios"
import {  withRouter } from "react-router-dom";
import { Table } from 'reactstrap';


class AdminScreen extends Component {
    constructor(props){
        super(props);
        this.state={
            pages: [],
            users: [],
            email: "",
            userPages: []
        };
    }

    componentDidMount(){
        if(!TokenHelper.CheckToken()){
            this.props.history.push("/login");
        }   
        this.fetchPages();
        this.fetchUsers();
    }   

    fetchPages = () => {
        axios.get(BASE_URL + "/admin/GroupedPages")
        .then((response) => { 
            console.log(response);
            this.setState({pages: response.data});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    fetchUsers = () => {
        axios.get(BASE_URL + "/admin/Users")
        .then((response) => { 
            console.log(response);
            this.setState({users: response.data});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    fetchUserPages = () => {
        axios.get(BASE_URL + "/admin/UserPages?email="+this.state.email)
        .then((response) => { 
            console.log(response);
            this.setState({userPages: response.data});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    renderPages = () => {
        return this.state.pages.map((page, index, arr) => (
            <tr key={index}>
                <th scope="row">{index+1}</th>
                <th>{page.url}</th>
                <th>{page.count}</th>
            </tr>
        ))
    }

    renderUsers = () => {
        return this.state.users.map(user => (
            <tr key={user.userId}>
                <th scope="row">{user.userId}</th>
                <th>{user.email}</th>
                <th>{user.role}</th>
                <th>{user.verified ? "Yes" : "No"}</th>
            </tr>
        ))
    }

    renderUserPages = () => {
        return this.state.userPages.map(page => (
            <tr key={page.userId}>
                <th scope="row">{page.pageId}</th>
                <th>{page.name}</th>
                <th>{page.checkingType}</th>
                <th>{page.creationDate}</th>
                <th>{page.elementXPath}</th>
                <th>{page.hasChanged}</th>
                <th>{page.highAccuracy}</th>
                <th>{page.primaryTextId}</th>
                <th>{page.refreshRate}</th>
                <th>{page.stopped}</th>
                <th>{page.url}</th>
                <th>{page.userId}</th>

            </tr>
        ))
    }

    handleChangeEmail = e => {
        this.setState({email: e.target.value});
    }

    render(){
        return (
            <div className="container">
                <div className="pages-group">
                    <h2 className="title">Statistics</h2>
                    <div>
                        <Table responsive>
                            <thead>
                                <th>#</th>
                                <th>url</th>
                                <th>count</th>
                            </thead>
                            <tbody>
                                {this.renderPages()}                             
                            </tbody>
                        </Table>

                        <Table responsive>
                            <thead>
                                <th>id</th>
                                <th>email</th>
                                <th>role</th>
                                <th>verified</th>
                            </thead>
                            <tbody>
                                {this.renderUsers()}                             
                            </tbody>
                        </Table>

                        <div>
                            <div className="input-group mb-3">
                                <div className="input-group-prepend">
                                    <span className="input-group-text" id="inputGroup-sizing-default">Email</span>
                                </div>
                                <input 
                                    className="form-control" 
                                    id="EmailInput" 
                                    value={this.state.email}
                                    onChange={this.handleChangeEmail}
                                    required
                                />
                            </div>
                            <div className="col-sm-4 button-form">
                                <button
                                    type="submit"
                                    id="submitButton"
                                    className="btn btn-dark"
                                    onClick={this.fetchUserPages}
                                >
                                    Search
                                </button>
                            </div>

                            
                            <Table responsive>
                                <thead>
                                    <th>pageId</th>
                                    <th>name</th>
                                    <th>checkingType</th>
                                    <th>creationDate</th>
                                    <th>elementXPath</th>
                                    <th>hasChanged</th>
                                    <th>highAccuracy</th>
                                    <th>primaryTextId</th>
                                    <th>refreshRate</th>
                                    <th>stopped</th>
                                    <th>url</th>
                                    <th>userId</th>
                                </thead>
                                <tbody>
                                    {this.renderUserPages()}                 
                                </tbody>
                            </Table>

                        </div>
                    </div>
                </div>
            </div>        
        )
    }
}

export default withRouter(AdminScreen);