import React, { useState } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import contactValidation from '../../schema/contactValidation';
import debug from 'sabio-debug';
import '../contactus/contactstyle.css';
import * as contactService from '../../services/contactService';
import toastr from 'toastr';

const _logger = debug.extend('ContactUs');

function ContactUsForm() {
    const [contactFormData] = useState({
        name: '',
        surname: '',
        email: '',
        subject: '',
        message: '',
    });
    const onSubmit = (values, onSubmitProps) => {
        _logger('Contact Works', values);
        _logger('Submit', onSubmitProps);
        onSubmitProps.setSubmitting(false);
        onSubmitProps.resetForm(contactFormData);
        contactService.contactEmail(values).then(onCreateEmailSuccess).catch(onCreateEmailError);
    };

    const onCreateEmailSuccess = (response) => {
        toastr.success('Successful Submit!');
        _logger('oncreateEmailSuccess response ->', response);
    };

    const onCreateEmailError = (err) => {
        toastr.error('Unuccessful Submission');
        _logger('error', err);
    };

    return (
        <Formik initialValues={contactFormData} validationSchema={contactValidation} onSubmit={onSubmit}>
            <Form id="contact-form" role="form">
                <div className="card mt-2 mx-auto p-4 bg-none">
                    <div className="card-body">
                        <div className="controls">
                            <div className="row">
                                <div className="col-md-6">
                                    <div className="form-group">
                                        <label htmlFor="form-name">Firstname</label>
                                        <Field
                                            id="form-name"
                                            type="text"
                                            name="name"
                                            className="form-control"
                                            placeholder="John"
                                        />
                                        <ErrorMessage name="name" component="div" className="contact-error" />
                                    </div>
                                </div>
                                <div className="col-md-6">
                                    <div className="form-group">
                                        <label htmlFor="form-lastname">Lastname</label>
                                        <Field
                                            id="form-lastname"
                                            type="text"
                                            name="surname"
                                            className="form-control"
                                            placeholder="Smith"
                                        />
                                        <ErrorMessage name="surname" component="div" className="contact-error" />
                                    </div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-md-6">
                                    <div className="form-group">
                                        <label htmlFor="form-email">Email</label>
                                        <Field
                                            id="form-email"
                                            type="email"
                                            name="email"
                                            className="form-control"
                                            placeholder="johnsmith@abc.com"
                                        />
                                        <ErrorMessage name="email" component="div" className="contact-error" />
                                    </div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-md-12">
                                    <div className="form-grup">
                                        <label htmlFor="form-subject">Subject</label>
                                        <Field
                                            id="form-subject"
                                            type="text"
                                            name="subject"
                                            className="form-control"
                                            placeholder="Subject"
                                        />
                                        <ErrorMessage name="subject" component="div" className="contact-error" />
                                    </div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="contact-col-md-12">
                                    <div className="form-group">
                                        <label htmlFor="form-message">Write your message here</label>{' '}
                                        <Field
                                            as="textarea"
                                            id="form-message"
                                            name="message"
                                            className="form-control"
                                            placeholder="Write your message here."
                                            rows="4">
                                            /
                                        </Field>
                                        <ErrorMessage name="message" component="div" className="contact-error" />
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col">
                                        <div className="text-center">
                                            <button type="submit" className="btn btn-primary">
                                                Send Message
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </Form>
        </Formik>
    );
}
export default ContactUsForm;
