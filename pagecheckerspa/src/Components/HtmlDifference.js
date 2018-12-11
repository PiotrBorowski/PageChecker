import React, { Component } from "react";
import "../Styles/HtmlDifference.css"

export default class HtmlDifference extends Component{
    constructor(props){
        super(props);
    }


    render(){
        return(<div className="differenceBorder">
            <span dangerouslySetInnerHTML={{__html: this.props.html}} />
        </div>);
    }
}