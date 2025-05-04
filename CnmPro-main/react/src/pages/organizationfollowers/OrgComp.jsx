import React from 'react';
import debug from 'sabio-debug';
import PropTypes from 'prop-types';

function OrgComp(props) {
    const aUserOrg = props.orgUser;
    const _logger = debug.extend('userorgs');
    _logger('props', props.orgUser);

    const onLocalDeleteClicked = (e) => {
        e.preventDefault();
        props.onDeleted(aUserOrg, e);
    };
    return (
        <div
            className="card mb-3"
            id={aUserOrg.id}
            key={'ListA-' + aUserOrg.id}
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
                        src={aUserOrg.logo}
                        style={{ height: '220px', width: 'Flex' }}
                        className="img-fluid rounded-start"
                        alt={aUserOrg.id}></img>
                </div>
                <div className="col-md-8">
                    <div className="card-body">
                        <p className="card-titles">
                            <strong className="text">{aUserOrg.name}</strong>
                        </p>
                        <p className="card-texthead">
                            <small className="text-muted">{aUserOrg.headline}</small>
                        </p>
                        <p className="card-text ">
                            <small className="text-muted">{aUserOrg.description}</small>
                        </p>
                        <div className="row">
                            <div className="col-sm-3 website" style={{ marginRight: '5px', marginLeft: '5px' }}>
                                <a href={aUserOrg.siteUrl} className="btn btn-primary">
                                    Website
                                </a>
                            </div>
                            <div className="col-sm-1 follow">
                                <button type="submit" className="btn btn-danger" onClick={onLocalDeleteClicked}>
                                    Unfollow
                                </button>
                            </div>
                        </div>
                        <div>
                            <small className="text-muted">contact us at {aUserOrg.phone}</small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

OrgComp.propTypes = {
    onDeleted: PropTypes.func,
    orgUser: PropTypes.shape({
        id: PropTypes.number.isRequired,
        headline: PropTypes.string.isRequired,
        description: PropTypes.string.isRequired,
        siteUrl: PropTypes.string.isRequired,
        name: PropTypes.string.isRequired,
        logo: PropTypes.string.isRequired,
        phone: PropTypes.string.isRequired,
    }),
};

export default React.memo(OrgComp);
