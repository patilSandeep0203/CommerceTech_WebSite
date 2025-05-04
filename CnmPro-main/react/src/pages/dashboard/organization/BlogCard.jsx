import React, { useEffect, useState } from 'react';
import blogService from '../../../services/blogService';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import moment from 'moment/moment';
import logger from 'sabio-debug';
const _logger = logger.extend('Blog');

function LatestBlog() {
    const [blogFormData, setBlogFormData] = useState({
        title: '',
        subject: '',
        imageUrl: '',
        content: '',
        datePublish: '',
    });

    useEffect(() => {
        blogService.getAllBlogs(0, 1).then(onGetBlogSuccess).catch(onGetBlogError);
    }, []);

    const onGetBlogSuccess = (response) => {
        _logger('blog response success', response);

        const blogResponse = response.item.pagedItems[0];

        setBlogFormData((prevState) => {
            const newState = { ...prevState };
            newState.title = blogResponse.title;
            newState.subject = blogResponse.subject;
            newState.imageUrl = blogResponse.imageUrl;
            newState.content = blogResponse.content;
            newState.datePublish = blogResponse.datePublish;

            return newState;
        });

        _logger('new state', blogFormData);
    };

    const onGetBlogError = (error) => {
        _logger('blog response error', error);
    };

    const truncateCardText = (text) => {
        let newText = text.substring(0, 50);
        return newText;
    };
    return (
        <>
            <Card className="orgDash-latest-height m-0">
                <img
                    className="card-img-top orgDash-cover orgDash-card-img-height"
                    src={blogFormData.imageUrl}
                    alt=""
                />
                <div className="card-img-overlay">
                    <div className="bg-success badge">Latest Blog</div>
                </div>
                <Card.Body>
                    <h4 className="mt-0">
                        <Link to={`/blogdetails/${blogFormData.id}`} className="text-title">
                            {blogFormData.title}
                        </Link>
                    </h4>
                    <p className="text-muted font-13 my-1">{truncateCardText(blogFormData.content)}...</p>
                    <p className="mb-1">
                        <span className="pe-2 text-nowrap mb-2 d-inline-block">
                            <i className="mdi mdi-calendar text-muted me-1"></i>
                            <b className="me-1">Published</b>
                            {moment(blogFormData.datePublish).format('MM/DD/YY')}
                        </span>
                    </p>
                    <Link to={`/blogdetails/${blogFormData.id}`}>
                        <button className="btn btn-primary">View More</button>
                    </Link>
                </Card.Body>
            </Card>
        </>
    );
}

export default LatestBlog;
