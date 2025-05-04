import * as Yup from 'yup';

const jobValidation = Yup.object().shape({
    acceptCheck: Yup.bool().required('Must Accept Terms and Conditions'),
});

export default jobValidation;