import React, { useEffect } from 'react';
import jobContactSchema from '../../schema/jobs/jobContactSchema';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as wizardPropTypes from './jobsPropTypes';
import debug from 'sabio-debug';

const _logger = debug.extend('JobContact');

const JobContact = (props) => {
    const {
        values,
        touched,
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

    useEffect(() => {
        onChange();
    }, [values]);

    const onChange = () => {
        props.onChange(values);
    };

    const onNextClicked = () => {
        onNext(values);
    };

    const onBackClicked = () => {
        onBack(values);
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto jobs-form-card-width">
                    <Card.Header>
                        <h3 className="text-center">Contact Information</h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <label htmlFor="contactName">Contact Name</label>
                        <div className="form-group my-2">
                            <input
                                name="contactName"
                                value={values.contactName}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter Your Name"
                                className={`form-control ${errors.contactName && touched.contactName && 'is-invalid'}`}
                            />
                            {errors.contactName && touched.contactName && (
                                <div className="invalid-feedback">{errors.contactName}</div>
                            )}
                        </div>
                        <label htmlFor="contactEmail">Contact Email</label>
                        <div className="form-group my-2">
                            <input
                                name="contactEmail"
                                value={values.contactEmail}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                className={`form-control ${
                                    errors.contactEmail && touched.contactEmail && 'is-invalid'
                                }`}
                            />
                            {errors.contactEmail && touched.contactEmail && (
                                <div className="invalid-feedback">{errors.contactEmail}</div>
                            )}
                        </div>
                        <label htmlFor="contactPhone">Contact Phone Number</label>
                        <div className="form-group my-2">
                            <input
                                name="contactPhone"
                                value={values.contactPhone}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                className={`form-control ${
                                    errors.contactPhone && touched.contactPhone && 'is-invalid'
                                }`}
                            />
                            {errors.contactPhone && touched.contactPhone && (
                                <div className="invalid-feedback">{errors.contactPhone}</div>
                            )}
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
                                <button
                                    type="submit"
                                    className="btn btn-primary ml-2"
                                    disabled={
                                        !values.contactName ||
                                        Boolean(errors.contactName) ||
                                        !values.contactPhone ||
                                        Boolean(errors.contactPhone) ||
                                        !values.contactEmail ||
                                        Boolean(errors.contactEmail)
                                    }
                                    onClick={onNextClicked}>
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

JobContact.propTypes = wizardPropTypes.jobPropTypes;

export default withFormik({
    enableReinitialize: true,
    mapPropsToValues: (props) => ({
        contactName: props.formData.contactName,
        contactEmail: props.formData.contactEmail,
        contactPhone: props.formData.contactPhone,
    }),

    validationSchema: jobContactSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(JobContact);
