import React, { useEffect, useState } from 'react';
import ProposalCount7Days from './ProposalCount7Days';
import PropTypes from 'prop-types';
import orgAdminDashServices from '../../../services/orgAdminDashboardService';
import logger from 'sabio-debug';
import ProposalChart from './ProposalChart';
import { Card, Row, Col } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import classNames from 'classnames';

const _logger = logger.extend('ProposalCharts');

function ProposalCharts(props) {
    const [count15DayData, setCount15DayData] = useState([]);
    const [count30DayData, setCount30DayData] = useState([]);
    const [currentChart, setCurrentChart] = useState(0);
    const timezoneOffset = new Date().getTimezoneOffset();

    useEffect(() => {
        if (props.orgId !== 0) {
            orgAdminDashServices
                .getProposalLast15Days(props.orgId, timezoneOffset)
                .then(onGetSuccess15)
                .catch(onGetError15);
            orgAdminDashServices
                .getProposalLast30Days(props.orgId, timezoneOffset)
                .then(onGetSuccess30)
                .catch(onGetError30);
        }
    }, [props.orgId]);

    const onGetSuccess15 = (response) => {
        _logger('15 success Get', response);
        const reversedArray = response.items.reverse();
        setCount15DayData(reversedArray);
    };

    const onGetError15 = (response) => {
        _logger('15 error Get', response);
    };

    const onGetSuccess30 = (response) => {
        _logger('30 success Get', response);
        const reversedArray = response.items.reverse();
        setCount30DayData(reversedArray);
    };

    const onGetError30 = (response) => {
        _logger('30 error Get', response);
    };

    const renderChartTitle = () => {
        switch (currentChart) {
            case 0:
                return 7;
            case 1:
                return 15;
            case 2:
                return 30;
            default:
                return 0;
        }
    };

    return (
        <>
            <Card className="p-2 orgDash-chart-height orgDash-mb-0">
                <Card.Body>
                    <Row>
                        <Col>
                            <h4 className="mt-2 me-4 orgDash-color-black">
                                Proposals in the Last {renderChartTitle()} Days
                            </h4>
                        </Col>
                        <Col>
                            <ul className="nav float-end d-none d-lg-flex">
                                <li className="nav-item">
                                    <Link
                                        to="#"
                                        className={classNames('nav-link', {
                                            'text-muted': currentChart !== 0,
                                        })}
                                        onClick={() => setCurrentChart(0)}>
                                        7d
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link
                                        to="#"
                                        className={classNames('nav-link', {
                                            'text-muted': currentChart !== 1,
                                        })}
                                        onClick={() => setCurrentChart(1)}>
                                        15d
                                    </Link>
                                </li>
                                <li className="nav-item">
                                    <Link
                                        to="#"
                                        className={classNames('nav-link', {
                                            'text-muted': currentChart !== 2,
                                        })}
                                        onClick={() => setCurrentChart(2)}>
                                        1m
                                    </Link>
                                </li>
                            </ul>
                        </Col>
                    </Row>

                    {currentChart === 0 && <ProposalCount7Days orgId={props.orgId} />}
                    {currentChart === 1 && <ProposalChart countData={count15DayData} />}
                    {currentChart === 2 && <ProposalChart countData={count30DayData} />}
                </Card.Body>
            </Card>
        </>
    );
}

ProposalCharts.propTypes = {
    orgId: PropTypes.number.isRequired,
};
export default ProposalCharts;
