import React, { Component } from "react";
import axios from "axios"
import { BASE_URL } from "../constants";
import "../Styles/Page.css"

export default class Page extends Component{
    constructor(props){
        super(props);
        this.state = {
            pageId: 0,
            url: ""           
        };
    }

    render(){
        return (
            <div className="page">
                <div className="row">
                    <div className="col-md-5">
                        <h4>{this.props.url}</h4>
                    </div>
                    <div className="col-md-5">
                        <button className="btn btn-danger" onClick={() => this.props.onDelete(this.props.pageId)}>Delete</button>
                    </div>
                </div>
            </div>
        )
    }

}