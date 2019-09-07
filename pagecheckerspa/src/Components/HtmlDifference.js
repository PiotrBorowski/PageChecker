import React, { Component } from "react";
import "../Styles/HtmlDifference.css"

export default class HtmlDifference extends Component{
    render(){
        var HtmlToReactParser = require('html-to-react').Parser;
         
        var htmlToReactParser = new HtmlToReactParser();
        var reactElement = htmlToReactParser.parse(this.props.html);

        return(<div className="differenceBorder">
            {reactElement}
        </div>);
    }
}