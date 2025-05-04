import './organizations.css';
import React, {useState} from "react";
import PropTypes from  "prop-types";
import {useNavigate} from "react-router-dom";
import logger from 'sabio-debug';
import swal from '@sweetalert/with-react';

const _logger = logger.extend('Organization');

function Organization(props){
    _logger(props.organization)

    const organization = props.organization;

    const [orgState] = useState(organization);
    const navigate = useNavigate();

    const onDeleteOrg = (e) => {
        e.preventDefault();
        props.onOrgClicked(props.organization);
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your Organization has been deleted!',
            showConfirmButton: false,
            timer: 5000
          })
    };

    const onEditOrgClick = (e) => {
    e.preventDefault();
    navigateOrgForm(orgState)
    };

    const navigateOrgForm = () => {
        const stateForTransport = { type: "ORGANIZATION_FORM_EDIT", payload: organization };
        navigate(`/organization/${organization.id}/edit`, { state: stateForTransport });
    };
    _logger(organization);
    return(
        <div className="card-form-org-cnmpro">
            <div className="card"> 
            <div className="card-body card-org-cnmpro">
                <div className="row">
                <img
                src={organization.logo}
                className="card-img-top img-organization-cnmpro"
                alt="Organization Logo">
                </img>
                <h3 className="card-title"> {organization.name} </h3>
            <div className="row">
            </div>
            <h5 className="card-text card-headline-org-cnmpro">{organization.headline} </h5>
            <h5 className="card-text card-description-org-cnmpro">Description: {organization.description} </h5>
                <a href={organization.siteUrl} target={organization.siteUrl} className="card-url-org-cnmpro">
                    <div className='text-decoration-line'>
                    <strong><u>{organization.name} website </u></strong>
                    </div>
                </a>
            <h5 className="card-phone-org-cnmpro"> Phone Number: {organization.phone} </h5>
            <div className='inline-display'>
            <h5 className="card-locations-org-cnmpro"> {organization.location.city}, </h5>
            <h5 className="card-locations-org-cnmpro"> {organization.location.state.code} </h5>
            <h5 className="card-phone-org-cnmpro"> {organization.location.zip} </h5>
            </div>
            <div className="row">
                <button
                type="submit"
                className="btn btn-secondary button-orgform-cnmpro"
                onClick={onEditOrgClick}
                >Edit</button>
                
                <button
                type="submit"
                className="btn btn-danger mx-4 del-button-orgform-cnmpro"
                onClick={onDeleteOrg}
                >Delete
                </button>
            </div>
            </div>
            </div>
            </div>
        </div>
    )
    }

Organization.propTypes = {
    organization: PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired,
        headline: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        logo: PropTypes.string.isRequired,
        siteUrl: PropTypes.string,
        phone: PropTypes.string.isRequired,
        location: PropTypes.shape({
            zip: PropTypes.string.isRequired,
            city: PropTypes.string.isRequired,
            state: PropTypes.shape({
                code: PropTypes.string.isRequired,
            }),
        }),
    }),
    onOrgClicked : PropTypes.func
}

export default Organization;
