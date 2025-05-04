import './events.css';
import React, { useState, useEffect } from 'react';
import { Row, Col, Card } from 'react-bootstrap';
import PropTypes from 'prop-types';
import { useNavigate } from 'react-router-dom';
import { BsTrash, BsInfoCircle } from 'react-icons/bs';
import logger from 'sabio-debug';

const _logger = logger.extend('Event');

function Event(props) {
    _logger(props.eventProp);

    const [isOrgAdmin, setIsOrgAdmin] = useState(false);

    const event = props.eventProp;

    const navigate = useNavigate();

    useEffect(() => {
        if (props.user.roles.includes('Org Admin')) {
            setIsOrgAdmin(true);
        }
    }, []);

    const onDeleteEvent = (e) => {
        e.preventDefault();
        props.onEventClicked(props.eventProp.id);
    };
    
    const onEventDetailsClick = () => {
        const stateForTransport = { type: 'EVENT_CARD_DETAILS', payload: event };
        navigate(`/event/${event.id}/details`, { state: stateForTransport });
    };
    _logger(event);

    return (
        <Col className="col-xs-12 col-sm-12 col-md-4 col-lg-3 col-xl-3 d-flex align-items-stretch text-center">
            <Card className="shadow-lg p-3 bg-white rounded">
                <Card.Img src={event.imageUrl} />
                <Card.Body>
                    <Row>
                        <Card.Title as="h5">{event.title}</Card.Title>
                        <Card.Text>{event.description} </Card.Text>
                        <Card.Text>
                            <strong>Organization: </strong>
                            {event.organization.name}
                        </Card.Text>
                        <Card.Text>
                            <strong>Address: </strong>
                            {event.location.lineOne}
                        </Card.Text>
                        <Card.Text>{event.location.city}</Card.Text>
                        <Card.Text>{event.location.stateName}</Card.Text>
                        <div className="mb-2">
                            {isOrgAdmin ? (
                                <Row className="mt-2">
                                    <Col className="text-center">
                                        <BsInfoCircle type="button" onClick={onEventDetailsClick} />
                                    </Col>
                                    <Col>
                                        <BsTrash type="button" onClick={onDeleteEvent} />
                                    </Col>
                                </Row>
                            ) : (
                                <Row className="mt-2">
                                    <Col className="text-center">
                                        <BsInfoCircle type="button" onClick={onEventDetailsClick} />
                                    </Col>
                                </Row>
                            )}
                        </div>
                    </Row>
                </Card.Body>
            </Card>
        </Col>
    );
}

Event.propTypes = {
    eventProp: PropTypes.shape({
        id: PropTypes.number.isRequired,
        title: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        imageUrl: PropTypes.string.isRequired,
        organization: PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string.isRequired,
            description: PropTypes.string.isRequired,
            headline: PropTypes.string.isRequired,
            logo: PropTypes.string.isRequired,
        }),
        location: PropTypes.shape({
            lineOne: PropTypes.string.isRequired,
            lineTwo: PropTypes.string,
            city: PropTypes.string.isRequired,
            zip: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
            stateName: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
            latitude: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
            longitude: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
        }),
    }),
    onEventClicked: PropTypes.func,
    user: PropTypes.shape({
        email: PropTypes.string,
        id: PropTypes.number,
        isLoggedIn: PropTypes.bool,
        roles: PropTypes.arrayOf(PropTypes.string),
    }).isRequired,
};

export default Event;
