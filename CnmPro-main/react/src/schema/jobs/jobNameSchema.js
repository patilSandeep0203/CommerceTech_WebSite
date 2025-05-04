import * as Yup from 'yup';

const jobValidation = Yup.object().shape({
    jobTypeId: Yup.number().required('Required'),
    title: Yup.string().min(2, "Please enter more than two characters").max(50, "You have exceeded the limit").required('Required'),
    description: Yup.string().min(2, 'Please enter more than two characters').max(5000, "You have exceeded the limit").required('Required'),
    requirements: Yup.string().min(2, 'Please enter more than two characters').max(5000, "You have exceeded the limit").required('Required'),
});

export default jobValidation;