import PropTypes from 'prop-types';

const surveyPropTypes = {
    formData: PropTypes.shape({
        openEnded: PropTypes.string.isRequired,
        isTrue: PropTypes.bool.isRequired,
    }),

    values: PropTypes.shape({
        openEnded: PropTypes.string.isRequired,
        isTrue: PropTypes.bool.isRequired,
    }),

    handleChange: PropTypes.func.isRequired,
    // handleBlur: PropTypes.func.isRequired,
    // handleSubmit: PropTypes.func.isRequired,
    onChange: PropTypes.func.isRequired,
    // nextLabel: PropTypes.string,
    // backLabel: PropTypes.string,
    // onBack: PropTypes.func.isRequired,
    // onNext: PropTypes.func.isRequired,
    // isSubmitting: PropTypes.bool.isRequired,
    // cantBack: PropTypes.bool.isRequired,
};

export { surveyPropTypes };
