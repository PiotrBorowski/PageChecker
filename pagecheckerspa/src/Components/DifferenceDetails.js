import React, {Component} from 'react';
import { CardBody, Card } from 'reactstrap';
import HtmlDifference from './HtmlDifference';
import axios from 'axios';
import {BASE_URL} from '../constants';
import "../Styles/Page.css"
import Spinner from './Spinner';

class DifferenceDetails extends Component {
    constructor(props){
        super(props);
        this.state = {
            difference: null,
        }
    }

    componentDidMount(){
        const { id } = this.props.match.params;
        this.getDifference(id);
    }

    getDifference = (pageId) => {
        axios.get(BASE_URL + "/difference?guid=" + pageId)
        .then((response) => { 
            this.setState({difference: response.data});
        }, (error) => {
            console.log(error);
            if(error.response.status === 401){
                this.props.history.push("/unauthorized");
            }
        }
        )
    }

    render(){
        
        const Difference = (
            <div className="col-lg-12"> 
                <h5 style={{"fontWeight":"normal"}}>Difference:</h5>   
                {this.state.difference ? 
                (<HtmlDifference html = {this.state.difference.text} />) :
                (<Spinner/>) 
            }     
            </div>
        )
        return (
            <div className="pages-group">
                <Card>
                    <CardBody>
                        {Difference}
                    </CardBody>
                </Card>
            </div>
        );
    }
}

export default DifferenceDetails;
