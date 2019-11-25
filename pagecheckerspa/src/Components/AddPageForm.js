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
            refreshRate: 15,
            checkingType: 0,
            xpath: "",
            highAccuracy: false
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

    handleXPath = e => {
        this.setState({xpath: e.target.value});
    }

    handleUserInputName = e =>{
        this.setState({name: e.target.value});
    }

    handleHighAccuracy = e =>{
        this.setState({highAccuracy: e.target.checked});
        console.log(this.state)
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
        console.log(this.state);
        axios.post(BASE_URL + "/page", {
            Name: this.state.name,
            Url: "http://" + this.state.url,
            RefreshRate: this.state.refreshRate,
            CheckingType: this.state.checkingType,
            ElementXPath: this.state.xpath,
            HighAccuracy: this.state.highAccuracy
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

    renderRefreshRates = () => {
        const values = [
            {name: "15 min", value: 15},
            {name: "30 min", value: 30},
            {name: "1 hour", value: 60},
            {name: "3 hours", value: 180},
            {name: "12 hours", value: 720},
            {name: "1 day", value: 1440},
        ]

        return values.map(x => (
            <option value={x.value}>{x.name}</option>
        ))
    }

    render(){
        return (
        <div className="form-center">
        <form onSubmit={this.handleSubmit}>
            <h2 className="title">Add Page</h2>
            <div className="form-group col-md-10 offset-md-1">
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="inputGroup-sizing-default">Name</span>
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
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text" id="inputGroup-sizing-default">http://</span>
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
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <label className="input-group-text" for="inputGroupSelect01">Refresh Rate</label>
                    </div>
                    <Input type="select" className="custom-select" id="inputGroupSelect01" onChange={this.handleUserInputRefreshRate}>
                        {this.renderRefreshRates()}
                    </Input>
                </div>    
                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <label className="input-group-text" for="inputGroupSelect02">Type</label>
                    </div>
                    <Input type="select" className="custom-select" id="inputGroupSelect02" onChange={this.handleUserInputCheckingType}>
                        <option selected value="0">Full (scripts included)</option>
                        <option value="1">Only text</option>
                        <option value="2">Element</option>
                    </Input>
                </div>        
                {this.state.checkingType == 2 && 
                    <div className="input-group mb-3">
                        <div className="input-group-prepend">
                            <span className="input-group-text" id="inputGroup-sizing-default">ElelentXPath</span>
                        </div>
                        <input
                            className="form-control"
                            type="text"
                            name="xpath"
                            ref="xpath"
                            value={this.state.xpath}
                            onChange={this.handleXPath}
                            required
                        />
                    </div>
                }         
                <div className="input-group mb-3">
                        <div className="input-group-prepend">
                            <span className="input-group-text" id="inputGroup-sizing-default">High Accuracy</span>
                        </div>
                        {/* <Input
                            className="form-control"
                            type="checkbox"
                            name="accuracy"
                            ref="accuracy"
                            value={this.state.highAccuracy}
                            onChange={this.handleHighAccuracy}                   
                        /> */}
                        <Input type="select" className="custom-select" id="inputGroupSelect03" onChange={this.handleHighAccuracy}>
                            <option selected value={true}>Enable</option>
                            <option value={false}>Disable</option>
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