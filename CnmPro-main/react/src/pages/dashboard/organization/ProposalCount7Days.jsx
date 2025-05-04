import React, { useEffect, useState } from 'react';
import orgAdminDashServices from '../../../services/orgAdminDashboardService';
import Chart from 'react-apexcharts';
import logger from 'sabio-debug';
import PropTypes from 'prop-types';
import moment from 'moment/moment';

const _logger = logger.extend('OrgAdminChart');

function ProposalCount7Days({ orgId }) {
    const [countData, setCountData] = useState([]);
    const [options, setOptions] = useState({});
    const [series, setSeries] = useState([]);
    const [timezoneOffset] = useState(new Date().getTimezoneOffset());
    useEffect(() => {
        if (orgId !== 0) {
            orgAdminDashServices.getProposalLast7Days(orgId, timezoneOffset).then(onGetSuccess).catch(onErrorGet);
        }
    }, [orgId]);

    useEffect(() => {
        const categories2 = countData.map((countData) => moment(countData.dateCreated).format('dddd'));
        setOptions({
            grid: {
                padding: {
                    left: 0,
                    right: 0,
                },
                row: {
                    colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                    opacity: 0.5,
                },
            },
            chart: {
                type: 'line',
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
            xaxis: {
                type: 'string',
                categories: categories2,
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
        });

        const counts = countData.map((countData) => countData.count);
        setSeries([
            {
                name: 'Counts',
                data: counts,
            },
        ]);
    }, [countData]);

    const onGetSuccess = (response) => {
        _logger('ProposalCount', response);
        const reversedArray = response.items.reverse();
        setCountData(reversedArray);
    };

    const onErrorGet = (response) => {
        _logger('Error', response);
    };

    return (
        <>
            <Chart className="p-0 my-0" options={options} series={series} height="400" type="line" />
        </>
    );
}

ProposalCount7Days.propTypes = {
    orgId: PropTypes.number.isRequired,
};

export default ProposalCount7Days;
