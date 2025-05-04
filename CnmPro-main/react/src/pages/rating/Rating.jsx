import React, { useState, useEffect } from 'react';
import debug from 'sabio-debug';
import { PropTypes } from 'prop-types';
import StarsRating from 'react-star-rate';
import ratingService from '../../services/ratingService';
import toastr from 'toastr';

const _logger = debug.extend('Rating');

function Rating(props) {
    const [ratingData, setRatingData] = useState({
        rating: 0,
        entityId: props.entityId,
        entityTypeId: props.entityTypeId,
        commentId: props.commentId,
        isDeleted: false,
    });

    const changeRating = (rating) => {
        _logger(rating, 'change rating');
        if (rating === 0) {
            ratingService.deleteRating(ratingData.id).then(deleteRatingSuccess).catch(deleteRatingError);
        } else if (ratingData?.id) {
            ratingService
                .updateRating({ ...ratingData, rating }, ratingData.id)
                .then(ratingUpdateSuccess)
                .catch(ratingUpdateError);
        } else {
            ratingService
                .addRating({ ...ratingData, rating })
                .then(addRatingSuccess)
                .catch(addRatingError);
        }
        setRatingData((prevState) => {
            const ratingInfo = { ...prevState };
            ratingInfo.rating = rating;
            return ratingInfo;
        });
    };

    const deleteRatingSuccess = (response) => {
        _logger('rating deleted', response);
        toastr.success('Rating Delete Success');
    };

    const deleteRatingError = (response) => {
        _logger('Rating Delete Error', response);
        toastr.error('Rating Delete Error');
    };

    const addRatingSuccess = (response) => {
        _logger(response);
        setRatingData((prevState) => {
            const rating = { ...prevState };
            rating.id = response.item;
            return rating;
        });
        toastr.success('Rating Add Success');
    };
    const addRatingError = (response) => {
        _logger('error', response);
        toastr.error('Add Rating Error');
    };

    const ratingUpdateSuccess = (response) => {
        _logger('Rating Update Successful', response);
        toastr.success('Rating Update Success');
    };

    const ratingUpdateError = (response) => {
        _logger('Rating Update Error', response);
        toastr.error('Rating Update Error');
    };

    useEffect(() => {
        ratingService
            .GetRatingByComment(ratingData.commentId)
            .then(GetRatingByCommentSuccess)
            .catch(GetRatingByCommentError);
    }, []);

    const GetRatingByCommentSuccess = (response) => {
        _logger(response);
        setRatingData((prevState) => {
            return {
                ...prevState,
                ...response.item,
            };
        });
    };

    const GetRatingByCommentError = (response) => {
        _logger(response, 'GetRatingCreatedByError');
        toastr.error('GetRatingCreatedBy Error');
    };

    return (
        <React.Fragment>
            <div className="RatingContainer">
                <div className="row col-12 text-center">
                    <div>
                        <h2>Rating</h2>
                    </div>
                    <div>
                        <StarsRating onChange={changeRating} value={ratingData.rating} />
                        <p className="currentRating">({ratingData.rating} of 5)</p>
                    </div>
                </div>
            </div>
        </React.Fragment>
    );
}

export default Rating;

Rating.propTypes = {
    entityId: PropTypes.number,
    entityTypeId: PropTypes.number,
    commentId: PropTypes.number,
};
