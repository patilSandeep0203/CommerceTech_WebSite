import './resources.css';
import { Row, Col, Button } from 'react-bootstrap';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import debug from 'sabio-debug';
import * as resourceService from '../../services/resourcesService';
import Resource from '../resources/Resource';
import Pagination from 'rc-pagination';
import locale from 'rc-pagination/lib/locale/en_US';
import 'rc-pagination/assets/index.css';
import swal from '@sweetalert/with-react';
import PropTypes from 'prop-types';
import ResourceCategories from './ResourceCategories';

const _logger = debug.extend('Resources');

function Resources(props) {
    const navigate = useNavigate();
    const [resourceData, setResourceData] = useState({
        arrayOfResources: [],
        resourceComponents: [],
        page: {
            pageIndex: 0,
            pageSize: 8,
            totalCount: 0,
            totalPages: 0,
        },
        search: '',
        isChanged: false,
    });

    const [role, setRole] = useState({
        isOrgAdmin: false,
        isAdmin: false,
    });

    false && _logger(resourceData.arrayOfResources);

    useEffect(() => {
        _logger('Firing useEffect for get resources');
        resourceService
            .getAllResources(resourceData.page.pageIndex, resourceData.page.pageSize)
            .then(onGetAllResourcesSuccess)
            .catch(onGetAllResourcesError);
        _logger('current role', props.currentUser.roles);
        if (props.currentUser.roles.includes('Org Admin')) {
            setRole((prevState) => {
                const newRole = { ...prevState };
                newRole.isOrgAdmin = true;
                return newRole;
            });
        } else if (props.currentUser.roles.includes('Admin')) {
            setRole((prevState) => {
                const newRole = { ...prevState };
                newRole.isAdmin = true;
                return newRole;
            });
        }
    }, [resourceData.isChanged]);

    const onDeleteResource = useCallback((id) => {
        resourceService.deleteResource(id).then(onDeleteSuccess).catch(onDeleteError);
    }, []);

    const onDeleteSuccess = (response) => {
        _logger('getDeleteSuccessHandler -->', response);

        setResourceData((prevState) => {
            const rData = { ...prevState, isChanged: true };

            _logger('rData ->', rData);

            const idxOf = rData.arrayOfResources.findIndex((resource) => {
                return parseInt(resource.id) === response;
            });

            const updatedArrayOfResources = [...prevState.arrayOfResources];

            if (idxOf >= 0) {
                updatedArrayOfResources.splice(idxOf, 1);
                rData.resourceComponents = updatedArrayOfResources.map(mapResource);
            }

            return rData;
        });

        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your resource has been deleted!',
            showConfirmButton: false,
            timer: 5000,
        });
    };

    const onDeleteError = (err) => {
        _logger('Delete error', err);
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Error occured with deletion.',
            icon: 'error',
        });
    };

    const mapResource = (resource) => {
        return (
            <Resource
                resourceProp={resource}
                onResourceClicked={onDeleteResource}
                user={props.currentUser}
                key={resource.id}
            />
        );
    };

    const onGetAllResourcesSuccess = (response) => {
        _logger('getAll response', response);
        let resourceArray = response.item.pagedItems;
        _logger('resource Item', resourceArray);

        setResourceData((prevState) => {
            let pd = { ...prevState };
            pd.arrayOfResources = resourceArray;
            pd.resourceComponents = resourceArray.map(mapResource);
            pd.page.pageIndex = response.item.pageIndex;
            pd.page.pageSize = response.item.pageSize;
            pd.page.totalCount = response.item.totalCount;
            pd.page.totalPages = response.item.totalPages;
            pd.isChanged = false;
            _logger('RESPONSE HERE', response);
            return pd;
        });
    };

    const onGetAllResourcesError = (err) => {
        _logger('Get resources error', err);
    };

    const onChange = (currentPage) => {
        _logger('CURRENT PAGE', currentPage);

        resourceService
            .getAllResources(currentPage - 1, resourceData.page.pageSize)
            .then(onGetAllResourcesSuccess)
            .catch(onGetAllResourcesError);
    };

    const navigateToAdd = () => {
        navigate(`/resources/new`);
    };

    const handleSubmit = useCallback((values) => {
        _logger('name to be searched', values);
        resourceService
            .searchResourceByKeyWord(values.page.pageIndex, values.page.pageSize, values.search)
            .then(onSearchSuccess)
            .catch(onSuccessError);
    }, []);

    const onSearchSuccess = (response) => {
        _logger('onSearchSuccess', response);

        let resourceArray = response.item.pagedItems;

        setResourceData((prevState) => {
            let pd = { ...prevState };
            pd.arrayOfResources = resourceArray;
            pd.resourceComponents = resourceArray.map(mapResource);
            pd.page.pageIndex = response.item.pageIndex;
            pd.page.pageSize = response.item.pageSize;
            pd.page.totalCount = response.item.totalCount;
            pd.page.totalPages = response.item.totalPages;
            _logger('RESPONSE HERE', response);
            return pd;
        });
    };

    const onSuccessError = (response) => {
        _logger('onSearchError', response);
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Error occured with search.',
            icon: 'error',
        });
    };

    const onGetByResourceCategory = (id) => {
        _logger('ResCat id -->', id);
        resourceService
            .getResourceByResourceCategoryId(id, resourceData.page.pageIndex, resourceData.page.pageSize)
            .then(onGetResByResCatSuccess)
            .catch(onGetResByResCatError);
    };

    const onGetResByResCatSuccess = (response) => {
        _logger('ResCatSuccess -->', response);
        let resourceArray = response.item.pagedItems;
        setResourceData((prevState) => {
            let pd = { ...prevState };
            pd.arrayOfResources = resourceArray;
            pd.resourceComponents = resourceArray.map(mapResource);
            pd.page.pageIndex = response.item.pageIndex;
            pd.page.pageSize = response.item.pageSize;
            pd.page.totalCount = response.item.totalCount;
            pd.page.totalPages = response.item.totalPages;
            _logger('RESPONSE HERE', response);
            return pd;
        });
    };

    const onGetResByResCatError = (error) => {
        _logger('ResCatError -->', error);
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Resource Category error.',
            icon: 'error',
        });
    };

    const clickReset = () => {
        setResourceData((prevState) => {
            const rData = { ...prevState };
            rData.isChanged = true;
            return rData;
        });
    };

    return (
        <React.Fragment>
            <div className="resources-min-height">
                <Row className="title-res-cnmpro">
                    <Col className="text-center">
                        <h1>Resources</h1>
                    </Col>
                </Row>
                {role.isOrgAdmin ? (
                    <Row className="align-item-center mb-2">
                        <Col className="col-2">
                            <ResourceCategories
                                currentUser={role}
                                onGetByResCat={onGetByResourceCategory}
                                resetClicked={clickReset}
                            />
                        </Col>
                        <Col>
                            <button
                                type="button"
                                id="newResource"
                                className="btn btn-primary btn-success"
                                onClick={navigateToAdd}>
                                Create a Resource
                            </button>
                        </Col>
                        <Col className="">
                            <Pagination
                                onChange={onChange}
                                current={resourceData.page.pageIndex + 1}
                                total={resourceData.page.totalCount}
                                locale={locale}
                                pageSize={resourceData.page.pageSize}
                                prevIcon="<<<"
                                nextIcon=">>>"
                                className="mb-2"
                            />
                        </Col>
                        <Col className="col-4">
                            <Formik enableReinitialize={true} initialValues={resourceData} onSubmit={handleSubmit}>
                                {() => (
                                    <Form>
                                        <div className="input-group mb-3">
                                            <Field
                                                type="text"
                                                className="form-control"
                                                placeholder="Search Resources"
                                                id="top-search"
                                                name="search"
                                            />
                                            <ErrorMessage
                                                name="name"
                                                component="div"
                                                className="has-error-res-cnmpro"
                                            />
                                            <Button className="btn-primary" type="submit">
                                                Search
                                            </Button>
                                        </div>
                                    </Form>
                                )}
                            </Formik>
                        </Col>
                    </Row>
                ) : (
                    <div></div>
                )}
                {role.isAdmin ? (
                    <Row>
                        <Col>
                            <ResourceCategories
                                currentUser={role}
                                resetClicked={clickReset}
                                onGetByResCat={onGetByResourceCategory}></ResourceCategories>
                        </Col>
                        <Col className="justify-content-end ml-3">
                            <Pagination
                                onChange={onChange}
                                current={resourceData.page.pageIndex + 1}
                                total={resourceData.page.totalCount}
                                locale={locale}
                                pageSize={resourceData.page.pageSize}
                                prevIcon="<<<"
                                nextIcon=">>>"
                                className="mb-2"
                            />
                        </Col>
                        <Col className="col-4">
                            <Formik enableReinitialize={true} initialValues={resourceData} onSubmit={handleSubmit}>
                                {() => (
                                    <Form>
                                        <div className="input-group mb-3">
                                            <Field
                                                type="text"
                                                className="form-control"
                                                placeholder="Search Resources"
                                                id="top-search"
                                                name="search"
                                            />
                                            <ErrorMessage
                                                name="name"
                                                component="div"
                                                className="has-error-res-cnmpro"
                                            />
                                            <Button className="btn-primary" type="submit">
                                                Search
                                            </Button>
                                        </div>
                                    </Form>
                                )}
                            </Formik>
                        </Col>
                    </Row>
                ) : (
                    <div></div>
                )}
                <Row>{resourceData.resourceComponents}</Row>
            </div>
        </React.Fragment>
    );
}
Resources.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string,
        id: PropTypes.number,
        isLoggedIn: PropTypes.bool,
        roles: PropTypes.arrayOf(PropTypes.string),
    }).isRequired,
};
export default Resources;
