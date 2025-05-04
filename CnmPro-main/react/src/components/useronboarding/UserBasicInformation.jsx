import React, { useEffect } from 'react';
import userBasicInfoSchema from '../../schema/useronboarding/userBasicInfoSchema';
import { withFormik, ErrorMessage } from 'formik';
import { Form, Card, Row, Col } from 'react-bootstrap';
import * as wizardPropTypes from './userOnboardingPropTypes';

const UserBasicInformation = (props) => {
    const { values, errors, handleChange, handleBlur, handleSubmit, nextLabel, onNext, onChange } = props;

    useEffect(() => {
        onChange(values);
    }, []);

    const onNextClicked = () => {
        onNext(values);
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto" style={{ width: 950 }}>
                    <Card.Header>
                        <h3 className="text-center">Basic Information</h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <Row>
                            <Col lg={6}>
                                <label htmlFor="firstName">First Name</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="firstName"
                                        value={values.firstName}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter First Name"
                                        className="form-control"
                                    />
                                    <ErrorMessage name="firstName" component="div" className="has-error" />
                                </div>
                            </Col>
                            <Col lg={6}>
                                <label htmlFor="lastName">Last Name</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="lastName"
                                        value={values.lastName}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter Last Name"
                                        className="form-control"
                                    />
                                    <ErrorMessage name="lastName" component="div" className="has-error" />
                                </div>
                            </Col>
                        </Row>
                        <Row>
                            <Col lg={6}>
                                <label htmlFor="mi">MI</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="mi"
                                        value={values.mi}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="MI"
                                        className="form-control"
                                    />
                                    <ErrorMessage name="mi" component="div" className="has-error" />
                                </div>
                            </Col>
                            <Col lg={6}>
                                <label htmlFor="email">Email</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="email"
                                        value={values.email}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Email"
                                        className="form-control"
                                    />
                                    <ErrorMessage name="email" component="div" className="has-error" />
                                </div>
                            </Col>
                        </Row>
                        <Row>
                            <Col lg={6}>
                                <label htmlFor="dob">Date Of Birth</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="dob"
                                        type="date"
                                        value={values.dob}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="EX:01/01/1985"
                                        className="form-control"
                                    />
                                </div>
                                <ErrorMessage name="dob" component="div" className="has-error" />
                            </Col>
                            <Col lg={6}>
                                <label htmlFor="phone">Phone Number</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="phone"
                                        value={values.phone}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Phone Number"
                                        className="form-control"
                                    />
                                    <ErrorMessage name="phone" component="div" className="has-error" />
                                </div>
                            </Col>
                        </Row>
                        <label htmlFor="avatarUrl">Avatar Url</label>
                        <div className="form-group my-2">
                            <Form.Control
                                name="avatarUrl"
                                value={values.avatarUrl}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Avatar Url"
                                className="form-control"
                            />
                            <ErrorMessage name="avatarUrl" component="div" className="has-error" />
                        </div>
                        <div className="button-group pt-3">
                            <button
                                type="submit"
                                className="btn btn-primary ml-2"
                                disabled={
                                    !values.dob ||
                                    Boolean(errors.dob) ||
                                    !values.email ||
                                    Boolean(errors.email) ||
                                    !values.firstName ||
                                    Boolean(errors.firstName) ||
                                    !values.lastName ||
                                    Boolean(errors.lastName) ||
                                    !values.mi ||
                                    Boolean(errors.mi) ||
                                    !values.phone ||
                                    Boolean(errors.phone) ||
                                    !values.avatarUrl ||
                                    Boolean(errors.avatarUrl)
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

UserBasicInformation.propTypes = wizardPropTypes.userOnboardingPropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        firstName: props.formData.firstName,
        lastName: props.formData.lastName,
        mi: props.formData.mi,
        avatarUrl: props.formData.avatarUrl,
        dob: props.formData.dob,
        email: props.formData.email,
        phone: props.formData.phone,
        isActive: props.formData.isActive,
    }),

    validationSchema: userBasicInfoSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(UserBasicInformation);
