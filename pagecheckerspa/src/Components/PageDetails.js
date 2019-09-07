import React, {Component} from 'react';
import DeleteModal from './DeleteModal';
import {connect} from 'react-redux';
import { withRouter } from "react-router-dom";
import { deletePageThunk } from "../Actions/pageActions";
import {ButtonGroup, UncontrolledCollapse, CardBody, Card } from 'reactstrap';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import HtmlDifference from './HtmlDifference';
import axios from 'axios';
import {BASE_URL} from '../constants';
import "../Styles/Page.css"
import Spinner from './Spinner';

class PageDetails extends Component {
    constructor(props){
        super(props);
        this.state = {
            modal: false,
            page: null,
            bodyDifference: null
        }
    }

    componentDidMount(){
        const { pageId } = this.props.match.params;
        axios.get(BASE_URL + "/page/details?pageId=" + pageId)
        .then((response) => { 
            console.log(response)
            this.setState({page: response.data});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    getDifference = () => {
        if(this.state.bodyDifference) {
            return;
        }

        axios.get(BASE_URL + "/page/Difference?websiteTextId=" + this.state.page.secondaryTextId)
        .then((response) => { 
            console.log(response);
            this.setState({bodyDifference: response.data.text});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    deletePage = id =>{
        this.props.dispatch(deletePageThunk(id));
        this.props.history.push('/Pages');
    }

    handleStopChecking = () => {
        console.log(this.state)
        axios.delete(BASE_URL + "/page/StopChecking?pageId=" + this.state.page.pageId)
        .then((response) => { 
            console.log(response);
            this.setState(prevState => ({page: {...prevState.page, stopped: true}}));
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    handleStartChecking = () => {
        axios.get(BASE_URL + "/page/StartChecking?pageId=" + this.state.page.pageId)
        .then((response) => { 
            console.log(response);
            this.setState(prevState => ({page: {...prevState.page, stopped: false}}));
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
                {this.state.bodyDifference ? 
                (<HtmlDifference html = {this.state.bodyDifference} />) :
                (<Spinner/>) 
            }     
            </div>
        )

        if(this.state.page)
        return(
            <div className="pages-group" >
                <DeleteModal isOpen={this.state.modal} onDelete={this.deletePage} pageId={this.state.page.pageId}></DeleteModal>    
       
                <h5 className="text-center text-truncate">
                    {this.state.page.name}
                    <ButtonGroup className="float-right">
                        {this.state.page.stopped ? StartButton : StopButton}
                        <button className="btn" onClick={() => this.openModal()}>                                 
                            <FontAwesomeIcon icon="trash" color="black" aria-hidden="true"/>
                        </button>
                    </ButtonGroup>
                </h5>
                <div className="row">
                    <div className="col-lg-5 text-truncate">
                        <h6>Link: <a href={this.state.page.url} target="_blank">{this.state.page.url}</a></h6>
                        Status: {this.state.page.hasChanged ? Changed : NotChanged}<br/>
                        <span style={{"fontWeight":"normal"}}>Refresh rate: </span><time>{this.state.page.refreshRate}</time><br/>
                        <span style={{"fontWeight":"normal"}}>Checking Type: </span>{CheckingType(this.state.page)}<br/><br/>
                    </div>
                </div>

                {this.state.page.hasChanged && 
                    <div>
                        <div className="btn btn-dark btn-block" id="toggler" onClick={this.getDifference}>
                            <i className="fas fa-angle-down" ></i>
                        </div>
                        <UncontrolledCollapse toggler={"#toggler"}>
                            <Card>
                                <CardBody>
                                    {this.state.page.hasChanged ? Difference : null}
                                </CardBody>
                            </Card>
                        </UncontrolledCollapse>
                    </div>
                }


            </div>
        );

        return (<div></div>)
    }
}

export default withRouter(connect(null)(PageDetails));
