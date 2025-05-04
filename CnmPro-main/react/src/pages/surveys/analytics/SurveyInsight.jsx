import React, { useState, useEffect } from 'react';
import { Row, Col, Card, Button } from 'react-bootstrap';
import './surveys.css';
import toastr from 'toastr';
import surveysInstanceService from '../../../services/surveysInstanceService.js';
import debug from 'sabio-debug';
import { useParams } from 'react-router-dom';
import QuestionChartTemplate from './QuestionChartTemplate.jsx';
import SurveyInstanceAccordian from './SurveyInstanceAccordian.jsx';
import { useNavigate } from 'react-router-dom';
const _logger = debug.extend('surveys');
_logger('call log', surveysInstanceService);

const SurveyInsight = () => {
    let { surveyId } = useParams();
    const [insightState, setInsightState] = useState({
        insight: {
            id: '',
            name: '',
            instanceCount: '',
            description: '',
            logo: '',
            dateCreated: '',
            creatorUserId: '',
            creatorFirstName: '',
            creatorLastName: '',
            insightData: [],
        },
        mappedQuestions: [],
    });

    const navigate = useNavigate();
    const onReturnToDashboardClick = () => {
        navigate(`/admin/surveys`);
    };

    useEffect(() => {
        _logger('use effect');
        surveysInstanceService.getInsightById(surveyId).then(onGetInsightsSuccess).catch(onGetInsightsFail);
    }, []);

    const onGetInsightsSuccess = (response) => {
        setInsightState(() => {
            const newState = { insight: response.item, mappedQuestions: response.item.insightData.map(qTemplateFunc) };
            return newState;
        });
        _logger(insightState);
    };
    const onGetInsightsFail = (response) => {
        _logger('Failed to get insights', response);
        toastr.error('Failed to get insights');
    };
    const qTemplateFunc = (qAObj) => {
        return <QuestionChartTemplate key={qAObj.question.sortOrder} qAObj={qAObj} />;
    };
    let datUTC = insightState.insight.dateCreated;
    let dat = new Date(datUTC).toString().substring(4, 16);
    return (
        <React.Fragment>
            <Row>
                <Col>
                    <Button className="font-20 fw-bold float-end" variant="link" onClick={onReturnToDashboardClick}>
                        Return to Survey Dashboard
                    </Button>
                </Col>
            </Row>
            <Row>
                <Col md={6}>
                    <Card className="d-block surveys-title-card">
                        <Card.Body>
                            <Row>
                                <Col md={9}>
                                    <h1>{insightState.insight.name}</h1>
                                    <p className="text-muted mb-2 font-22">{insightState.insight.description}</p>
                                    <div className="surveys-list-div">
                                        {' '}
                                        <p className="font-20 surveys-list-data">Published: {dat}</p>
                                        <p className="font-20 surveys-list-data">
                                            Questions: {insightState.insight.insightData.length}
                                        </p>
                                        <p className="font-20 surveys-list-data">
                                            Responses: {insightState.insight.instanceCount}
                                        </p>
                                        <p className="font-20 surveys-list-data">
                                            Survey Author: {insightState.insight.creatorFirstName}{' '}
                                            {insightState.insight.creatorLastName}
                                        </p>
                                    </div>
                                </Col>
                                <Col md={3}>
                                    <div className="float-end">
                                        <img src={insightState.insight.logo} alt="" className="img-fluid" />
                                    </div>
                                </Col>
                            </Row>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={6}>
                    <Card className="d-block surveys-title-card">
                        <Card.Body className="d-block">
                            <SurveyInstanceAccordian surveyId={surveyId} />
                        </Card.Body>
                    </Card>
                </Col>
            </Row>

            <Row lg={2} md={1} sm={1}>
                {insightState.mappedQuestions}
            </Row>
        </React.Fragment>
    );
};

export default SurveyInsight;
