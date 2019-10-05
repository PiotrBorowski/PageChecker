import React, { Component } from "react";
import axios from "axios";
import {BASE_URL} from "../../constants"
import PageSplit from "./PageSplit";


export default class PageSplitList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            splits: [],
        }
    }

    componentDidMount() {
        const { url } = this.props.match.params
        this.getSplits(url);
    }

    getSplits = (url) => {
        axios.post(BASE_URL + "/page/Split", {Url: "http://onet.pl/"})
        .then((response) => {
            console.log(response);
            this.setState({splits: response.data});
        }, (error) => {
            console.log(error);
        })
    }

    renderSplits = () => {
        return this.state.splits.map(split => 
            <PageSplit html={split}/>
        );
    }

    render() {
        return (
            <div>
                {this.renderSplits()}
            </div>
        )
    }
}