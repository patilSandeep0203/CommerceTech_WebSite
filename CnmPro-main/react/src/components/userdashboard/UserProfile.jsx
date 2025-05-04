// @flow
import React, { useState } from 'react';
import { Card, Row, Col } from 'react-bootstrap';
import './profile.css';
import PropTypes from 'prop-types';
import debug from 'sabio-debug';

const _logger = debug.extend('UserProfile');

const UserProfile = (props) => {
    _logger(props.userProfileData);
    const [data] = useState(props.userProfileData);

    const editClicked = (e) => {
        _logger('Send Files That Are Deleted Clicked');
        e.preventDefault();
        props.onEditClick(e);
    };

    return (
        { data } && (
            <Card className="bg-white">
                <Card.Body className="profile-user-box">
                    <Row>
                        <Col sm={8}>
                            <Row className="align-items-center">
                                <Col className="col-auto">
                                    <div className="avatar-lg-center">
                                        <img
                                            src={props?.userProfileData.avatarUrl}
                                            style={{ height: '100px' }}
                                            alt=""
                                            className="rounded-circle img-thumbnail"
                                        />
                                    </div>
                                </Col>
                                <Col>
                                    <div>
                                        <h4 className="mt-1 mb-1 text-black">
                                            {props?.userProfileData.firstName} {props?.userProfileData.lastName}
                                        </h4>
                                        <p className="mt-1 mb-1 font-20 text-black"> Civil Engineer</p>
                                        <div className="select-form-profile">
                                            <p className="mt-1 mb-1 text-black">
                                                <strong>Mobile:</strong>
                                                <span className="ms-1">{props?.userProfileData.phone}</span>
                                            </p>

                                            <p className="mt-1 mb-1 text-black">
                                                <strong>Email:</strong>
                                                <span className="ms-0"> {props?.userProfileData.email}</span>
                                            </p>
                                        </div>
                                    </div>
                                </Col>
                            </Row>
                        </Col>

                        <Col sm={4}>
                            <div className="text-align-left mt-sm-0 mt-3 text-sm-end">
                                <button type="button" className="bg-light btn userprofile-multiple-button text-black" onClick={editClicked}>
                                    Edit Profile
                                </button>
                            </div>
                        </Col>
                    </Row>
                </Card.Body>
            </Card>
        )
    );
};

UserProfile.propTypes = {
    userProfileData: PropTypes.shape({
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        avatarUrl: PropTypes.string,
        email: PropTypes.string,
        phone: PropTypes.string,
    }).isRequired,
    onEditClick: PropTypes.func.isRequired,
    onXLinkClick: PropTypes.func.isRequired,
    onFollowersClick: PropTypes.func.isRequired,
};

export default UserProfile;
