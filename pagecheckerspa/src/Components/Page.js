import React, { Component } from "react";
import { Link } from "react-router-dom";

export default class Page extends Component{
    constructor(props){
        super(props);
        this.state = {
            url: ""           
        };
    }

    render(){
        return (
            <div className="row">
                <div className="col-md-5">
                  <h4>{this.props.url}</h4>
                </div>
            </div>
        )
    }

}