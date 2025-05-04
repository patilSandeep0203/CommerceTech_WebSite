// @flow
import React from 'react';
import { Row, Col } from 'react-bootstrap';
// components
import Statistics from './Statistics';
import CountrySessionsChart from './CountrySessionsChart';
import OrganizationTable from './OrganizationTable';
import SessionsChart from './SessionsChart';
import SiteRefChart from './SiteRefChart';

const AnalyticsDashboardPage = () => {
    return (
        <>
            <Row>
                <Col>
                    <h4 className="my-5"> </h4>
                    <span>&nbsp;&nbsp;</span>
                </Col>
            </Row>

            <Row className="mt-5">
                <Col xl={3} lg={4}>
                    <Statistics />
                </Col>
                <Col>
                    <SessionsChart></SessionsChart>
                </Col>
            </Row>

            <Row>
                <Col>
                    <OrganizationTable />
                </Col>
            </Row>

            <Row>
                <Col xl={5} lg={6}>
                    <SiteRefChart />
                </Col>
                <Col>
                    <CountrySessionsChart />
                </Col>
            </Row>
        </>
    );
};

export default AnalyticsDashboardPage;
