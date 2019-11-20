import React, { Component } from "react";
import "../../Styles/Page.css";
import TokenHelper from '../../helpers/tokenHelper';
import {connect} from 'react-redux';
import {  withRouter } from "react-router-dom";


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
    }   

    render(){
        return (
            <div className="container">
                <div className="pages-group">
                    <h2 className="title">Statistics</h2>
                    <div>
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