import React, { Component } from "react";
import "../Styles/Page.css"
import axios from "axios"
import {BASE_URL} from "../constants"
import {ButtonGroup, UncontrolledCollapse, CardBody, Card } from 'reactstrap';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import HtmlDifference from './HtmlDifference';
import DeleteModal from "./DeleteModal";
import {connect} from 'react-redux';
import { withRouter, NavLink } from "react-router-dom";
import { deletePageThunk } from "../Actions/pageActions";



class Page extends Component{
    constructor(props){
        super(props);
        this.state = {
            pageId: this.props.pageId,
            url: this.props.url,
            stopped: this.props.stopped,
            modal: false,
            bodyDifference: ""
        };
    }

    componentDidMount(){
        axios.get(BASE_URL + "/page/Difference?websiteTextId=" + this.props.secondaryTextId)
        .then((response) => { 
            this.setState({bodyDifference: response.data.text});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    handleStopChecking = () => {
        console.log(this.state)
        axios.delete(BASE_URL + "/page/StopChecking?pageId=" + this.props.pageId)
        .then((response) => { 
            console.log(response);
            this.setState({stopped: true});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    handleStartChecking = () => {
        axios.get(BASE_URL + "/page/StartChecking?pageId=" + this.props.pageId)
        .then((response) => { 
            console.log(response);
            this.setState({stopped: false});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    openModal = () =>{
        this.setState({
          modal: true
        });
      }

    deletePage = id =>{
        this.props.dispatch(deletePageThunk(id));
    }

    render(){
        const Changed = (
            <React.Fragment>
                <span className="changed-text">Changed</span>
            </React.Fragment>);

        const NotChanged = (
            <React.Fragment>
                <span>Not Changed</span>
            </React.Fragment>)

        const StopButton = (
            <React.Fragment>
                <button className="btn" onClick={this.handleStopChecking}>
                    <i className="fa fa-pause" aria-hidden="true"></i>
                </button>
            </React.Fragment>)

        const StartButton = (
            <React.Fragment>
                <button className="btn" onClick={this.handleStartChecking}>
                    <FontAwesomeIcon icon="play" color="red" aria-hidden="true"/>
                </button>
            </React.Fragment>)

        const Difference = (
            <div className="col-lg-12"> 
                <h5 style={{"fontWeight":"normal"}}>Difference:</h5>        
                <HtmlDifference html = {this.state.bodyDifference} />
            </div>
        )

        return (
            <div className="page" >
                <DeleteModal isOpen={this.state.modal} onDelete={this.deletePage} pageId={this.props.pageId}></DeleteModal>
           
                <h5 className="text-center text-truncate">
                    {this.props.name}
                </h5>
                <div className="row">
                    <div className="col-lg-5 text-truncate">
                        <h6>Link: <a href={this.props.url} target="_blank">{this.props.url}</a></h6>
                    </div>
         
                    <div className="col-lg-3 offset-lg-2">
                        Status: {this.props.hasChanged ? Changed : NotChanged}
                    </div>
                    <div className="col-lg">
                    <ButtonGroup className="float-right">
                        {this.state.stopped ? StartButton : StopButton}
                        <button className="btn" onClick={() => this.openModal()}>                                 
                            <FontAwesomeIcon icon="trash" color="black" aria-hidden="true"/>
                        </button>
                    </ButtonGroup>
                    </div>
                </div>
                <NavLink className="nav-link" to={`/Page/${this.props.pageId}`}>Details</NavLink>
            </div>
        )
    }

}

export default withRouter(connect(null)(Page));
