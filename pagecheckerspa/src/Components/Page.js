import React, { Component } from "react";
import axios from "axios"
import { BASE_URL } from "../constants";

export default class Page extends Component{
    constructor(props){
        super(props);
        this.state = {
            pageId: 0,
            url: ""           
        };
    }

    deletePage = () =>{
        return axios.delete(BASE_URL + "/page?pageId=" + this.props.pageId)
        .catch(err => {
            console.log(err);
        })
    }

    render(){
        return (
            <div className="row">
                <div className="col-md-5">
                  <h4>{this.props.url}</h4>
                </div>
                <div className="col-md-5">
                    <button className="btn btn-danger" onClick={this.deletePage}>Delete</button>
                </div>
            </div>
        )
    }

}