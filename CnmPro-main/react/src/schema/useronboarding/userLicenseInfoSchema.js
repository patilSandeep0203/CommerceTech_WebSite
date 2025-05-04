import * as Yup from 'yup';

const userLicenseInfoValidation = Yup.object().shape({
    license: Yup.array()
        .of(
            Yup.object().shape({
                licenseStateId: Yup.number().min(1).notOneOf(['']).required('Required'),
                licenseTypeId: Yup.number().min(1).notOneOf(['']).required('Required'),
                licenseNumber: Yup.string().min(2).max(50).required('Required'),
                dateExpires: Yup.date()
                    .notOneOf([null, '', new Date().setFullYear(0)])
                    .required('Required'),
            })
        )
        .required(),
    yearsOfExperience: Yup.string().min(1).max(10).required('Required'),
    desiredHourlyRate: Yup.string().min(2).max(10).required('Required'),
    profession: Yup.array().of(Yup.string()).min(1).max(800).required('Required'),
});

export default userLicenseInfoValidation;
