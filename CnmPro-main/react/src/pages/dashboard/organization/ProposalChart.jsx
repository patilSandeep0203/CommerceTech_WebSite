import React, { useEffect, useState } from 'react';
import Chart from 'react-apexcharts';
import moment from 'moment/moment';
import PropTypes from 'prop-types';

function ProposalChart(props) {
    const [options, setOptions] = useState({});
    const [series, setSeries] = useState([]);

    useEffect(() => {
        const categories2 = props.countData.map((countData) => moment(countData.dateCreated).format('MMM Do'));
        setOptions({
            grid: {
                padding: {
                    left: 30,
                    right: 10,
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

        const counts = props.countData.map((countData) => countData.count);
        setSeries([
            {
                name: 'Counts',
                data: counts,
            },
        ]);
    }, [props.countData]);
    return (
        <>
            <Chart className="p-0 my-0" options={options} series={series} height="400" type="line" />
        </>
    );
}

ProposalChart.propTypes = {
    countData: PropTypes.arrayOf(PropTypes.shape({ dateCreated: PropTypes.string, count: PropTypes.number }))
        .isRequired,
    count: PropTypes.number.isRequired,
};

export default ProposalChart;
