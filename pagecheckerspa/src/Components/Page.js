import React, { Component } from "react";
import "../Styles/Page.css"
import axios from "axios"
import {BASE_URL} from "../constants"
import {ButtonGroup, UncontrolledCollapse, Button, CardBody, Card } from 'reactstrap';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {  Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import HtmlDifference from './HtmlDifference';

export default class Page extends Component{
    constructor(props){
        super(props);
        this.state = {
            pageId: this.props.pageId,
            url: this.props.url,
            stopped: this.props.stopped,
            modal: false
        };
    }

    handleStopChecking = () => {
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

    toggleModal = () =>{
        this.setState({
          modal: !this.state.modal
        });
      }

    render(){

        function CheckingType(props)
        {
            switch(props.checkingType)
            {
                case 0:
                    return <span >Full</span>
                case 1:
                    return <span >Text Only</span>
                default:
                    return <span >Undefined</span>
            }          
        }
    
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
                    <i class="fa fa-pause" aria-hidden="true"></i>
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
                <HtmlDifference html = {this.props.bodyDifference} />
            </div>
        )

        return (
            <div className="page" >
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                <ModalHeader toggle={this.toggleModal}>Confirm</ModalHeader>
                <ModalBody>
                    Do you really want to delete this page?
                </ModalBody>
                <ModalFooter>
                    <Button color="danger" onClick={() => this.props.onDelete(this.props.pageId)}>Delete</Button>
                    <Button color="secondary" onClick={this.toggleModal}>Cancel</Button>
                </ModalFooter>
                </Modal>
           
                <h5 className="text-center text-truncate">
                    {this.props.name}
                </h5>
                <div className="row">
                    <div className="col-lg-5 text-truncate">
                        <h6><a href={this.props.url} target="_blank">{this.props.url}</a></h6>
                    </div>
         
                    <div className="col-lg-2 offset-lg-2">
                        {this.props.hasChanged ? Changed : NotChanged}
                    </div>
                    <div className="col-lg">
                    <ButtonGroup className="float-right">
                        {this.state.stopped ? StartButton : StopButton}
                        <button className="btn" onClick={() => this.toggleModal()}>                                 
                            <FontAwesomeIcon icon="trash" color="black" aria-hidden="true"/>
                        </button>
                    </ButtonGroup>
                    </div>
                </div>
                <div className="btn btn-dark btn-block" id={"toggler" + this.props.pageId} >
                    More Info
                </div>
                <UncontrolledCollapse toggler={"#toggler"+this.props.pageId}>
                    <Card>
                        <CardBody>
                         <div className="col-lg-4">
                            <span style={{"fontWeight":"normal"}}>Refresh rate: </span><time>{this.props.refreshRate}</time><br/>
                            <span style={{"fontWeight":"normal"}}>Checking Type: </span>{CheckingType(this.props)}<br/><br/>
                        </div>
                        {this.props.hasChanged ? Difference : null}
                        </CardBody>
                    </Card>
                </UncontrolledCollapse>
            </div>
        )
    }

}