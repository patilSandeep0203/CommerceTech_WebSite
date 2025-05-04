import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { useNavigate } from 'react-router-dom';
import debug from 'sabio-debug';
import userProfilesService from '../../services/userProfilesService';
import * as toastr from 'toastr';
import { Modal, Row, Col } from 'react-bootstrap';
import ExternalLinks from '../../pages/auth/ExternalLinks';
import UserProfile from './UserProfile';
import DetailsInfo from './DetailsInfo';
import LatestBlog from './LatestBlog';
import RecentNewsletters from './RecentNewsletters';
import OrgHrsChart from './OrgHrsChart';
import JobTable from './JobTable';
import './profile.css';

const _loggerPage = debug.extend('UserProfile');

function UserDashboard(props) {
    _loggerPage('UserProfile', props);
    const navigate = useNavigate();

    const [aUserProfileData, setAUserProfileData] = useState({
        firstName: '',
        lastName: '',
        mi: '',
        location: '',
        avatarUrl: '',
        profession: [],
        dob: '',
        email: '',
        phone: '',
        licenseNumber: '',
        yearsOfExperience: 0,
        desiredHourlyRate: '',
        isActive: '',
    });
    const [dashboardData, setDashboardData] = useState({});
    const [loading, setLoading] = useState(false);
    const [showModal, setShowModal] = useState(false);
    const toggleModal = () => {
        setShowModal(!showModal);
    };

    useEffect(() => {
        _loggerPage('useEffect is firing');
        userProfilesService.getCurrentUserProfile().then(onGetCurrentUserSuccess).catch(onGetCurrentUserError);
        userProfilesService.getDashboard().then(onGetDashboardSuccess).catch(onGetDashboardError);
    }, []);

    _loggerPage('props current user', props.currentUser.id);

    const onGetCurrentUserSuccess = (response) => {
        _loggerPage('onGetCurrentUserSuccess', response);

        setAUserProfileData(() => {
            const aUserProfileData = response.item;

            return aUserProfileData;
        });

        setLoading(true);
    };

    const onGetCurrentUserError = (err) => {
        _loggerPage('onGetCurrentUserError', err);
        toastr.error('Unable to register profile');
        setLoading(false);
    };

    const onGetDashboardSuccess = (response) => {
        setDashboardData(() => {
            return response.item;
        });
    };

    _loggerPage('dashboardData', dashboardData);

    const onGetDashboardError = (err) => {
        _loggerPage('onGetDashboardError', err);
    };

    const onClickToEditProfile = (e) => {
        _loggerPage('navigate to edit the profile', e.currentTarget);
        const stateForSendingProfileData = { type: 'USERPROFILE_VIEW', payload: aUserProfileData };
        navigate(`/onboarding`, { state: stateForSendingProfileData });
    };

    const onClickTofollowers = (e) => {
        _loggerPage('navigate to edit the profile', e.currentTarget);
        navigate(`/followers`);
    };

    return (
        <React.Fragment>
            <Row>
                <Col>
                    <div className="page-title-box">
                        <div className="page-title-right"></div>
                        <h4 className="page-title-cnmpro">My Profile</h4>
                    </div>
                </Col>
            </Row>

            <Row>
                <Col sm={12}>
                    {loading && (
                        <UserProfile
                            userProfileData={aUserProfileData}
                            onEditClick={onClickToEditProfile}
                            onXLinkClick={toggleModal}
                            onFollowersClick={onClickTofollowers}
                        />
                    )}
                </Col>
            </Row>

            <Row>
                <Col xl={4}>
                    {loading && (
                        <DetailsInfo
                            userProfileData={aUserProfileData}
                            onXLinkClick={toggleModal}
                            onFollowersClick={onClickTofollowers}
                        />
                    )}
                    {dashboardData.blog && <LatestBlog blogData={dashboardData.blog} />}
                    {dashboardData.newsletters && <RecentNewsletters newslettersData={dashboardData.newsletters} />}
                </Col>
                {dashboardData.orgStats && dashboardData.timesheets && dashboardData.jobs ? (
                    <Col xl={8}>
                        <OrgHrsChart orgStats={dashboardData.orgStats} timesheets={dashboardData.timesheets} />
                        <JobTable jobData={dashboardData.jobs} />
                    </Col>
                ) : (
                    <Col>
                        <div className="text-center content-center">
                            <h4 className="text-muted"> You have not entered any timesheet entries to display.</h4>
                        </div>
                    </Col>
                )}
            </Row>

            {showModal && (
                <Modal show={showModal} onHide={toggleModal} size="lg" centered>
                    <Modal.Dialog style={{ display: 'contents' }}></Modal.Dialog>
                    <Modal.Body>
                        {' '}
                        <ExternalLinks toggleModal={toggleModal} />
                    </Modal.Body>
                </Modal>
            )}
        </React.Fragment>
    );
}

UserDashboard.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
        roles: PropTypes.arrayOf(PropTypes.string).isRequired,
    }),
};

export default UserDashboard;
