import React from 'react';
import { Col } from 'react-bootstrap';
import { FaFolderOpen, FaClock, FaTrash } from 'react-icons/fa';
import FileUploader from './../uploader/FileUploader';
import debug from 'sabio-debug';
import PropTypes from 'prop-types';
import './filemanager.css';

const _logger = debug.extend('FileManagerUploader');

function FManagerActions(props) {
    const onHandleUploadSuccess = (data) => {
        _logger('on handle upload success', data);
        props.onSuccessFileUpload(data);
    };

    const myFilesClicked = (e) => {
        _logger('My Files Clicked');
        e.preventDefault();
        props.myFilesClick(e);
    };

    const recentClicked = (e) => {
        _logger('Recent Clicked');
        e.preventDefault();
        props.recentClick(e);
    };

    const isDeletedClicked = (e) => {
        _logger('Send Files That Are Deleted Clicked');
        e.preventDefault();
        props.isDeletedClick(e);
    };

    return (
        <React.Fragment>
            <div className="email-menu-list mt-3">
                <FileUploader onHandleUploadSuccess={onHandleUploadSuccess} />
                <Col className="actioncomponent-filemanager-aquiferpe" onClick={myFilesClicked}>
                    <i onClick={myFilesClicked} className="actioncomponent-icon-filemanager-aquiferpe">
                        <FaFolderOpen className="actioncomponent-img-filemanager-aquiferpe"></FaFolderOpen>
                    </i>
                    My Files
                </Col>

                <Col className="actioncomponent-filemanager-aquiferpe" onClick={recentClicked}>
                    <i className="actioncomponent-icon-filemanager-aquiferpe">
                        <FaClock className="actioncomponent-img-filemanager-aquiferpe"></FaClock>
                    </i>
                    My Recent
                </Col>
                <Col className="actioncomponent-filemanager-aquiferpe" onClick={isDeletedClicked}>
                    <i className="actioncomponent-icon-filemanager-aquiferpe">
                        <FaTrash className="actioncomponent-img-filemanager-aquiferpe"></FaTrash>
                    </i>
                    Deleted Files
                </Col>
            </div>
        </React.Fragment>
    );
}

FManagerActions.propTypes = {
    onSuccessFileUpload: PropTypes.func.isRequired,
    myFilesClick: PropTypes.func.isRequired,
    recentClick: PropTypes.func.isRequired,
    isDeletedClick: PropTypes.func.isRequired,
};

export default React.memo(FManagerActions);
