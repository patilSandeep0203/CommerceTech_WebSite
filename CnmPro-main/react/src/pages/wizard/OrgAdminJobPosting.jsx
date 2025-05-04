import React, { useEffect, useState } from 'react';
import organizationSchema from '../../schema/jobs/jobNameSchema';
import { withFormik, ErrorMessage } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as orgAdminPropTypes from './orgAdminPropTypes';
import lookUpService from '../../services/lookUpService';
import toastr from 'toastr';
import debug from 'sabio-debug';

const _logger = debug.extend('OrgAdminJobPosting');

function OrgAdminJobPosting(props) {
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

    const [jobType, setJobType] = useState([]);
    const [states, setStates] = useState([]);

    useEffect(() => {
        lookUpService.LookUp(['JobTypes']).then(onSuccessJobTypes).catch(onErrorJobTypes);
        lookUpService.LookUp(['States']).then(onSuccessStates).catch(onErrorStates);
    }, []);

    const onSuccessJobTypes = (response) => {
        _logger(response);
        setJobType((prevState) => {
            let jobType = { ...prevState };
            jobType = response.item.jobTypes;
            _logger(response, 'TYPES LOG ---------------');
            return jobType;
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

    const onErrorJobTypes = (err) => {
        _logger('Get Job Types error', err);
        toastr.error('Error');
    };

    const onErrorStates = (err) => {
        _logger('Get State Types error', err);
        toastr.error('Error');
    };

    const mapJobTypesOptions = (jobType) => {
        return (
            <option key={jobType.id} value={jobType.id} type="number">
                {jobType.name}
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
                        <h3 className="text-center">Add a First Job Posting {'(Optional)'} </h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="jobTypeId">Job Type</label>
                                <div className="form-group my-2">
                                    <Form.Select
                                        name="jobTypeId"
                                        value={values.jobTypeId}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        className="form-control">
                                        <option>Select One</option>
                                        {jobType.map(mapJobTypesOptions)}
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
                                <label htmlFor="jobTitle">Job Title</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="jobTitle"
                                        value={values.jobTitle}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter Job Title"
                                        className="form-control"></Form.Control>
                                </div>
                            </div>

                            <div className="col-sm-6">
                                <label htmlFor="email">Contact Email</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="email"
                                        value={values.email}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter Contact Email"
                                        className="form-control"></Form.Control>
                                    <ErrorMessage name="email" component="div" className="has-error" />
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
                                placeholder="Enter Job Description"
                                className="form-control"></Form.Control>
                        </div>

                        <label htmlFor="requirements">Requirements</label>
                        <div className="form-group my-2">
                            <Form.Control
                                as="textarea"
                                rows={2}
                                name="requirements"
                                value={values.requirements}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter Job Requirements"
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
                                        !values.jobTypeId ||
                                        Boolean(errors.jobTypeId) ||
                                        !values.stateId ||
                                        Boolean(errors.stateId) ||
                                        !values.jobTitle ||
                                        Boolean(errors.jobTitle) ||
                                        !values.description ||
                                        Boolean(errors.description) ||
                                        !values.requirements ||
                                        Boolean(errors.requirements) ||
                                        !values.email ||
                                        Boolean(errors.email)
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

OrgAdminJobPosting.propTypes = orgAdminPropTypes.orgAdminPropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        jobTypeId: props.formData.jobTypeId,
        jobTitle: props.formData.jobTitle,
        email: props.formData.email,
        requirements: props.formData.requirements,
    }),

    validationSchema: organizationSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(OrgAdminJobPosting);
