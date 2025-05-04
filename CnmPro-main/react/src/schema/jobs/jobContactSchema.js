import * as Yup from 'yup';

const phoneRegExp =
    /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/;

const jobValidation = Yup.object().shape({
    contactName: Yup.string().min(2).max(50).required('Required'),
    contactEmail: Yup.string().min(2).email('Enter a valid email').max(50).required('Required'),
    contactPhone: Yup.string()
        .required('required')
        .matches(phoneRegExp, 'Phone number is not valid')
        .min(10, 'Too short')
        .max(10, 'Too long'),
});

export default jobValidation;
