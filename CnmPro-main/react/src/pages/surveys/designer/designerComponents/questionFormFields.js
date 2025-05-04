import React from 'react';
import { ErrorMessage, useField } from 'formik';
import PropTypes from 'prop-types';
import './questionFormFields.css';

const Select = ({ label, ...props }) => {
    // useField() returns [formik.getFieldProps(), formik.getFieldMeta()]
    // which we can spread on <input> and alse replace ErrorMessage entirely.
    const [field, meta] = useField(props);
    return (
        <>
            <label className="questionFormFields-form-label" htmlFor={props.name}>
                {label}
            </label>
            <select className="questionFormFields-form-control questionFormFields-react-select" {...field} {...props} />
            {meta.touched && meta.error ? <ErrorMessage>{meta.error}</ErrorMessage> : null}
        </>
    );
};

Select.propTypes = {
    label: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
};

const TextArea = ({ label, ...props }) => {
    const [field, meta] = useField(props);
    return (
        <>
            <label className="questionFormFields-form-label" htmlFor={props.name}>
                {label}
            </label>
            <textarea className="questionFormFields-text-area questionFormFields-form-control" {...field} {...props} />
            {meta.touched && meta.error ? <ErrorMessage>{meta.error}</ErrorMessage> : null}
        </>
    );
};

TextArea.propTypes = {
    label: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
};

const Checkbox = ({ ...props }) => {
    const [field, meta] = useField({ ...props, type: 'checkbox' });
    return (
        <>
            <label className="questionFormFields-checkbox questionFormFields-form-label">
                <input {...field} {...props} type="checkbox" />
            </label>
            {meta.touched && meta.error ? <ErrorMessage>{meta.error}</ErrorMessage> : null}
        </>
    );
};

const TextInput = ({ label, ...props }) => {
    const [field, meta] = useField(props);
    return (
        <>
            <label className="questionFormFields-form-label" htmlFor={props.name}>
                {label}
            </label>
            <input className="questionFormFields-text-input questionFormFields-form-control" {...field} {...props} />
            {meta.touched && meta.error ? (
                <ErrorMessage className="questionFormFields-error">{meta.error}</ErrorMessage>
            ) : null}
        </>
    );
};

const RadioButtons = ({ ...props }) => {
    const [field, meta] = useField({ ...props, type: 'radio' });
    return (
        <>
            <label className="questionFormFields-checkbox questionFormFields-form-label">
                <input {...field} {...props} type="radio" />
            </label>
            {meta.touched && meta.error ? <ErrorMessage>{meta.error}</ErrorMessage> : null}
        </>
    );
};

TextInput.propTypes = {
    label: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
};

export { Select, Checkbox, TextArea, TextInput, RadioButtons };
