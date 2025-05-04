import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import debug from 'sabio-debug';
import PropTypes from 'prop-types';
import { Card, Button, Container } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './shareStory.css';

const _logger = debug.extend('StoryDetails');

function StoryDetails() {
    const location = useLocation();
    _logger(location);
    let details = location.state.payload;
    _logger({ details });

    const [currImgIndex, setCurrImgIndex] = useState(0);
    const nextImage = () => {
        if (currImgIndex !== details.file.length - 1) {
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
            <div className="container">
                <Link to="/sharestories" className="btn btn-primary" type="button">
                    {' '}
                    Back
                </Link>
                <div className="row d-flex justify-content-center">
                    <div className="col-md-6 bg-transparent rounded-3 p-3">
                        <Card.Body>
                            <Card.Title as="h2" className=" mt-5 mb-2 bg-light rounded-3 p-3 text-center">
                                {details.name}
                            </Card.Title>
                            <Card.Img variant="top" src={details.file?.[currImgIndex].url}></Card.Img>
                            <Card.Text className=" mb-4 mt-3 bg-light rounded-3 p-3 storydetail-cardbody-text-fs">
                                {' '}
                                {details.story}
                            </Card.Text>
                            <Container className="text-center">
                                {' '}
                                <Button
                                    className="story-image-button"
                                    onClick={nextImage}
                                    disabled={currImgIndex >= details.file.length - 1}>
                                    Next
                                </Button>{' '}
                                <Button className="story-image-button" onClick={prevImage} disabled={currImgIndex <= 0}>
                                    Prev
                                </Button>
                            </Container>
                        </Card.Body>
                    </div>
                </div>
            </div>
        </React.Fragment>
    );
}
StoryDetails.propTypes = {
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
};
export default StoryDetails;
