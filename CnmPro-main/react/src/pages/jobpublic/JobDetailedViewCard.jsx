import React from 'react';
import PropTypes from 'prop-types';
import { Container, Row, Col } from 'react-bootstrap';
import './jobstyle.css';
import DynamicPdfCreator from '../dynamicpdf/DynamicPdfCreator';

const JobDetailedViewCard = (props) => {
    const jobObj = props.job;

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
                    content: jobObj.title,
                },
                {
                    type: 'Title',
                    content: jobObj.organization.name,
                },
                {
                    type: 'Text',
                    content: `Location: ${jobObj.location.city}`,
                },
                {
                    type: 'Image',
                    content: jobObj.organization.logo,
                },
                {
                    type: 'Title',
                    content: 'Description',
                },
                {
                    type: 'Text',
                    content: jobObj.description,
                },
                {
                    type: 'Title',
                    content: 'Requirements',
                },
                {
                    type: 'Text',
                    content: jobObj.requirements,
                },
            ],
        ],
    };

    const myPdfDoc = (
        <DynamicPdfCreator
            pdfContent={pdfContentData}
            pdfStyle={pdfStyleObj}
            fileName={`${jobObj.title}_${jobObj.organization.name}`}
        />
    );

    return (
        <Container className="card-job-details scrollbar-jobs-cnmpro">
            <Row className="card-body-job-details">
                <br />
                <Col className="card-job-details-image">
                    <img className="logo-img-jobs-cnmpro" src={jobObj.organization.logo} alt=""></img>
                </Col>
                <Col>
                    <h1 className="jobmapper-location-cnmpro">
                        <br />
                        Location:
                        <br />
                        {jobObj.location.city}, {jobObj.location.state.code}
                    </h1>
                </Col>
            </Row>
            <br />
            <div className="card-job-details-title">
                <strong>{jobObj.title} </strong>

                <br />
                <strong className="card-job-details-company-name">{jobObj.organization.name}</strong>
            </div>
            <br />
            <div className="card-job-details-description-cnmpro">
                {' '}
                <strong className="card-job-details-tag-cnmpro">Description</strong>
                <div>{jobObj.description}</div>
            </div>
            <br />
            <div className="card-job-details-requirements-cnmpro">
                {' '}
                <strong className="card-job-details-tag-cnmpro">Desired Qualifications</strong>
                <div>{jobObj.requirements}</div>
            </div>
            <br />
            <div className="text-center">{myPdfDoc}</div>
        </Container>
    );
};
JobDetailedViewCard.propTypes = {
    job: PropTypes.shape({
        id: PropTypes.number.isRequired,
        jobType: PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string.isRequired,
        }),
        title: PropTypes.string,
        description: PropTypes.string,
        requirements: PropTypes.string,
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

export default JobDetailedViewCard;
