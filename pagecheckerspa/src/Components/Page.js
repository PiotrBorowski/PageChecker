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

        const Changed = (
            <React.Fragment>
                <h6 className="changed-text">Changed</h6>
            </React.Fragment>);

        const NotChanged = (
            <React.Fragment>
                <h6>Not Changed</h6>
            </React.Fragment>)

        return (
            <div className="page">
                <div className="row">
                    <div className="col-lg-5">
                        <h6><a href={this.props.url} target="_blank">{this.props.url}</a></h6>
                    </div>
                    <div className="col-lg-3">
                        <span>Refresh rate: </span><time>{this.props.refreshRate}</time>
                    </div>
                    <div className="col-lg-2">
                        {this.props.hasChanged ? Changed : NotChanged}
                    </div>
                    <div className="col-lg-2">
                        <button className="btn btn-danger float-right" onClick={this.handleStopChecking}>Stop</button>
                        <button className="btn btn-danger float-right" onClick={() => this.props.onDelete(this.props.pageId)}>Delete</button>
                    </div>
                </div>
            </div>
        )
    }

}