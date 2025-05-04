import * as Yup from 'yup';

const searchFormSchema = Yup.object().shape({
    organizationId: Yup.number().required('Required'),
    timePeriod: Yup.number().required('Required'),
});

export default searchFormSchema;
