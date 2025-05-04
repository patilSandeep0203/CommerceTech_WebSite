// @flow
import React from 'react';
import { PropTypes } from 'prop-types';
import InsightBarChartTemplate from './InsightBarChartTemplate.jsx';
import DonutChartTemplate from './InsightDonutTemplate.jsx';
import InsightOpenEndedListTemplate from './InsightOpenEndedListTemplate.jsx';
import './surveys.css';
import InsightPieChartTemplate from './InsightPieChartTemplate.jsx';

const QuestionChartTemplate = (props) => {
    let questionCard;
    if (props.qAObj.question.questionTypeId === 2 || props.qAObj.question.questionTypeId === 3) {
        questionCard =
            props.qAObj.answers.length < 3 ? (
                <DonutChartTemplate qAObj={props.qAObj} />
            ) : props.qAObj.answers.length < 6 ? (
                <InsightBarChartTemplate qAObj={props.qAObj} />
            ) : (
                <InsightPieChartTemplate qAObj={props.qAObj} />
            );
    } else if (props.qAObj.question.questionTypeId === 10) {
        questionCard = <DonutChartTemplate qAObj={props.qAObj} />;
    } else if (props.qAObj.question.questionTypeId === 1) {
        questionCard = <InsightOpenEndedListTemplate qAObj={props.qAObj} />;
    }
    return <div>{questionCard}</div>;
};
QuestionChartTemplate.propTypes = {
    qAObj: PropTypes.shape({
        question: PropTypes.shape({
            id: PropTypes.number.isRequired,
            question: PropTypes.string.isRequired,
            questionTypeId: PropTypes.number.isRequired,
            questionTypeName: PropTypes.string.isRequired,
            sortOrder: PropTypes.number.isRequired,
        }),
        answers: PropTypes.arrayOf(
            PropTypes.shape({
                optionId: PropTypes.number,
                answerString: PropTypes.string,
                answerBinary: PropTypes.number,
                total: PropTypes.number,
            })
        ),
    }),
};

export default QuestionChartTemplate;
