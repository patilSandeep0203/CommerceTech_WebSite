import './orgdash.css';
import { Row, Col, Button } from 'react-bootstrap';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import React, { useState, useEffect, useCallback } from 'react';
import debug from 'sabio-debug';
import * as eventsService from '../../../services/eventsService';
import OrgEvent from '../../../components/events/OrgEvent';
import Pagination from 'rc-pagination';
import locale from 'rc-pagination/lib/locale/en_US';
import 'rc-pagination/assets/index.css';
import swal from '@sweetalert/with-react';
import PropTypes from 'prop-types';

const _logger = debug.extend('Events');

function OrgEvents(props) {
    const [eventData, setEventData] = useState({
        arrayOfEvents: [],
        eventsComponents: [],
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

    false && _logger(eventData.arrayOfEvents);

    useEffect(() => {
        _logger('Firing useEffect for get events');
        eventsService
            .getAll(eventData.page.pageIndex, eventData.page.pageSize)
            .then(onGetAllSuccess)
            .catch(onGetAllError);
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
    }, [eventData.isChanged]);

    const onDeleteEvent = useCallback((id) => {
        eventsService.deleteById(id).then(onDeleteSuccess).catch(onDeleteError);
    }, []);

    const onDeleteSuccess = (response) => {
        _logger('getDeleteSuccessHandler -->', response);

        setEventData((prevState) => {
            const rData = { ...prevState, isChanged: true };

            _logger('rData ->', rData);

            const idxOf = rData.arrayOfEvents.findIndex((event) => {
                return parseInt(event.id) === response;
            });

            const updatedArrayOfEvents = [...prevState.arrayOfEvents];

            if (idxOf >= 0) {
                updatedArrayOfEvents.splice(idxOf, 1);
                rData.eventComponents = updatedArrayOfEvents.map(mapEvent);
            }

            return rData;
        });

        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your event has been deleted!',
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

    const mapEvent = (event) => {
        return <OrgEvent eventProp={event} onEventClicked={onDeleteEvent} user={props.currentUser} key={event.id} />;
    };

    const onGetAllSuccess = (response) => {
        _logger('getAll response', response);
        if (response?.item?.pagedItems) {
            let eventArray = response.item.pagedItems;
            _logger('event Item', eventArray);

            setEventData((prevState) => {
                let pd = { ...prevState };
                pd.arrayOfEvents = eventArray;
                pd.eventComponents = eventArray.map(mapEvent);
                pd.page.pageIndex = response.item.pageIndex;
                pd.page.pageSize = response.item.pageSize;
                pd.page.totalCount = response.item.totalCount;
                pd.page.totalPages = response.item.totalPages;
                pd.isChanged = false;
                _logger('RESPONSE HERE', response);
                return pd;
            });
        } else {
            _logger('wrong shape ----');
        }
    };

    const onGetAllError = (err) => {
        _logger('Get events error', err);
    };

    const onChange = (currentPage) => {
        _logger('CURRENT PAGE', currentPage);

        eventsService
            .getAll(currentPage - 1, eventData.page.pageSize)
            .then(onGetAllSuccess)
            .catch(onGetAllError);
    };

    const handleSubmit = useCallback((values) => {
        _logger('name to be searched', values);
        eventsService
            .getSearchedEvents(values.page.pageIndex, values.page.pageSize, values.search)
            .then(onSearchSuccess)
            .catch(onSuccessError);
    }, []);

    const onSearchSuccess = (response) => {
        _logger('onSearchSuccess', response);

        let eventArray = response.item.pagedItems;

        setEventData((prevState) => {
            let pd = { ...prevState };
            pd.arrayOfEvents = eventArray;
            pd.eventComponents = eventArray.map(mapEvent);
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

    return (
        <React.Fragment>
            <Row className="title-res-cnmpro">
                <Col className="text-center">
                    <h1>Events</h1>
                </Col>
            </Row>
            {role.isOrgAdmin ? (
                <Row className="align-item-center mb-2">
                    <Col></Col>
                    <Col className="justify-content-end ml-3">
                        <Pagination
                            onChange={onChange}
                            current={eventData.page.pageIndex + 1}
                            total={eventData.page.totalCount}
                            locale={locale}
                            pageSize={eventData.page.pageSize}
                            prevIcon="<<<"
                            nextIcon=">>>"
                            className="mb-2"
                        />
                    </Col>
                    <Col className="col-4">
                        <Formik enableReinitialize={true} initialValues={eventData} onSubmit={handleSubmit}>
                            {() => (
                                <Form>
                                    <div className="input-group mb-3">
                                        <Field
                                            type="text"
                                            className="form-control"
                                            placeholder="Search Events"
                                            id="top-search"
                                            name="search"
                                        />
                                        <ErrorMessage name="name" component="div" className="has-error-res-cnmpro" />
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
                    <Col className="justify-content-end ml-3">
                        <Pagination
                            onChange={onChange}
                            current={eventData.page.pageIndex + 1}
                            total={eventData.page.totalCount}
                            locale={locale}
                            pageSize={eventData.page.pageSize}
                            prevIcon="<<<"
                            nextIcon=">>>"
                            className="mb-2"
                        />
                    </Col>
                    <Col className="col-4">
                        <Formik enableReinitialize={true} initialValues={eventData} onSubmit={handleSubmit}>
                            {() => (
                                <Form>
                                    <div className="input-group mb-3">
                                        <Field
                                            type="text"
                                            className="form-control"
                                            placeholder="Search Events"
                                            id="top-search"
                                            name="search"
                                        />
                                        <ErrorMessage name="name" component="div" className="has-error-res-cnmpro" />
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
            <Row>{eventData.eventComponents}</Row>
        </React.Fragment>
    );
}
OrgEvents.propTypes = {
    currentUser: PropTypes.shape({
        id: PropTypes.number.isRequired,
        roles: PropTypes.arrayOf(PropTypes.string).isRequired,
        email: PropTypes.string.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
};
export default OrgEvents;
