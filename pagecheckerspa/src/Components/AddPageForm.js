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
            url: "",
            refreshSpan: "00:15"    
        };
    }

    handleUserInput = e => {
        this.setState({ [e.target.name]: e.target.value });
    }

    handleUserInputRefreshSpan = e => {
        this.setState({ refreshSpan: e.target.value });
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
            Url: this.state.url,
            RefreshSpan: this.state.refreshSpan
        }).then((response) => { 
            console.log(response);
            this.sendStartCheckingRequest(response.data.pageId);
            this.props.history.push('/Pages');
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
            this.props.history.push("/unauthorized");
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
        <form noValidate>
            <h2 className="title">Add Page</h2>
            <div className="form-group col-md-10 offset-md-1">
            <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <span class="input-group-text" id="inputGroup-sizing-default">Website URL</span>
                </div>
                <input
                    className="form-control"
                    type="text"
                    name="url"
                    ref="url"
                    value={this.state.url}
                    onChange={this.handleUserInput}
                    required
                    />
            </div>
            <div class="input-group mb-3">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">Refresh Span</label>
                    </div>
                    <Input type="select" class="custom-select" id="inputGroupSelect01" onChange={this.handleUserInputRefreshSpan}>
                        <option selected value="00:15">15 min</option>
                        <option value="00:30">30 min</option>
                        <option value="01:00">1 hour</option>
                        <option value="03:00">3 hours</option>
                        <option value="12:00">12 hours</option>
                    </Input>
                    </div>               
            </div>
            <div className="row">
                <div className="col-sm-4 button-form">
                    <button
                        id="submitButton"
                        className="btn btn-primary"
                        onClick={this.handleSubmit}
                    >
                        Add Page
                    </button>
                </div>
            </div>
            </form>
        </div>
        )
    }
}