import './events.css';
import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import debug from 'sabio-debug';
import { Row, Col, Card } from 'react-bootstrap';
import * as eventsService from '../../services/eventsService';
import EventMap from '../events/EventMap';

const _logger = debug.extend('EventDetails');

function EventDetails() {
    const load = useLocation();

    const [eventCardData, setEventCardData] = useState({
        id: 0,
        title: '',
        description: '',
        imageUrl: '',
        organization: {
            id: 0,
            organizationType: {
                id: 0,
                name: null,
            },
            name: '',
            headline: '',
            description: '',
            logo: '',
            phone: '',
            siteUrl: '',
            dateCreated: '',
            dateModified: '',
            createdBy: 0,
        },
        location: {
            id: 0,
            locationType: {
                id: 0,
                name: '',
            },
            lineOne: '',
            lineTwo: '',
            city: '',
            zip: '',
            state: {
                code: '',
                id: 0,
                name: '',
            },
            latitude: 0,
            longitude: 0,
            dateCreated: '',
            dateModified: '',
            createdBy: 0,
            modifiedBy: 0,
        },
        contactName: '',
        contactEmail: '',
        phone: '',
        siteUrl: '',
        dateCreated: '',
        dateModified: '',
        modifiedBy: 0,

        createdBy: {
            firstName: '',
            lastname: '',
        },
    });
    useEffect(() => {
        _logger('load', load);
        _logger('id', load.state.payload.id);
        const loadId = load.state.payload.id;
        eventsService.getEventById(loadId).then(GetEventByIdSuccess).catch(GetEventByIdError);
    }, []);

    const GetEventByIdSuccess = (response) => {
        _logger('response:', response);
        setEventCardData((prevState) => {
            let eData = { ...prevState };
            eData = response.item;
            return eData;
        });
        _logger('state', eventCardData);
    };
    const GetEventByIdError = (response) => {
        _logger(response);
    };

    return (
        <Row className="align-items-center mr-0 ml-0">
            <Card className="col-12 mt-3 mb-3 shadow">
                <Row>
                    <Col className="card-body col-6 ml-3">
                        <Row>
                            <Card.Title>{eventCardData.title}</Card.Title>
                            <Card.Text>
                                <strong>Event: </strong>
                                {eventCardData.description}
                            </Card.Text>
                            <Card.Text>
                                <strong>Organization: </strong>
                                {eventCardData.organization.name}
                            </Card.Text>
                            <Card.Text>
                                <strong>Organization Description: </strong> {eventCardData.organization.description}
                            </Card.Text>
                            <Card.Text>
                                <strong>Address: </strong>
                                {eventCardData.location.lineOne},{eventCardData.location.city},
                                {eventCardData.location.state.name}, {eventCardData.location.zip}
                            </Card.Text>
                            <Card.Text>
                                <strong>Contact Name: </strong>
                                {eventCardData.createdBy.firstName},{eventCardData.createdBy.lastName}
                            </Card.Text>
                            <Card.Text>
                                <strong>Contact Phone: </strong>
                                {eventCardData.organization.phone}
                            </Card.Text>
                            <Card.Text>
                                <strong>Date Created: </strong>
                                {eventCardData.dateCreated}
                            </Card.Text>
                        </Row>
                    </Col>
                    <Col className="col-6">
                        <Card.Img src={eventCardData.imageUrl} />
                    </Col>
                </Row>
            </Card>
            <EventMap></EventMap>
        </Row>
    );
}

export default EventDetails;
