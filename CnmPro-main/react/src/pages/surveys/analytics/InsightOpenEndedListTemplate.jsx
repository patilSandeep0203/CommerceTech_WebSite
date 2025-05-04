import React from 'react';
import { PropTypes } from 'prop-types';
import { Card, ListGroup } from 'react-bootstrap';
import './surveys.css';
import SimpleBar from 'simplebar-react';
import debug from 'sabio-debug';
import OpenEndedListItemTemplate from './OpenEndedListItemTemplate';
const _logger = debug.extend('surveys');
_logger();

const InsightOpenEndedListTemplate = (props) => {
    const answerStringMapper = (answerObj, index) => {
        return <OpenEndedListItemTemplate answerString={answerObj.answerString} key={index} />;
    };
    const mappedStrings = props.qAObj.answers?.map(answerStringMapper);

    return (
        <Card>
            <Card.Body>
                <h4 className="header-title">{props.qAObj.question.question}</h4>
                <SimpleBar className="px-3" style={{ maxHeight: '270px', width: '100%' }}>
                    <ListGroup variant="flush">{mappedStrings}</ListGroup>
                </SimpleBar>
            </Card.Body>
        </Card>
    );
};

InsightOpenEndedListTemplate.propTypes = {
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

export default InsightOpenEndedListTemplate;
