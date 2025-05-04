import PropTypes from 'prop-types';

const blogPropTypes = {
    blogFormData: PropTypes.shape({
        id: PropTypes.number,
        title: PropTypes.string.isRequired,
        subject: PropTypes.string.isRequired,
        blogTypeId: PropTypes.string.isRequired,
        imageUrl: PropTypes.string.isRequired,
        content: PropTypes.string.isRequired,
        datePublish: PropTypes.string,
        isPublished: PropTypes.bool,
    }),

    data: PropTypes.shape({
        id: PropTypes.number,
        title: PropTypes.string.isRequired,
        subject: PropTypes.string.isRequired,
        blogTypeId: PropTypes.string.isRequired,
        imageUrl: PropTypes.string.isRequired,
        content: PropTypes.string.isRequired,
        datePublish: PropTypes.string,
        isPublished: PropTypes.bool,
    }),

    user: PropTypes.shape({
        isLoggedIn: PropTypes.bool,
        roles: PropTypes.arrayOf(PropTypes.string),
    }),
};

export default blogPropTypes;
