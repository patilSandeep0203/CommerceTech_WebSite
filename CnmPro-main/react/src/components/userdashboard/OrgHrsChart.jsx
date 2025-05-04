// @flow
import React, { useState } from 'react';
import { Card } from 'react-bootstrap';
import { Bar, withResponsiveness } from 'britecharts-react';
import { PropTypes } from 'prop-types';
import Table from 'react-bootstrap/Table';
import { BsFillBarChartFill } from 'react-icons/bs';
import debug from 'sabio-debug';

const _loggerPage = debug.extend('OrgHrsChart');
const ResponsiveBarChart = withResponsiveness(Bar);

const OrgHrsChart = (props) => {
    const [showBarChart, setShowBarChart] = useState(false);

    const chartContainerStyle = {
        width: '100%',
        height: '300px',
    };

    const orgStats = props?.orgStats?.map((org) => {
        return { name: org?.name, value: org?.total };
    });

    const newDate = (date) => {
        return date.split('T')[0];
    };

    _loggerPage('chartData', orgStats, 'timesheets', props.timesheets);

    return (
        <Card>
            {showBarChart ? (
                <Card.Body>
                    <h4 className="header-title mb-4">
                        Timesheets
                        <div className="text-end">
                            <BsFillBarChartFill onClick={() => setShowBarChart(!showBarChart)} />
                        </div>
                        <hr />
                    </h4>
                    <Table className="mb-0" size="sm">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Organization</th>
                                <th>Total Hrs</th>
                            </tr>
                        </thead>
                        <tbody>
                            {props.timesheets.map((entry, index) => {
                                return (
                                    <tr key={index}>
                                        <td>{newDate(entry.date)}</td>
                                        <td>{entry.name}</td>
                                        <td>{entry.totalHrs}</td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </Table>
                </Card.Body>
            ) : (
                <Card.Body>
                    <h4 className="header-title mb-4">
                        Total Hours Per Organization
                        <div className="text-end">
                            <BsFillBarChartFill onClick={() => setShowBarChart(!showBarChart)} />
                        </div>
                        <hr />
                    </h4>
                    <div className="bar-container" style={chartContainerStyle}>
                        <ResponsiveBarChart
                            data={orgStats}
                            isHorizontal={true}
                            height={300}
                            enableLabels={true}
                            percentageAxisToMaxRatio={1.3}
                            labelsNumberFormat={1}
                            colorSchema={['#727cf5', '#0acf97', '#6c757d', '#fa5c7c', '#ffbc00', '#39afd1', '#e3eaef']}
                            margin={{ top: 10, left: 50, bottom: 20, right: 10 }}
                        />
                    </div>
                </Card.Body>
            )}
        </Card>
    );
};

OrgHrsChart.propTypes = {
    orgStats: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            total: PropTypes.number,
            orgId: PropTypes.number,
        }).isRequired
    ),
    timesheets: PropTypes.arrayOf(
        PropTypes.shape({
            date: PropTypes.string,
            logo: PropTypes.string,
            name: PropTypes.string,
            totalHrs: PropTypes.number,
            orgId: PropTypes.number,
        }).isRequired
    ),
};

export default OrgHrsChart;
