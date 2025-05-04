import * as Yup from 'yup';

const surveyCreationValidation = Yup.object().shape({
    name: Yup.string()
        .min(3, 'Minimum length of 3 characters.')
        .max(100, 'Maximum length of 100 characters.')
        .required('Please enter a title for your survey.'),
    description: Yup.string().min(3, 'Minimum length of 3 characters.').max(2000, 'Maximum length of 2000 characters.'),
    statusId: Yup.number().required(),
    questions: Yup.array()
        .of(
            Yup.object().shape({
                question: Yup.string()
                    .min(3, 'Minimum length of 3 characters.')
                    .max(500, 'Maximum length of 500 characters.')
                    .required('Please enter a valid question.'),
                helpText: Yup.string().max(255, 'Maximum length of 255 characters.'),
                isRequired: Yup.boolean(),
                isMultipleAllowed: Yup.boolean(),
                questionTypeId: Yup.number().required('Please select a valid question type.'),
                questionAnswerOptions: Yup.array().of(
                    Yup.object().shape({
                        questionId: Yup.number().required(),
                        text: Yup.string().max(500, 'Maximum length of 500 characters'),
                        value: Yup.string(),
                        additionalInfo: Yup.string().max(200, 'Maximum length of 200 characters'),
                        sortOrder: Yup.number().required(),
                    })
                ),
                statusId: Yup.number().required(),
                sortOrder: Yup.number(),
            })
        )
        .required('Survey must contain at least one question.'),
});

export default surveyCreationValidation;
