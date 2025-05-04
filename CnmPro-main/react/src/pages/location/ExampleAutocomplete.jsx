import React, { useState } from 'react';
import AddressAutocomplete from './AddressAutocomplete';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import autocompleteSchema from '../../schema/autocomplete/autocompleteSchema';
import './locationStyle.css';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import PropTypes from 'prop-types';

const ExampleAutocomplete = () => {
    const [formData, setFormData] = useState({
        title: 'Some title',
        summary: 'Some summary',
        location: {},
    });
    const [selectedAddress, setSelectedAddress] = useState();

    const [showModal, setShowModa] = useState(false);

    const toggleModal = () => setShowModa(!showModal);

    const onFormSubmit = (values) => {
        setFormData(() => {
            const newState = { ...values };

            newState.location = { ...selectedAddress };
            newState.location.locationTypeId = parseInt(newState.locationTypeId);
            delete newState.locationTypeId;

            return newState;
        });

        toggleModal();
    };

    const onAddressSelect = (placeObj) => {
        setSelectedAddress({ ...placeObj });
    };

    return (
        <>
            <div className="autocomplete-container">
                <div className="shadow-lg px-4 mb-2 bg-body rounded w-25 mx-auto ">
                    <h2 className="text-center">Example Autocomplete</h2>

                    <Formik
                        onSubmit={onFormSubmit}
                        initialValues={formData}
                        enableReinitialize={true}
                        validationSchema={autocompleteSchema}>
                        <Form>
                            <div className="input-group mb-3">
                                <Field
                                    type="text"
                                    className="form-control font-monospace"
                                    placeholder="Title"
                                    name="title"
                                />
                                <ErrorMessage name="title" component="div" className="has-val-error" />
                            </div>

                            <div className="input-group mb-3">
                                <Field
                                    type="text"
                                    className="form-control font-monospace"
                                    placeholder="Summary"
                                    name="summary"
                                />
                                <ErrorMessage name="summary" component="div" className="has-val-error" />
                            </div>

                            <AddressAutocomplete onAddressSelected={onAddressSelect} />

                            <div className="pb-2 text-center">
                                <button type="submit" id="submitFormBtt" className="btn btn-outline-dark">
                                    Submit
                                </button>
                            </div>
                        </Form>
                    </Formik>
                </div>
            </div>

            <div className="text-center my-2">
                <Button variant="outline-success" onClick={toggleModal}>
                    Show Results
                </Button>
            </div>

            {showModal && <ShowModal objData={formData} isShowing={showModal} toggle={toggleModal} />}
        </>
    );
};

const ShowModal = (props) => {
    return (
        <>
            <Modal show={props.isShowing} onHide={props.toggle}>
                <Modal.Header closeButton>
                    <Modal.Title>Result</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <pre>{JSON.stringify(props.objData, null, 2)}</pre>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-success" onClick={props.toggle}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

ShowModal.propTypes = {
    isShowing: PropTypes.bool,
    toggle: PropTypes.func,
    objData: PropTypes.shape({
        title: PropTypes.string.isRequired,
        summary: PropTypes.string.isRequired,
        location: PropTypes.shape({
            lineOne: PropTypes.string.isRequired,
            city: PropTypes.string.isRequired,
            zip: PropTypes.string.isRequired,
            stateId: PropTypes.number.isRequired,
            latitude: PropTypes.number.isRequired,
            longitude: PropTypes.number.isRequired,
        }),
    }),
};

export default ExampleAutocomplete;
