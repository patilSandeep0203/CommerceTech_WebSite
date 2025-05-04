import React from 'react';
import { PropTypes } from 'prop-types';
import { Formik, Form, Field } from 'formik';
import { Modal, ModalHeader, ModalBody, Button, ModalFooter } from 'react-bootstrap';
import timesheetFormSchema from '../../schema/timesheet/timesheetFormSchema';

function TimesheetForm(props) {
    const { orgs, jobs, timesheetEntryData } = props;

    const onSubmitClicked = (values) => {
        props.onSubmit(values);
    };

    const mapOrg = (org) => {
        return (
            <option key={org.organizationId} value={org.organizationId}>
                {org.organization}
            </option>
        );
    };

    const mapJob = (job) => {
        return (
            <option key={job.jobId} value={job.jobId}>
                {job.title}
            </option>
        );
    };

    const newDate = (date) => {
        return date.split('T')[0];
    };

    return (
        <div>
            <Modal
                show={props.isShowing}
                onHide={props.toggle}
                className="timesheetFormModal"
                size="md"
                backdrop="static">
                <Formik
                    initialValues={timesheetEntryData}
                    validationSchema={timesheetFormSchema}
                    enableReinitialize={true}
                    onSubmit={onSubmitClicked}>
                    {({ values, handleChange, errors, touched }) => (
                        <Form>
                            <ModalHeader>
                                <h5>Add or Edit Your Hours</h5>
                                <button
                                    type="button"
                                    className="btn-close"
                                    data-bs-dismiss="modal"
                                    aria-label="Close"
                                    onClick={props.toggle}></button>
                            </ModalHeader>
                            <ModalBody>
                                <div className="row">
                                    <div className="col-6">
                                        <label htmlFor="date">Date</label>
                                        <Field
                                            type="date"
                                            name="date"
                                            className="form-control"
                                            value={newDate(values.date)}
                                            onChange={handleChange}
                                            error={errors.date && Boolean(touched.date)}
                                            helpertext={touched.date && errors.date}
                                        />
                                    </div>
                                    <div className="col-6">
                                        <label htmlFor="organizationId">Org</label>
                                        <Field
                                            component="select"
                                            name="organizationId"
                                            className="form-control"
                                            onChange={handleChange}>
                                            <option value="">Select Organization</option>
                                            {orgs.map(mapOrg)}
                                        </Field>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col-6">
                                        <label htmlFor="timeFrom">TimeFrom</label>
                                        <Field
                                            type="time"
                                            name="timeFrom"
                                            className="form-control"
                                            value={values.timeFrom}
                                            onChange={handleChange}
                                            error={errors.timeFrom && Boolean(touched.timeFrom)}
                                            helpertext={touched.timeFrom && errors.timeFrom}
                                        />
                                    </div>
                                    <div className="col-6">
                                        <label htmlFor="timeTo">TimeTo</label>
                                        <Field
                                            type="time"
                                            name="timeTo"
                                            className="form-control"
                                            value={values.timeTo}
                                            onChange={handleChange}
                                            error={errors.timeTo && Boolean(touched.timeTo)}
                                            helpertext={touched.timeTo && errors.timeTo}
                                        />
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col-12">
                                        <label htmlFor="jobId">Job</label>
                                        <Field
                                            component="select"
                                            name="jobId"
                                            className="form-control"
                                            onChange={handleChange}>
                                            <option value="">Select Job</option>
                                            {jobs.map(mapJob)}
                                        </Field>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col-12">
                                        <label htmlFor="comment">Comment</label>
                                        <Field
                                            type="text"
                                            name="comment"
                                            component="textarea"
                                            className="form-control"
                                            value={values.comment}
                                            onChange={handleChange}
                                            error={errors.comment && Boolean(touched.comment)}
                                            helpertext={touched.comment && errors.comment}
                                        />
                                    </div>
                                </div>
                            </ModalBody>
                            <ModalFooter>
                                <Button type="button" className="btn btn-secondary" onClick={props.toggle}>
                                    Close
                                </Button>
                                <Button className="btn sm-btn" type="submit">
                                    Submit
                                </Button>
                            </ModalFooter>
                        </Form>
                    )}
                </Formik>
            </Modal>
        </div>
    );
}

TimesheetForm.propTypes = {
    timesheetEntryData: PropTypes.shape({
        date: PropTypes.string.isRequired,
        timeFrom: PropTypes.string.isRequired,
        timeTo: PropTypes.string.isRequired,
        comment: PropTypes.string.isRequired,
        jobId: PropTypes.number.isRequired,
        organizationId: PropTypes.number.isRequired,
    }).isRequired,
    onSubmit: PropTypes.func.isRequired,
    isShowing: PropTypes.bool.isRequired,
    toggle: PropTypes.func.isRequired,
    jobs: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    orgs: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
};

export default TimesheetForm;
