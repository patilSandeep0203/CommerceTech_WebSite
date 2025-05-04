// @flow
import React from 'react';
import { Card } from 'react-bootstrap';
import { PropTypes } from 'prop-types';
import './surveys.css';
import { Bar, withResponsiveness } from 'britecharts-react';

const ResponsiveBarChart = withResponsiveness(Bar);

// simple bar chart
const InsightBarChartTemplate = (props) => {
    // container style

    const barChartDataTemplate = (answerObj) => {
        return { name: answerObj.answerString, value: answerObj.total };
    };
    // chart data
    const barChartData = props.qAObj.answers?.map(barChartDataTemplate);

    return (
        <Card className="insight-card-height">
            <Card.Body className="insight-card-height">
                <h4 className="header-title">{props.qAObj.question.question}</h4>
                <div className="bar-container insight-card-height">
                    <ResponsiveBarChart
                        isAnimated={false}
                        data={barChartData}
                        isHorizontal={false}
                        height={300}
                        betweenBarsPadding={0.5}
                        colorSchema={['#39afd1', '#476066', '#92ba92', '#ab5852', '#007f4e']}
                        margin={{ top: 10, left: 55, bottom: 20, right: 10 }}
                    />
                </div>
            </Card.Body>
        </Card>
    );
};

InsightBarChartTemplate.propTypes = {
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

export default InsightBarChartTemplate;
