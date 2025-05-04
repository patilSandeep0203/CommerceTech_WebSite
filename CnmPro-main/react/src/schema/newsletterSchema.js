import * as Yup from 'yup';

const newsletterSchema = Yup.object().shape({
    email: Yup.string().min(3, 'Too Short!').max(320, 'Too Long!').email('Invalid email').required('Required'),
});

export default newsletterSchema;
