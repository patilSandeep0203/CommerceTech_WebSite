// @flow
import React from 'react';
import { Card, Row, Col } from 'react-bootstrap';
import { PropTypes } from 'prop-types';
import '../../assets/scss/custom/plugins/icons/_unicons.scss';
import debug from 'sabio-debug';

const _loggerPage = debug.extend('RecentNewsletters');

const CurrentNewsletter = (props) => {
    const newsData = props.newslettersData;

    _loggerPage('newsData', newsData);

    const mapNews = (newsletter) => {
        return (
            <div key={newsletter.id} className="d-flex mt-3">
                <i className="uil uil-arrow-growth me-2 font-18 text-primary"></i>
                <div>
                    <a className="mt-1 font-18" target="_blank" href={newsletter.url} rel="noreferrer">
                        <strong>{newsletter?.name}: </strong>
                        <span className="text-black">{newsletter?.description}</span>
                    </a>
                </div>
            </div>
        );
    };

    return (
        <>
            <Card>
                <Card.Body>
                    <Row>
                        <Col>
                            <div className="page-title-box">
                                <div className="page-title-right"></div>
                                <h4 className="page-title font-22 text-black">Recent Newsletters</h4>
                                <hr />

                            </div>
                        </Col>
                    </Row>
                    {newsData?.map(mapNews)}
                </Card.Body>
            </Card>
        </>
    );
};

CurrentNewsletter.propTypes = {
    newslettersData: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            name: PropTypes.string,
            description: PropTypes.string,
            url: PropTypes.string,
        }).isRequired
    ).isRequired,
};
export default CurrentNewsletter;
