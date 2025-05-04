import React, { useEffect, useState } from 'react';
import Chart from 'react-apexcharts';
import { Card } from 'react-bootstrap';
import orgAdminDashServices from '../../../services/orgAdminDashboardService';
import logger from 'sabio-debug';
import PropTypes from 'prop-types';

const _logger = logger.extend('OrgAdminChart');

const JobTypesBarChart = ({ orgId }) => {
    const colors = ['#0acf97'];

    const [jobTypeData, setJobTypeData] = useState({
        contractCount: 0,
        fullTimeCount: 0,
        partTimeCount: 0,
    });

    useEffect(() => {
        orgAdminDashServices.getJobTypeStats(orgId).then(onGetJobTypeSucccess).catch(onGetJobTypeError);
    }, [orgId]);

    const onGetJobTypeSucccess = (response) => {
        _logger('Success', response);
        setJobTypeData({ ...response.item });
    };

    const onGetJobTypeError = (response) => {
        _logger('Error', response);
    };

    const apexBarChartOpts = {
        grid: {
            padding: {
                left: 0,
                right: 0,
            },
        },
        chart: {
            height: 309,
            type: 'bar',
            parentHeightOffset: 0,
            toolbar: {
                show: false,
            },
        },
        dataLabels: {
            enabled: false,
        },
        stroke: {
            curve: 'smooth',
            width: 4,
        },
        zoom: {
            enabled: false,
        },
        legend: {
            show: false,
        },
        colors: colors,
        xaxis: {
            type: 'string',
            categories: ['Full Time', 'Part Time', 'Contract'],
            tooltip: {
                enabled: false,
            },
            axisBorder: {
                show: false,
            },
            labels: {},
        },
        yaxis: {
            labels: {
                formatter: function (val) {
                    return val;
                },
                offsetX: -15,
            },
        },
        fill: {
            type: 'sold',
        },
    };

    const apexBarChartData = [
        {
            name: 'Sessions',
            data: [jobTypeData.fullTimeCount, jobTypeData.partTimeCount, jobTypeData.contractCount],
        },
    ];

    return (
        <>
            <Card className="card-h-200">
                <Card.Body>
                    <h4 className="mt-0 me-2 color-black">Jobs by Job Type</h4>

                    <Chart
                        options={apexBarChartOpts}
                        series={apexBarChartData}
                        type="bar"
                        className="apex-charts mt-3"
                        height={400}
                    />
                </Card.Body>
            </Card>
        </>
    );
};

JobTypesBarChart.propTypes = {
    orgId: PropTypes.number.isRequired,
};

export default JobTypesBarChart;
