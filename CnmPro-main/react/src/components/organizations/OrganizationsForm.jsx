import './organizations.css';
import React, { useState, useEffect } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import debug from 'sabio-debug';
import { useNavigate, useLocation } from 'react-router-dom';
import * as organizationsService from '../../services/organizationsService';
import organizationSchema from '../../schema/organization/organizationSchema';
import swal from '@sweetalert/with-react';

const _logger = debug.extend('OrganizationForm');

function OrganizationForm() {
    const navigate = useNavigate();
    const { state } = useLocation();

    const [organizationFormData, setOrganizationFormData] = useState({
        locationTypeId: '',
        lineOne: '',
        lineTwo: '',
        city: '',
        zip: '',
        stateId: '',
        organizationTypeId: '',
        name: '',
        headline: '',
        description: '',
        logo: '',
        phone: '',
        siteUrl: '',
    });

    useEffect(() => {
        let type = ['OrganizationTypes'];
        let locType = ['LocationTypes'];
        organizationsService.getLookups(type).then(onSuccessOrganizationTypes).catch(onGetOrganizationTypesError);
        organizationsService.getStates().then(onSuccessState).catch(onGetStatesError);
        organizationsService.getLookups(locType).then(onSuccessLocationTypes).catch(onLocationTypesError);
    }, []);

    useEffect(() => {
        if (state?.type === 'ORGANIZATION_FORM_EDIT') {
            _logger('STATE HERE ------>', state);
            let organizationFormData = { ...state.payload };
            let locationFormData = { ...state.payload.location };

            setOrganizationFormData((prevState) => {
                let orgValues = { ...prevState };
                orgValues = {
                    locationTypeId: locationFormData.locationType.id,
                    lineOne: locationFormData.lineOne,
                    lineTwo: locationFormData.lineTwo,
                    city: locationFormData.city,
                    zip: locationFormData.zip,
                    stateId: locationFormData.state.id,
                    organizationTypeId: organizationFormData.organizationType.id,
                    name: organizationFormData.name,
                    headline: organizationFormData.headline,
                    description: organizationFormData.description,
                    logo: organizationFormData.logo,
                    phone: organizationFormData.phone,
                    siteUrl: organizationFormData.siteUrl,
                };
                return orgValues;
            });
        } 
    }, [state]);

    const [organizationTypes, setOrganizationTypes] = useState([]);
    const [locationTypes, setLocationTypes] = useState([]);
    const [states, setStates] = useState([]);

    const goToPage = () => {
        navigate('/organizations');
    };

    const mapOrganizationTypesOptions = (type) => {
        return (
            <option key={type.id} value={type.id} type="number">
                {type.name}
            </option>
        );
    };

    const mapLocTypesOptions = (locType) => {
        return (
            <option key={locType.id} value={locType.id} type="number">
                {locType.name}
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

    const onSuccessState = (response) => {
        setStates((prevState) => {
            let s = { ...prevState };
            s = response.data.item;
            _logger(response, 'logger ---------------');
            return s;
        });
    };

    const onSuccessLocationTypes = (response) => {
        setLocationTypes((prevState) => {
            let ot = { ...prevState };
            ot = response.data.item.locationTypes;
            return ot;
        });
    };

    const onLocationTypesError = (err) => {
        _logger('Get Location Types error', err);
    };

    const onSuccessOrganizationTypes = (response) => {
        setOrganizationTypes((prevState) => {
            let ot = { ...prevState };
            ot = response.data.item.organizationTypes;
            return ot;
        });
    };

    const onGetOrganizationTypesError = (err) => {
        _logger('Get Organization Types error', err);
    };

    const onGetStatesError = (err) => {
        _logger('Get States Error', err);
    };

    const onUpdateSuccess = (response) => {
        _logger('Successful Edited', response);
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your Organization has been updated!',
            showConfirmButton: false,
            timer: 5000
          })
        goToPage();
    };

    const onUpdateError = (response) => {
        _logger('Update error occurec', response);
    };

    const onSubmitClick = (values) => {
       
        if (state?.type === 'ORGANIZATION_FORM_EDIT') {

            let updateOfd = {
                locationTypeId: values.locationTypeId,
                lineOne: values.lineOne,
                lineTwo: values.lineTwo,
                city: values.city,
                zip: values.zip,
                stateId: values.stateId,
                organizationTypeId: values.organizationTypeId,
                name: values.name,
                headline: values.headline,
                description: values.description,
                logo: values.logo,
                locationId: state.payload.location.id,
                phone: values.phone,
                siteUrl: values.siteUrl,
            };

            let orgId = state.payload.id
            _logger("C",orgId, updateOfd);
            organizationsService
                .UpdateOrganization( orgId, updateOfd )
                .then(onUpdateSuccess)
                .catch(onUpdateError);
        } else {

            let ofd = {
                locationTypeId: values.locationTypeId,
                lineOne: values.lineOne,
                lineTwo: values.lineTwo,
                city: values.city,
                zip: values.zip,
                stateId: values.stateId,
                organizationTypeId: values.organizationTypeId,
                name: values.name,
                headline: values.headline,
                description: values.description,
                logo: values.logo,
                phone: values.phone,
                siteUrl: values.siteUrl,
            };
            organizationsService
            .CreateOrganization(ofd)
            .then(onSubmitClickSuccess)
            .catch(onSubmitClickError);
        }
    };

    const onSubmitClickSuccess = (response) => {
        _logger('Successfuly submitted', response);
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your Organization has successfully been created!',
            showConfirmButton: false,
            timer: 5000
          })
        goToPage();
    };

    const onSubmitClickError = () => {
        swal ({
            buttons: {
            cancel: 'Ok',
            },
            title: 'Please check the missing fields',
            icon: 'error'
        });
    };

    return (
        <div className="container ">
            <div className="row">
                <Formik
                    enableReinitialize={true}
                    initialValues={organizationFormData}
                    validationSchema={organizationSchema}
                    onSubmit={onSubmitClick}>
                    {({ values, handleChange }) => (
                        <Form className="form-organization-pos form-organization-background">
                            <div className="inline-display">
                                <div className="container" style={{ marginTop: '75px' }}>
                                    <button
                                        type="button"
                                        id="organizationsreturn"
                                        className="btn btn-danger mx-1 back-button-form-cnmpro"
                                        data-page="/organizations"
                                        onClick={goToPage}>
                                        Go Back
                                    </button>
                                    {state ? (
                                        <h1 className="form-title-org-cnmpro">Edit Organization</h1>
                                    ) : (
                                        <h1 className="form-title-org-cnmpro">Create an Organization</h1>
                                    )}
                                </div>
                            </div>
                            <div className="col mx-4">
                                <h3 className="section-header-org-cnmpro">Organizations Information</h3>
                                <div className="form-group field-orglabel-cnmpro">
                                    <label htmlFor="name-of-organization">Name of Organization</label>
                                    {values.organizationFormData}
                                    <Field
                                        placeholder="Organizations Name"
                                        type="text"
                                        name="name"
                                        className="form-control input-org-cnmpro"
                                    />
                                    <ErrorMessage name="name" component="div" className="has-error-org-cnmpro" />
                                </div>
                                <div className="className field-orglabel-cnmpro">
                                    <label htmlFor="type-of-organization">Organization Type</label>
                                    <select
                                        className="form-select form-select-md org-type-org-cnmpro"
                                        aria-label=".form-select-lg example"
                                        name="organizationTypeId"
                                        value={values.organizationTypeId}
                                        onChange={handleChange}>
                                        <option>Select a Type</option>
                                        {organizationTypes.map(mapOrganizationTypesOptions)}
                                    </select>

                                    <div className="side-by-side-org-cnmpro"></div>
                                    <div className="form-group field-orglabel-cnmpro">
                                        <label htmlFor="Headline">Headline</label>
                                        <Field
                                            placeholder="Mission Statement, Slogan, etc."
                                            type="text"
                                            name="headline"
                                            className="form-control input-org-cnmpro"
                                        />
                                        <ErrorMessage
                                            name="headline"
                                            component="div"
                                            className="has-error-org-cnmpro"
                                        />
                                    <div className="side-by-side-org-cnmpro">
                                        <div className="form-group short-left-label-org-cnmpro">
                                            <label htmlFor="site-url">Organizations Website</label>
                                            <Field
                                                placeholder="www.example.com"
                                                type="text"
                                                name="siteUrl"
                                                className="form-control input-pair1-org-cnmpro"
                                            />
                                            <ErrorMessage
                                                name="siteUrl"
                                                component="div"
                                                className="has-error-org-cnmpro"
                                            />
                                        </div>
                                        <div className="form-group short-right-label-org-cnmpro">
                                            <label htmlFor="Logo">Logo URL</label>
                                            <Field
                                                placeholder="www.example.com"
                                                type="text"
                                                name="logo"
                                                className="form-control input-logo-org-cnmpro"
                                            />
                                            <ErrorMessage
                                                name="logo"
                                                component="div"
                                                className="has-error-org-cnmpro"
                                            />
                                        </div>
                                        </div>
                                            <div className="form-group side-by-side-field-org-cnmpro">
                                                <label htmlFor="number">Phone Number</label>
                                                <Field
                                                    placeholder="(000)000-0000"
                                                    type="text"
                                                    name="phone"
                                                    className="form-control org-select-input-org-cnmpro"
                                                />
                                                <ErrorMessage
                                                    name="phone"
                                                    component="div"
                                                    className="has-error-org-cnmpro"
                                                />
                                            </div>
                                        <div className="form-group field-orglabel-cnmpro">
                                            <label htmlFor="Description">Description</label>
                                            <Field
                                                placeholder="ex. Organizations mission statement, Accomplishments, etc."
                                                type="text"
                                                name="description"
                                                component="textarea"
                                                className="form-control desc-input-org-cnmpro"
                                            />
                                            <ErrorMessage
                                                name="description"
                                                component="div"
                                                className="has-error-org-cnmpro"
                                            />
                                        </div>
                                        <h3 className="section-header-org-cnmpro">Location Information</h3>
                                        <div className="">
                                            <label htmlFor="locationTypeId">Location Type</label>
                                            <Field
                                                as="select"
                                                className="form-select form-select-md location-type-org-cnmpro"
                                                aria-label=".form-select-lg example"
                                                name="locationTypeId">
                                                <option>Select a Type of Location</option>
                                                {locationTypes?.map(mapLocTypesOptions)}
                                            </Field>
                                            <ErrorMessage
                                                name="locationTypeId"
                                                component="div"
                                                className="has-error-org-cnmpro"
                                            />
                                        </div>
                                        <div className="side-by-side-org-cnmpro">
                                            <div className="form-group short-left-label-org-cnmpro">
                                                <label htmlFor="Logo">Street Address</label>
                                                <Field
                                                    placeholder="123 Main St"
                                                    type="text"
                                                    name="lineOne"
                                                    className="form-control input-pair1-org-cnmpro"
                                                />
                                                <ErrorMessage
                                                    name="lineOne"
                                                    component="div"
                                                    className="has-error-org-cnmpro"
                                                />
                                            </div>
                                        <div className="form-group short-right-label-org-cnmpro">
                                            <label htmlFor="Logo">Street Address 2</label>
                                            <Field
                                                placeholder="Apartment, Suite, etc."
                                                type="text"
                                                name="lineTwo"
                                                className="form-control input-logo-org-cnmpro"
                                            />
                                            <ErrorMessage
                                                name="lineTwo"
                                                component="div"
                                                className="has-error-org-cnmpro"
                                            />
                                        </div>
                                        </div>
                                        <div className="side-by-side-org-cnmpro">
                                        <div className="form-group side-by-side-fields-cnmpro">
                                            <label htmlFor="Logo">City</label>
                                            <Field
                                                type="text"
                                                name="city"
                                                className="form-control city-field-org-cnmpro"
                                            />
                                            <ErrorMessage
                                                name="city"
                                                component="div"
                                                className="has-error-org-cnmpro"
                                            />
                                        </div>
                                        <div className="className side-by-side-fields-cnmpro">
                                            <label htmlFor="location" >
                                                State
                                            </label>
                                            <Field
                                                as="select"
                                                className="form-select form-select-md state-field-org-cnmpro"
                                                aria-label=".form-select-lg example"
                                                name="stateId">
                                                <option>Select a State</option>
                                                {states?.map(mapStateOptions)}
                                            </Field>
                                            <ErrorMessage
                                                name="stateId"
                                                component="div"
                                                className="has-error-org-cnmpro"
                                            />
                                        </div>
                                        <div className="form-group side-by-side-fields-cnmpro">
                                            <label htmlFor="Logo">ZipCode</label>
                                            <Field
                                                placeholder="00000"
                                                type="text"
                                                name="zip"
                                                className="form-control zip-field-org-cnmpro"
                                            />
                                            <ErrorMessage name="zip" component="div" className="has-error-org-cnmpro" />
                                        </div>
                                        </div>
                                    </div>
                                </div>
                                <button type="submit" className="btn btn-primary sub-button-orgform-cnmpro">
                                    Submit
                                </button>
                            </div>
                        </Form>
                    )}
                </Formik>
            </div>
        </div>
    );
}

export default OrganizationForm;
