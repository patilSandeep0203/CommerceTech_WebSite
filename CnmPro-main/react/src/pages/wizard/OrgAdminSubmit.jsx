import React from 'react';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as orgAdminPropTypes from './orgAdminPropTypes';
import { BsPatchCheck } from 'react-icons/bs';
import organization from '../../schema/organization/organization';
function OrgAdminSubmit(props) {
    const { formData, values, isSubmitting, handleSubmit, onBack, backLabel, nextLabel, onNext, cantBack } = props;

    const onNextClicked = () => {
        onNext(values);
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto org-form-card-width">
                    <Card.Header>
                        <h4 className="text-center">Review Information</h4>
                    </Card.Header>

                    <Card className="h-100 p-2">
                        <Card.Header>
                            <h4 className="header-title mb-3">Basic Information</h4>
                        </Card.Header>
                        <Card.Body>
                            <div className="row">
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Name:</p>
                                    {formData.name}
                                </div>

                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Organization Type:</p>
                                    {formData.organizations}
                                </div>
                            </div>

                            <div className="row">
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Description:</p>
                                    {formData.description}
                                </div>

                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Website:</p> {formData.siteUrl}
                                </div>
                            </div>
                        </Card.Body>
                    </Card>

                    <Card className="h-100 p-2">
                        <Card.Header>
                            <h4 className="header-title mb-3">Location</h4>
                        </Card.Header>

                        <Card.Body>
                            <div className="row">
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Organization:</p> {formData.organizationTypeId}
                                </div>
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Location:</p> {formData.locationTypeId}
                                </div>
                            </div>

                            <div className="row">
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Line One:</p> {formData.lineOne}
                                </div>
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Line Two:</p>
                                    {formData.lineTwo}
                                </div>
                            </div>

                            <div className="row">
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">City:</p> {formData.city}
                                </div>
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Zip:</p>
                                    {formData.zip}
                                </div>
                            </div>

                            <div className="row">
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">State:</p> {formData.stateId}
                                </div>
                                <div className="col mb-2 d-flex">
                                    <p className="fw-bold me-2">Phone:</p> {formData.phone}
                                </div>
                            </div>
                        </Card.Body>
                    </Card>

                    {formData.id === 0 ? (
                        <Card className="h-100 p-2">
                            <Card.Header>
                                <h4 className="header-title mb-3">Job Posting</h4>
                            </Card.Header>

                            <Card.Body>
                                <div className="row">
                                    <div className="col mb-2 d-flex">
                                        <p className="fw-bold me-2">jobTypeId:</p> {formData.jobTypeId}
                                    </div>

                                    <div className="col mb-2 d-flex">
                                        <p className="fw-bold me-2">Location:</p> {formData.stateId}
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col mb-2 d-flex">
                                        <p className="fw-bold me-2">jobTitle:</p> {formData.jobTitle}
                                    </div>

                                    <div className="col mb-2 d-flex">
                                        <p className="fw-bold me-2">Contact Email:</p>
                                        {formData.email}
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="col mb-2 d-flex">
                                        <p className="fw-bold me-2">Description:</p> {formData.description}
                                    </div>

                                    <div className="col mb-2 d-flex">
                                        <p className="fw-bold me-2">Requirements:</p>
                                        {formData.requirements}
                                    </div>
                                </div>
                            </Card.Body>
                        </Card>
                    ) : (
                        ''
                    )}
                </Card>

                <Card className="p-4 mx-auto org-form-card-width">
                    <Card.Header>
                        <h3 className="text-center">Submit</h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <div className="text-center">
                            <h2 className="mt-0">
                                <BsPatchCheck />
                            </h2>
                            <h3 className="mt-0">Review Before Submit !</h3>
                            <div className="mb-3">
                                <Form.Check type="checkbox" className="d-inline-block" name="acceptCheck">
                                    <Form.Check.Input type="checkbox" />{' '}
                                    <Form.Check.Label>I agree with the Terms and Conditions</Form.Check.Label>
                                </Form.Check>
                            </div>
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
}

OrgAdminSubmit.propTypes = orgAdminPropTypes.orgAdminPropTypes;

export default withFormik({
    validationSchema: organization,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(OrgAdminSubmit);
