// @flow
import React from 'react';
import Chart from 'react-apexcharts';
import { Card, Dropdown, Row, Col } from 'react-bootstrap';

// components

const CountrySessionsChart = () => {
    const colors = ['rgba(114, 124, 245, 0.85)'];

    // apex chart config
    const apexBarChartOpts = {
        grid: {
            padding: {
                left: 0,
                right: 15,
            },
        },
        chart: {
            type: 'bar',
            height: 350,
            parentHeightOffset: 0,
            toolbar: {
                show: false,
            },
        },
        plotOptions: {
            bar: {
                horizontal: true,
            },
        },
        colors: colors,
        dataLabels: {
            enabled: false,
        },
        xaxis: {
            categories: [
                'Sacramento',
                'Central Valley',
                'Greater LA',
                'Bay Area',
                'I-5 Corridor',
                'Northern Ca',
                'NV',
                'OR',
                'Mex Border',
                'AZ',
            ],
            labels: {
                formatter: function (val) {
                    return val + '%';
                },
            },
        },
    };
    const apexBarChartData = [
        {
            name: 'Sessions',
            data: [90, 75, 60, 50, 45, 36, 28, 20, 15, 12],
        },
    ];

    return (
        <Card>
            <Card.Body>
                <Dropdown className="float-end" align="end">
                    <Dropdown.Toggle variant="link" className="arrow-none card-drop p-0 shadow-none">
                        <i className="mdi mdi-dots-vertical"></i>
                    </Dropdown.Toggle>
                    <Dropdown.Menu>
                        <Dropdown.Item>Refresh Report</Dropdown.Item>
                        <Dropdown.Item>Export Report</Dropdown.Item>
                    </Dropdown.Menu>
                </Dropdown>

                <h4 className="header-title">Sessions Overview</h4>

                <Row>
                    <Col lg={4}>
                        <Chart options={apexBarChartOpts} series={apexBarChartData} type="bar" height={320} />
                    </Col>
                </Row>
            </Card.Body>
        </Card>
    );
};

export default CountrySessionsChart;
