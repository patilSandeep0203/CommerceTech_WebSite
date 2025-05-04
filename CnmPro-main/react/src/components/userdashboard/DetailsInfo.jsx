// @flow
import React from 'react';
import { Card } from 'react-bootstrap';
import PropTypes from 'prop-types';
import debug from 'sabio-debug';

const _logger = debug.extend('DetailsInfo');
const DetailsInfo = (props) => {
    _logger(props.userProfileData);

    const exLinksClicked = (e) => {
        _logger('Send Files That Are Deleted Clicked');
        e.preventDefault();
        props.onXLinkClick(e);
    };

    const followersClicked = (e) => {
        _logger('Send Files That Are Deleted Clicked');
        e.preventDefault();
        props.onFollowersClick(e);
    };

    return (
        <Card>
            <Card.Body>
                <div>
                    <h4 className="header-title text-black font-22 mt-1 mb-2">Additional User Information</h4>
                    <p className="font-20 text-dark">
                        Welcome to my Profile. My name is {''}
                        {props?.userProfileData.firstName}. I specialize in overseeing contruction sites and managing
                        workers and logistics. My dream is to build the tallest skyscraper!
                    </p>
                </div>

                <hr />

                <div className="text-start">
                    <p className="text-black">
                        <strong> DOB :</strong>
                        <span className="ms-2">{props.userProfileData.dob.substring(0, 10)}</span>
                    </p>

                    <p className="text-black">
                        <strong>Experience (Yrs) :</strong>
                        <span className="ms-2">{props.userProfileData.yearsOfExperience}</span>
                    </p>

                    <p className="text-black">
                        <strong>Location :</strong>
                        <span className="ms-2">
                            {props?.userProfileData.location?.city},{props?.userProfileData.location?.state?.code}
                        </span>
                    </p>

                    <p className="text-black font-18">
                        <strong>Desired Rate (Hr) :</strong>
                        <span className="ms-2">
                            {'$'}
                            {props.userProfileData.desiredHourlyRate}
                        </span>
                    </p>
                    <Card.Link onClick={exLinksClicked}>External Links</Card.Link>
                    <Card.Link onClick={followersClicked}>Followers</Card.Link>
                </div>
            </Card.Body>
        </Card>
    );
};

DetailsInfo.propTypes = {
    userProfileData: PropTypes.shape({
        firstName: PropTypes.string,
        dob: PropTypes.string,
        yearsOfExperience: PropTypes.string,
        desiredHourlyRate: PropTypes.string,
        location: PropTypes.shape({
            city: PropTypes.string,
            state: PropTypes.shape({
                code: PropTypes.string,
            }).isRequired,
        }).isRequired,
    }).isRequired,
    onXLinkClick: PropTypes.func.isRequired,
    onFollowersClick: PropTypes.func.isRequired,
};
export default DetailsInfo;
