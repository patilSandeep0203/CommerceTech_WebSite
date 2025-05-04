import React, { useEffect, useState } from 'react';
import ReactApexChart from 'react-apexcharts';
import { Card } from 'react-bootstrap';
import siteReferenceService from '../../../services/siteReferenceService';
import { onGlobalError } from '../../../services/serviceHelpers';

const SiteRefChart = () => {
    const [apexBarChartData, setApexBarChartData] = useState({
        data: [
            {
                name: 'References Collected',
                data: [],
                categories: [],
            },
        ],
    });

    const apexBarChartOpts = {
        chart: {
            height: 250,
            type: 'bar',
            toolbar: {
                show: false,
            },
        },
        plotOptions: {
            bar: {
                horizontal: false,
                dataLabels: {
                    position: 'top',
                },
            },
        },
        dataLabels: {
            enabled: true,
            offsetY: 5,
            style: {
                fontSize: '10px',
                colors: ['#000000'],
            },
        },
        colors: ['#3e14b0bc'],
        stroke: {
            show: true,
            width: 5,
            colors: ['#000000'],
        },
        xaxis: {
            categories: apexBarChartData.data[0].categories,
        },
        legend: {
            offsetY: -10,
        },
        states: {
            hover: {
                filter: 'none',
            },
        },
        grid: {
            borderColor: '#000000',
        },
    };

    useEffect(() => {
        siteReferenceService.getAllForChart().then(onSuccess).catch(onGlobalError);
    }, []);

    const toTitleCase = (str) => {
        str = str.toLowerCase().split(' ');
        for (var i = 0; i < str.length; i++) {
            str[i] = str[i].charAt(0).toUpperCase() + str[i].slice(1);
        }
        return str.join(' ');
    };

    const onSuccess = (response) => {
        if (response?.item) {
            const newSeries = [];
            const labels = [];

            for (const type in response.item) {
                newSeries.push(response.item[type]);
                labels.push(toTitleCase(type));
            }

            setApexBarChartData((prevState) => {
                const data = { ...prevState };
                data.data[0].data = newSeries;
                data.data[0].categories = labels;
                return data;
            });
        }
    };

    return (
        <div className="container">
            <div className="row" style={{ justifyContent: 'center' }}>
                <Card style={{ width: '1000px' }}>
                    <Card.Body>
                        <h4 className="header-title mb-3">Site Reference Tracker</h4>
                        <ReactApexChart
                            options={apexBarChartOpts}
                            series={apexBarChartData.data}
                            type="bar"
                            className="apex-charts"
                        />
                    </Card.Body>
                </Card>
            </div>
        </div>
    );
};

export default SiteRefChart;
