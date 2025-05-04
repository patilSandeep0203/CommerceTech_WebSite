import * as Yup from 'yup';

const timesheetFormSchema = Yup.object().shape({
    date: Yup.date().required('Date is required'),
    timeFrom: Yup.string().required('Time From is required'),
    timeTo: Yup.string().required('Time To is required'),
    organizationId: Yup.number().required('Org is required'),
    comment: Yup.string().required('Comments is required'),
    jobId: Yup.number().required('Job is required'),
});

export default timesheetFormSchema;
