import React from 'react';
import PropTypes from 'prop-types';
import { Container, Row, Col } from 'react-bootstrap';
import './eventstyle.css';
import DynamicPdfCreator from '../dynamicpdf/DynamicPdfCreator';

const EventDetailedViewCard = (props) => {
    const eventObj = props.event;

    const pdfStyleObj = {
        body: {
            paddingTop: 35,
            paddingBottom: 65,
            paddingHorizontal: 35,
        },

        text: {
            margin: 12,
            fontSize: 14,
            textAlign: 'justify',
        },
        image: {
            marginVertical: 15,
            marginHorizontal: 100,
        },
        title: {
            fontSize: 24,
            textAlign: 'center',
        },
    };

    const pdfContentData = {
        pages: [
            [
                {
                    type: 'Title',
                    content: eventObj.title,
                },
                {
                    type: 'Title',
                    content: eventObj.organization.name,
                },
                {
                    type: 'Text',
                    content: `Location: ${eventObj.location.city}`,
                },
                {
                    type: 'Image',
                    content: eventObj.organization.logo,
                },
                {
                    type: 'Title',
                    content: 'Description',
                },
                {
                    type: 'Text',
                    content: eventObj.description,
                },
                {
                    type: 'Title',
                    content: 'Requirements',
                },
                {
                    type: 'Image',
                    content: eventObj.imageUrl,
                },
            ],
        ],
    };

    const myPdfDoc = (
        <DynamicPdfCreator
            pdfContent={pdfContentData}
            pdfStyle={pdfStyleObj}
            fileName={`${eventObj.title}_${eventObj.organization.name}`}
        />
    );

    return (
        <Container className="card-event-details scrollbar-events-cnmpro">
            <Row className="card-body-event-details">
                <br />
                <Col className="card-event-details-image">
                    <img className="logo-img-events-cnmpro" src={eventObj.organization.logo} alt=""></img>
                </Col>
                <Col>
                    <h1 className="eventmapper-location-cnmpro">
                        <br />
                        Location:
                        <br />
                        {eventObj.location.city}, {eventObj.location.state.code}
                    </h1>
                </Col>
            </Row>
            <br />
            <div className="card-event-details-title">
                <strong>{eventObj.title} </strong>
                <br />
                <strong className="card-event-details-company-name">{eventObj.organization.name}</strong>
            </div>
            <br />
            <div className="card-event-details-description-cnmpro">
                {' '}
                <strong className="card-event-details-tag-cnmpro">Description</strong>
                <div>{eventObj.description}</div>
            </div>
            <br />
            <br />
            <Col className="card-event-details-image">
                <img className="logo-img-events-cnmpro" src={eventObj.imageUrl} alt=""></img>
            </Col>
            <br />
            <div className="text-center">{myPdfDoc}</div>
        </Container>
    );
};
EventDetailedViewCard.propTypes = {
    event: PropTypes.shape({
        id: PropTypes.number.isRequired,
        eventType: PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string.isRequired,
        }),
        title: PropTypes.string,
        description: PropTypes.string.isRequired,
        imageUrl: PropTypes.string.isRequired,
        location: PropTypes.shape({
            id: PropTypes.number.isRequired,
            locationType: PropTypes.shape({
                id: PropTypes.number.isRequired,
                name: PropTypes.string,
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

export default EventDetailedViewCard;
