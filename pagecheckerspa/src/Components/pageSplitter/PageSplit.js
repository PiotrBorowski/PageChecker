import React, { Component } from "react";
import "../../Styles/PageSplit.css"
import HtmlDifference from "../HtmlDifference";

export default class PageSplit extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="pageSplit">
                <span>XPath: {this.props.xpath}</span>
                <div className="pageSplitText">{this.props.html}</div>
                {/* <HtmlDifference html={this.props.html}/> */}
                <button className="btn btn-dark">Select</button>
            </div>
        );
    }
}