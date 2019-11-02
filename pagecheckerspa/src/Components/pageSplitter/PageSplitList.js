import React, { Component } from "react";
import axios from "axios";
import {BASE_URL} from "../../constants"
import PageSplit from "./PageSplit";
import "../../Styles/Page.css";


export default class PageSplitList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            splits: [],
        }
    }

    componentDidMount() {
        let { url } = this.props.match.params;
        if(!url.includes("http")) {
            url = "http://" + url;
        }
        this.getSplits(url);
    }

    getSplits = (url) => {
        axios.post(BASE_URL + "/page/Split", {Url: url})
        .then((response) => {
            console.log(response);
            this.setState({splits: response.data});
        }, (error) => {
            console.log(error);
        })
    }

    renderSplits = () => {
        return this.state.splits.map(split => 
            <PageSplit key={split.xPath} html={split.html} xpath={split.xPath}/>
        );
    }

    render() {
        return (
            <div className="pages-group">
                {this.renderSplits()}
            </div>
        )
    }
}