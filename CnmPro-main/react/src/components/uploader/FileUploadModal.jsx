import React from 'react';
import FileUploader from './FileUploader';
import { Modal } from 'react-bootstrap';
import PropTypes from 'prop-types';
import debug from 'sabio-debug';
import './filestyle.css';

const _logger = debug.extend('Files');

const FileUploadModal = (props) => {
    const onHandleUploadSuccess = (response) => {
        _logger('Modal File Upload', response);
    };

    return (
        <Modal
            style={{ background: 'light-grey', borderRadius: '15%' }}
            show={props.isShowModal}
            onHide={props.toggleModal}
            backdrop="static"
            keyboard={false}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered>
            <Modal.Header closeButton>
                <Modal.Title>
                    <div className="container">
                        <div className="col">
                            <div className="row fu-row">
                                <h2 className="mod-header">File Upload</h2>
                            </div>
                        </div>
                    </div>
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>{<FileUploader onHandleUploadSuccess={onHandleUploadSuccess}></FileUploader>}</Modal.Body>
        </Modal>
    );
};

FileUploadModal.propTypes = {
    isShowModal: PropTypes.bool.isRequired,
    toggleModal: PropTypes.func.isRequired,
};

export default FileUploadModal;
