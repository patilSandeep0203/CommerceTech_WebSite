import React, { useState, useEffect } from 'react';
import ratingService from '../../services/ratingService';
import debug from 'sabio-debug';
import { PropTypes } from 'prop-types';
import StarsRating from 'react-star-rate';
import toastr from 'toastr';

const _logger = debug.extend('AverageRating');

function AverageRating(props) {
    const [averageRating, setAverageRating] = useState({
        entityId: props.entityId,
        entityTypeId: props.entityTypeId,
    });

    useEffect(() => {
        ratingService
            .ratingAverage(averageRating.entityId, averageRating.entityTypeId)
            .then(ratingAverageSuccess)
            .catch(ratingAverageError);
    }, []);

    const ratingAverageSuccess = (response) => {
        _logger(response);
        setAverageRating((prevState) => {
            return {
                ...prevState,
                average: response.item.average,
            };
        });
    };
    const average = averageRating.average;
    const averageDecimal = parseFloat(average).toFixed(1);

    const ratingAverageError = (response) => {
        _logger('error', response);
        toastr.error('Average error detected');
    };

    return (
        <React.Fragment>
            <div className="RatingContainer">
                <div className="row col-12 text-center">
                    <div>
                        <h2>AverageRating</h2>
                    </div>
                    <div>
                        <StarsRating value={averageRating.average} disabled />
                        <p className="StartRatingAverage">({averageDecimal} of 5)</p>
                    </div>
                </div>
            </div>
        </React.Fragment>
    );
}
export default AverageRating;

AverageRating.propTypes = {
    entityId: PropTypes.number,
    entityTypeId: PropTypes.number,
    average: PropTypes.number,
};
