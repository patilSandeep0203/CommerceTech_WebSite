import React, { useEffect, useState } from 'react';
import surveyService from '../../services/surveyService';
import { Formik, Form, Field, FieldArray, ErrorMessage } from 'formik';
import { Row, Col, Button } from 'react-bootstrap';
import { useParams, useNavigate } from 'react-router-dom';
import './survey.css';
import toastr from 'toastr';
import debug from 'sabio-debug';
const _logger = debug.extend('Survey');

const questionTypes = {
    openAnswer: 1,
    singleChoiceMultAns: 2,
    multChoiceMultAns: 3,
    boolean: 10,
};

const validateAnswer = (value) => {
    let error;
    if (!value) {
        error = 'Required';
    }
    return error;
};

const validateAnsOptId = (value) => {
    let error;
    _logger('value from ansOptId ->', value);
    if (!value || value.length === 0) {
        error = 'Required';
    }
    return error;
};

const validateAnswerNumber = (value) => {
    let error;
    _logger('value from answerNumber ->', value);
    if (!value) {
        error = 'Required';
    }
    return error;
};

function Survey() {
    let { surveyId } = useParams();

    const [questionData, setQuestionData] = useState({
        questions: [],
        name: '',
        id: '',
        surveyDescription: '',
        questionTypeId: '',
    });

    useEffect(() => {
        surveyService.getSurveyDetails(surveyId).then(onGetSurveySuccess).catch(onGetSurveyError);
    }, []);
    const onGetSurveySuccess = (response) => {
        _logger('response ->', response.item);

        const surveyData = response.item;

        setQuestionData((prevState) => {
            const fd = { ...prevState };
            fd.name = surveyData.name;
            fd.id = surveyData.id;
            fd.surveyDescription = surveyData.description;
            fd.questions = surveyData.qandA;
            return fd;
        });
    };

    const mapAllQuestion = () => (question, index) => {
        const questionObj = questionData.questions[index];

        const answerMapperCheckBox = (answer) => {
            return (
                <div key={`${questionObj.question}-${answer.text || answer.value}`}>
                    <label className="form-group text-center">
                        <Field
                            type="checkbox"
                            name={`questions[${index}].ansOptId`}
                            value={`${Number(answer.ansOptId)}`}
                            className="form-check-input"
                            validate={validateAnsOptId}
                        />{' '}
                        {answer.text ? answer.text : answer.value}
                    </label>
                </div>
            );
        };

        const answerMapperRadio = (answer) => {
            return (
                <div key={`${questionObj.question}-${answer.text || answer.value}`}>
                    <label>
                        <Field
                            type="radio"
                            name={`questions[${index}].ansOptId`}
                            value={`${Number(answer.ansOptId)}`}
                            className="form-check-input"
                            validate={validateAnsOptId}
                        />{' '}
                        {answer.text ? answer.text : answer.value}
                    </label>
                </div>
            );
        };

        if (questionObj.questionTypeId === questionTypes.openAnswer) {
            return (
                <div key={questionObj.questionId}>
                    <h3>{questionObj.question}</h3>
                    <Field
                        component="textarea"
                        name={`questions[${index}].answer`}
                        value={question.answer}
                        className="form-control survey-text-box"
                        placeholder="Type your answer here"
                        key={questionObj.questionId}
                        validate={validateAnswer}
                    />
                    <ErrorMessage
                        name={`questions[${index}].answer`}
                        component="div"
                        className="is-invalid survey-answer-has-error"
                    />
                    <hr />
                </div>
            );
        } else if (questionObj.questionTypeId === questionTypes.singleChoiceMultAns) {
            return (
                <div key={questionObj.questionId}>
                    <h3>{questionObj.question}</h3>
                    <div className="form-check" key={questionObj.questionId}>
                        {questionObj.answerOptions.map(answerMapperRadio)}
                    </div>
                    <ErrorMessage
                        name={`questions[${index}].ansOptId`}
                        component="div"
                        className="is-invalid survey-answer-has-error"
                    />
                    <hr />
                </div>
            );
        } else if (questionObj.questionTypeId === questionTypes.multChoiceMultAns) {
            return (
                <div key={questionObj.questionId}>
                    <h3>{questionObj.question}</h3>
                    <div className="col">{questionObj.answerOptions.map(answerMapperCheckBox)}</div>
                    <ErrorMessage
                        name={`questions[${index}].ansOptId`}
                        component="div"
                        className="is-invalid survey-answer-has-error"
                    />
                    <hr />
                </div>
            );
        } else if (questionObj.questionTypeId === questionTypes.boolean) {
            return (
                <div key={questionObj.questionId}>
                    <h3>{questionObj.question}</h3>
                    <label>
                        <Field
                            type="radio"
                            name={`questions[${index}].answerNumber`}
                            value="0"
                            label="True"
                            className="form-check-input"
                            key={`true_${questionObj.questionId}`}
                            validate={validateAnswerNumber}
                        />{' '}
                        True
                    </label>
                    <div key={questionObj.questionId}>
                        <label>
                            <Field
                                type="radio"
                                name={`questions[${index}].answerNumber`}
                                value="1"
                                label="False"
                                className="form-check-input"
                                key={`false_${questionObj.questionId}`}
                                validate={validateAnswerNumber}
                            />{' '}
                            False
                        </label>
                    </div>
                    <ErrorMessage
                        name={`questions[${index}].answerNumber`}
                        component="div"
                        className="is-invalid survey-answer-has-error"
                    />
                    <hr />
                </div>
            );
        }
    };

    const onGetSurveyError = (error) => {
        _logger('error ->', error);
        toastr.error('Survey could not be found');
    };

    const onSubmitClicked = (surveyData, onSubmitProps) => {
        const answers = surveyData.questions;
        _logger('surveyData ->', surveyData);

        const convertAnsObjToArray = (answers) => {
            const ansObjArrayTemplate = (ansObj) => {
                let newArr = { ...ansObj };
                if (!Array.isArray(ansObj.ansOptId)) {
                    newArr.ansOptId = [ansObj.ansOptId];
                }
                return newArr;
            };

            const optIdTypeChanged = answers.map(ansObjArrayTemplate);
            _logger('optIdTypeChanged', optIdTypeChanged);

            const optIdSplitArray = optIdTypeChanged.reduce((acc, cur) => {
                cur.ansOptId.forEach((optId) =>
                    acc.push({
                        questionId: cur.questionId,
                        answerOptionId: optId,
                        answer: cur.answer,
                        answerNumber: cur.answerNumber,
                    })
                );
                return acc;
            }, []);
            return optIdSplitArray;
        };
        const answerPayload = convertAnsObjToArray(answers);
        _logger('massagedArray ->', answerPayload);

        surveyData.questions = answerPayload;

        surveyService.submitSurvey(surveyData).then(onSubmitSurveySuccess).catch(onSubmitSurveyError);

        onSubmitProps.resetForm();
    };

    const onSubmitSurveySuccess = (response) => {
        _logger('success ->', response);

        toastr.success('Survey Submitted Successfully!');
    };

    const onSubmitSurveyError = (error) => {
        _logger('error ->', error);
        toastr.error('Something went wrong, please try again');
    };

    const navigate = useNavigate();
    const onReturnToDashboardClick = () => {
        navigate(`/admin/surveys`);
    };

    return (
        <React.Fragment>
            <Row>
                <Col>
                    <Button className="font-20 fw-bold float-end" variant="link" onClick={onReturnToDashboardClick}>
                        Return to Survey Dashboard
                    </Button>
                </Col>
            </Row>
            <div className="container card" style={{ width: 950 }}>
                <h1 className="text-center">{questionData.name} Survey</h1>
                <p className="text-center">{questionData.surveyDescription}</p>
                <hr />
                <Formik
                    enableReinitialize={true}
                    initialValues={{
                        surveyId: questionData.id,
                        statusId: 1, //this should always be 1 - ACTIVE --Maybe change later if surveyInstance is deleted
                        questions: questionData.questions.map((question) => {
                            let ques = {
                                questionId: question.questionId,
                                questionTypeId: question.questionTypeId,
                                ansOptId: undefined,
                                answer: undefined,
                                answerNumber: undefined,
                            };
                            return ques;
                        }),
                    }}
                    onSubmit={onSubmitClicked}>
                    {({ values, isSubmitting }) => (
                        <Form>
                            <FieldArray name="questions" value={values.questions}>
                                <div>{values.questions.map(mapAllQuestion(values))}</div>
                            </FieldArray>
                            <div className="card-body text-dark text-center">
                                <button type="submit" className="btn btn-outline-primary ml-2" disabled={isSubmitting}>
                                    Submit Survey
                                </button>
                            </div>
                        </Form>
                    )}
                </Formik>
            </div>
        </React.Fragment>
    );
}
export default Survey;
