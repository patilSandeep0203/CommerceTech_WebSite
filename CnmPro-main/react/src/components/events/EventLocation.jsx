import React, { useEffect, useState } from 'react';
import eventLocationSchema from '../../schema/events/eventLocationSchema';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as wizardPropTypes from './eventsPropTypes';
import lookUpService from '../../services/lookUpService';
import * as organizationService from '../../services/organizationsService';
import debug from 'sabio-debug';
import AddressAutocomplete from '../../pages/location/AddressAutocomplete';
import toastr from 'toastr';

const _logger = debug.extend('eventLocation');

const EventLocation = (props) => {
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
        setValues,
    } = props;
    _logger(props, 'Event Location props');

    const [states, setStates] = useState([]);
    const [locationTypes, setLocationTypes] = useState([]);
    const [organization, setOrganizations] = useState([]);
    const [selectedAddress, setSelectedAddress] = useState(null);
    const [isLookUpDone, setIsLookUpDone] = useState(false);
    const [changeAddress, setChangeAddress] = useState(false);

    _logger(states, 'new state States');
    _logger(locationTypes, 'new locationType');
    _logger(organization, 'new locationType');

    useEffect(() => {
        onChange();
        lookUpService.LookUp(['LocationTypes', 'States']).then(onSuccessLookUp).catch(onErrorLookUp);
        organizationService.GetAllOrganizations(0, 200).then(onSuccessOrganization).catch(onErrorOrganization);
    }, []);

    useEffect(() => {
        if (selectedAddress !== null) {
            setValues({
                ...values,
                lineOne: selectedAddress.lineOne,
                lineTwo: selectedAddress.lineTwo,
                city: selectedAddress.city,
                zip: selectedAddress.zip,
                stateId: selectedAddress.stateId,
                latitude: selectedAddress.latitude,
                longitude: selectedAddress.longitude,
            });
        }
    }, [selectedAddress]);

    const onSuccessLookUp = (response) => {
        setLocationTypes(response.item.locationTypes);
        setStates(response.item.states);
        setIsLookUpDone(true);
    };

    const onSuccessOrganization = (response) => {
        setOrganizations((prevState) => {
            let s = { ...prevState };
            s = response.data.item.pagedItems;
            _logger(response, 'TYPES LOG ---------------');
            return s;
        });
    };

    const onErrorLookUp = (err) => {
        _logger('Get Location Types error', err);
        toastr.error('Error adding comments', err);
    };

    const onErrorOrganization = (err) => {
        _logger('Get Organization error', err);
        toastr.error('Error adding comments', err);
    };

    const mapOrganizationOptions = (organization) => {
        return (
            <option key={organization.id} value={organization.id} type="number">
                {organization.name}
            </option>
        );
    };

    const mapLocationTypesOptions = (locationType) => {
        return (
            <option key={locationType.id} value={locationType.id} type="number">
                {locationType.name}
            </option>
        );
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

    const onAddressSelect = (placeObj) => {
        setSelectedAddress({ ...placeObj });
    };

    const getLookUpType = (lookUpType) => {
        switch (lookUpType) {
            case 'locationType':
                const locType = locationTypes.filter((locationType) => locationType.id === values.locationTypeId);
                return locType[0].name;
            case 'state':
                const state = states.filter((state) => state.id === values.stateId);
                return state[0].name;
            default:
                return 'No LookUp Type';
        }
    };

    const handleChangeAddress = (event) => {
        event.preventDefault();
        setChangeAddress((prevState) => !prevState);
    };

    const renderCurrentAddress = () => {
        return (
            <div>
                <h4>Current Address</h4>
                <p>
                    <strong className="me-1">Address:</strong>
                    {values.lineOne}, {values.city}, {isLookUpDone && getLookUpType('state')} {values.zip}
                </p>
                <button className="btn btn-primary" type="button" onClick={handleChangeAddress}>
                    Change Address
                </button>
            </div>
        );
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto" style={{ width: 950 }}>
                    {props.isUpdate}
                    <Card.Header>
                        <h3 className="text-center">Event Location</h3>
                    </Card.Header>

                    {props.isUpdate && renderCurrentAddress()}

                    {(changeAddress || !props.isUpdate) && (
                        <div className="form-group mt-2">
                            <div className="row">
                                <div className="col-sm-6">
                                    <label htmlFor="organizationId">Organization:</label>
                                    <div className="form-group my-2">
                                        <Form.Select
                                            name="organizationId"
                                            value={values.organizationId}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            variant="outlined"
                                            className="form-control">
                                            <option>Select One</option>
                                            {organization.map(mapOrganizationOptions)}
                                        </Form.Select>
                                    </div>
                                </div>
                                <div className="col-sm-12">
                                    <label htmlFor="lineOne">Address:</label>
                                    <div className="form-group my-2">
                                        <Form.Select
                                            name="locationTypeId"
                                            value={values.locationTypeId}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            variant="outlined"
                                            className="form-control">
                                            <option>Select One</option>
                                            {locationTypes.map(mapLocationTypesOptions)}
                                        </Form.Select>
                                    </div>
                                    <AddressAutocomplete onAddressSelected={onAddressSelect} />
                                </div>
                            </div>
                        </div>
                    )}

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
                                    !values.latitude ||
                                    Boolean(errors.latitude) ||
                                    !values.longitude ||
                                    Boolean(errors.longitude) ||
                                    !values.city ||
                                    Boolean(errors.city)
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

EventLocation.propTypes = wizardPropTypes.eventPropTypes;

export default withFormik({
    enableReinitialize: true,
    mapPropsToValues: (props) => ({
        organizationId: props.formData.organizationId,
        locationTypeId: props.formData.locationTypeId,
        lineOne: props.formData.lineOne,
        lineTwo: props.formData.lineTwo,
        city: props.formData.city,
        zip: props.formData.zip,
        stateId: props.formData.stateId,
        latitude: props.formData.latitude,
        longitude: props.formData.longitude,
    }),

    validationSchema: eventLocationSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(EventLocation);
