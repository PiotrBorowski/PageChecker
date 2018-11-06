import React from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

class DeleteModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      modal: this.props.modal
    };

    this.toggle = this.toggle.bind(this);
  }

  toggle = () =>{
    this.setState({
      modal: !this.state.modal
    });
  }

  render() {
    return (
        <Modal isOpen={this.state.modal} toggle={this.toggle}>
          <ModalHeader toggle={this.toggle}>Confirm</ModalHeader>
          <ModalBody>
            Do you really want to delete this page?
          </ModalBody>
          <ModalFooter>
            <Button color="danger" onClick={() => this.props.onDelete(this.props.pageId)}>Delete</Button>
            <Button color="secondary" onClick={this.toggle}>Cancel</Button>
          </ModalFooter>
        </Modal>
    );
  }
}

export default DeleteModal;