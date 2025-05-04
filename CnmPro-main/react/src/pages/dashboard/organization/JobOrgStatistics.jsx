// @flow
import React, { useState, useEffect } from 'react';
import { Card, Col } from 'react-bootstrap';
import '../../../assets/scss/custom/plugins/icons/_materialdesignicons.scss';
import '../../../assets/scss/custom/plugins/icons/_unicons.scss';
import logger from 'sabio-debug';
import orgAdminDashServices from '../../../services/orgAdminDashboardService';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import toastr from 'toastr';

const _logger = logger.extend('OrgAdminChart');

const JobOrgStatistics = ({ orgId }) => {
    const [jobActiveData, setJobActiveData] = useState({
        activeJobCount: 0,
        inactiveCount: 0,
    });

    useEffect(() => {
        orgAdminDashServices.getJobsByActiveStatus(orgId).then(onGetJobTypeSucccess).catch(onGetJobTypeError);
    }, [orgId]);

    const onGetJobTypeSucccess = (response) => {
        _logger('Success', response);
        setJobActiveData({ ...response.item });
    };

    const onGetJobTypeError = (response) => {
        _logger('Error', response);
        toastr.error("Couldn't load job types");
    };

    return (
        <>
            <Col className="p-1">
                <Card className="tilebox-one m-0 p-2 orgDash-box-height">
                    <Card.Body>
                        <h4 className="mt-0 me-2 orgDash-color-black">Active Jobs</h4>
                        <h2 className="my-2">{jobActiveData.activeJobCount}</h2>
                        <p className="orgDash-mb-0 text-muted">
                            <span className="text-danger me-2">
                                <span className="mdi mdi-arrow-down-bold"></span> 1.08%
                            </span>
                            <span className="text-nowrap">Since previous week</span>
                        </p>
                    </Card.Body>
                </Card>
            </Col>

            <Col className="p-1">
                <Card className="tilebox-one orgDash-mb-0 p-2 mx-0 orgDash-box-height">
                    <Card.Body>
                        <h4 className="mt-0 me-2 orgDash-color-black">Inactive Jobs</h4>
                        <h2 className="my-2">{jobActiveData.inactiveCount}</h2>
                        <p className="orgDash-mb-0 text-muted">
                            <span className="text-danger me-2">
                                <span className="mdi mdi-arrow-down-bold"></span> 1.08%
                            </span>
                            <span className="text-nowrap">Since previous week</span>
                        </p>
                    </Card.Body>
                </Card>
            </Col>

            <Col className="p-1">
                <Link to={`/admin/organization/dashboard/jobs/${orgId}`}>
                    <Card className="cta-box  text-black orgDash-mb-0 p-2 orgDash-link-hover h-100 orgDash-box-height">
                        <Card.Body>
                            <div className="orgDash-d-flex align-items-start align-items-center">
                                <div className="w-100 overflow-hidden">
                                    <div className="avatar-sm">
                                        <span className="avatar-title bg-success-lighten text-success rounded">
                                            <i className="mdi mdi-wallet-travel font-24"></i>
                                        </span>
                                    </div>
                                    <h3 className="m-0 fw-normal cta-box-title">
                                        View <b>Organization</b> Jobs <i className="mdi mdi-arrow-right"></i>
                                    </h3>
                                </div>
                            </div>
                        </Card.Body>
                    </Card>
                </Link>
            </Col>

            <Col className="p-1">
                <Link to="/admin/organization/job/add">
                    <Card className="cta-box text-black orgDash-mb-0 p-2 orgDash-link-hover h-100 orgDash-box-height">
                        <Card.Body>
                            <div className="d-flex align-items-start align-items-center">
                                <div className="w-100 overflow-hidden">
                                    <div className="avatar-sm">
                                        <span className="avatar-title bg-success-lighten text-success rounded">
                                            <i className="mdi mdi-plus font-24"></i>
                                        </span>
                                    </div>
                                    <h3 className="m-0 fw-normal cta-box-title">
                                        Create a <b>Job</b> Listing <i className="mdi mdi-arrow-right"></i>
                                    </h3>
                                </div>
                            </div>
                        </Card.Body>
                    </Card>
                </Link>
            </Col>
        </>
    );
};

JobOrgStatistics.propTypes = {
    orgId: PropTypes.number.isRequired,
};

export default JobOrgStatistics;
