import React, { useEffect, useState } from 'react';
import eventNameSchema from '../../schema/events/eventNameSchema';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as eventService from '../../services/eventsService';
import * as wizardPropTypes from './eventsPropTypes';
import debug from 'sabio-debug';
import FileUploader from '../../components/uploader/FileUploader';

const _logger = debug.extend('EventName');

const EventName = (props) => {
    const { values, touched, errors, handleChange, handleBlur, handleSubmit, nextLabel, onNext, setValues } = props;

    _logger(props, 'props');

    useEffect(() => {
        onChange();
        eventService.lookUp(['EventTypes']).then(onSuccessEventTypes).catch(onErrorEventTypes);
    }, []);

    const [eventType, setEventType] = useState([]);
    _logger(eventType, 'new EventType');

    const onChange = () => {
        props.onChange(values);
    };

    const onSuccessEventTypes = (response) => {
        setEventType((prevState) => {
            let eventType = { ...prevState };
            eventType = response.data.item.eventTypes;
            _logger(response, 'TYPES LOG ---------------');
            return eventType;
        });
    };

    const mapEventTypesOptions = (eventType) => {
        return (
            <option key={eventType.id} value={eventType.id} type="number">
                {eventType.name}
            </option>
        );
    };

    const onErrorEventTypes = (err) => {
        _logger('Get Event Types error', err);
    };

    const onNextClicked = () => {
        onNext(values);
    };

    const handleUploadSuccess = (response) => {
        _logger(response);
        let imageFile = response.items.map((item) => item.url);
        setValues({ imageUrl: imageFile[0] });
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto" style={{ width: 950 }}>
                    <Card.Header>
                        {props.isUpdate && <h3 className="text-center">Edit Event</h3>}
                        {!props.isUpdate && <h3 className="text-center">Create Event</h3>}
                    </Card.Header>
                    <div className="form-group mt-2">
                        <div className="col-sm-6">
                            <label htmlFor="eventTypeId">Event Type:</label>
                            <div className="form-group my-2">
                                <Form.Select
                                    name="eventTypeId"
                                    value={values?.eventTypeId || 0}
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    variant="outlined"
                                    className="form-control">
                                    <option>Select One</option>
                                    {eventType.map(mapEventTypesOptions)}
                                </Form.Select>
                            </div>
                        </div>
                        <div className="form-group my-2">
                            <label htmlFor="title">Title</label>
                            <input
                                className={`form-control ${errors.title && touched.title && 'is-invalid'}`}
                                name="title"
                                id="title"
                                value={values.title}
                                onChange={handleChange}
                                onBlur={handleBlur}
                            />
                            {errors.title && touched.title && <div className="invalid-feedback">{errors.title}</div>}
                        </div>
                        <label htmlFor="description">Description:</label>
                        <div className="form-group my-2">
                            <textarea
                                rows="8"
                                name="description"
                                value={values.description}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                className={`form-control ${errors.description && touched.description && 'is-invalid'}`}
                            />
                            {errors.description && touched.description && (
                                <div className="invalid-feedback">{errors.description}</div>
                            )}
                        </div>
                        <div className="row"></div>
                        <FileUploader
                            onHandleUploadSuccess={(response) => handleUploadSuccess(response)}
                            name="fileUploader"
                            className={`form-control ${errors.imageUrl && touched.imageUrl && 'is-invalid'}`}
                        />
                        {errors.imageUrl && touched.imageUrl && (
                            <div className="invalid-feedback">{errors.imageUrl}</div>
                        )}
                        <div className="button-group pt-3">
                            <button
                                type="submit"
                                className="btn btn-primary ml-2"
                                disabled={
                                    !values.eventTypeId ||
                                    Boolean(errors.eventTypeId) ||
                                    !values.title ||
                                    Boolean(errors.title) ||
                                    !values.description ||
                                    Boolean(errors.description) ||
                                    !values.imageUrl ||
                                    Boolean(errors.imageUrl)
                                }
                                onClick={onNextClicked}>
                                {nextLabel}
                            </button>
                        </div>
                    </div>
                </Card>
            </Form>
        </React.Fragment>
    );
};

EventName.propTypes = wizardPropTypes.eventPropTypes;

export default withFormik({
    enableReinitialize: true,
    mapPropsToValues: (props) => ({
        eventTypeId: props.formData.eventTypeId,
        title: props.formData.title,
        description: props.formData.description,
        imageUrl: props.formData.imageUrl,
    }),

    validationSchema: eventNameSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(EventName);
