// @flow
import React, { useState } from 'react';
import { Form, InputGroup } from 'react-bootstrap';
import classNames from 'classnames';
import PropTypes from 'prop-types';

/* Password Input */
const PasswordInput = ({ name, placeholder, refCallback, errors, register, className }) => {
    const [showPassword, setShowPassword] = useState(false);

    return (
        <>
            <InputGroup className="mb-0">
                <Form.Control
                    type={showPassword ? 'text' : 'password'}
                    placeholder={placeholder}
                    name={name}
                    id={name}
                    as="input"
                    ref={(r) => {
                        if (refCallback) refCallback(r);
                    }}
                    className={className}
                    isInvalid={errors && errors[name] ? true : false}
                    {...(register ? register(name) : {})}
                    autoComplete={name}
                />
                <div
                    className={classNames('input-group-text', 'input-group-password', {
                        'show-password': showPassword,
                    })}
                    data-password={showPassword ? 'true' : 'false'}>
                    <span
                        className="password-eye"
                        onClick={() => {
                            setShowPassword(!showPassword);
                        }}></span>
                </div>
            </InputGroup>
        </>
    );
};

const FormInput = ({
    label,
    type,
    name,
    placeholder,
    register,
    errors,
    className,
    labelClassName,
    containerClass,
    refCallback,
    children,
    ...otherProps
}) => {
    // handle input type
    const comp = type === 'textarea' ? 'textarea' : type === 'select' ? 'select' : 'input';

    return (
        <>
            {type === 'hidden' ? (
                <input type={type} name={name} {...(register ? register(name) : {})} {...otherProps} />
            ) : (
                <>
                    {type === 'password' ? (
                        <>
                            <Form.Group className={containerClass}>
                                {label ? (
                                    <>
                                        {' '}
                                        <Form.Label className={labelClassName}>{label}</Form.Label> {children}{' '}
                                    </>
                                ) : null}
                                <PasswordInput
                                    name={name}
                                    placeholder={placeholder}
                                    refCallback={refCallback}
                                    errors={errors}
                                    register={register}
                                    className={className}
                                />

                                {errors && errors[name] ? (
                                    <Form.Control.Feedback type="invalid" className="d-block">
                                        {errors[name]['message']}
                                    </Form.Control.Feedback>
                                ) : null}
                            </Form.Group>
                        </>
                    ) : (
                        <>
                            {type === 'select' ? (
                                <>
                                    <Form.Group className={containerClass}>
                                        {label ? <Form.Label className={labelClassName}>{label}</Form.Label> : null}

                                        <Form.Select
                                            type={type}
                                            label={label}
                                            name={name}
                                            id={name}
                                            ref={(r) => {
                                                if (refCallback) refCallback(r);
                                            }}
                                            comp={comp}
                                            className={className}
                                            isInvalid={errors && errors[name] ? true : false}
                                            {...(register ? register(name) : {})}
                                            {...otherProps}>
                                            {children}
                                        </Form.Select>

                                        {errors && errors[name] ? (
                                            <Form.Control.Feedback type="invalid">
                                                {errors[name]['message']}
                                            </Form.Control.Feedback>
                                        ) : null}
                                    </Form.Group>
                                </>
                            ) : (
                                <>
                                    {type === 'checkbox' || type === 'radio' ? (
                                        <>
                                            <Form.Group className={containerClass}>
                                                <Form.Check
                                                    type={type}
                                                    label={label}
                                                    name={name}
                                                    id={name}
                                                    ref={(r) => {
                                                        if (refCallback) refCallback(r);
                                                    }}
                                                    className={className}
                                                    isInvalid={errors && errors[name] ? true : false}
                                                    {...(register ? register(name) : {})}
                                                    {...otherProps}
                                                />

                                                {errors && errors[name] ? (
                                                    <Form.Control.Feedback type="invalid">
                                                        {errors[name]['message']}
                                                    </Form.Control.Feedback>
                                                ) : null}
                                            </Form.Group>
                                        </>
                                    ) : (
                                        <Form.Group className={containerClass}>
                                            {label ? <Form.Label className={labelClassName}>{label}</Form.Label> : null}

                                            <Form.Control
                                                type={type}
                                                placeholder={placeholder}
                                                name={name}
                                                id={name}
                                                as={comp}
                                                ref={(r) => {
                                                    if (refCallback) refCallback(r);
                                                }}
                                                className={className}
                                                isInvalid={errors && errors[name] ? true : false}
                                                {...(register ? register(name) : {})}
                                                {...otherProps}
                                                autoComplete={name}>
                                                {children ? children : null}
                                            </Form.Control>

                                            {errors && errors[name] ? (
                                                <Form.Control.Feedback type="invalid">
                                                    {errors[name]['message']}
                                                </Form.Control.Feedback>
                                            ) : null}
                                        </Form.Group>
                                    )}
                                </>
                            )}
                        </>
                    )}
                </>
            )}
        </>
    );
};

PasswordInput.propTypes = {
    name: PropTypes.string.isRequired,
    placeholder: PropTypes.string.isRequired,
    refCallback: PropTypes.string.isRequired,
    errors: PropTypes.string.isRequired,
    register: PropTypes.string.isRequired,
    className: PropTypes.string.isRequired,
};

FormInput.propTypes = {
    label: PropTypes.string.isRequired,
    type: PropTypes.string.isRequired,
    name: PropTypes.string.isRequired,
    placeholder: PropTypes.string.isRequired,
    register: PropTypes.string.isRequired,
    errors: PropTypes.string.isRequired,
    className: PropTypes.string.isRequired,
    labelClassName: PropTypes.string.isRequired,
    containerClass: PropTypes.string.isRequired,
    refCallback: PropTypes.string.isRequired,
    children: PropTypes.string.isRequired,
};

export default FormInput;
