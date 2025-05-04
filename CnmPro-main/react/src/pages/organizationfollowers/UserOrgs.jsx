import React, { useEffect, useState, useCallback } from 'react';
import debug from 'sabio-debug';
import orgFollowersService from '../../services/orgFollowersService';
import OrgComp from './OrgComp';
import toastr from 'toastr';
import InfiniteScroll from 'react-infinite-scroll-component';
import SimpleBar from 'simplebar-react';
import { Col, Row, Container } from 'react-bootstrap';
import * as organizationService from '../../services/organizationsService';
import OrgGetAll from './OrgGetAllComp';

function UserOrgs() {
    const [UserOrg, setUserOrg] = useState({
        arrayUserOrgs: [],
        usersComponents: [],
        hasMore: true,
        pageIndex: 0,
        pageSize: 5,
    });

    const [organizationData, setOrganizationData] = useState({
        arrayOfOrganizations: [],
        organizationComponents: [],
        hasMore: true,
        pageIndex: 0,
        pageSize: 5,
    });

    const _logger = debug.extend('userorgs');
    _logger('testing code');

    useEffect(() => {
        orgFollowersService
            .getOrgByUserId(UserOrg.pageIndex, UserOrg.pageSize)
            .then(onUserOrgPagSuccess)
            .catch(onUserOrgPagError);
        organizationService
            .GetAllOrganizations(organizationData.pageIndex, organizationData.pageSize)
            .then(onGetOrganizationSuccess)
            .catch(onGetOrganizationError);
    }, []);

    const onUserOrgPagSuccess = (response) => {
        if (response?.item?.pagedItems?.length > 0) {
            let arrayUserOrg = response.item.pagedItems;
            _logger('onUserOrgPagSuccess response', { arrayUserOrg });

            setUserOrg((prevState) => {
                const currentUserOrgs = { ...prevState };
                currentUserOrgs.arrayUserOrgs = arrayUserOrg;
                currentUserOrgs.hasMore = true;
                currentUserOrgs.usersComponents = currentUserOrgs.arrayUserOrgs.map(mapUserOrgs);
                _logger('setUserOrg testing code', UserOrg);
                return currentUserOrgs;
            });
        } else {
            _logger('onUserOrgPagSuccess else response', response);
        }
    };

    const onUserOrgPagError = (err) => {
        const _logger = debug.extend(err);
        _logger('onUserOrgPagError testing code', err);
    };

    const mapOrganization = (aOrganization) => {
        return <OrgGetAll organization={aOrganization} key={aOrganization.id} />;
    };

    const onGetOrganizationSuccess = (response) => {
        _logger(response);
        let arrayOfOrgs = response.data.item.pagedItems;
        _logger(arrayOfOrgs);

        setOrganizationData((prevState) => {
            const orgData = { ...prevState };
            orgData.arrayOfOrganizations = arrayOfOrgs;
            orgData.hasMore = true;
            const test = (orgData.organizationComponents = arrayOfOrgs.map(mapOrganization));
            _logger(test, 'test');
            return orgData;
        });
    };
    const onGetOrganizationError = (err) => {
        _logger('Get Organization error', err);
    };

    const onDeleteRequested = useCallback((userOrg, e) => {
        e.preventDefault();
        _logger('ondeletedrquested ID', userOrg.id, e, { userOrg, e });

        const deleteHandler = () => {
            _logger('deletehandler', userOrg.id);
            getDeleteSuccessHandler(userOrg.id);
        };

        orgFollowersService.deleteOrgFol(userOrg.id).then(deleteHandler).catch(onDeleteError);
    }, []);

    const getDeleteSuccessHandler = (idToBeDeleted) => {
        _logger('getDeleteSucceshandler', idToBeDeleted);
        toastr.success('Successly Deleted');

        setUserOrg((prevState) => {
            const newUserOrg = { ...prevState };
            newUserOrg.arrayUserOrgs = [...newUserOrg.arrayUserOrgs];

            const idxOf = newUserOrg.arrayUserOrgs.findIndex((userOrg) => {
                let result = false;
                if (userOrg.id === idToBeDeleted) {
                    result = true;
                }
                return result;
            });

            if (idxOf >= 0) {
                newUserOrg.arrayUserOrgs.splice(idxOf, 1);
                newUserOrg.usersComponents = newUserOrg.arrayUserOrgs.map(mapUserOrgs);
            }
            _logger('setOrg', newUserOrg);
            return newUserOrg;
        });
    };
    const onDeleteError = (err) => {
        toastr.error('failed error to delete');
        _logger('deleteById error', err);
    };

    const mapUserOrgs = (aUserOrg) => {
        return <OrgComp orgUser={aUserOrg} key={'ListA-' + aUserOrg.id} onDeleted={onDeleteRequested}></OrgComp>;
    };

    const [show, setShow] = React.useState(true);

    const onShowCLicked = (e) => {
        e.preventDefault();
        setShow(!show);
    };

    const onPageChange = () => {
        setUserOrg((prevState) => {
            const ugData = { ...prevState };
            ugData.pageIndex = ugData.pageIndex + 1;
            ugData.usersComponents = ugData.arrayUserOrgs.map(mapUserOrgs);
            _logger('onPageChange setData Data:', ugData);
            return ugData;
        });
    };

    const onPage = () => {
        setOrganizationData((prevState) => {
            const ugData = { ...prevState };
            ugData.pageIndex = ugData.pageIndex + 1;
            ugData.organizationComponents = ugData.arrayOfOrganizations.map(mapOrganization);
            _logger('onPageChange setData Data:', ugData);
            return ugData;
        });
    };

    return (
        <Container>
            <div className="col-sm-3 website" style={{ margin: '10px' }}>
                <a href="/userprofile" className="btn btn-primary">
                    Back Profile
                </a>
            </div>
            <Container>
                <div className="card">
                    <h4 className="card-header">My Network</h4>
                    <div className="card-body" style={{ marginLeft: '15%', marginBottom: '40px' }}>
                        <button
                            type="button"
                            className="btn btn-primary me-2"
                            style={{ margin: '10px', marginBottom: '40px' }}
                            onClick={onShowCLicked}>
                            {show ? 'Hide Organizations' : 'Show Organizations'}
                        </button>
                        <Row className="w-75 h-50">
                            <Col className="simplebar-content-wrapper">
                                {show && (
                                    <SimpleBar
                                        scrollableNodeProps={{ id: 'scrollableDiv' }}
                                        className="card-body py-0 user-list-container">
                                        <InfiniteScroll
                                            dataLength={UserOrg.usersComponents}
                                            next={onPageChange}
                                            loader={<h3>Loading...</h3>}
                                            hasMore={UserOrg.hasMore}
                                            scrollableTarget="scrollableDiv"
                                            endMessage={
                                                <p style={{ textAlign: 'center' }}>
                                                    <b>Could not find any more</b>
                                                </p>
                                            }>
                                            {UserOrg?.usersComponents}
                                        </InfiniteScroll>
                                    </SimpleBar>
                                )}
                            </Col>
                        </Row>
                    </div>
                </div>
            </Container>

            <Container>
                <Col className="simplebar-content-wrapper">
                    <div className="card">
                        <h4 className="card-header">Recommended Organizations</h4>
                        <div className="card-body" style={{ marginLeft: '15%', marginBottom: '40px' }}>
                            <SimpleBar
                                scrollableNodeProps={{ id: 'scrollableDiv' }}
                                className="card-body py-0 list-container">
                                <InfiniteScroll
                                    dataLength={organizationData.organizationComponents}
                                    next={onPage}
                                    loader={<h3>Loading...</h3>}
                                    hasMore={organizationData.hasMore}
                                    scrollableTarget="scrollableDiv"
                                    endMessage={
                                        <p style={{ textAlign: 'center' }}>
                                            <b>Could not find any more</b>
                                        </p>
                                    }>
                                    {organizationData?.organizationComponents}
                                </InfiniteScroll>
                            </SimpleBar>
                        </div>
                    </div>
                </Col>
            </Container>
        </Container>
    );
}

export default UserOrgs;
