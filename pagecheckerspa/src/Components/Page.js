import React, { Component } from "react";
import "../Styles/Page.css"
import axios from "axios"
import {BASE_URL} from "../constants"

export default class Page extends Component{
    constructor(props){
        super(props);
        this.state = {
            pageId: 0,
            url: "" ,       
        };
    }

    handleStopChecking = () => {
        axios.delete(BASE_URL + "/page/StopChecking?pageId=" + this.props.pageId)
        .then((response) => { 
            console.log(response);
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
            <div className="page">
                <div className="row">
                    <div className="col-md-4">
                        <h5>{this.props.url}</h5>
                    </div>
                    <div className="col-md-3">
                        <time >{this.props.refreshSpan}</time>
                    </div>
                    <div className="col-md-1">
                        <button className="btn btn-danger" onClick={this.handleStopChecking}>Stop</button>
                    </div>
                    <div className="col-md-1">
                        <button className="btn btn-danger" onClick={() => this.props.onDelete(this.props.pageId)}>Delete</button>
                    </div>
                </div>
            </div>
        )
    }

}