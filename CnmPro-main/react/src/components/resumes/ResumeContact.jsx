import React, { useEffect } from 'react';
import resumeContactSchema from '../../schema/resumes/resumeContactSchema';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as wizardPropTypes from './resumesPropTypes';
import debug from 'sabio-debug';

const _logger = debug.extend('ResumeContact');

const ResumeContact = (props) => { //Functional
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
        touched,
        onChange,
    } = props;

    _logger(props, 'props');

    useEffect(() => {
        onChange(values);
    }, []);

    const onNextClicked = () => {
        onNext(values);
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto res-card-width">
                    <Card.Header>
                        <h3 className="text-center">Contact Information</h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <label htmlFor="name">Full Name</label>
                        <div className="form-group my-2">
                            <input
                                name="name"
                                value={values.name}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter your name"
                                className={`form-control ${errors.name && touched.name && 'is-invalid'}`}
                            />
                            {errors.name && touched.name && <div className="invalid-feedback">{errors.name}</div>}
                        </div>
                        <label htmlFor="email">Email</label>
                        <div className="form-group my-2">
                            <input
                                name="email"
                                value={values.email}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter email"
                                className={`form-control ${errors.email && touched.email && 'is-invalid'}`}
                            />
                            {errors.email && touched.email && <div className="invalid-feedback">{errors.email}</div>}
                        </div>
                        <label htmlFor="phone">Phone Number</label>
                        <div className="form-group my-2">
                            <input
                                name="phone"
                                value={values.phone}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter Phone Number"
                                className={`form-control ${errors.phone && touched.phone && 'is-invalid'}`}
                            />
                            {errors.phone && touched.phone && <div className="invalid-feedback">{errors.phone}</div>}
                        </div>
                        <label htmlFor="notes">Notes</label>
                        <div className="form-group my-2">
                            <input
                                name="notes"
                                value={values.notes}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter Notes"
                                className={`form-control ${errors.notes && touched.notes && 'is-invalid'}`}
                            />
                            {errors.notes && touched.notes && <div className="invalid-feedback">{errors.notes}</div>}
                        </div>
                        <label htmlFor="description">Description</label>
                        <div className="form-group my-2">
                            <input
                                name="description"
                                value={values.description}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter a short intro of yourself"
                                className={`form-control ${errors.description && touched.description && 'is-invalid'}`}
                            />
                            {errors.description && touched.description && <div className="invalid-feedback">{errors.description}</div>}
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
                                        !values.name ||
                                        Boolean(errors.name) ||
                                        !values.email ||
                                        Boolean(errors.email) ||
                                        !values.phone ||
                                        Boolean(errors.phone) ||
                                        !values.notes ||
                                        Boolean(errors.notes) ||
                                        !values.description ||
                                        Boolean(errors.description)
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

ResumeContact.propTypes = wizardPropTypes.resumePropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        name: props.formData.name,
        email: props.formData.email,
        phone: props.formData.phone,
        notes: props.formData.notes,
        description: props.formData.description,
    }),

    validationSchema: resumeContactSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(ResumeContact);
