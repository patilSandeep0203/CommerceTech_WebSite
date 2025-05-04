import React, { useEffect, useState } from 'react';
import organizationSchema from '../../schema/organization/organization';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import lookUpService from '../../services/lookUpService';
import * as orgAdminPropTypes from './orgAdminPropTypes';
import debug from 'sabio-debug';
import toastr from 'toastr';

const _logger = debug.extend('OrgAdminLocation');

function OrgAdminLocation(props) {
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
    } = props;

    const [locationTypes, setLocationTypes] = useState([]);
    const [states, setStates] = useState([]);

    useEffect(() => {
        lookUpService.LookUp(['LocationTypes', 'States']).then(onSuccessLookUpTypes).catch(onErrorLookUpTypes);
    }, []);

    const onSuccessLookUpTypes = (response) => {
        _logger('LookUp', response);
        setLocationTypes(response.item.locationTypes);
        setStates(response.item.states);
    };

    const onErrorLookUpTypes = (err) => {
        _logger('Get Organization Types error', err);
        toastr.error('Error');
    };

    const mapLocationTypesOptions = (locationType) => {
        return (
            <option key={locationType.id} value={locationType.id} type="number">
                {locationType.name}
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
        onNext(values);
        _logger('orgadminlocation values', values);
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto org-form-card-width">
                    <Card.Header>
                        <h3 className="text-center">Organization Admin Location</h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="locationTypeId"> Location Type</label>
                                <div className="form-group my-2">
                                    <Form.Select
                                        name="locationTypeId"
                                        value={values?.locationTypeId || 0}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        className="form-control">
                                        <option>Select One</option>
                                        {locationTypes.map(mapLocationTypesOptions)}
                                    </Form.Select>
                                </div>
                            </div>
                            <div className="col-sm-12">
                                <label htmlFor="lineOne">Address 1</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="lineOne"
                                        value={values.lineOne}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter Address"
                                        className={`form-control ${
                                            errors.lineOne && touched.lineOne && 'is-invalid'
                                        }`}></Form.Control>
                                </div>
                            </div>
                        </div>

                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="lineTwo">Address 2</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="lineTwo"
                                        value={values.lineTwo}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter Address"
                                        className="form-control"></Form.Control>
                                </div>
                            </div>
                            <div className="col-sm-6">
                                <label htmlFor="city">City</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="city"
                                        value={values.city}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter City"
                                        className={`form-control ${
                                            errors.city && touched.city && 'is-invalid'
                                        }`}></Form.Control>
                                </div>
                            </div>
                        </div>

                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="stateId">State</label>
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
                            <div className="col-sm-6">
                                <label htmlFor="zip">Zipcode</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="zip"
                                        value={values.zip}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter Zipcode"
                                        className={`form-control ${
                                            errors.zip && touched.zip && 'is-invalid'
                                        }`}></Form.Control>
                                </div>
                            </div>
                        </div>

                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="phone">Phone</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="phone"
                                        value={values.phone}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="e.g. 999-999-9999"
                                        className={`form-control ${
                                            errors.phone && touched.phone && 'is-invalid'
                                        }`}></Form.Control>
                                </div>
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
                                <button
                                    type="submit"
                                    className="btn btn-primary"
                                    disabled={
                                        !values.locationTypeId ||
                                        Boolean(errors.locationTypeId) ||
                                        !values.lineOne ||
                                        Boolean(errors.lineOne) ||
                                        !values.zip ||
                                        Boolean(errors.zip) ||
                                        !values.stateId ||
                                        Boolean(errors.stateId) ||
                                        !values.phone ||
                                        Boolean(errors.phone)
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
}

OrgAdminLocation.propTypes = orgAdminPropTypes.orgAdminPropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        locationTypeId: `${parseInt(props.formData.locationTypeId)}`,
        lineOne: props.formData.lineOne,
        lineTwo: props.formData.lineTwo,
        city: props.formData.city,
        zip: props.formData.zip,
        stateId: `${props.formData.stateId}`,
        stateName: props.formData.stateName || '',
        phone: props.formData.phone,
    }),

    validationSchema: organizationSchema,

    handleSubmit: (values, { props }) => {
        props.onNext({
            locationTypeId: parseInt(values.locationTypeId),
            lineOne: values.lineOne,
            lineTwo: values.lineTwo,
            city: values.city,
            zip: values.zip,
            stateId: parseInt(values.stateId),
            stateName: values.stateName,
            phone: values.phone,
        });
    },
})(OrgAdminLocation);
