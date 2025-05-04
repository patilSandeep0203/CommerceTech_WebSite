import React from 'react';
import PropTypes from 'prop-types';
import debug from 'sabio-debug';
import { Row, Col, Card } from 'react-bootstrap';
import { FaFile, FaTrashAlt, FaUndo } from 'react-icons/fa';

const _logger = debug.extend('FileCard');

function FileCard(props) {
    const aFile = props.file;

    const deleteCard = (e) => {
        _logger(' File Card Deleted Clicked');
        e.preventDefault();
        props.deleteCardClicked(aFile.id);
    };

    const undoCard = (e) => {
        _logger(' File Card Deleted Clicked');
        e.preventDefault();
        props.undoCardClicked(aFile.id);
    };

    const deleteBtn = () => {
        return <FaTrashAlt className="delete-btn-filemanager-aquiferpe" color="red" onClick={deleteCard} />;
    };

    const undoBtn = () => {
        return <FaUndo className="undo-btn-filemanager-aquiferpe" color="blue" onClick={undoCard} />;
    };

    return (
        <React.Fragment>
            <Col xxl={4} lg={4}>
                <Card className="m-1 shadow-none border">
                    <div className="p-2">
                        <Row>
                            <Col className="col-auto">
                                <div className="avatar-sm">
                                    <span className="avatar-title bg-light text-secondary rounded">
                                        <i>
                                            <FaFile />
                                        </i>
                                    </span>
                                </div>
                            </Col>
                            <Col className="ps-0">
                                <a
                                    href={aFile.url}
                                    className="text-muted fw-bold url-image-filemanager-aquiferpe"
                                    target="_blank"
                                    rel="noreferrer">
                                    {aFile.name.substring(0, 25)}
                                </a>
                                <p className="mb-0 font-13">
                                    {aFile.isDeleted === true ? <>{undoBtn()}</> : <>{deleteBtn()}</>}
                                </p>
                            </Col>
                        </Row>
                    </div>
                </Card>
            </Col>
        </React.Fragment>
    );
}

FileCard.propTypes = {
    file: PropTypes.shape({
        id: PropTypes.number,
        name: PropTypes.string,
        url: PropTypes.string,
        isDeleted: PropTypes.bool,
        fileType: PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
        }).isRequired,
    }).isRequired,
    deleteCardClicked: PropTypes.func.isRequired,
    undoCardClicked: PropTypes.func.isRequired,
};

export default React.memo(FileCard);
