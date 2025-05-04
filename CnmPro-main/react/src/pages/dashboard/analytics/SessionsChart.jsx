// @flow
import React, { useState, useEffect } from 'react';
import Chart from 'react-apexcharts';
import { ButtonGroup, Card } from 'react-bootstrap';
import adminDashboardService from '../../../services/adminDashboardService';
import logger from 'sabio-debug';
import { Button } from 'react-bootstrap';
import toastr from 'toastr';
import './admindashboard.css';
import allChartOpt from './chartOptions';

const _logger = logger.extend('UserChart');

const SessionsChart = () => {
    const [userWeek, setUserWeek] = useState({ totalUsersDemo: [], datesWeek: [] });
    const [title, setTitle] = useState('THE LAST WEEK');

    useEffect(() => {
        adminDashboardService.getUserWeekStats().then(onGetUserWeekSuccess).catch(onGetUserWeekError);
    }, []);

    const onGetUserWeekSuccess = (data) => {
        let infoUserWeek = data.items;
        const userInfo = infoUserWeek.map((eachUser) => eachUser.countUsers);
        const timeInfo = infoUserWeek.map((eachTime) => eachTime.dateOfTheDay);
        setUserWeek((prevState) => {
            let updatedAdminInfo = { ...prevState };
            updatedAdminInfo.totalUsersDemo = userInfo;
            updatedAdminInfo.datesWeek = timeInfo;
            return updatedAdminInfo;
        });
        _logger('onGetUserWeekSuccess', infoUserWeek);
    };

    const onGetUserWeekError = (data) => {
        toastr.error('The chart have no valid data to show');
        _logger('onGetUserWeekError', data);
    };

    const updateWeek = () => {
        updateTitle('THE LAST WEEK');
        adminDashboardService.getUserWeekStats().then(onGetUserWeekSuccess).catch(onGetUserWeekError);
    };

    const updateTwoWeek = () => {
        updateTitle('THE LAST TWO WEEKS');
        adminDashboardService.getUserTwoWeekStats().then(onGetUserWeekSuccess).catch(onGetUserWeekError);
    };

    const updateMonth = () => {
        updateTitle('THE LAST MONTH');
        adminDashboardService.getUserMonthStats().then(onGetUserWeekSuccess).catch(onGetUserWeekError);
    };

    const updateYear = () => {
        updateTitle('THE LAST YEAR');
        adminDashboardService.getUserYearStats().then(onGetUserWeekSuccess).catch(onGetUserWeekError);
    };

    const updateTitle = (period) => {
        setTitle((prevState) => {
            let updateTitle = { ...prevState };
            updateTitle = period;
            return updateTitle;
        });
    };

    const apexBarChartData = [
        {
            name: 'Sessions',
            data: userWeek.totalUsersDemo,
        },
    ];

    return (
        <Card className="card-h-100 cards-adm-cnmpro">
            <Card.Body>
                <ul className="nav float-end d-none d-lg-flex">
                    <ButtonGroup size="sm">
                        <li className="nav-item">
                            <Button
                                style={{ backgroundColor: '#0A1F64' }}
                                size="sm"
                                className="mx-1"
                                onClick={updateWeek}>
                                7 days
                            </Button>
                        </li>
                        <li className="nav-item">
                            <Button
                                style={{ backgroundColor: '#0A1F64' }}
                                size="sm"
                                className="mx-1"
                                onClick={updateTwoWeek}>
                                15 days
                            </Button>
                        </li>
                        <li className="nav-item">
                            <Button
                                style={{ backgroundColor: '#0A1F64' }}
                                size="sm"
                                className="mx-1"
                                onClick={updateMonth}>
                                1 month
                            </Button>
                        </li>
                        <li className="nav-item">
                            <Button
                                style={{ backgroundColor: '#0A1F64' }}
                                size="sm"
                                className="mx-1"
                                onClick={updateYear}>
                                1 year
                            </Button>
                        </li>
                    </ButtonGroup>
                </ul>

                <h4 className="header-title mb-3">{`USERS GROWTH IN A PERIOD OF ${title}`}</h4>

                <Chart
                    options={allChartOpt.apexBarChartOpts(userWeek.datesWeek)}
                    series={apexBarChartData}
                    type="area"
                    className="apex-charts mt-3"
                    height={308}
                />
            </Card.Body>
        </Card>
    );
};

export default SessionsChart;
