import * as Yup from 'yup';

const shareStorySchema = Yup.object().shape({
    name: Yup.string()
        .min(2, 'Name must be at least 2 characters')
        .max(50, 'Name may not exceed 50 characters')
        .required('Name is Required'),
    email: Yup.string()
        .min(2, 'Email must be at least 2 characters')
        .max(50, 'Email may not exceed 50 characters')
        .email('Invalid Email')
        .required('Email is Required'),
    story: Yup.string()
        .min(2, 'Story must be at least 2 characters')
        .max(3000, 'Story may not exceed 3000 characters')
        .required('Story is Required'),
});

export default shareStorySchema;
