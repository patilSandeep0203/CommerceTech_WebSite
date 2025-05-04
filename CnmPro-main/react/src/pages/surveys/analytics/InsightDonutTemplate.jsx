// @flow
import React from 'react';
import { Card, Row, Col } from 'react-bootstrap';
import { Donut, Legend } from 'britecharts-react';
import './surveys.css';
import { PropTypes } from 'prop-types';

// donut chart
const DonutChartTemplate = (props) => {
    let binTotal = props.qAObj.answers[0].total + props.qAObj.answers[1].total;
    let noPercent = (props.qAObj.answers[0].total / binTotal).toFixed(3) * 100;
    let yesPercent = (props.qAObj.answers[1].total / binTotal).toFixed(3) * 100;
    // chart data
    const donutData = [
        { name: 'No', id: 1, quantity: props.qAObj.answers[0].total, percentage: noPercent },
        { name: 'Yes', id: 2, quantity: props.qAObj.answers[1].total, percentage: yesPercent },
    ];
    let colors = ['#0acf97', '#fa5c7c'];
    if (props.qAObj.question.questionTypeId !== 10) {
        donutData[0].name = props.qAObj.answers[0].answerString;
        donutData[1].name = props.qAObj.answers[1].answerString;
        colors = ['#9600db', '#39afd1'];
    }

    return (
        <Card>
            <Card.Body>
                <h4 className="header-title">{props.qAObj.question.question}</h4>
                <div className="donut-container">
                    <Row>
                        <Col md={7}>
                            <Donut
                                data={donutData}
                                height={300}
                                internalRadius={50}
                                colorSchema={colors}
                                isAnimated={false}
                                hasFixedHighlightedSlice={true}
                            />
                        </Col>
                        <Col md={5}>
                            <Legend
                                data={donutData}
                                height={180}
                                width={225}
                                numberFormat={''}
                                colorSchema={colors}
                                margin={{ top: 10, bottom: 10, left: 0, right: 100 }}
                            />
                        </Col>
                    </Row>
                </div>
            </Card.Body>
        </Card>
    );
};

DonutChartTemplate.propTypes = {
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

export default DonutChartTemplate;
