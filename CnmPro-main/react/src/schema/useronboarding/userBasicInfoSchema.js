import * as Yup from 'yup';

const userBasicInfoValidation = Yup.object().shape({
    firstName: Yup.string().min(2).max(50).required('Required'),
    lastName: Yup.string().min(2).max(50).required('Required'),
    mi: Yup.string().min(0).max(2).required('Required'),
    avatarUrl: Yup.string().min(2).max(255).required('Required'),
    dob: Yup.string().length(10, 'EX: 01/01/1985').required('Required'),
    email: Yup.string().email('Invalid email').required('Required'),
    phone: Yup.string().min(0).max(20).required('Required'),
    isActive: Yup.bool().required('Required').required('Required'),
});

export default userBasicInfoValidation;
