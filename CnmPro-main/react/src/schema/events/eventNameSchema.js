import * as Yup from 'yup';

const eventValidation = Yup.object().shape({
    eventTypeId: Yup.number().required('Required'),
    title: Yup.string().min(2, "Please enter more than two characters").max(50, "You have exceeded the limit").required('Required'),
    description: Yup.string().min(2, 'Please enter more than two characters').max(5000, "You have exceeded the limit").required('Required'),
    imageUrl: Yup.string().required('Required')
});

export default eventValidation;