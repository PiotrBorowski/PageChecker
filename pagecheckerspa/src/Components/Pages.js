import React, { Component } from "react";
import axios from "axios"
import {BASE_URL} from "../constants"
import Page from "../Components/Page"
import "../Styles/Page.css"

export default class Pages extends Component {
    constructor(props){
        super(props);
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
            })
        }).catch((error) => {
            console.log(error);
            
            if(error.response.status === 401){
                    this.props.history.push('/unauthorized');
            }
        });
    }

    renderPages = () => {
        return this.state.pages.map(page => 
            <Page onDelete={this.deletePage} url={page.url} pageId={page.pageId} />
        );
    }

    deletePage = id =>{
        return axios.delete(BASE_URL + "/page?pageId=" + id)
        .then(this.setState({ pages: this.pagesExceptSpecified(id)}))
        .then(() => this.props.history.push('/Pages'))
        .catch(err => {
            console.log(err);
        })
    }

    pagesExceptSpecified = id =>{
        return this.state.pages.filter(page => page.pageId !== id)
    }

    render(){
        return (
            <div className="container">
                <div className="pages-group">
                    <h2 className="title">Your Pages</h2>
                    <div>
                        {this.renderPages()}
                    </div>
                </div>
            </div>        
        )
    }
}