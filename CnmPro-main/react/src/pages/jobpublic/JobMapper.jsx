import React from 'react';
import JobDetailed from './JobDetailedViewCard';
import { Container, Row, Col } from 'react-bootstrap';
import PropTypes from 'prop-types';
import './jobstyle.css';
import { useNavigate } from 'react-router-dom';
import debug from 'sabio-debug';
const _logger = debug.extend('jobMapper');

function JobMapper(props) {
    const aJob = props.job;

    const navigate = useNavigate();

    const showJobDetails = (e) => {
        e.preventDefault();
        props.onClickShowDetail(aJob);
    };

    const createProposal = (e) => {
        e.preventDefault();
        navigateToProposal(aJob);
        _logger(aJob, 'response for aJobV2');
    };

    const navigateToProposal = (job) => {
        const jobState = { type: 'jobObject', payload: { ...job } };
        navigate('/proposals', { state: jobState });
    };

    const goToCompanySite = () => {};
    return (
        <Container className="job-card-container-cnmpro" onClick={showJobDetails}>
            <Row className="job-card-body" onClick={showJobDetails}>
                <br />
                <Col className="job-card-image-col">
                    <div className="company-logo-jobs-cnmpro">
                        <img className="logo-img-jobs-cnmpro" src={aJob.organization.logo} alt=""></img>
                    </div>
                </Col>
                <Col>
                    <h1 className="jobmapper-location-cnmpro">
                        <br />
                        Location:
                        <br />
                        {aJob.location.city}, {aJob.location.state.code}
                    </h1>
                </Col>
                <div className="job-title">
                    <strong>{aJob.title} </strong>
                </div>

                <br />
                <div>
                    <strong className="card-job-details-tag-cnmpro">Description</strong>
                    <h1 className="jobmapper-description-cnmpro">{aJob.description.substring(0, 200)}...</h1>
                </div>

                <br />
                <div>
                    <strong className="card-job-details-tag-cnmpro">Desired Qualifications</strong>
                    <h1 className="jobmapper-requirements-cnmpro">{aJob.requirements.substring(0, 150)}...</h1>
                </div>

                <br />
                <div className="job-buttons-container mb-3">
                    <button type="button" className="proposal-card-button" onClick={createProposal}>
                        Create Proposal
                    </button>

                    <button type="button" className="job-card-button job-button" onClick={showJobDetails}>
                        View Job
                    </button>
                    <button type="button" className="job-card-button job-button" onClick={goToCompanySite}>
                        Apply Now
                    </button>
                </div>
            </Row>
        </Container>
    );
}

JobMapper.propTypes = {
    onClickShowDetail(jobObj) {
        <JobDetailed job={jobObj} key={jobObj.id} />;
    },
    job: PropTypes.shape({
        id: PropTypes.number.isRequired,
        jobType: PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string.isRequired,
        }),
        title: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        requirements: PropTypes.string.isRequired,
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
            description: PropTypes.string,
            logo: PropTypes.string.isRequired,
            phone: PropTypes.string.isRequired,
            siteUrl: PropTypes.string.isRequired,
            dateCreated: PropTypes.string.isRequired,
            dateModified: PropTypes.string.isRequired,
        }),
    }),
};

export default React.memo(JobMapper);
