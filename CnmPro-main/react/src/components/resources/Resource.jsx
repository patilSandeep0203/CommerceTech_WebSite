import './resources.css';
import React, { useState, useEffect } from 'react';
import { Row, Col, Card, Dropdown } from 'react-bootstrap';
import PropTypes from 'prop-types';
import { useNavigate } from 'react-router-dom';
import { BsTrash, BsPencil, BsInfoCircle } from 'react-icons/bs';
import { MdMoreVert } from 'react-icons/md';

import logger from 'sabio-debug';

const _logger = logger.extend('Resource');

function Resource(props) {
    _logger('resource props', props.resourceProp);

    const [isOrgAdmin, setIsOrgAdmin] = useState(false);

    const resource = props.resourceProp;

    const navigate = useNavigate();

    useEffect(() => {
        if (props.user.roles.includes('Org Admin')) {
            setIsOrgAdmin(true);
        }
    }, []);

    const onDeleteResource = (e) => {
        e.preventDefault();
        props.onResourceClicked(props.resourceProp.id);
    };

    const onEditResourceClick = (e) => {
        e.preventDefault();
        const stateForTransport = { type: 'RESOURCE_FORM_EDIT', payload: resource };

        navigate(`/resources/${resource.id}/edit`, { state: stateForTransport });
    };

    const onResourceDetailsClick = () => {
        const stateForTransport = { type: 'RESOURCE_CARD_DETAILS', payload: resource };
        navigate(`/resources/${resource.id}/details`, { state: stateForTransport });
    };
    _logger(resource);

    return (
        <Col className="col-xs-12 col-sm-12 col-md-4 col-lg-3 col-xl-3 text-center">
            <Card className="shadow-lg p-3 bg-white rounded">
                <Row>
                    <Col>
                        <Dropdown>
                            <Dropdown.Toggle
                                variant="light"
                                className="bg-transparent border-0 p-0 dropdown-resource-manage float-end">
                                <MdMoreVert className="resource-vert-icon" />
                            </Dropdown.Toggle>
                            <Dropdown.Menu>
                                {isOrgAdmin && resource.createdBy === props.user.id ? (
                                    <>
                                        <Dropdown.Item onClick={onResourceDetailsClick}>
                                            <BsInfoCircle /> Details
                                        </Dropdown.Item>
                                        <Dropdown.Item onClick={onEditResourceClick}>
                                            <BsPencil /> Edit
                                        </Dropdown.Item>
                                        <Dropdown.Item onClick={onDeleteResource}>
                                            <BsTrash /> Delete
                                        </Dropdown.Item>
                                    </>
                                ) : (
                                    <Dropdown.Item onClick={onResourceDetailsClick}>
                                        <BsInfoCircle />
                                    </Dropdown.Item>
                                )}
                            </Dropdown.Menu>
                        </Dropdown>
                    </Col>
                </Row>
                <Card.Body>
                    <Row>
                        <img src={resource.logo} className="res-card-img center" alt="Resource Logo" />
                        <h3 className="card-title res-card-title-cnmpro"> {resource.name} </h3>
                        <h5 className="card-text">{resource.headline} </h5>
                        <h5>
                            {' '}
                            Phone Number:{' '}
                            {`(${resource.phone.substring(0, 3)}) ${resource.phone.substring(
                                3,
                                6
                            )}-${resource.phone.substring(6, 10)}`}
                        </h5>
                    </Row>
                </Card.Body>
            </Card>
        </Col>
    );
}

Resource.propTypes = {
    resourceProp: PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired,
        headline: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        logo: PropTypes.string.isRequired,
        resourceCategoryId: PropTypes.number,
        orgId: PropTypes.number,
        contactName: PropTypes.string,
        contactEmail: PropTypes.string,
        phone: PropTypes.string.isRequired,
        siteUrl: PropTypes.string,
        createdBy: PropTypes.number.isRequired,
    }),
    onResourceClicked: PropTypes.func,
    user: PropTypes.shape({
        email: PropTypes.string,
        id: PropTypes.number,
        isLoggedIn: PropTypes.bool,
        roles: PropTypes.arrayOf(PropTypes.string),
    }).isRequired,
};

export default Resource;
