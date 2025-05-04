import React from 'react';
import { Row, Col, Card } from 'react-bootstrap';
import PropTypes from 'prop-types';
import logger from 'sabio-debug';
import { BsInfoCircle } from 'react-icons/bs';
import { useNavigate } from 'react-router-dom';

const _logger = logger.extend('Event');

function Event(props) {
    _logger(props.eventProp);

    const event = props.eventProp;
    const navigate = useNavigate();

    const onEventDetailsClick = () => {
        const stateForTransport = { type: 'EVENT_CARD_DETAILS', payload: event };
        navigate(`/event/${event.id}/details`, { state: stateForTransport });
    };

    return (
        <Col className="col-xs-12 col-sm-12 col-md-4 col-lg-3 col-xl-3 d-flex align-items-stretch text-center">
            <Card className="shadow-lg p-3 bg-white rounded">
                <Card.Img src={event.imageUrl} />
                <Card.Body>
                    <Card.Text>
                        <strong>Position: </strong>
                        {event.title}
                    </Card.Text>
                    <Card.Text>
                        <strong>Details: </strong>
                        {event.description}{' '}
                    </Card.Text>
                    <Card.Text>
                        <strong>Organization: </strong>
                        {event.organization.name}
                    </Card.Text>
                    <div className="mb-2">
                        <Row className="mt-2">
                            <Col className="text-center">
                                <BsInfoCircle type="button" onClick={onEventDetailsClick} />
                            </Col>
                        </Row>
                    </div>
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
            name: PropTypes.string.isRequired,
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
