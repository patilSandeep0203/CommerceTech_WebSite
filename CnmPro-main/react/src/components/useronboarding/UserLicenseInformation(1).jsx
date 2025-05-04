import React, { useEffect, useState } from 'react';
import { withFormik, FieldArray, Field, ErrorMessage } from 'formik';
import { Form, Card, Table } from 'react-bootstrap';
import userLicenseInfoSchema from '../../schema/useronboarding/userLicenseInfoSchema';
import * as wizardPropTypes from './userOnboardingPropTypes';
import { TableBody, TableHead, TableRow } from '@material-ui/core';
import { Typeahead } from 'react-bootstrap-typeahead';
import debug from 'sabio-debug';
import 'react-bootstrap-typeahead/css/Typeahead.css';

const _logger = debug.extend('UserLicenseInformation');

const UserLicenseInformation = (props) => {
    const {
        values,
        errors,
        touched,
        handleChange,
        handleBlur,
        isSubmitting,
        handleSubmit,
        onBack,
        backLabel,
        nextLabel,
        onNext,
        cantBack,
        onChange,
        lookUpResults,
    } = props;

    useEffect(() => {
        onChange(values);
    }, []);

    const mapLicenseTypesOptions = (licenseType) => {
        return (
            <option key={licenseType.id} value={licenseType.id} type="number">
                {licenseType.name}
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

    const mapProfession = (profession) => {
        return { id: profession.id, value: profession.name, label: profession.name };
    };

    const onNextClicked = () => {
        onNext(values);
    };

    const [multiSelections, setMultiSelections] = useState([]);

    const handleMultiChange = (selected) => {
        setMultiSelections(selected);
        _logger('selected', selected);
        handleChange({ target: { name: 'profession', value: selected.map((option) => option.value) } });
    };

    _logger(multiSelections, 'selections', values);

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto" style={{ width: 950 }}>
                    <h3 className="text-center">User License And Profession Information</h3>
                    <Card.Body>
                        <div className="form-group my-2">
                            <FieldArray
                                name="license"
                                value={values.license}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                render={({ push, remove }) => (
                                    <div>
                                        <label htmlFor="licenseNumber" className="col-2">
                                            Licenses
                                        </label>
                                        <button
                                            className="btn btn-outline-primary btn-sm"
                                            type="button"
                                            onClick={() =>
                                                push({
                                                    licenseStateId: '',
                                                    licenseTypeId: '',
                                                    licenseNumber: '',
                                                    dateExpires: '',
                                                })
                                            }>
                                            Add
                                        </button>
                                        {values.license[0] && (
                                            <Table>
                                                <TableHead>
                                                    <TableRow>
                                                        <th>State</th>
                                                        <th>License Type</th>
                                                        <th>License Number</th>
                                                        <th>Date Expires</th>
                                                    </TableRow>
                                                </TableHead>
                                                <TableBody>
                                                    {values.license &&
                                                        values.license.map((license, index) => (
                                                            <TableRow key={index}>
                                                                <td>
                                                                    <Field
                                                                        name={`license[${index}].licenseStateId`}
                                                                        component="select">
                                                                        <option value="">Select One</option>
                                                                        {lookUpResults.states.map(mapStateOptions)}
                                                                    </Field>
                                                                    <ErrorMessage
                                                                        name={`license[${index}].licenseStateId`}
                                                                        component="div"
                                                                        className="has-error"
                                                                    />
                                                                </td>
                                                                <td>
                                                                    <Field
                                                                        name={`license[${index}].licenseTypeId`}
                                                                        component="select">
                                                                        <option value="">Select One</option>
                                                                        {lookUpResults.licenseTypes.map(
                                                                            mapLicenseTypesOptions
                                                                        )}
                                                                    </Field>
                                                                    <ErrorMessage
                                                                        name={`license[${index}].licenseTypeId`}
                                                                        component="div"
                                                                        className="has-error"
                                                                    />
                                                                </td>
                                                                <td>
                                                                    <Field
                                                                        name={`license[${index}].licenseNumber`}
                                                                        type="text"
                                                                    />
                                                                    <ErrorMessage
                                                                        name={`license[${index}].licenseNumber`}
                                                                        component="div"
                                                                        className="has-error"
                                                                    />
                                                                </td>
                                                                <td>
                                                                    <Field
                                                                        name={`license[${index}].dateExpires`}
                                                                        type="date"
                                                                    />
                                                                    <ErrorMessage
                                                                        name={`license[${index}].dateExpires`}
                                                                        component="div"
                                                                        className="has-error"
                                                                    />
                                                                </td>
                                                                <td>
                                                                    <button
                                                                        className="btn btn-outline-danger btn-sm"
                                                                        onClick={() => remove(index)}>
                                                                        X
                                                                    </button>
                                                                </td>
                                                            </TableRow>
                                                        ))}
                                                </TableBody>
                                            </Table>
                                        )}
                                    </div>
                                )}
                            />
                        </div>
                        <div className="form-group my-2">
                            <label htmlFor="profession">Profession</label>
                            <Typeahead
                                name="profession"
                                id="select3"
                                multiple
                                onChange={handleMultiChange}
                                options={lookUpResults.professions.map(mapProfession)}
                                placeholder="Choose a profession"
                                selected={multiSelections}
                                isInvalid={!!touched.profession && !!errors.profession}
                                onBlur={() => handleBlur({ target: { name: 'profession' } })}
                            />
                            <ErrorMessage name="profession" component="div" className="has-error" />
                        </div>
                        <label htmlFor="yearsOfExperience">Total Years Of Experience</label>
                        <div className="form-group my-2">
                            <Form.Control
                                name="yearsOfExperience"
                                value={values.yearsOfExperience}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Years Of Experience"
                                className="form-control"
                            />
                            <ErrorMessage name="yearsOfExperience" component="div" className="has-error" />
                        </div>
                        <label htmlFor="desiredHourlyRate">Desired Hourly Rate</label>
                        <div className="form-group my-2">
                            <Form.Control
                                name="desiredHourlyRate"
                                value={values.desiredHourlyRate}
                                onChange={handleChange}
                                onBlur={handleBlur}
                                variant="outlined"
                                placeholder="Desired Hourly Rate"
                                className="form-control"
                            />
                            <ErrorMessage name="desiredHourlyRate" component="div" className="has-error" />
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
                                        !values.license[0] ||
                                        Boolean(errors.license) ||
                                        !values.profession ||
                                        Boolean(errors.profession) ||
                                        !values.yearsOfExperience ||
                                        Boolean(errors.yearsOfExperience) ||
                                        !values.desiredHourlyRate ||
                                        Boolean(errors.desiredHourlyRate)
                                    }
                                    onClick={onNextClicked}>
                                    {nextLabel}
                                </button>
                            </div>
                        </div>
                    </Card.Body>
                </Card>
            </Form>
        </React.Fragment>
    );
};

UserLicenseInformation.propTypes = wizardPropTypes.userOnboardingPropTypes;

export default withFormik({
    mapPropsToValues: (props) => ({
        license: props.formData.license,
        yearsOfExperience: props.formData.yearsOfExperience,
        desiredHourlyRate: props.formData.desiredHourlyRate,
        profession: props.formData.profession,
    }),

    validationSchema: userLicenseInfoSchema,

    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(UserLicenseInformation);
