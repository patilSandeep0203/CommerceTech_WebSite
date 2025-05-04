// @flow
import React from 'react';
import { Row, Col, Tab, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import classnames from 'classnames';
import '../../assets/scss/icons.scss';
import SurveyDesigner from './designer/SurveyDesigner';
import SurveySearchList from './analytics/SurveySearchList';
import ViewAllSurveys from './surveyTaker/ViewAllSurveys';

const TabsExample = () => {
    const tabContents = [
        {
            id: '1',
            title: 'Design',
            icon: 'mdi mdi-home-variant',
            component: <SurveyDesigner />,
        },
        {
            id: '2',
            title: 'Share',
            icon: 'mdi mdi-cog-outline',
            component: <ViewAllSurveys />,
        },
        {
            id: '3',
            title: 'Analytics',
            icon: 'mdi mdi-cog-outline',
            component: <SurveySearchList />,
        },
    ];

    return (
        <Row className="justify-content-center">
            <Col lg={12}>
                <h4 className="header-title">Surveys Dashboard</h4>
                <Tab.Container defaultActiveKey="Analytics">
                    <Nav variant="tabs" justify className="nav-bordered" as="ul">
                        {tabContents.map((tab, index) => {
                            return (
                                <Nav.Item key={index} as="li">
                                    <Nav.Link as={Link} to="" eventKey={tab.title}>
                                        <i className={classnames(tab.icon, 'd-md-none', 'd-block', 'me-1')}></i>
                                        <span className="d-none d-md-block">{tab.title}</span>
                                    </Nav.Link>
                                </Nav.Item>
                            );
                        })}
                    </Nav>
                    <Tab.Content>
                        {tabContents.map((tab, index) => {
                            return (
                                <Tab.Pane eventKey={tab.title} id={tab.id} key={index}>
                                    <Row>
                                        <Col>{tab.component}</Col>
                                    </Row>
                                </Tab.Pane>
                            );
                        })}
                    </Tab.Content>
                </Tab.Container>
            </Col>
        </Row>
    );
};

export default TabsExample;
