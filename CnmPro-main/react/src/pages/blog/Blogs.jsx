import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import './blog.css';
import BlogCard from './BlogCard';
import debug from 'sabio-debug';
import BlogService from '../../services/blogService';
import PropTypes from 'prop-types';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import { Card } from 'react-bootstrap';

const _logger = debug.extend('Blog');

function Blogs(props) {
    const [blogPageData, setBlogPageData] = useState({
        arrayOfCards: [],
        blogComponents: [],
    });

    const [pageData, setPageData] = useState({
        currentPage: 1,
        pageSize: 9,
        pageIndex: 0,
        total: 0,
    });

    const navigate = useNavigate();

    const mapAllBlogs = (aBlog) => {
        return <BlogCard blog={aBlog} key={'Blog #' + aBlog.id} roles={props.currentUser.roles} />;
    };

    useEffect(() => {
        _logger('useEffect for getBlogs');
        BlogService.getAllBlogs(pageData.pageIndex, pageData.pageSize).then(onGetSuccess).catch(onGetError);
    }, [pageData.pageIndex]);

    const onGetSuccess = (response) => {
        _logger(response);
        let arrayOfBlogs = response.item.pagedItems;
        _logger({ arrayOfBlogs });

        setBlogPageData((prevState) => {
            const pd = { ...prevState };
            pd.arrayOfCards = arrayOfBlogs;
            pd.blogComponents = arrayOfBlogs.map(mapAllBlogs);
            pd.pgItems = 9;
            return pd;
        });
        setPageData((prevState) => {
            const pd = { ...prevState };
            pd.total = response.item.totalCount;
            return pd;
        });
    };

    const onGetError = (err) => {
        _logger(err);
    };

    const onClickAdd = () => {
        navigate('/addblog');
    };

    const onPageChange = (page) => {
        _logger('onPageChange', page);
        setPageData((prevState) => {
            return {
                ...prevState,
                currentPage: page,
                pageIndex: page - 1,
            };
        });
    };

    return (
        <React.Fragment>
            <div className="container">
                <Card>
                    <div className="row title-blog-cnmpro" style={{ margin: 20 }}>
                        <div className="col-4">
                            <h1>Blogs</h1>
                        </div>
                        <div className="col-8 text-end justify-content-end">
                            <Pagination
                                currentPage={pageData.currentPage}
                                pageSize={pageData.pageSize}
                                total={pageData.total}
                                onChange={onPageChange}
                                prevIcon="<<<"
                                nextIcon=">>>"
                            />
                        </div>
                        {props.currentUser.roles.includes('Admin') && (
                            <div className="col">
                                <p></p>
                                <button type="button" className="btn btn-success addButton-cnmpro" onClick={onClickAdd}>
                                    Create a New Blog
                                </button>
                            </div>
                        )}
                    </div>
                    <div className="row" style={{ marginLeft: 20, marginRight: 20 }}>
                        {blogPageData.blogComponents}
                    </div>
                </Card>
            </div>
        </React.Fragment>
    );
}

Blogs.propTypes = {
    currentUser: PropTypes.shape({
        id: PropTypes.number,
        roles: PropTypes.arrayOf(PropTypes.string),
        email: PropTypes.string,
        isLoggedIn: PropTypes.bool,
    }),
};

export default Blogs;
