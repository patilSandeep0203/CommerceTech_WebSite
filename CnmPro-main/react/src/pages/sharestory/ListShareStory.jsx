import { useState, useEffect } from 'react';
import * as shareStoryService from '../../services/shareStoryService';
import { Navigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import StoryCard from './StoryCard';
import PropTypes from 'prop-types';
import toastr from 'toastr';
import React from 'react';
import debug from 'sabio-debug';
import { Col, Container, Row } from 'react-bootstrap';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import './shareStory.css';

const _logger = debug.extend('listShareStoryForm');

const ShowStories = (props) => {
    const [storyInfo, setStoryInfo] = useState({
        stories: [],
        storyComponent: [],
        current: 1,
        pageIndex: 0,
        pageSize: 4,
        countOfItems: 0,
    });
    const [user, setUser] = useState({});
    useEffect(() => {
        setUser(props.currentUser);
    }, [props.currentUser]);
    useEffect(() => {
        _logger('using useffect');
        if (user !== {}) {
            shareStoryService
                .getAllPage(storyInfo.pageIndex, storyInfo.pageSize)
                .then(onGetStorySuccess)
                .catch(onGetStoryError);
        }
    }, [user]);

    const onGetStorySuccess = (res) => {
        const storyArray = res.data.item.pagedItems;
        _logger(storyArray);
        _logger('get story success!');
        setStoryInfo((prevState) => {
            const bi = { ...prevState };
            bi.stories = storyArray;
            bi.storyComponent = storyArray.map(mapStories);
            bi.countOfItems = res.data.item.totalCount;
            return bi;
        });
    };

    const onGetStoryError = () => {
        toastr.error('unable to get stories :(');
        _logger('get stories error!');
    };
    const mapStories = (aStory) => {
        _logger('map stories', aStory);
        return <StoryCard story={aStory} key={'ListA' + aStory.id} onStoryClick={clickDeleteCard} user={user} />;
    };

    const clickDeleteCard = (aStory) => {
        _logger(aStory.id);
        const idToBeDeleted = aStory.id;

        const onDeleteSuccess = () => {
            toastr.success('Story was deleted');
            _logger('Story was deleted');
        };
        const onDeleteError = () => {
            toastr.error('unable to delete stories :(');
            _logger('get delete error!');
        };
        shareStoryService.deleteStory(idToBeDeleted).then(onDeleteSuccess).catch(onDeleteError);

        setStoryInfo((prevState) => {
            const { stories } = prevState;
            const updatedStories = stories.filter((story) => story.id !== idToBeDeleted);

            return {
                ...prevState,
                stories: updatedStories,
                storyComponent: updatedStories.map(mapStories),
            };
        });
    };

    const onPaginationClicked = (page) => {
        const pageIndex = page - 1;
        setStoryInfo((prevState) => {
            let pd = { ...prevState };
            pd.current = page;
            pd.pageIndex = pageIndex;
            return pd;
        });
        shareStoryService.getAllPage(pageIndex, storyInfo.pageSize).then(onGetStorySuccess).catch(onGetStoryError);
    };
    const addFriendClick = (e) => {
        Navigate(e.currentTarget.dataset.page);
    };
    return (
        <React.Fragment>
            <Container>
                <Row>
                    <Col>
                        <div className="text-center mt-2 listsharestory-pas-text-styling-fs-ff ">Pick a Story</div>
                        <Col className="mb-3 text-end me-4 d-flex justify-content-between">
                            <Link
                                to="/sharestory"
                                type="button"
                                data-page=""
                                className="btn btn-primary"
                                href="#"
                                onClick={addFriendClick}>
                                {' '}
                                Add Story
                            </Link>
                        </Col>
                        <Col>
                            <Pagination
                                className="lss-pagination-fs"
                                current={storyInfo.current}
                                pageSize={storyInfo.pageSize}
                                total={storyInfo.countOfItems}
                                onChange={onPaginationClicked}></Pagination>
                        </Col>
                        <Row>{storyInfo.storyComponent}</Row>
                    </Col>
                </Row>
            </Container>
        </React.Fragment>
    );
};

ShowStories.propTypes = {
    currentUser: PropTypes.shape({
        id: PropTypes.number,
        email: PropTypes.string,
        isLoggedIn: PropTypes.bool,
        roles: PropTypes.arrayOf(PropTypes.string),
    }).isRequired,
};

export default ShowStories;
