import './proposals.css';
import React, { useState } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import debug from 'sabio-debug';
import proposalSchema from '../../schema/proposals/proposalsSchema';
import { useLocation, useNavigate } from 'react-router-dom';
import proposalService from '../../services/proposalService';
import swal from '@sweetalert/with-react';
import { Card, Col } from 'react-bootstrap';

const _logger = debug.extend('ProposalForm');

function Proposal() {
    const [proposalFormData, setProposalFormData] = useState({
        jobTitle: '',
        jobId: '',
        bid: '',
        duration: '',
        details: '',
    });

    const navigate = useNavigate();

    const jobState = useLocation();

    _logger(jobState, 'Jobstate response', useLocation());

    const newJobTitle = jobState.state.payload;

    let payload = null;

    _logger(proposalFormData);

    const navigateToViewProposals = (proposal) => {
        const jobState = { type: 'proposalObject', payload: { ...proposal } };
        navigate('/proposals/all', { state: jobState });
    };

    const onSubmitClick = (values) => {
        payload = {
            jobId: jobState.state.payload.id,
            bid: values.bid,
            duration: values.duration,
            details: values.details,
        };
        _logger(payload);
        proposalService.addProposal(payload).then(onSubmitSuccess).catch(onSubmitError);
    };

    const onSubmitSuccess = (response) => {
        _logger(response, 'onSubmitSuccess');
        payload.title = newJobTitle.title;
        navigateToViewProposals(payload);
        setProposalFormData((prevState) => {
            return {
                ...prevState,
                proposal: response.item.pagedItems,
            };
        });
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Congratulations you have created a proposal',
            icon: 'success',
        });
    };

    const onSubmitError = (response) => {
        _logger(response, 'onSubmitError');
    };

    return (
        <div className="container">
            <div className="row">
                <div className="card">
                    <Col>
                    <Formik
                        enableReinitialize={true}
                        initialValues={proposalFormData}
                        validationSchema={proposalSchema}
                        onSubmit={onSubmitClick}>
                        <Form className="form-proposal-pos form-proposal-background">
                            <Card.Header>
                                <h1 className="form-title-proposal-cnmpro">
                                    <strong>Create a Proposal</strong>
                                </h1>
                            </Card.Header>
                            <Card.Body>
                                <h3>Proposal Information</h3>
                                <label htmlFor="name-of-job"> Job </label>
                                <Field
                                    className="proposal-form-select form-select-md proposal-select-input-proposal-cnmpro"
                                    aria-label=".form-select-lg example"
                                    name="jobId"
                                    value={newJobTitle.title}
                                />
                                <div className="form-group field-proposallabel-cnmpro">
                                    <label htmlFor="bid">Bid</label>
                                    <Field
                                        placeholder="Your proposed budget..."
                                        type="text"
                                        name="bid"
                                        className="form-control input-proposal-cnmpro"
                                    />
                                    <ErrorMessage name="bid" component="div" className="has-error-proposal-cnmpro" />
                                    <div className="form-group field-proposallabel-cnmpro">
                                        <label htmlFor="duration">Duration</label>
                                        <Field
                                            placeholder="The time it'll take to complete this job..."
                                            type="text"
                                            name="duration"
                                            className="form-control input-proposal-cnmpro"
                                        />
                                        <ErrorMessage
                                            name="duration"
                                            component="div"
                                            className="has-error-proposal-cnmpro"
                                        />
                                    </div>
                                    <div className="form-group field-proposallabel-cnmpro">
                                        <label htmlFor="details">Details</label>
                                        <Field
                                            placeholder="Describe how you'll be able to complete this job. Give specfics as to the equipment that you'll use, the material that you need, etc..."
                                            type="text"
                                            name="details"
                                            component="textarea"
                                            className="form-control input-proposal-cnmpro"
                                        />
                                        <ErrorMessage
                                            name="details"
                                            component="div"
                                            className="has-error-proposal-cnmpro"
                                        />
                                    </div>
                                </div>
                            </Card.Body>
                            <Card.Footer>
                                <button type="submit" className="btn btn-primary button-proposalform-cnmpro">
                                    Submit
                                </button>
                            </Card.Footer>
                        </Form>
                    </Formik>
                    </Col>
                </div>
            </div>
        </div>
    );
}

export default Proposal;
