import React, { Component } from "react";
import {BASE_URL} from "../constants"
import "../Styles/Form.css"
import axios from "axios"
import TokenHelper from '../helpers/tokenHelper'
import {Input} from 'reactstrap'

export default class AddPageForm extends Component{
    constructor(props){
        super(props);

        this.state =
        {
            name: "",
            url: "",
            refreshRate: "00:15",
            checkingType: 0
        };
    }

    handleUserInputUrl = e => {
        var str = e.target.value.replace("https://", "");
        if(str === e.target.value){
            str = e.target.value.replace("http://", "");
        }
        this.setState({ [e.target.name]: str});
    }

    handleUserInputRefreshRate = e => {
        this.setState({ refreshRate: e.target.value });
    }

    handleUserInputCheckingType = e => {
        this.setState({ checkingType: e.target.value });
    }

    handleUserInputName = e =>{
        this.setState({name: e.target.value});
    }

    handleSubmit = e => {
        e.preventDefault();
        if(!TokenHelper.CheckToken()){
            this.props.history.push("/login");
        }
        else{
            this.sendRequest();
        }
    }

    sendRequest = () => {
        axios.post(BASE_URL + "/page", {
            Name: this.state.name,
            Url: "http://" + this.state.url,
            RefreshRate: this.state.refreshRate,
            CheckingType: this.state.checkingType
        }).then((response) => { 
            console.log(response);
            this.sendStartCheckingRequest(response.data.pageId);
            this.props.history.push('/Pages');
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/login");
            }

            if(error.response.status === 400){
                this.refs.url.style.borderColor = "red";
            }
        }
        )
    }

    sendStartCheckingRequest = (pageId) => {
        axios.get(BASE_URL + "/page/StartChecking?pageId=" + pageId)
        .then((response) => { 
            console.log(response);
            this.props.history.push('/Pages');
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
            this.props.history.push("/unauthorized");
            }
        }
        )
    }

    render(){
        return (
        <div className="form-center">
        <form onSubmit={this.handleSubmit}>
            <h2 className="title">Add Page</h2>
            <div className="form-group col-md-10 offset-md-1">
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="inputGroup-sizing-default">Name</span>
                    </div>
                    <input
                        className="form-control"
                        type="text"
                        name="name"
                        ref="name"
                        value={this.state.name}
                        onChange={this.handleUserInputName}
                        required
                        />
                </div>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="inputGroup-sizing-default">http://</span>
                    </div>
                    <input
                        className="form-control"
                        type="text"
                        name="url"
                        ref="url"
                        value={this.state.url}
                        onChange={this.handleUserInputUrl}
                        required
                        />
                </div>
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">Refresh Rate</label>
                    </div>
                    <Input type="select" class="custom-select" id="inputGroupSelect01" onChange={this.handleUserInputRefreshRate}>
                        <option selected value="00:15">15 min</option>
                        <option value="00:30">30 min</option>
                        <option value="01:00">1 hour</option>
                        <option value="03:00">3 hours</option>
                        <option value="12:00">12 hours</option>
                    </Input>
                </div>    
                <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect02">Type</label>
                    </div>
                    <Input type="select" class="custom-select" id="inputGroupSelect02" onChange={this.handleUserInputCheckingType}>
                        <option selected value="0">Full (scripts included)</option>
                        <option value="1">Only text</option>
                    </Input>
            </div>              
            </div>
                    
            <div className="row">
                <div className="col-sm-4 button-form">
                    <button
                        type="submit"
                        id="submitButton"
                        className="btn btn-dark"                        
                    >
                        Submit
                    </button>
                </div>
            </div>
            </form>
        </div>
        )
    }
}