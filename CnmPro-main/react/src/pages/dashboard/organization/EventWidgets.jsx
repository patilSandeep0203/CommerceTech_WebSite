import React from 'react';
import { Card, Col } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import '../../../assets/scss/custom/plugins/icons/_materialdesignicons.scss';
import '../../dashboard/organization/orgdash.css';
import PropTypes from 'prop-types';

const EventWidgets = () => {
    return (
        <React.Fragment>
            <Col className="ps-0 pt-2">
                <Link to="/admin/organization/events">
                    <Card className="cta-box  text-black orgDash-mb-0 p-2 orgDash-link-hover h-100 orgDash-box-height">
                        <Card.Body>
                            <div className="orgDash-d-flex align-items-start align-items-center">
                                <div className="w-100 overflow-hidden">
                                    <div className="avatar-sm">
                                        <span className="avatar-title bg-info-lighten text-info rounded">
                                            <i className="mdi mdi-watch font-24"></i>
                                        </span>
                                    </div>
                                    <h3 className="m-0 fw-normal cta-box-title">
                                        View <b>Events</b> <i className="mdi mdi-arrow-right"></i>
                                    </h3>
                                </div>
                            </div>
                        </Card.Body>
                    </Card>
                </Link>
            </Col>
            <Col className="ps-0 pt-2">
                <Link to="/admin/organization/event/add">
                    <Card className="cta-box text-black orgDash-mb-0 p-2 link-hover orgDash-link-hover h-100 orgDash-box-height">
                        <Card.Body>
                            <div className="d-flex align-items-start align-items-center">
                                <div className="w-100 overflow-hidden">
                                    <div className="avatar-sm">
                                        <span className="avatar-title bg-info-lighten text-info rounded">
                                            <i className="mdi mdi-plus font-24"></i>
                                        </span>
                                    </div>
                                    <h3 className="m-0 fw-normal cta-box-title">
                                        Create a <b>Event</b> <i className="mdi mdi-arrow-right"></i>
                                    </h3>
                                </div>
                            </div>
                        </Card.Body>
                    </Card>
                </Link>
            </Col>
        </React.Fragment>
    );
};

EventWidgets.propTypes = {
    orgId: PropTypes.number.isRequired,
};

export default EventWidgets;
