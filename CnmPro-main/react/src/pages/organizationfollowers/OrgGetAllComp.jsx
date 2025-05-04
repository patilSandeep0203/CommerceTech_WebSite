import React from 'react';
import PropTypes from 'prop-types';
import logger from 'sabio-debug';
import orgFollowersService from '../../services/orgFollowersService';
import toastr from 'toastr';

const _logger = logger.extend('Organization');

function OrgGetAll(props) {
    _logger(props.organization);
    const organization = props.organization;

    const onSubmitClick = () => {
        _logger('Submit Clicked', organization);
        orgFollowersService.addOrgFol(organization.id).then(onSubmitClickSuccess).catch(onSubmitClickError);
    };
    const onSubmitClickSuccess = (response) => {
        toastr.success('Successly Added');
        _logger('Successfuly submitted', response);
    };

    const onSubmitClickError = (response) => {
        toastr.error('failed error to Add');
        _logger('Submit error', response);
    };

    return (
        <div
            className="card mb-3"
            id={organization.id}
            key={'ListA-' + organization.id}
            style={{
                height: 'Flex',
                width: '700px',
                border: 'solid',
                borderTopWidth: '15px',
                borderBottomWidth: '3px',
                borderRightWidth: '3px',
                borderLeftWidth: '3px',
                borderColor: 'rgba(71, 8, 126, 0.7)',
            }}>
            <div className="row g-0">
                <div className="col-md-4">
                    <img
                        src={organization.logo}
                        style={{ height: '220px', width: 'Flex' }}
                        className="img-fluid rounded-start"
                        alt={organization.id}></img>
                </div>
                <div className="col-md-8">
                    <div className="card-body">
                        <p className="card-titles">
                            <strong className="text">{organization.name}</strong>
                        </p>
                        <p className="card-texthead">
                            <small className="text-muted">{organization.headline}</small>
                        </p>
                        <p className="card-text">
                            <small className="text-muted">{organization.description}</small>
                        </p>
                        <div className="row">
                            <div className="col-sm-3 website" style={{ marginRight: '5px', marginLeft: '5px' }}>
                                <a href={organization.siteUrl} className="btn btn-primary">
                                    Website
                                </a>
                            </div>
                            <div className="col-md-5 follow">
                                <button type="submit" className="btn btn-success" onClick={onSubmitClick}>
                                    Follow
                                </button>
                            </div>
                        </div>
                        <div>
                            <small className="text-muted">contact us at {organization.phone}</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

OrgGetAll.propTypes = {
    onAddClick: PropTypes.func,
    organization: PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired,
        headline: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        logo: PropTypes.string.isRequired,
        siteUrl: PropTypes.string.isRequired,
        phone: PropTypes.string.isRequired,
    }),
};

export default React.memo(OrgGetAll);
