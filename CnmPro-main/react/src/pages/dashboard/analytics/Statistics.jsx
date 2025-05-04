// @flow
import { FiUser, FiUserCheck } from 'react-icons/fi';
import { CgOrganisation, CgDollar } from 'react-icons/cg';
import { AiOutlineCheck } from 'react-icons/ai';
import React from 'react';
import { useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import logger from 'sabio-debug';
import adminDashboardService from '../../../services/adminDashboardService';
import * as stripeService from '../../../services/stripeServices';
import toastr from 'toastr';
import './admindashboard.css';

const _logger = logger.extend('Statistics');

const Statistics = () => {
    const [dashInfo, setDashInfo] = useState({});
    const [weekDashInfo, setWeekDashInfo] = useState({});
    const [subInfo, setSubInfo] = useState({});
    const [iconSize] = useState({
        size: 35,
    });

    useEffect(() => {
        stripeService.subscriptionCounts(0, 10).then(onSubCountSuccess).catch(onSubCountError);
        adminDashboardService.adminDashboard().then(onAdmnDashSuccess).catch(onAdmnDashError);
        adminDashboardService.adminDashboardWeek().then(onAdmnWeekDashSuccess).catch(onAdmnDashError);
    }, []);

    const onSubCountSuccess = (response) => {
        let dailySubInfo = response.item.totalCount;
        _logger('SUBSCRIPTOIN INFO -->>>', dailySubInfo);
        setSubInfo((prevState) => {
            const subInfo = { ...prevState };
            subInfo.subs = dailySubInfo;
            return subInfo;
        });
        _logger('SubInfoSuccess', dailySubInfo);
    };

    const onAdmnWeekDashSuccess = (response) => {
        let infoAdminDashWeek = response.items[0];

        setWeekDashInfo((prevState) => {
            let updatedAdminInfo = { ...prevState };
            updatedAdminInfo = infoAdminDashWeek;
            return updatedAdminInfo;
        });
        _logger('AdmWeekSuccess', infoAdminDashWeek);
    };

    const onAdmnDashSuccess = (response) => {
        let infoAdminDash = response.items[0];

        setDashInfo((prevState) => {
            let updatedAdminInfo = { ...prevState };
            updatedAdminInfo = infoAdminDash;
            return updatedAdminInfo;
        });
        _logger('AdmSuccess', infoAdminDash);
    };

    const onAdmnDashError = (data) => {
        _logger('Error', data);
        toastr.error('The growth ratio is not updated', 'Error');
    };

    const onSubCountError = (data) => {
        _logger('Error', data);
        toastr.error('The sub data is not updated', 'Error');
    };

    const onClickNavToStripePage = (e) => {
        _logger('navigating to Stripe page', e.currentTarget);
        window.open(`https://dashboard.stripe.com/test/dashboard`);
    };
    return (
        <>
            <Card className="tilebox-one cards-adm-cnmpro">
                <Card.Body>
                    <div className="row">
                        <h6 className="text-uppercase mt-0">Total Users</h6>
                        <h2 className="my-2 col-6" id="total-users-count">
                            {dashInfo.totalUsers}
                        </h2>
                        <div className="col-6 pr-2 my-auto mx-auto">
                            <FiUser size={iconSize.size} />
                        </div>
                        <p className="mb-0 text-muted">
                            <span className="text-success me-2">
                                <span>
                                    <AiOutlineCheck />
                                </span>{' '}
                                {`${weekDashInfo.usersGrowth}%`}
                            </span>
                            <span className="text-nowrap">Since previous week</span>
                        </p>
                    </div>
                </Card.Body>
            </Card>

            <Card className="tilebox-one cards-adm-cnmpro">
                <Card.Body>
                    <div className="row">
                        <h6 className="text-uppercase mt-0">Active Users</h6>
                        <h2 className="my-2 col-6" id="active-users-count">
                            {dashInfo.activeUsers}
                        </h2>
                        <div className="col-6 pr-2 my-auto mx-auto">
                            <FiUserCheck size={iconSize.size} />
                        </div>
                        <p className="mb-0 text-muted">
                            <span className="text-success me-2">
                                <span>
                                    <AiOutlineCheck />
                                </span>{' '}
                                {`${weekDashInfo.activeUsersGrowth}%`}
                            </span>
                            <span className="text-nowrap">Since previous week</span>
                        </p>
                    </div>
                </Card.Body>
            </Card>

            <Card className="tilebox-one cards-adm-cnmpro">
                <Card.Body>
                    <div className="row">
                        <h6 className="text-uppercase mt-0">Total Organizations</h6>
                        <h2 className="my-2 col-6" id="total-organizations-count">
                            {dashInfo.totalOrg}
                        </h2>
                        <div className="col-6 pr-2 my-auto mx-auto">
                            <CgOrganisation size={iconSize.size} />
                        </div>
                        <p className="mb-0 text-muted">
                            <span className="text-success me-2">
                                <span>
                                    <AiOutlineCheck />
                                </span>{' '}
                                {`${weekDashInfo.orgGrowth}%`}
                            </span>
                            <span className="text-nowrap">Since previous week</span>
                        </p>
                    </div>
                </Card.Body>
            </Card>

            <Card className="tilebox-one cards-adm-cnmpro">
                <Card.Body>
                    <div className="row">
                        <h6 className="text-uppercase mt-0">Subscriptions</h6>
                        <h2 className="my-2 col-6" id="subscriptions-count">
                            {subInfo.subs}
                        </h2>
                        <div className="col-6 pr-2 my-auto mx-auto">
                            <CgDollar size={iconSize.size} />
                        </div>
                        <p className="mb-0 text-muted">
                            {/*<span className="text-success me-2">
                                <span>
                                    <AiOutlineCheck />
                                </span>{' '}
                                 {`${weekDashInfo.orgGrowth}%`} 
                            </span>
                            <span className="text-nowrap">Since previous week</span>*/}
                            <button
                                className="btn btn-secondary"
                                type="button"
                                id="subscriptions-btn"
                                name="subscriptions-btn"
                                onClick={onClickNavToStripePage}>
                                Stripe Storefront
                            </button>
                        </p>
                    </div>
                </Card.Body>
            </Card>
        </>
    );
};

export default Statistics;
