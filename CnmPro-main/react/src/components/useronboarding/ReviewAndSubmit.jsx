import React, { useState } from 'react';
import { Form, Card, Col, Row } from 'react-bootstrap';
import * as wizardPropTypes from './userOnboardingPropTypes';
import debug from 'sabio-debug';
import { withFormik } from 'formik';
import { FaUserCircle } from 'react-icons/fa';
import { TbLicense } from 'react-icons/tb';
import { ImLocation } from 'react-icons/im';

const _logger = debug.extend('ReviewAndSubmit');

const ReviewAndSubmit = (props) => {
    const { formData, isSubmitting, handleSubmit, onBack, backLabel, nextLabel, onNext, cantBack, lookUpResults } =
        props;

    _logger('formData', formData);

    const [terms, setTerms] = useState(false);

    const termsHandler = () => {
        setTerms(!terms);
    };

    const licenseMapper = (license) => {
        return (
            <React.Fragment>
                <Col xs={12} sm={12} md={6} lg={5}>
                    <Card className="border">
                        <Card.Body>
                            <Col className="mb-2">
                                <span className="fw-bold me-2">Type:</span> {licenseTypeMapper(license?.licenseTypeId)}
                            </Col>
                            <Col className="mb-2">
                                <span className="fw-bold me-2">State:</span> {stateMapper(license?.licenseStateId)}
                            </Col>
                            <Col className="mb-2">
                                <span className="fw-bold me-2">Number:</span> {license?.licenseNumber}
                            </Col>
                            <Col className="mb-2">
                                <span className="fw-bold me-2">Expiration:</span> {license?.dateExpires}
                            </Col>
                        </Card.Body>
                    </Card>
                </Col>
            </React.Fragment>
        );
    };

    const licenseTypeMapper = (licenseType) => {
        let name = '';
        for (let i = 0; i < lookUpResults.licenseTypes.length; i++) {
            if (lookUpResults.licenseTypes[i].id === Number(licenseType)) {
                name = lookUpResults.licenseTypes[i].name;
            }
        }
        return name;
    };

    const stateMapper = (stateId) => {
        let name = '';
        for (let i = 0; i < lookUpResults.states.length; i++) {
            if (lookUpResults.states[i].id === Number(stateId)) {
                name = lookUpResults.states[i].name;
            }
        }
        return name;
    };

    const locationTypeMapper = (locationType) => {
        let name = '';
        for (let i = 0; i < lookUpResults.locationTypes.length; i++) {
            if (lookUpResults.locationTypes[i].id === Number(locationType)) {
                name = lookUpResults.locationTypes[i].name;
            }
        }
        return name;
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Row className="justify-content-center g-4">
                    <h3 className="text-center">Review and Submit</h3>
                    <Col lg={4}>
                        <Card className="h-100 p-2">
                            <Card.Header>
                                <h4 className="header-title text-center">
                                    <FaUserCircle style={{ margin: 10 }} />
                                    User Information
                                </h4>
                            </Card.Header>
                            <Card.Body>
                                <ul className="list-unstyled mb-0">
                                    <li>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Name:</span> {formData.lastName},{' '}
                                            {formData.firstName}, {formData.mi}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">DOB:</span> {formData.dob}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Phone:</span> {formData.phone}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Avatar URL:</span> {formData.avatarUrl}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Email:</span> {formData.email}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Desired Hourly Rate:</span>
                                            {formData.desiredHourlyRate}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Profession:</span> {formData.profession}
                                        </p>
                                    </li>
                                </ul>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col lg={4}>
                        <Card className="h-100 p-2">
                            <Card.Header>
                                <h4 className="header-title text-center">
                                    <ImLocation style={{ margin: 10 }} />
                                    Location Information
                                </h4>
                            </Card.Header>
                            <Card.Body>
                                <ul className="list-unstyled mb-0">
                                    <li>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Location Type:</span>
                                            {locationTypeMapper(formData.locationTypeId)}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Address One:</span> {formData.lineOne}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Address Two:</span> {formData.lineTwo}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">State:</span> {stateMapper(formData.stateId)}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">City:</span> {formData.city}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Zip:</span> {formData.zip}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Latitude:</span> {formData.latitude}
                                        </p>
                                        <p className="mb-2">
                                            <span className="fw-bold me-2">Longitude:</span> {formData.longitude}
                                        </p>
                                    </li>
                                </ul>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
                <Row className="justify-content-center" style={{ marginTop: 30 }}>
                    <Col lg={8}>
                        <Card className="h-100 p-2">
                            <Card.Header>
                                <h4 className="header-title text-center">
                                    <TbLicense style={{ margin: 10 }} />
                                    License Information
                                </h4>
                            </Card.Header>
                            <Card.Body>
                                <Row className="justify-content-center">{formData.license.map(licenseMapper)}</Row>
                                <Form.Check type="checkbox" className="d-inline-block" name="terms">
                                    <Form.Check.Input type="checkbox" value={terms} onChange={termsHandler} />
                                    <Form.Check.Label>I agree that the above information is correct</Form.Check.Label>
                                </Form.Check>
                            </Card.Body>
                            <Row>
                                <Col sm={1}>
                                    <button
                                        type="button"
                                        className="btn btn-secondary"
                                        onClick={onBack}
                                        disabled={isSubmitting || cantBack}>
                                        {backLabel}
                                    </button>
                                </Col>
                                <Col sm={1}>
                                    <button
                                        type="submit"
                                        className="btn btn-primary"
                                        onClick={onNext}
                                        disabled={!terms}>
                                        {nextLabel}
                                    </button>
                                </Col>
                            </Row>
                        </Card>
                    </Col>
                </Row>
            </Form>
        </React.Fragment>
    );
};

ReviewAndSubmit.propTypes = wizardPropTypes.userOnboardingPropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        ...props.formData,
    }),
    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(ReviewAndSubmit);
