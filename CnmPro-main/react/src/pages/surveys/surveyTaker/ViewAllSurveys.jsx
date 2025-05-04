import React, { useState, useEffect } from 'react';
// import surveysInstanceService from '../../../services/surveysInstanceService.js';
import debug from 'sabio-debug';
import { Card, Row, Col, Form, Button } from 'react-bootstrap';
import '../analytics/surveys.css';
import '../../../assets/scss/icons.scss';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import toastr from 'toastr';
import moment from 'moment-timezone';
import { useNavigate } from 'react-router-dom';
import surveyService from '../../../services/surveyService.js';

const _logger = debug.extend('surveys');

const SurveySearchList = () => {
    const [tableState, setTableState] = useState({
        arrayofInsights: [],
        mappedInsights: [],
        currPage: 1,
        pageIndex: 0,
        pageSize: 5,
        totalCount: 0,
    });
    const [searchData, setSearchData] = useState({
        query: '',
    });

    const onSearchFieldChange = (event) => {
        _logger('searchChange', { syntheticEvent: event });
        const target = event.target;
        const value = target.value;
        const name = target.name;

        setSearchData((prevState) => {
            const newSearchState = { ...prevState };
            newSearchState[name] = value;
            _logger(newSearchState);
            return newSearchState;
        });
    };

    const onSearchBtnClick = (e) => {
        e.preventDefault();
        _logger(searchData);
        if (searchData.query === '') {
            surveyService
                .getAllSurveys(tableState.pageIndex, tableState.pageSize)
                .then(onGetInsightsSuccess)
                .catch(onGetInsightsFail);
        } else {
            surveyService
                .searchSurveysPaginated(tableState.pageIndex, tableState.pageSize, searchData.query)
                .then(onGetInsightsSuccess)
                .catch(onSearchInsightsFail);
        }
    };

    const onSearchInsightsFail = (response) => {
        _logger('Failed to get insights', response);
        toastr.error('No records match search query');
    };

    const SearchForm = () => {
        return (
            <Form key={'search'}>
                <Form.Group as={Row} className="mb-3" controlId="formSearch">
                    <Col sm={8}>
                        <Form.Control
                            type="text"
                            placeholder="Search Query"
                            name="query"
                            autoFocus="autoFocus"
                            value={searchData.query}
                            onChange={onSearchFieldChange}
                        />
                    </Col>
                    <Col sm={2}>
                        <Button variant="primary" type="submit" onClick={onSearchBtnClick}>
                            Search
                        </Button>
                    </Col>
                </Form.Group>
            </Form>
        );
    };

    const navigate = useNavigate();
    const onResultCardClick = (id) => {
        navigate(`/surveys/${id}`);
    };

    useEffect(() => {
        _logger('use effect');
        surveyService
            .getAllSurveys(tableState.pageIndex, tableState.pageSize)
            .then(onGetInsightsSuccess)
            .catch(onGetInsightsFail);
    }, []);

    const onGetInsightsSuccess = (response) => {
        _logger('got insights');
        setTableState((prevState) => {
            const tState = { ...prevState };
            tState.arrayofInsights = response.item.pagedItems;
            tState.totalCount = response.item.totalCount;
            tState.mappedInsights = tState.arrayofInsights.map(resultRow);
            return tState;
        });
    };

    const onGetInsightsFail = (response) => {
        _logger('Failed to get insights', response);
        toastr.error('Failed to get insights');
    };

    const onPaginationClicked = (page) => {
        const pageIdx = page - 1;
        _logger('paginate', page);
        setTableState((prevState) => {
            let iState = { ...prevState };
            iState.currPage = page;
            iState.pageIndex = pageIdx;
            return iState;
        });
        if (searchData.query === '') {
            surveyService
                .getAllSurveys(pageIdx, tableState.pageSize)
                .then(onGetInsightsSuccess)
                .catch(onGetInsightsFail);
        } else {
            surveyService
                .searchSurveysPaginated(pageIdx, tableState.pageSize, searchData.query)
                .then(onGetInsightsSuccess)
                .catch(onSearchInsightsFail);
        }
    };

    const resultRow = (insight) => {
        _logger('insight ->', insight);
        let utc = new Date(moment.tz(insight.dateCreated, 'UTC').format());
        const userLocale = navigator.language;
        const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
        let date = utc.toLocaleString(userLocale, { timeZone: timezone });

        return (
            <Card className="d-block" key={insight.id}>
                <Card.Body
                    onClick={() => {
                        onResultCardClick(insight.id);
                    }}>
                    <Row>
                        <Col md={6}>
                            <h3 className="surveys-list-title">{insight.name}</h3>
                            <p className="mb-2 font-18">{insight.description}</p>
                            <Button variant="primary" className="mb-2 font-18">
                                View Survey
                            </Button>
                        </Col>
                        <Col md={5}>
                            <div>
                                <p className="font-16 surveys-list-data">Published: {date}</p>
                                <p className="font-16 surveys-list-data">
                                    Survey Author: {insight.creatorFirstName} {insight.creatorLastName}
                                </p>
                            </div>
                        </Col>
                        <Col md={1}>
                            <div className="float-end">
                                <img src={insight.companyLogo} alt="" className="img-fluid" />
                            </div>
                        </Col>
                    </Row>
                </Card.Body>
            </Card>
        );
    };

    return (
        <React.Fragment>
            <Card>
                <Card.Body>
                    <Row>
                        <Col className="float-start">
                            <h3>Surveys</h3>
                            <p className="text-muted">Click on a card to view the survey</p>
                        </Col>
                        <Col>
                            <SearchForm />
                        </Col>
                        <Col className="float-end">
                            <Pagination
                                className="lss-pagination-fs float-end mt-2"
                                current={tableState.currPage}
                                pageSize={tableState.pageSize}
                                total={tableState.totalCount}
                                onChange={onPaginationClicked}></Pagination>
                        </Col>
                    </Row>
                    <Row>{tableState.mappedInsights}</Row>
                </Card.Body>
            </Card>
        </React.Fragment>
    );
};

export default SurveySearchList;
