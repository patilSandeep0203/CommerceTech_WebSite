import React, { useState } from 'react';
import PropTypes from 'prop-types';

const StoryPreview = (props) => {
    const [showPreview, setShowPreview] = useState(false);

    const handleClick = () => {
        setShowPreview((prev) => !prev);
    };

    return (
        <div className="row-4" style={{ textAlign: 'center' }}>
            <div className="card">
                <div className="card-body share-story-border-radius-for-card">
                    {showPreview && (
                        <React.Fragment>
                            <div className="form-group">
                                <label htmlFor="name">Name</label>
                                <p className="card-title bg-light rounded-3 p-3 ">{props.story.name}</p>
                            </div>
                            <div className="form-group">
                                <label htmlFor="story">Write your story</label>
                                <p className="card-title bg-light border rounded-3 p-3">{props.story.story}</p>
                            </div>
                            <div>
                                {props.story.urls &&
                                    props.story.urls.map((url) => <img key={url} src={url} alt="Preview" />)}
                            </div>
                        </React.Fragment>
                    )}
                    <button href="#" className="btn btn-primary" onClick={handleClick}>
                        View Details
                    </button>
                </div>
            </div>
        </div>
    );
};
StoryPreview.propTypes = {
    story: PropTypes.shape({
        id: PropTypes.number,
        fileIds: PropTypes.arrayOf(PropTypes.number),
        name: PropTypes.string,
        story: PropTypes.string,
        urls: PropTypes.arrayOf(PropTypes.string),
    }),
    onStoryClick: PropTypes.func,
};
export default StoryPreview;
