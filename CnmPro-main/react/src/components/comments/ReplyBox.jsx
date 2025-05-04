import React, { useState } from 'react';
import debug from 'sabio-debug';
import commentService from '../../services/commentService';
import toastr from 'toastr';
import { PropTypes } from 'prop-types';

const _logger = debug.extend('replybox');

const ReplyBox = (props) => {
    const parentCommentId = props.comment.id;
    const parentEntityTypeId = props.comment.entityTypeId;
    const parentEntityId = props.comment.entityId;

    const [replyFormData, setReplyFormData] = useState({
        text: '',
        parentId: parentCommentId,
        entityTypeId: parentEntityTypeId,
        entityId: parentEntityId,
        replies: [],
    });

    const replyOnChange = (e) => {
        const value = e.target.value;
        setReplyFormData((prevState) => {
            const commentReply = { ...prevState };
            commentReply.text = value;
            return commentReply;
        });
    };

    const hideBoxAndSubmit = () => {
        _logger(replyFormData);
        commentService.addComment(replyFormData).then(replySuccess).catch(replyError);
    };

    const replySuccess = () => {
        _logger('added');
        toastr.success('Reply Added');
        props.changeState();
        props.setIsShown(false);
    };

    const replyError = (error) => {
        _logger('error adding');
        toastr.error('Reply Add Error', error);
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            hideBoxAndSubmit();
        }
    };

    return (
        <div>
            <input
                onChange={replyOnChange}
                className="form-control form-control-light mb-2"
                placeholder="Write comment"
                id="reply"
                onKeyDown={handleKeyDown}
                value={replyFormData.text}
                rows="3"></input>
            <button onClick={hideBoxAndSubmit} type="button" className="btn btn-primary btn-sm">
                Submit
            </button>
        </div>
    );
};

ReplyBox.propTypes = {
    comment: PropTypes.shape({
        id: PropTypes.number.isRequired,
        entityTypeId: PropTypes.number.isRequired,
        entityId: PropTypes.number.isRequired,
    }),
    changeState: PropTypes.func.isRequired,
    setIsShown: PropTypes.func.isRequired,
};

export default ReplyBox;
