import * as Yup from 'yup';

const SendTokenSchema = Yup.object().shape({
    phoneNumber: Yup.string().length(14).required('Enter a valid phone number'),
});

export default SendTokenSchema;
