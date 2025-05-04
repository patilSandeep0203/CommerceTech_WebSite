import React from 'react';
import { Card, Row, Col } from 'react-bootstrap';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';

function ProposalOrgStatistics({ orgId, totalProposals }) {
    return (
        <>
            <Row>
                <Col className="ps-2 pe-0">
                    <Link to={`/admin/organization/dashboard/proposals/${orgId}`}>
                        <Card className="cta-box  text-black orgDash-mb-0 p-2 orgDash-link-hover orgDash-box-height">
                            <Card.Body>
                                <div className="orgDash-d-flex align-items-start align-items-center">
                                    <div className="overflow-hidden">
                                        <div className="avatar-sm">
                                            <span className="avatar-title bg-primary-lighten text-primary rounded">
                                                <i className="mdi mdi-file-document-edit font-24"></i>
                                            </span>
                                        </div>
                                        <h3 className="m-0 fw-normal cta-box-title">
                                            View <b>Organization</b> Proposals <i className="mdi mdi-arrow-right"></i>
                                        </h3>
                                    </div>
                                </div>
                            </Card.Body>
                        </Card>
                    </Link>
                </Col>
                <Col>
                    <Card className="tilebox-one orgDash-mb-0 p-2  orgDash-box-height">
                        <Card.Body>
                            <h4 className="mt-0 me-2 orgDash-color-black">Total Proposals</h4>
                            <h2 className="my-2">{totalProposals}</h2>
                            <p className="orgDash-mb-0 text-muted">
                                <span className="text-danger me-2">
                                    <span className="mdi mdi-arrow-down-bold"></span> 1.08%
                                </span>
                                <span className="text-nowrap">Since previous week</span>
                            </p>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </>
    );
}

ProposalOrgStatistics.propTypes = {
    orgId: PropTypes.number.isRequired,
    totalProposals: PropTypes.number.isRequired,
};

export default ProposalOrgStatistics;
