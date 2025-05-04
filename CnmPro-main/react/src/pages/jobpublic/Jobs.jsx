import React, { useState, useEffect, useCallback } from 'react';
import { Row, Col } from 'react-bootstrap';
import InfiniteScroll from 'react-infinite-scroll-component';
import JobDetailed from './JobDetailedViewCard';
import JobMapper from './JobMapper';
import './jobstyle.css';
import debug from 'sabio-debug';
import * as jobsService from '../../services/jobsService';
import Geocode from 'react-geocode';
import toastr from 'toastr';

const Jobs = () => {
    //state hooks and consts
    const logger = debug.extend('Jobs');

    const [jobsData, setJobsData] = useState({
        arrayOfJobs: [],
        nextArrayOfJobs: [],
        mappedArrayJobs: [],
        jobDetailed: [],
        mappedDetailed: [],
    });

    const [query, setQuery] = useState({
        jobQuery: 'engineer',
        locationQuery: '',
        latitude: null,
        longitude: null,
        distance: 25,
    });
    const [pageData, setPageData] = useState({
        current: 0,
        size: 5,
        totalcount: 0,
    });

    //Geocode functions

    Geocode.setApiKey('AIzaSyCjZW9TtLXAGFAThuOk3MM3IAELqOJHxbI');
    Geocode.setRegion('US');
    const getLatLongFromAddress = (address) => {
        Geocode.fromAddress(address).then(
            (response) => {
                const { lat, lng } = response.results[0].geometry.location;
                logger(response.results[0], 'Google Response');
                logger('lat long is =>', lat, lng);

                setQuery((prevState) => {
                    const queryData = { ...prevState };
                    queryData.latitude = lat;
                    queryData.longitude = lng;
                    return queryData;
                });
            },
            (error) => {
                logger(error);
            }
        );
    };

    //API calls

    const makeApiCall = useCallback((pgIdx, pgSize, query) => {
        jobsService.getJobsSearch(pgIdx, pgSize, query).then(onGetJobSearchSuccess).catch(onGetJobsSearchError);
    });
    const makeNextApiCall = useCallback((pgIdx, pgSize, query) => {
        jobsService.getJobsSearch(pgIdx, pgSize, query).then(onGetNextJobPageSuccess).catch(onGetJobsSearchError);
    });
    const makeGeographicJobSearchApiCall = useCallback((pgIdx, pgSize, query, lat, long, distance) => {
        jobsService
            .getJobSearchWithDistance(pgIdx, pgSize, query, lat, long, distance)
            .then(onGetJobSearchSuccess)
            .catch(onGetJobsSearchError);
    });
    //Infinite scroll functions
    useEffect(() => {
        if (query.jobQuery === 'engineer' && query.locationQuery === '' && pageData.current === 0) {
            makeApiCall(pageData.current, pageData.size, query.jobQuery);
        }
        if (pageData.current > 0 && query.jobQuery.length >= 3 && query.locationQuery === '') {
            makeNextApiCall(pageData.current, pageData.size, query.jobQuery);
        }
        if (pageData.current > 0 && query.locationQuery.length > 4) {
            makeNextApiCall(pageData.current, pageData.size, query.locationQuery);
        }
        if (query.latitude && query.longitude) {
            makeGeographicJobSearchApiCall(
                pageData.current,
                pageData.size,
                query.jobQuery,
                query.latitude,
                query.longitude,
                query.distance
            );
        }
    }, [query, pageData.current]);

    const onSearchSubmit = () => {
        if (query.jobQuery.length >= 3 && query.locationQuery.length >= 3) {
            getLatLongFromAddress(query.locationQuery);
        }

        if (query.jobQuery.length >= 3 && query.locationQuery === '' && pageData.current === 0) {
            makeApiCall(pageData.current, pageData.size, query.jobQuery);
        }
    };

    const loadNextPage = () => {
        setPageData((prevState) => {
            const pgData = { ...prevState };
            pgData.current = pgData.current + 1;
            return pgData;
        });
    };
    //detailed view of card data from JobMapper
    const onClickShowDetail = (jobObj) => {
        let jobDetailed = jobObj;
        setJobsData((prevState) => {
            const jobData = { ...prevState };
            jobData.jobDetailed = [jobDetailed];
            return jobData;
        });
    };

    const showDetails = () => {
        if (jobsData.jobDetailed === undefined) {
            return;
        } else {
            return <div>{jobsData?.jobDetailed.map(mapJobDetailed)}</div>;
        }
    };

    //mapping functions
    const mapJobDetailed = (aJob) => {
        return <JobDetailed job={aJob} id={aJob.id} key={aJob.id} />;
    };

    const mapJob = (aJob) => {
        return <JobMapper job={aJob} id={aJob.id} key={aJob.id} onClickShowDetail={onClickShowDetail} />;
    };
    //API success and error functions
    const onGetJobSearchSuccess = (data) => {
        let totalCount = data.item.totalCount;
        let arrayOfJobs = data.item.pagedItems;
        toastr.success(totalCount, 'results found');
        setJobsData((prevState) => {
            const jobData = { ...prevState };
            jobData.arrayOfJobs = arrayOfJobs;
            jobData.mappedArrayJobs = arrayOfJobs.map(mapJob);
            return jobData;
        });
        setPageData((prevState) => {
            const pgData = { ...prevState };
            pgData.totalcount = totalCount;
            return pgData;
        });
    };

    const onGetNextJobPageSuccess = (data) => {
        let nextArrayOfJobs = data.item.pagedItems;

        setJobsData((prevState) => {
            const jobData = { ...prevState };
            jobData.nextArrayOfJobs = nextArrayOfJobs;
            jobData.arrayOfJobs = jobsData.arrayOfJobs.concat(jobData.nextArrayOfJobs);
            jobData.mappedArrayJobs =
                jobsData.arrayOfJobs.length > 0 && jobsData.arrayOfJobs.concat(jobData.nextArrayOfJobs).map(mapJob);
            return jobData;
        });
    };

    const onGetJobsSearchError = (response) => {
        logger('Error', response);
        toastr.error('No records match search query');
    };

    // function to set query from search bar input
    const onSearchJobsFieldChange = (e) => {
        e.preventDefault();
        const target = e.target;
        const newSearchParam = target.value;
        setQuery((prevState) => {
            const queryData = { ...prevState };
            queryData.jobQuery = newSearchParam;

            return queryData;
        });
    };
    const onSearchLocationFieldChange = (e) => {
        e.preventDefault();
        const target = e.target;
        const newSearchParam = target.value;
        setQuery((prevState) => {
            const queryData = { ...prevState };
            queryData.locationQuery = newSearchParam;

            return queryData;
        });
    };

    return (
        <div className="jobs-container">
            <Row className="job-component-searchBar">
                <Col className="searchbar-col-jobs-cnmpro">
                    <form>
                        <div className="input-group">
                            <input
                                type="text"
                                className="form-control dropdown-toggle"
                                placeholder="Keyword"
                                id="top-search"
                                name="search"
                                onChange={onSearchJobsFieldChange}
                            />
                        </div>
                    </form>
                </Col>
                <Col>
                    {' '}
                    <form>
                        <div className="input-group">
                            <input
                                type="text"
                                className="form-control dropdown-toggle"
                                placeholder="Location"
                                id="location-search"
                                name="location"
                                onChange={onSearchLocationFieldChange}
                            />
                        </div>
                    </form>
                </Col>
                <Col>
                    {' '}
                    <button
                        type="button"
                        className="job-component-button-search job-button btn btn-light btn-sm"
                        onClick={onSearchSubmit}>
                        Search
                    </button>
                </Col>
            </Row>

            <div className="container-cnmpro-jobs">
                <Row className="py-4">
                    <Col className="job-window-cnmpro scrollbar-jobs-infiniteScroll-cnmpro" id="jobWindow">
                        <InfiniteScroll
                            className="infinite-scroll-component-jobs"
                            dataLength={jobsData?.mappedArrayJobs?.length}
                            scrollableTarget="jobWindow"
                            scrollThreshold="25px"
                            next={loadNextPage}
                            hasMore={true}
                            loader={<div>{jobsData?.mappedArrayJobs}</div>}></InfiniteScroll>
                    </Col>

                    <Col className="detailed-view-col">{showDetails()}</Col>
                </Row>
            </div>
        </div>
    );
};

export default Jobs;
