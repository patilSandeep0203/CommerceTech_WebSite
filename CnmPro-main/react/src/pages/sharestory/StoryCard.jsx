import React, { useState } from 'react';
import { Card, Container, Col, Row } from 'react-bootstrap';
import debug from 'sabio-debug';
import PropTypes from 'prop-types';
import { BsFillTrashFill, BsFillPencilFill, BsFillAspectRatioFill } from 'react-icons/bs';
import { useNavigate } from 'react-router-dom';
import './shareStory.css';
import { HiChevronDoubleLeft, HiChevronDoubleRight } from 'react-icons/hi';

const _logger = debug.extend('StoryCard');

function StoryCard(props) {
    const aStory = props.story;
    const navigate = useNavigate();
    _logger(aStory);
    const [currImgIndex, setCurrImgIndex] = useState(0);
    const isAdmin = props.user.roles?.filter((role) => {
        let result = false;
        if (role === 'Admin') {
            result = true;
        }
        return result;
    });
    _logger(isAdmin);
    const onLocalEditCard = () => {
        _logger('editStory');
        const state = { type: 'update', payload: aStory, isEditMode: true };
        navigate(`/sharestory`, { state });
    };

    _logger('story passed in prop', aStory);

    const onLocalclickdeleteCard = () => {
        _logger('deleteStory', aStory);
        props.onStoryClick(props.story);
    };

    const navToStoryPage = (story) => {
        const stateForTransport = { type: 'story_view', payload: story };
        _logger('StoryDetail', story);

        navigate(`/storydetails/${story.id}`, { state: stateForTransport });
    };

    const onReadMore = () => {
        navToStoryPage(aStory);
    };

    const nextImage = () => {
        if (currImgIndex !== aStory.file.length - 1) {
            setCurrImgIndex(currImgIndex + 1);
        }
    };

    const prevImage = () => {
        if (currImgIndex > 0) {
            setCurrImgIndex(currImgIndex - 1);
        }
    };

    return (
        <React.Fragment>
            <Col className=" col-12 col-xs-1 col-md-4 col-lg-3 col-xl m-2 d-flex align-items-stretch storycard-position">
                <Row>
                    <Card className="border rounded  mt-1 mb-1 ">
                        <div style={{ container: 'sm' }} className="">
                            <Card.Img
                                className="storycard-img-top pt-3"
                                variant="top"
                                src={aStory.file?.[currImgIndex].url}></Card.Img>
                            <Container style={{ textAlign: 'center' }}>
                                <HiChevronDoubleLeft onClick={prevImage} disabled={currImgIndex <= 0}>
                                    Prev
                                </HiChevronDoubleLeft>
                                <HiChevronDoubleRight
                                    onClick={nextImage}
                                    disabled={currImgIndex >= aStory.file.length - 1}>
                                    Next
                                </HiChevronDoubleRight>
                            </Container>

                            <Card.Title as="h5" className=" mt-3 mb-2 bg-light rounded-3 p-2">
                                {aStory.name}
                            </Card.Title>
                            <Card.Text className=" mb-4 mt-3 bg-light border rounded-3 p-3 storycard-cardtext-pas-fs">
                                {' '}
                                {aStory.story}
                            </Card.Text>
                            <div className="d-grid gap-2 col-6 mx-auto ">
                                <BsFillPencilFill
                                    to={`/sharestory/${aStory.id}`}
                                    type="button"
                                    onClick={onLocalEditCard}
                                    className={
                                        isAdmin?.length > 0
                                            ? 'col-3 mb-2 mt-2 me-4  position-relative  position-absolute bottom-0 start-50 translate-middle-x storycard-reacticons-bs '
                                            : 'col-3 mb-2 mt-2 me-4  position-relative  position-absolute bottom-0 start-50 translate-middle-x storycard-reacticons-bs d-none'
                                    }>
                                    edit
                                </BsFillPencilFill>
                                <BsFillTrashFill
                                    type="button"
                                    key={`${aStory.id}`}
                                    onClick={onLocalclickdeleteCard}
                                    className={
                                        isAdmin?.length > 0
                                            ? 'ms-4  me-4 position-relative position-absolute bottom-0 mb-2 end-50 translate-middle-x storycard-reacticons-bs'
                                            : 'ms-4  me-4 position-relative position-absolute bottom-0 mb-2 end-50 translate-middle-x storycard-reacticons-bs d-none'
                                    }>
                                    Delete
                                </BsFillTrashFill>
                                <BsFillAspectRatioFill
                                    className=" ms-5  position-relative position-absolute bottom-0 mb-2 start-50 translate-middle-x storycard-reacticons-bs"
                                    onClick={onReadMore}
                                    to="/shareStoryForm "
                                    type="button"
                                    data-page=""
                                    href="#"></BsFillAspectRatioFill>
                            </div>
                        </div>
                    </Card>
                </Row>
            </Col>
        </React.Fragment>
    );
}

StoryCard.propTypes = {
    story: PropTypes.shape({
        id: PropTypes.string,
        name: PropTypes.string.isRequired,
        email: PropTypes.string.isRequired,
        file: PropTypes.arrayOf(
            PropTypes.shape({
                url: PropTypes.number,
                id: PropTypes.string,
            })
        ),
        story: PropTypes.string,
    }),
    onStoryClick: PropTypes.func,
    user: PropTypes.shape({
        id: PropTypes.number,
        email: PropTypes.string,
        isLoggedIn: PropTypes.bool,
        roles: PropTypes.arrayOf(PropTypes.string),
    }).isRequired,
};
export default StoryCard;
