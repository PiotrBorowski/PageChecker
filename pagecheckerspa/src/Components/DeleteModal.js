import React, {Component} from 'react';
import {  Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { Button } from 'reactstrap';


export default class DeleteModal extends Component {
    constructor(props){
        super(props);
        this.state = {
            isOpen: this.props.isOpen
        };
    }

    closeModal = () => {
        this.setState({isOpen: false});
    }

    componentWillReceiveProps(props) {
        this.setState({isOpen: props.isOpen});
    }

    render(){
        return(
            <Modal isOpen={this.state.isOpen}>
                <ModalHeader>Confirm</ModalHeader>
                <ModalBody>
                    Do you really want to delete this page?
                </ModalBody>
                <ModalFooter>
                    <Button color="danger" onClick={() => this.props.onDelete(this.props.pageId)}>Delete</Button>
                    <Button color="secondary" onClick={this.closeModal}>Cancel</Button>
                </ModalFooter>
            </Modal>
        )
    }

}