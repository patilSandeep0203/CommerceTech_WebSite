import React from 'react';
import EventDetailed from './EventDetailedViewCard';
import { Container, Row, Col} from 'react-bootstrap';
import PropTypes from 'prop-types';
import './eventstyle.css';
import { useNavigate } from 'react-router-dom';
import debug from 'sabio-debug';
const _logger = debug.extend('eventMapper');

function EventMapper(props) {
    const aEvent = props.event;

    const navigate = useNavigate();

    const showEventDetails = (e) => {
        e.preventDefault();
        props.onClickShowDetail(aEvent);
    };

    const createEvent= (e) => {
        e.preventDefault();
        navigateToEvent(aEvent);
        _logger(aEvent, 'response for aEventV2');
    };

    const navigateToEvent = (event) => {
        const eventState = { type: 'eventObject', payload: { ...event } };
        navigate('/proposals', { state: eventState });
    };

    return (
        <Container className="event-card-container-cnmpro" onClick={showEventDetails}>
            <Row className="event-card-body" onClick={showEventDetails}>
                <br />
                <Col className="event-card-image-col">
                    <div className="company-logo-events-cnmpro">
                        <img className="logo-img-events-cnmpro" src={aEvent.organization.logo} alt=""></img>
                    </div>
                </Col>
                <Col>
                    <h1 className="eventmapper-location-cnmpro">
                        <br />
                        Location:
                        <br />
                        {aEvent.location.city}, {aEvent.location.state.code}
                    </h1>
                </Col>
                <div className="event-title">
                    <strong>{aEvent.title} </strong>
                </div>

                <br />
                <div>
                    <strong className="card-event-details-tag-cnmpro">Description</strong>
                    <h1 className="eventmapper-description-cnmpro">{aEvent.description.substring(0, 200)}...</h1>
                </div>
                <br />
                <div className="event-buttons-container">
                    <button type="button" className="proposal-card-button" onClick={createEvent}>
                        Create Event
                    </button>
                    <button type="button" className="event-card-button event-button" onClick={showEventDetails}>
                        View Event
                    </button>
                </div>
                <br />
                <Col className="card-event-details-image">
                    <img className="logo-img-events-cnmpro" src={aEvent.imageUrl} alt=""></img>
                </Col>
                <br />
            </Row>
        </Container>
    );
}

EventMapper.propTypes = {
    onClickShowDetail(eventObj) {
        <EventDetailed event={eventObj} key={eventObj.id} />;
    },
    event: PropTypes.shape({
        id: PropTypes.number.isRequired,
        eventType: PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string.isRequired,
        }),
        title: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        imageUrl: PropTypes.string.isRequired,
        location: PropTypes.shape({
            id: PropTypes.number.isRequired,
            locationType: PropTypes.shape({
                id: PropTypes.number.isRequired,
                name: PropTypes.string.isRequired,
            }),
            lineOne: PropTypes.string.isRequired,
            lineTwo: PropTypes.string.isRequired,
            city: PropTypes.string.isRequired,
            zip: PropTypes.string.isRequired,
            state: PropTypes.shape({
                id: PropTypes.number.isRequired,
                name: PropTypes.string.isRequired,
                code: PropTypes.string.isRequired,
            }),
            latitude: PropTypes.number.isRequired,
            longitude: PropTypes.number.isRequired,
        }),
        organization: PropTypes.shape({
            id: PropTypes.number.isRequired,
            organizationType: PropTypes.shape({
                id: PropTypes.number.isRequired,
                name: PropTypes.string.isRequired,
            }),
            name: PropTypes.string.isRequired,
            headline: PropTypes.string.isRequired,
            description: PropTypes.string.isRequired,
            logo: PropTypes.string.isRequired,
            phone: PropTypes.string.isRequired,
            siteUrl: PropTypes.string.isRequired,
            dateCreated: PropTypes.string.isRequired,
            dateModified: PropTypes.string.isRequired,
        }),
    }),
};

export default React.memo(EventMapper);
