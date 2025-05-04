import * as Yup from 'yup';

const eventValidation = Yup.object().shape({
    organizationId: Yup.number().required('Required'),
    locationTypeId: Yup.number().required('Required'),
    lineOne: Yup.string().min(2, "At least add two characters").max(50, "You have exceeded the limit").required('Required'),
    zip: Yup.number().min(5, "At least five digits").required('Required'),
    stateId: Yup.number().required('Required'),
    latitude: Yup.number("Must be a number type").required('Required'),
    longitude: Yup.number("Must be a number type").required('Required'),
});

export default eventValidation;