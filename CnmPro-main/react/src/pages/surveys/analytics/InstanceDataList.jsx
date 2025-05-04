import React from 'react';
import { PropTypes } from 'prop-types';
import debug from 'sabio-debug';
import { ListGroup } from 'react-bootstrap';
import './surveys.css';
import '../../../assets/scss/icons.scss';
import 'rc-pagination/assets/index.css';

const _logger = debug.extend('instanceDataList');

const InstanceDataList = (props) => {
    const QuestionRow = (questionData) => {
        const AnswerOptionCol = (AnswerOptionObj) => {
            return AnswerOptionObj.answerOptionValue || AnswerOptionObj.answerOptionText;
        };
        let answerCol = null;
        if (questionData.question[0].questionTypeId === 1) {
            answerCol = questionData.openEndedAnswer;
        } else if (questionData.question[0].questionTypeId === 10) {
            answerCol = questionData.binaryAnswer === 0 ? 'No' : 'yes';
        } else if (questionData.question[0].questionTypeId === 2 || 3) {
            answerCol = questionData?.answerOptions?.map(AnswerOptionCol).join(', ');
        }
        return (
            <ListGroup.Item as="div" className="surveys-accordion" key={questionData.question[0].id}>
                <div className="d-flex w-100 justify-content-between surveys-accordion-question-div">
                    <h5 className="mb-1 surveys-accordion-question">{questionData.question[0].question}</h5>
                </div>
                <p className="mb-1 surveys-accordion-border">{answerCol}</p>
            </ListGroup.Item>
        );
    };
    _logger('single Instance card jsx running');
    return (
        <React.Fragment>
            <ListGroup>{props.detail?.surveyData?.map(QuestionRow)}</ListGroup>
        </React.Fragment>
    );
};

InstanceDataList.propTypes = {
    detail: PropTypes.shape({
        surveyName: PropTypes.string.isRequired,
        surveyType: PropTypes.shape({ name: PropTypes.string.isRequired }),
        firstName: PropTypes.string.isRequired,
        lastName: PropTypes.string.isRequired,
        companyLogo: PropTypes.string.isRequired,
        dateCreated: PropTypes.string.isRequired,
        surveyData: PropTypes.arrayOf(
            PropTypes.shape({
                question: PropTypes.arrayOf(
                    PropTypes.shape({
                        id: PropTypes.number.isRequired,
                        question: PropTypes.string.isRequired,
                        questionTypeId: PropTypes.number.isRequired,
                        questionTypeName: PropTypes.string.isRequired,
                    })
                ),
                binaryAnswer: PropTypes.number,
                openEndedAnswer: PropTypes.string,
                answerOptions: PropTypes.arrayOf(
                    PropTypes.shape({ answerOptionValue: PropTypes.string, answerOptionText: PropTypes.string })
                ),
            })
        ),
    }),
};

export default InstanceDataList;
