import * as Yup from 'yup';

const eventValidation = Yup.object().shape({
    acceptCheck: Yup.bool().required('Must Accept Terms and Conditions'),
});

export default eventValidation;