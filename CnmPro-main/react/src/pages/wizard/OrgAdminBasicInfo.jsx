import React, { useEffect, useState } from 'react';
import organizationSchema from '../../schema/organization/organization';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as adminPropTypes from './orgAdminPropTypes';
import sabioDebug from 'sabio-debug';
import lookUpService from '../../services/lookUpService';
import toastr from 'toastr';

const _logger = sabioDebug.extend('OrgAdminBasicInfo');

function OrgAdminBasicInfo(props) {
    const { values, errors, handleChange, handleBlur, handleSubmit, nextLabel, onNext, touched } = props;

    const [organizationTypes, setOrganizationTypes] = useState([]);

    const onNextClicked = () => {
        onNext(values);
        _logger('passing values', values);
        _logger('props', nextLabel);
    };

    const mapOrganizationTypesOptions = (organizationType) => {
        return (
            <option key={organizationType.id} value={organizationType.id} type="number">
                {organizationType.name}
            </option>
        );
    };

    useEffect(() => {
        lookUpService
            .LookUp(['OrganizationTypes', 'LocationTypes', 'States'])
            .then(onSuccessOrganizationTypes)
            .catch(onErrorOrganizationTypes);
    }, []);

    const onSuccessOrganizationTypes = (response) => {
        _logger('LookUp', response);
        setOrganizationTypes(response.item.organizationTypes);
    };

    const onErrorOrganizationTypes = (err) => {
        _logger('Get Organization Types error', err);
        toastr.error('Error');
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto org-form-card-width">
                    <Card.Header>
                        <h3 className="text-center">Enter Basic Information</h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <div className="text-center"></div>
                    </div>

                    <div className="form-group mt-2">
                        <div className="row">
                            <div className="col-sm-6">
                                <label htmlFor="name">Name</label>
                                <div className="form-group my-2">
                                    <Form.Control
                                        name="name"
                                        value={values.name}
                                        onChange={handleChange}
                                        onBlur={handleBlur}
                                        variant="outlined"
                                        placeholder="Enter name"
                                        className={`form-control ${
                                            errors.name && touched.name && 'is-invalid'
                                        }`}></Form.Control>
                                </div>

                                <div className="col-sm-6">
                                    <label htmlFor="organizationTypeId">Organization Type</label>
                                    <div className="form-group my-2">
                                        <Form.Select
                                            name="organizationTypeId"
                                            value={values?.organizationTypeId || 0}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            variant="outlined"
                                            className="form-control">
                                            <option>Select One</option>
                                            {organizationTypes.map(mapOrganizationTypesOptions)}
                                        </Form.Select>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <label htmlFor="description">Description</label>
                        <div className="form-group my-2">
                            <Form.Control
                                as="textarea"
                                rows={6}
                                name="description"
                                value={values.description}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                className={`form-control ${errors.description && touched.description && 'is-invalid'}`}
                            />
                        </div>

                        <label htmlFor="siteUrl">Website</label>
                        <div className="form-group my-2">
                            <Form.Control
                                as="textarea"
                                rows={1}
                                name="siteUrl"
                                value={values.siteUrl}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Enter URL"
                                className={`form-control ${errors.siteUrl && touched.siteUrl && 'is-invalid'}`}
                            />
                        </div>
                        <div className="button-group pt-3">
                            <button
                                type="submit"
                                className="btn btn-primary ml-2"
                                disabled={
                                    !values.organizationTypeId ||
                                    Boolean(errors.organizationTypeId) ||
                                    !values.name ||
                                    Boolean(errors.name) ||
                                    !values.description ||
                                    Boolean(errors.description) ||
                                    !values.siteUrl ||
                                    Boolean(errors.siteUrl)
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
}

OrgAdminBasicInfo.propTypes = adminPropTypes.orgAdminPropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        organizationTypeId: `${parseInt(props.formData.organizationTypeId)}`,
        name: props.formData.name,
        description: props.formData.description,
        siteUrl: props.formData.siteUrl,
    }),

    validationSchema: organizationSchema,

    handleSubmit: (values, { props }) => {
        props.onNext({
            organizationTypeId: parseInt(values.organizationTypeId),
            name: values.name,
            description: values.description,
            siteUrl: values.siteUrl,
        });
    },
    enableReinitialize: true,
})(OrgAdminBasicInfo);
