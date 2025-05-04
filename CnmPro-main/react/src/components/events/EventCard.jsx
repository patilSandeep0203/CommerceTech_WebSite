import React, { useEffect, useState } from 'react';
import * as eventsService from '../../services/eventsService';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import moment from 'moment/moment';
import logger from 'sabio-debug';
import PropTypes from 'prop-types';

const _logger = logger.extend('Event');

function EventCard(props) {
    const [eventFormData, setEventFormData] = useState({
        eventTypeId: '',
        title: '',
        description: '',
        imageUrl: '',
        organizationId: '',
        locationTypeId: '',
        lineOne: '',
        lineTwo: '',
        city: '',
        zip: '',
        stateId: '',
        latitude: '',
        longitude: '',
        isMultiDay: true,
        createdBy: props.currentUser.id,
    });

    useEffect(() => {
        eventsService.getAll(0, 1).then(onGetEventSuccess).catch(onGetEventError);
    }, []);

    const onGetEventSuccess = (response) => {
        _logger('event response success', response);

        const eventResponse = response.item.pagedItems[0];

        setEventFormData((prevState) => {
            const newState = { ...prevState };
            newState.title = eventResponse.title;
            newState.description = eventResponse.description;
            newState.imageUrl = eventResponse.imageUrl;
            newState.organizationId = eventResponse.organizationId;
            newState.locationTypeId = eventResponse.locationTypeId;
            newState.lineOne = eventResponse.lineOne;
            newState.city = eventResponse.city;
            newState.zip = eventResponse.zip;
            newState.stateId = eventResponse.stateId;
            newState.latitude = eventResponse.latitude;
            newState.longitude = eventResponse.longitude;
            newState.isMultiDay = eventResponse.isMultiDay;
            newState.createdBy = eventResponse.createdBy;
            return newState;
        });

        _logger('new state', eventFormData);
    };

    const onGetEventError = (error) => {
        _logger('event response error', error);
    };

    return (
        <>
            <Card className="d-block">
                <img
                    className="card-img-top orgDash-cover"
                    src={eventFormData.imageUrl}
                    alt=""
                    style={{ height: '18rem' }}
                />
                <div className="card-img-overlay">
                    <div className="bg-success badge">Latest Event</div>
                </div>
                <Card.Body className="position-relative">
                    <h4 className="mt-0">
                        <Link to={`/eventdetails/${eventFormData.id}`} className="text-title">
                            {eventFormData.title}
                        </Link>
                    </h4>
                    <p className="text-muted font-13 my-1">{eventFormData.content}...</p>
                    <p className="mb-1">
                        <span className="pe-2 text-nowrap mb-2 d-inline-block">
                            <i className="mdi mdi-calendar text-muted me-1"></i>
                            <b className="me-1">Published</b>
                            {moment(eventFormData.datePublish).format('MM/DD/YY')}
                        </span>
                    </p>
                    <Link to={`/eventdetails/${eventFormData.id}`}>
                        <button className="btn btn-primary">View More</button>
                    </Link>
                </Card.Body>
            </Card>
        </>
    );
}

EventCard.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
        roles: PropTypes.arrayOf(PropTypes.string).isRequired,
    }),
};

export default EventCard;
