import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import debug from 'sabio-debug';
import blogPropTypes from './blogPropTypes';
import './blog.css';
import image2 from '../../assets/images/cnmPro/image2.jpg';
import { HtmlDataProcessor } from '@ckeditor/ckeditor5-engine';
import Comment from '../../components/comments/Comments';
import { Card } from 'react-bootstrap';

const _logger = debug.extend('BlogDetail');
const { domParser } = new HtmlDataProcessor();

function BlogDetails(props) {
    const location = useLocation();
    const entityTypeId = 1;

    const [blogDetailData, setBlogDetailData] = useState({
        title: 'Title placeholder',
        subject: 'Subject placeholder',
        blogTypeId: '',
        imageUrl: '',
        content: 'content placeholder',
        datePublish: 'date published placeholder',
    });

    useEffect(() => {
        _logger('useEffect for location', location);
        if (location.state) {
            let payload = location.state.payload;
            setBlogDetailData({
                title: payload?.title,
                subject: payload?.subject,
                blogTypeId: payload?.blogTypeId,
                imageUrl: payload?.imageUrl,
                content: payload?.content,
                datePublish: payload?.datePublish,
            });
        }
    }, []);
    const content = domParser.parseFromString(blogDetailData.content, 'text/html').body?.children[0]?.innerHTML;

    const date = new Date(blogDetailData.datePublish);
    const localDate = date.toLocaleString();

    _logger('props passing ', location.state.payload.id, ' ', entityTypeId);
    return (
        <React.Fragment>
            <Card className="blogs-detali-main-card-component">
                <div className="container ">
                    <div className="row title-blog-cnmpro">
                        <div className="col-4 blogdetail-image-cnmpro">
                            <img src={blogDetailData.imageUrl} className="card-img-top" alt={image2} />
                        </div>
                        <div className="col-8">
                            <div className="row blogdetail-title-cnmpro center-align-cnmpro">
                                <h1>{blogDetailData.title}</h1>
                            </div>
                            <div className="row">
                                <div className="row blogdetail-title-cnmpro center-align-cnmpro">
                                    <h4>Published: {localDate}</h4>
                                </div>
                            </div>
                            <div className="blog-contents-cnmpro">{content}</div>
                        </div>
                    </div>
                </div>
            </Card>
            <Comment
                entTypeId={entityTypeId}
                entId={location.state.payload.id}
                currentUser={props.currentUser}></Comment>
        </React.Fragment>
    );
}

BlogDetails.propTypes = blogPropTypes;

export default BlogDetails;
