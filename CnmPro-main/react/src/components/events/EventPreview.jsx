import React, { useEffect, useState } from 'react';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as wizardPropTypes from './eventsPropTypes';
import lookUpService from '../../services/lookUpService';
import debug from 'sabio-debug';
import toastr from 'toastr';

const _logger = debug.extend('EventPreview');

const EventPreview = (props) => {
    const { values, isSubmitting, handleSubmit, onBack, backLabel, nextLabel, onNext, cantBack } = props;
    const [states, setStates] = useState([]);
    const [locationTypes, setLocationTypes] = useState([]);
    const [eventTypes, setEventTypes] = useState([]);
    const [lookUpDone, setLookUpDone] = useState(false);

    useEffect(() => {
        onChange();
    }, [values]);

    useEffect(() => {
        lookUpService.LookUp(['States', 'LocationTypes', 'EventTypes']).then(onSuccessLookup).catch(onErrorLookup);
    }, []);

    const onSuccessLookup = (response) => {
        _logger('Success Lookup', response);
        setStates(response.item.states);
        setLocationTypes(response.item.locationTypes);
        setEventTypes(response.item.eventTypes);
        setLookUpDone(true);
    };

    const onErrorLookup = (response) => {
        _logger('Error', response);
        toastr.error('Unable to get get Look up types');
    };

    const onChange = () => {
        props.onChange(values);
    };

    const onNextClicked = () => {
        onNext(values);
    };

    const onBackClicked = () => {
        onBack(values);
    };

    const getEventType = () => {
        const eventType = eventTypes.filter((eventType) => eventType.id === Number(props.formData.eventTypeId));
        return eventType[0].name;
    };

    const getState = () => {
        const state = states.filter((state) => state.id === Number(props.formData.stateId));
        return state[0].name;
    };

    const getLocationType = () => {
        _logger('Location', props.formData.locationTypeId);
        _logger('types', locationTypes);
        const locationType = locationTypes.filter(
            (locationType) => locationType.id === Number(props.formData.locationTypeId)
        );
        return locationType[0].name;
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto" style={{ width: 950 }}>
                    <Card.Header>
                        <h2 className="text-center">Event Preview</h2>
                    </Card.Header>
                    <h4 className="text-left">Event Information:</h4>
                    <div className="form-group mt-2">
                        <div className="row p-2">
                            <div className="col-sm-6">
                                <p>
                                    <strong>Event Type: </strong>
                                    {lookUpDone && getEventType()}
                                </p>
                            </div>
                        </div>
                        <div className="row p-2">
                            <div className="col-sm-6">
                                <p>
                                    <strong>Title: </strong>
                                    {props.formData.title}
                                </p>
                            </div>
                        </div>
                        <div className="row p-2">
                            <div className="col-sm-6">
                                <p>
                                    <strong>Description: </strong>
                                    {props.formData.description}
                                </p>
                            </div>
                        </div>
                        <div className="row p-2">
                            <div className="col-sm-6">
                                <p>
                                    <strong>ImageUrl: </strong>
                                    {props.formData.imageUrl}
                                </p>
                            </div>
                        </div>
                        <div className="row p-2">
                            <div className="col-sm-6">
                                <h4 className="text-left">Location Information:</h4>
                                <div className="form-group mt-2">
                                    <div className="row p-2">
                                        <div className="col-sm-6">
                                            <p>
                                                <strong>Location Type: </strong>
                                                {lookUpDone && getLocationType()}
                                            </p>
                                        </div>
                                    </div>
                                    <div className="row p-2">
                                        <div className="col-sm-6">
                                            <p>
                                                <strong>Address: </strong>
                                                {props.formData.lineOne} {props.formData.city} ,{' '}
                                                {lookUpDone && getState()} {props.formData.zip}
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="button-group pt-3 row">
                            <div className="col-sm-1">
                                <button
                                    type="submit"
                                    className="btn btn-secondary"
                                    onClick={onBackClicked}
                                    disabled={isSubmitting || cantBack}>
                                    {backLabel}
                                </button>
                            </div>
                            <div className="col-sm-1">
                                <button type="submit" className="btn btn-primary" onClick={onNextClicked}>
                                    {nextLabel}
                                </button>
                            </div>
                        </div>
                    </div>
                </Card>
            </Form>
        </React.Fragment>
    );
};

EventPreview.propTypes = wizardPropTypes.eventPropTypes;

export default withFormik({
    enableReinitialize: true,
    mapPropsToValues: (props) => ({
        ...props.formData,
    }),
    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(EventPreview);
