import React from 'react';
import debug from 'sabio-debug';
import image2 from '../../assets/images/cnmPro/image2.jpg';
import blogPropTypes from './blogPropTypes';
import Moment from 'moment';
import { useNavigate } from 'react-router-dom';
import './blog.css';

const _logger = debug.extend('BlogCard');

function BlogCard(props) {
    const aBlog = props.blog;
    _logger(props.blog);
    const navigate = useNavigate();

    const blogDate = Moment(aBlog.datePublish).format('MMM YYYY');

    const onEditCard = () => {
        blogEdit(aBlog);
    };

    const blogEdit = (blog) => {
        const stateForEdit = { type: 'update', payload: blog };
        _logger('editBlog', blog);
        navigate(`/editblog/${blog.id}`, { stateForEdit });
    };

    const onViewDetails = () => {
        blogDetailView(aBlog);
    };

    const blogDetailView = (blog) => {
        const stateForDetails = { type: 'blogdetail_view', payload: blog };
        _logger('navToBlogDetails', blog);
        navigate(`/blogdetails/${blog.id}`, { state: stateForDetails });
    };

    return (
        <React.Fragment>
            <div className="col-md-4">
                <div className="card blog-card-cnmpro border-black-cnmpro">
                    <img src={aBlog.imageUrl} className="card-img-top" alt={image2} height="250 px" width="350px" />
                    <div className="card-body">
                        <h3 className="card-title blog-card-title-cnmpro">{aBlog.title}</h3>
                        <p> {blogDate}</p>
                        <p></p>
                        <div className="row">
                            <div className="col">
                                <button
                                    className="btn btn-primary"
                                    width="200 px"
                                    onClick={onViewDetails}
                                    data-page={aBlog.id}>
                                    Details
                                </button>
                            </div>
                            <div className="col">
                                {props.roles.includes('Admin') && (
                                    <button
                                        type="button"
                                        className="btn btn-danger"
                                        width="200 px"
                                        onClick={onEditCard}
                                        data-page={aBlog.id}>
                                        Edit
                                    </button>
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </React.Fragment>
    );
}

BlogCard.propTypes = blogPropTypes;

export default BlogCard;
