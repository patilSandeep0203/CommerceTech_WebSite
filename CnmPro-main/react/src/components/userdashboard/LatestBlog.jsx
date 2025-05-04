// @flow
import React from 'react';
import { Card } from 'react-bootstrap';
import PropTypes from 'prop-types';

const LatestBlog = (props) => {
    const blogData = props.blogData;

    const newDate = (date) => {
        return date.split('T')[0];
    };

    return (
        <Card>
            <Card.Body>
                <div>
                    <h4 className="header-title mt-0 mb-3 font-22 text-black">Latest Blog</h4>
                </div>

                <hr />

                <div className="text-start square text-center">
                    <p className="text-muted-cnmpro1">
                        <strong>{blogData?.title}</strong>
                    </p>
                    <img src={blogData?.imageUrl} alt="" style={{ height: 250 }} className="img-fluid rounded" />
                    <p
                        className="text-dark font-20 text-justify card-text"
                        style={{ margin: 10, display: 'inline-block' }}>
                        {blogData?.content}
                    </p>
                </div>
                <div className="text-end">
                    <p className="text-dark font-14">{newDate(blogData?.datePublish)}</p>
                </div>
            </Card.Body>
        </Card>
    );
};

LatestBlog.propTypes = {
    blogData: PropTypes.shape({
        content: PropTypes.string,
        datePublish: PropTypes.string,
        imageUrl: PropTypes.string,
        title: PropTypes.string,
    }).isRequired,
};

export default LatestBlog;
