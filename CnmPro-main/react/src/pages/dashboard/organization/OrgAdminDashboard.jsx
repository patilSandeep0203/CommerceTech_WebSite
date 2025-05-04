import React, { useEffect, useState } from 'react';
import { GetOrganizationByCreatedBy } from '../../../services/organizationsService';
import * as stripeService from '../../../services/stripeServices';
import logger from 'sabio-debug';
import PropTypes from 'prop-types';
import BlogCard from './BlogCard';
import NewsletterCard from './NewsletterCard';
import JobOrgStatistics from './JobOrgStatistics';
import { Row, Col, Card, Stack } from 'react-bootstrap';
import toastr from 'toastr';
import statsImg from '../../../assets/images/email-campaign.svg';
import './orgdash.css';
import ProposalCharts from './ProposalCharts';
import orgAdminDashServices from '../../../services/orgAdminDashboardService';
import ProposalOrgStatistics from './ProposalOrgStatistics';
import { useNavigate } from 'react-router-dom';
import EventWidgets from './EventWidgets';

const _logger = logger.extend('OrgAdminDash');

function OrgDashboard(props) {
    const navigate = useNavigate();
    const [orgId, setOrgId] = useState(0);
    const [totalProposals, setProposals] = useState(0);
    const [adminData, setAdminData] = useState({
        name: '',
        logo: '',
        organizationType: '',
        subscriptionName: '',
        subscriptionStart: '',
    });
    const [subInfo, setSubInfo] = useState({
        subscriptionName: '',
        subscriptionStart: '',
    });
    const [orgData, setOrgData] = useState();
    const [isEditingOrg, setIsEditingOrg] = useState(false);

    false && _logger('admindata', adminData);

    useEffect(() => {
        GetOrganizationByCreatedBy(props.currentUser.id).then(getOrganizationSuccess).catch(getOrganizationError);
        stripeService.getSubStatusById(props.currentUser.id).then(getSubStatusSuccess).catch(getSubStatusError);
    }, []);

    useEffect(() => {
        if (orgId !== 0) {
            orgAdminDashServices
                .getProposalsByOrganizationPaginate(orgId, 0, 1)
                .then(getProposalsSuccess)
                .catch(getProposalsError);
        }
    }, [orgId]);

    useEffect(() => {
        if (isEditingOrg === true) {
            const state = { type: 'ORG_EDIT', payload: orgData };
            navigate(`/orgadmin/edit/${orgData.id}`, { state });
        }
    }, [isEditingOrg]);

    const getProposalsSuccess = (response) => {
        setProposals(response.item.totalCount);
    };

    const getProposalsError = (response) => {
        _logger('error', response);
        toastr.error("Couldn't load proposals");
    };

    const getOrganizationSuccess = (response) => {
        _logger('getorganizationsuccess', response);
        const organization = response.data.item;
        const orgId = response.data.item.id;

        setOrgId(orgId);

        setOrgData(organization);

        setAdminData((prevState) => {
            const newState = { ...prevState };
            newState.name = organization.name;
            newState.logo = organization.logo;
            newState.organizationType = organization.organizationType.name;

            return newState;
        });
    };

    const getSubStatusSuccess = (response) => {
        setSubInfo((prevState) => {
            const newState = { ...prevState };
            newState.subscriptionName = response.item.name;
            newState.subscriptionStart = response.item.subscriptionStartDate.substring(0, 10);

            return newState;
        });
    };

    _logger('SUBSCRIPTION Name UPDATED --->>>', subInfo.subscriptionName);
    _logger('SUBSCRIPTION Date UPDATED --->>>', subInfo.subscriptionStart);

    const getSubStatusError = (error) => {
        _logger('get subscriptoin error', error);
        toastr.error('Subscription info not available');
    };

    const getOrganizationError = (error) => {
        _logger('getorganizationerror', error);
    };

    const editOrganization = (event) => {
        event.preventDefault();
        setIsEditingOrg(true);
    };

    return (
        <React.Fragment>
            <Row className="ms-1 mt-3 orgDash-mb-2">
                <Col>
                    <h2 className="orgDash-color-black"> {adminData.name} Dashboard </h2>
                    <button className="btn btn-warning" onClick={editOrganization}>
                        Edit Organization
                    </button>
                </Col>
            </Row>

            <Row xl={4} lg={4}>
                <JobOrgStatistics orgId={orgId} />
            </Row>

            <Row xl={2} lg={2}>
                <Col className="p-1">
                    <ProposalCharts orgId={orgId} />
                </Col>
                <Stack className="p-1">
                    <ProposalOrgStatistics orgId={orgId} totalProposals={totalProposals} />
                    <EventWidgets />
                </Stack>
            </Row>
            <Row xl={2} lg={2}>
                <Col className="p-1">
                    <NewsletterCard />
                </Col>
                <Col className="p-1">
                    <BlogCard />
                </Col>
            </Row>

            <Row className="p-1">
                <Card className="cta-box overflow-hidden">
                    <Card.Body>
                        <div className="orgDash-d-flex align-items-center">
                            <div>
                                <h3 className="m-0 fw-normal cta-box-title">
                                    Subscribe to enhance your <b>Organization</b> for better outreach{' '}
                                    <i className="mdi mdi-arrow-right"></i>
                                </h3>
                            </div>
                            <a href="/subscriptions">
                                <img className="ms-3" src={statsImg} width="92" alt="Generic placeholder" />
                            </a>
                        </div>
                    </Card.Body>
                </Card>
            </Row>
        </React.Fragment>
    );
}

OrgDashboard.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
        roles: PropTypes.arrayOf(PropTypes.string).isRequired,
    }),
};

export default OrgDashboard;
