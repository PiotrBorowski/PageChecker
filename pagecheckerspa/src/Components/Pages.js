import React, { Component } from "react";
import axios from "axios"
import {BASE_URL} from "../constants"
import Page from "../Components/Page"
import "../Styles/Page.css"

export default class Pages extends Component {
    constructor(){
        super();
        this.state={
            pages: []
        };
    }

    componentDidMount(){
        this.getPages(BASE_URL + "/page");
    }   

    getPages(url){
        return axios.get(url).then(response => {
            console.log(response);
            this.setState({
                pages: response.data
            });
        });
    }

    renderPages = () => {
        return this.state.pages.map(page => 
            <Page url={page.url}/>
        );
    }

    render(){
        return (
            <div className="container">
                <div className="pages-group">
                    <div>{this.renderPages()}</div>
                </div>
            </div>        
        )
    }
}