import React, { Component } from "react";
import {BASE_URL} from "../constants"
import "../Styles/Form.css"
import axios from "axios"

export default class AddPageForm extends Component{
    constructor(props){
        super(props);

        this.state =
        {
            url: ""            
        };
    }

    handleUserInput = e => {
        this.setState({ url: e.target.value });
        console.log(this.state.url);
    }

    handleSubmit = e => {
        e.preventDefault();
        this.sendRequest();
    }

    sendRequest = () => {
        axios.post(BASE_URL + "/page", {
            UserId: 3,
            Url: this.state.url 
        }).then(response => {
            console.log(response);
            () => this.props.history.push('/Pages');
        }, (error) => {
            if(error)
            {
                if(error.response.status === 401)
                {
                    console.log("401 error debug");
                    () => this.props.history.push("/unauthorized")
                }
            }
        });
    }

    render(){
        return (
        <div className="form-center">
        <form noValidate>
            <h2 className="form-title">Add Page</h2>
            <div className="form-group col-md-10 offset-md-1">
                <label id="urlLabel">Website Url</label>
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