import * as Yup from 'yup';

const organizationSchema = Yup.object().shape({
    name: Yup.string().min(2).max(200).required('Name is Required'),
    organization: Yup.string().min(2).max(200).required('Organization Type is Required'),
    headline: Yup.string().min(2).max(200).required('Headline is Required'),
    description: Yup.string(2).min(3).required('Description is Required'),
    logo: Yup.string().min(2).max(255).required('Logo is Required'),
    location: Yup.string().max(200),
    city: Yup.string().max(20),
    number: Yup.string().min(3).max(50).required('Phone Number is Required'),
    url: Yup.string().min(3).max(255).required('Site URL is Required'),
});

export default organizationSchema;
