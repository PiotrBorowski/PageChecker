import React, { Component } from "react";
import "../Styles/Index.css";
import Axios from "axios";
import { BASE_URL } from "../constants";
import TokenHelper from "../helpers/tokenHelper";

export default class Verify extends Component{
    constructor(props)
    {
        super(props);
        this.state = {
            verified: false
        };
    }

    componentDidMount = () => {
        const { token } = this.props.match.params
        TokenHelper.setTokenInHeader(token);
        Axios.get(BASE_URL + "/user/verify")
        .then( response => this.setState({verified: true}))
    }


    render(){
        const Verified = (<h2 className="txt-center" style={{"color":"green"}}>VERIFIED !</h2>) 
        const Progress = (
        <div>
            <h2 className="txt-center">Verification in progress...</h2>
            <h2 className="txt-center">
            <svg aria-hidden="true" focusable="false" data-prefix="fas" data-icon="spinner" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" class="svg-inline--fa fa-spinner fa-w-16 fa-spin fa-lg"><path fill="currentColor" d="M304 48c0 26.51-21.49 48-48 48s-48-21.49-48-48 21.49-48 48-48 48 21.49 48 48zm-48 368c-26.51 0-48 21.49-48 48s21.49 48 48 48 48-21.49 48-48-21.49-48-48-48zm208-208c-26.51 0-48 21.49-48 48s21.49 48 48 48 48-21.49 48-48-21.49-48-48-48zM96 256c0-26.51-21.49-48-48-48S0 229.49 0 256s21.49 48 48 48 48-21.49 48-48zm12.922 99.078c-26.51 0-48 21.49-48 48s21.49 48 48 48 48-21.49 48-48c0-26.509-21.491-48-48-48zm294.156 0c-26.51 0-48 21.49-48 48s21.49 48 48 48 48-21.49 48-48c0-26.509-21.49-48-48-48zM108.922 60.922c-26.51 0-48 21.49-48 48s21.49 48 48 48 48-21.49 48-48-21.491-48-48-48z" class=""></path></svg>            
            </h2>
        </div>) 
        

        return(
            <div className="container">
                <div className="wrapper">
                    <h1 className="title">Email verification</h1>
                    
                    <div>
                        {this.state.verified ? Verified : Progress}
                    </div>
                </div>
            </div>        
        )
    }
}