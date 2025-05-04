import PropTypes from 'prop-types';
const userOnboardingPropTypes = {
    formData: PropTypes.shape({
        firstName: PropTypes.string.isRequired,
        lastName: PropTypes.string.isRequired,
        mi: PropTypes.string.isRequired,
        avatarUrl: PropTypes.string.isRequired,
        dob: PropTypes.string.isRequired,
        email: PropTypes.string.isRequired,
        phone: PropTypes.string.isRequired,
        license: PropTypes.arrayOf(
            PropTypes.shape({
                licenseStateId: PropTypes.string.isRequired || PropTypes.number.isRequired,
                licenseTypeId: PropTypes.string.isRequired,
                licenseNumber: PropTypes.string.isRequired,
                dateExpires: PropTypes.string.isRequired,
            })
        ).isRequired,
        yearsOfExperience: PropTypes.string.isRequired,
        desiredHourlyRate: PropTypes.string.isRequired,
        profession: PropTypes.arrayOf(PropTypes.string).isRequired,
        isActive: PropTypes.bool.isRequired,
        locationTypeId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
        lineOne: PropTypes.string.isRequired,
        lineTwo: PropTypes.string,
        zip: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
        stateId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
        latitude: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        longitude: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        lookUpResults: PropTypes.arrayOf(PropTypes.shape({})),
    }),
    values: PropTypes.shape({
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        mi: PropTypes.string,
        avatarUrl: PropTypes.string,
        dob: PropTypes.string,
        email: PropTypes.string,
        phone: PropTypes.string,
        license: PropTypes.arrayOf(
            PropTypes.shape({
                licenseStateId: PropTypes.string || PropTypes.number,
                licenseTypeId: PropTypes.string,
                licenseNumber: PropTypes.string,
                dateExpires: PropTypes.string,
            })
        ),
        yearsOfExperience: PropTypes.string,
        desiredHourlyRate: PropTypes.string,
        profession: PropTypes.arrayOf(PropTypes.string),
        isActive: PropTypes.bool,
        locationTypeId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        lineOne: PropTypes.string,
        lineTwo: PropTypes.string,
        zip: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        stateId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        latitude: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
        longitude: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
    }),

    handleChange: PropTypes.func.isRequired,
    handleBlur: PropTypes.func.isRequired,
    handleSubmit: PropTypes.func.isRequired,
    onChange: PropTypes.func.isRequired,
    nextLabel: PropTypes.string,
    backLabel: PropTypes.string,
    onBack: PropTypes.func.isRequired,
    onNext: PropTypes.func.isRequired,
    isSubmitting: PropTypes.bool.isRequired,
    cantBack: PropTypes.bool.isRequired,
};

export { userOnboardingPropTypes };
