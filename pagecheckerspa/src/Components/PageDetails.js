import React, {Component} from 'react';
import DeleteModal from './DeleteModal';
import {connect} from 'react-redux';
import { withRouter, NavLink } from "react-router-dom";
import { deletePageThunk } from "../Actions/pageActions";
import {ButtonGroup, ListGroup, ListGroupItem} from 'reactstrap';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import axios from 'axios';
import {BASE_URL} from '../constants';
import "../Styles/Page.css"
import * as moment from 'moment';

class PageDetails extends Component {
    constructor(props){
        super(props);
        this.state = {
            modal: false,
            page: null,
            differences: []
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
        this.getDifferences(pageId);
    }

    getDifferences = (pageId) => {
        axios.get(BASE_URL + "/difference/info?guid=" + pageId)
        .then((response) => { 
            this.setState({differences: response.data});
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

    openModal = () => {
        this.setState({
          modal: true
        });
    };

    renderDifferences = () => {
        return this.state.differences.map(x => (
            <ListGroupItem tag="a" style={{cursor: 'pointer'}} action>
                <NavLink className="nav-link" to={`/Difference/${x.differenceId}`}>
                    {moment(x.date).format("DD.MM.YY hh:mm")}
                </NavLink>
            </ListGroupItem>
        ));
    }

    render(){
        
        function CheckingType(props)
        {
            switch(props.checkingType)
            {
                case 0:
                    return <span>Full</span>
                case 1:
                    return <span>Text Only</span>
                case 2:
                    return <span>Element</span>
                default:
                    return <span>Undefined</span>
            }   
        }       

        function timespanString(span){
            const time = moment.duration(span*1000*60);
            console.log(time)
            const days = time.days() !== 0 ? time.days()+" day" : "";
            const hours = time.hours() !== 0 ? time.hours()+" hours" : "";
            const minutes = time.minutes() !== 0 ? time.minutes()+" minutes" : "";

            return `${days} ${hours} ${minutes}`;
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
                        <span style={{"fontWeight":"normal"}}>Refresh rate: </span><time>{timespanString(this.state.page.refreshRate)}</time><br/>
                        <span style={{"fontWeight":"normal"}}>Checking Type: </span>{CheckingType(this.state.page)}<br/>
                        <span style={{"fontWeight":"normal"}}>High Accuracy: </span><span>{this.state.page.highAccuracy ? "Yes" : "No"}</span><br/><br/>
                    </div>
                </div>

                <span>Changes list</span>
                <ListGroup>
                    {this.renderDifferences()}
                </ListGroup>
            </div>
        );

        return (<div></div>)
    }
}

export default withRouter(connect(null)(PageDetails));
