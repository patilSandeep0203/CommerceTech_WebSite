import React, { useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import PropTypes from 'prop-types';
import siteReferenceService from '../../services/siteReferenceService';
import SiteRefModalContent from './SiteRefModalContent.jsx';
import toastr from 'toastr';
import logo from '../../assets/images/aquiferPEOriginal.png';
import swal from '@sweetalert/with-react';
import debug from 'sabio-debug';
const _logger = debug.extend('SiteRefModal');

function SiteRefModal(props) {
    const [referenceType, setReferenceType] = useState({ referenceId: 0 });

    const changeSelection = (e) => {
        if (e.target.id !== null) {
            const { id } = e.target;
            setReferenceType((prevState) => {
                const rt = { ...prevState };
                rt.referenceId = parseInt(id, 10);
                return rt;
            });
        }
    };

    const onSubmit = (e) => {
        if (e.target.name === 'submit') {
            const userId = props.createdUserId.userId;
            const refId = referenceType.referenceId;
            const payload = { id: refId, userId: userId };

            siteReferenceService.addUserReference(payload).then(onPostReferenceSuccess).catch(onPostReferenceError);
        } else if (e.target.name === 'save') {
            props.toggleModal();
            props.redirectUser();
        }
    };

    const onPostReferenceSuccess = (response) => {
        _logger('onPostReferenceSuccess', response.data, response.data.item);
        toastr.success('References Added');
        props.toggleModal();
        swal({
            buttons: {
                cancel: 'Close',
            },
            content: (
                <div className="text-center m-auto">
                    <h4 className="text-dark-50 text-center mt-4 fw-bold">Please check your email</h4>
                    <p className="text-muted mb-4">
                        A email has been send to <b>{props.createdUserId.email}</b>. Please check for an email from
                        company and click on the included link to verify your account.
                    </p>
                    <p className="text-center"></p>
                </div>
            ),
            icon: 'success',
        }).then(props.redirectUser());
    };

    const onPostReferenceError = (error) => {
        toastr.error('User Not Found', error);
    };

    return (
        <div style={{ textAlign: 'center' }}>
            <Modal
                show={props.isShowModal}
                onHide={props.toggleModal}
                backdrop="static"
                keyboard={false}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered>
                <Modal.Header style={{ justifyContent: 'center' }}>
                    <Modal.Title>
                        <div className="row">
                            <img src={logo} alt="" className="logo-light" height="150px" style={{ width: '100%' }} />
                        </div>
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body style={{ textAlign: 'center' }}>
                    {<SiteRefModalContent changeSelection={changeSelection} currentSelection={referenceType} />}
                </Modal.Body>
                <Modal.Footer style={{ justifyContent: 'center' }}>
                    <Button variant="secondary" onClick={onSubmit} name="save">
                        Save For Later
                    </Button>
                    <Button variant="primary" onClick={onSubmit} name="submit">
                        Submit
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}

SiteRefModal.propTypes = {
    isShowModal: PropTypes.bool.isRequired,
    toggleModal: PropTypes.func.isRequired,
    redirectUser: PropTypes.func.isRequired,
    createdUserId: PropTypes.shape({ userId: PropTypes.number.isRequired, email: PropTypes.string.isRequired }),
};

export default React.memo(SiteRefModal);
