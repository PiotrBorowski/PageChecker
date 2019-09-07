import React, { Component } from "react";
import Page from "../Components/Page";
import "../Styles/Page.css";
import TokenHelper from '../helpers/tokenHelper';
import {connect} from 'react-redux';
import {  withRouter } from "react-router-dom";
import { getPagesThunk } from "../Actions/pageActions";


class Pages extends Component {
    constructor(props){
        super(props);
        this.state={
            pages: []
        };
    }

    componentDidMount(){
        if(!TokenHelper.CheckToken()){
            this.props.history.push("/login");
        }
        else{
            this.props.dispatch(getPagesThunk());
        }      
    }   

    renderPages = () => {
        if(this.props.pages.length !== 0){
            console.log(this.props.pages)
            return this.props.pages.map(page => 
                <Page key={page.pageId} name={page.name} url={page.url} pageId={page.pageId} refreshRate={page.refreshRate} hasChanged={page.hasChanged} stopped={page.stopped} checkingType={page.checkingType} secondaryTextId={page.secondaryTextId}/>
            );
        }

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

function mapStateToProps(state){
    console.log(state);
    return {
        pages: state.pages
    };
}

export default withRouter(connect(mapStateToProps)(Pages));