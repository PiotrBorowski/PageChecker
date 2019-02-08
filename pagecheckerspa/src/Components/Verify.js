import React, { Component } from "react";
import "../Styles/Index.css";
import Axios from "axios";
import { BASE_URL } from "../constants";
import TokenHelper from "../helpers/tokenHelper";
import Spinner from "./Spinner";

export default class Verify extends Component{
    constructor(props)
    {
        super(props);
        this.state = {
            verified: null
        };
    }

    componentDidMount = () => {
        const { token } = this.props.match.params
        TokenHelper.setTokenInHeader(token);
        Axios.get(BASE_URL + "/user/verify")
        .then( response => this.setState({verified: true}), error => this.setState({verified: false}))
    }


    render(){
        const Verified = (<h2 className="txt-center" style={{"color":"green"}}>VERIFIED !</h2>) 
        const Error = (<h2 className="txt-center" style={{"color":"red"}}>Error has occured</h2>) 
        const Progress = (
        <div>
            <h2 className="txt-center">Verification in progress...</h2>
            <h2 className="txt-center">
                <Spinner/>
            </h2>
        </div>) 
        

        return(
            <div className="container">
                <div className="wrapper">
                    <h1 className="title">Email verification</h1>
                    
                    <div>                       
                        {this.state.verified !== null ? null : Progress}
                        {this.state.verified ? Verified : Error}
                    </div>
                </div>
            </div>        
        )
    }
}