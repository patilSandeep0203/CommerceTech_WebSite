import React, { useEffect, useState } from 'react';
import organizationSchema from '../../schema/events/eventNameSchema';
import { withFormik, ErrorMessage } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as orgAdminPropTypes from './orgAdminPropTypes';
import lookUpService from '../../services/lookUpService';
import toastr from 'toastr';
import debug from 'sabio-debug';

const _logger = debug.extend('OrgAdminEventPosting');

function OrgAdminEventPosting(props) {
    const {
        values,
        errors,
        handleChange,
        handleBlur,
        isSubmitting,
        handleSubmit,
        onBack,
        backLabel,
        nextLabel,
        onNext,
        cantBack,
    } = props;

    _logger(props, 'props');

    const [eventType, setEventType] = useState([]);
    const [states, setStates] = useState([]);

    useEffect(() => {
        lookUpService.LookUp(['EventTypes']).then(onSuccessEventTypes).catch(onErrorEventTypes);
        lookUpService.LookUp(['States']).then(onSuccessStates).catch(onErrorStates);
    }, []);

    const onSuccessEventTypes = (response) => {
        _logger(response);
        setEventType((prevState) => {
            let eventType = { ...prevState };
            eventType = response.item.eventTypes;
            _logger(response, 'TYPES LOG ---------------');
            return eventType;
        });
    };

    const onSuccessStates = (response) => {
        setStates((prevState) => {
            let s = { ...prevState };
            s = response.item.states;
            _logger(response, 'TYPES LOG ---------------');
            return s;
        });
    };

    const onErrorEventTypes = (err) => {
        _logger('Get Event Types error', err);
        toastr.error('Error');
    };

    const onErrorStates = (err) => {
        _logger('Get State Types error', err);
        toastr.error('Error');
    };

    const mapEventTypesOptions = (eventType) => {
        return (
            <option key={eventType.id} value={eventType.id} type="number">
                {eventType.name}
            </option>
        );
    };

    const mapStateOptions = (state) => {
        return (
            <option key={state.id} value={state.id} type="number">
                {state.name}
            </option>
        );
    };

    const onNextClicked = () => {
        _logger('values', values);
        onNext(values);
    };

    const onSkipClicked = () => {
        props.onNext(values);
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto" style={{ width: 950 }}>
                    <Card.Header>
                        <h3 className="text-center">Add a First Event Posting {'(Optional)'} </h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="eventTypeId">Event Type</label>
                                <div className="form-group my-2">
                                    <Form.Select
                                        name="eventTypeId"
                                        value={values.eventTypeId}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        className="form-control">
                                        <option>Select One</option>
                                        {eventType.map(mapEventTypesOptions)}
                                    </Form.Select>
                                </div>
                            </div>

                            <div className="col-sm-6">
                                <label htmlFor="stateId">Location</label>
                                <div className="form-group my-2">
                                    <Form.Select
                                        name="stateId"
                                        value={values?.stateId || 0}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        className="form-control">
                                        <option>Select One</option>
                                        {states?.map(mapStateOptions)}
                                    </Form.Select>
                                </div>
                            </div>
                        </div>

                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="eventTitle">Event Title</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="eventTitle"
                                        value={values.eventTitle}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter Event Title"
                                        className="form-control"></Form.Control>
                                </div>
                            </div>
                            <div className="col-sm-6">
                                <label htmlFor="imageUrl">ImageUrl</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="imageUrl"
                                        value={values.imageUrl}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="ImageUrl"
                                        className="form-control"></Form.Control>
                                    <ErrorMessage name="imageUrl" component="div" className="has-error" />
                                </div>
                            </div>
                        </div>

                        <label htmlFor="description">Description</label>
                        <div className="form-group my-2">
                            <Form.Control
                                as="textarea"
                                rows={4}
                                name="description"
                                value={values.description}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter Event Description"
                                className="form-control"></Form.Control>
                        </div>
                        <div className="button-group pt-3 row">
                            <div className="col-sm-1">
                                <button
                                    type="button"
                                    className="btn btn-secondary"
                                    onClick={onBack}
                                    disabled={isSubmitting || cantBack}>
                                    {backLabel}
                                </button>
                            </div>
                            <div className="col-sm-1">
                                <button
                                    type="submit"
                                    className="btn btn-primary ml-2"
                                    disabled={
                                        !values.eventTypeId ||
                                        Boolean(errors.eventTypeId) ||
                                        !values.stateId ||
                                        Boolean(errors.stateId) ||
                                        !values.eventTitle ||
                                        Boolean(errors.eventTitle) ||
                                        !values.description ||
                                        Boolean(errors.description) ||
                                        !values.imageUrl ||
                                        Boolean(errors.imageUrl)
                                    }
                                    onClick={onNextClicked}>
                                    {nextLabel}
                                </button>
                            </div>
                            <div className="col-10">
                                <button
                                    type="skip"
                                    className="btn btn-warning ml-2"
                                    onClick={onSkipClicked}
                                    style={{ float: 'right' }}>
                                    Skip
                                </button>
                            </div>
                        </div>
                    </div>
                </Card>
            </Form>
        </React.Fragment>
    );
}

OrgAdminEventPosting.propTypes = orgAdminPropTypes.orgAdminPropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        eventTypeId: props.formData.eventTypeId,
        eventTitle: props.formData.eventTitle,
        email: props.formData.email,
        imageUrl: props.formData.imageUrl,
    }),

    validationSchema: organizationSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(OrgAdminEventPosting);
