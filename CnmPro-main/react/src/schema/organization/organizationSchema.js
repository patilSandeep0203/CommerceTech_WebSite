import * as Yup from 'yup';

const organizationSchema = Yup.object().shape({
    name: Yup.string().min(2).max(200).required('Name is Required'),
    organizationTypeId: Yup.number().max(200).required('Organization Type is Required'),
    headline: Yup.string().min(2).max(200).required('Headline is Required'),
    description: Yup.string(2).min(3),
    logo: Yup.string().min(2).max(255).required('Logo is Required'),
    phone: Yup.string().min(10).max(50).required('Phone Number is Required'),
    siteUrl: Yup.string().min(3).max(255),
    lineOne: Yup.string().min(3).max(50).required('Line One is Required'),
    lineTwo: Yup.string().min(3).max(50),
    city: Yup.string().min(3).max(50).required('City is Required'),
    stateId: Yup.string().required('State is Required'),
    zip: Yup.string().min(3).max(50).required('Zip Code is Required'),
    locationTypeId: Yup.number(),
});

export default organizationSchema;
