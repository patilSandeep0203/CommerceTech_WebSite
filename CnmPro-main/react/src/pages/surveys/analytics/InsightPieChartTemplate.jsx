// @flow
import React from 'react';
import Chart from 'react-apexcharts';
import { PropTypes } from 'prop-types';
import './surveys.css';
import { Card } from 'react-bootstrap';

// simple pie chart
const InsightPieChartTemplate = (props) => {
    // default options

    const pieDataTemplate = (answerObj) => {
        return answerObj.total;
    };
    const pieLabelTemplate = (answerObj) => {
        return answerObj.answerString;
    };
    const apexDonutOpts = {
        chart: {
            type: 'pie',
        },
        labels: props.qAObj.answers?.map(pieLabelTemplate),
        colors: [
            '#1a53ff',
            '#ff549a',
            '#715a92',
            '#ffd871',
            '#80b34a',
            '#f6e0d5',
            '#935d5d',
            '#d25dfb',
            '#ffaacd',
            '#dde993',
        ],
        legend: {
            show: true,
            position: 'left',
            horizontalAlign: 'center',
            verticalAlign: 'middle',
            floating: false,
            fontSize: '14px',
            offsetX: 0,
            offsetY: -10,
        },
    };

    // chart data
    const apexDonutData = props.qAObj.answers?.map(pieDataTemplate);

    return (
        <Card>
            <Card.Body>
                <h4 className="header-title mb-3">{props.qAObj.question.question}</h4>
                <Chart options={apexDonutOpts} series={apexDonutData} type="pie" height={265} className="apex-charts" />
            </Card.Body>
        </Card>
    );
};

InsightPieChartTemplate.propTypes = {
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

export default InsightPieChartTemplate;
