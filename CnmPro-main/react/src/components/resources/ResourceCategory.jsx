import React from 'react';
import PropTypes from 'prop-types';
import logger from 'sabio-debug';
import './resources.css';

const _logger = logger.extend('ResourceCategory');

function ResourceCategory(props) {
    _logger('Resource Category props', props.resourceCategoryProp);

    const getByRC = (e) => {
        _logger('getByRC clicked');
        e.preventDefault();
        props.onResourceCatClicked(props.resourceCategoryProp.id);
    };

    return (
        <div className="row res-category" onClick={getByRC}>
            <div className="card-title"> {props.resourceCategoryProp.name} </div>
            <input type="hidden" value={props.resourceCategoryProp.id}></input>
        </div>
    );
}

ResourceCategory.propTypes = {
    resourceCategoryProp: PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired,
    }),
    onResourceCatClicked: PropTypes.func,
};

export default ResourceCategory;
