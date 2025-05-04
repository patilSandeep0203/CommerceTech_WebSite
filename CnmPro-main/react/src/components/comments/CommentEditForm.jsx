import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import { PropTypes } from 'prop-types';
import commentService from '../../services/commentService';
import toastr from 'toastr';

const CommentEditForm = (props) => {
    const [commentData, setCommentData] = useState({
        id: props.comment.id,
        text: props.comment.text,
        isDeleted: props.comment.isDeleted,
        userId: props.currentUser.id,
    });

    const onCommentTextEdit = (e) => {
        let newText = e.target.value;
        setCommentData((prevState) => {
            const newComment = {
                ...prevState,
            };
            newComment.text = newText;
            return newComment;
        });
    };

    const onSubmitClicked = () => {
        commentService.updateComment(commentData, props.comment.id).then(onEditSuccess).catch(onEditError);
    };

    const onEditSuccess = () => {
        props.setShowEditForm(false);
        props.changeState();
        toastr.success('Comments Edit Success');
    };

    const onEditError = () => {
        toastr.error('Comments Edit Error');
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            onSubmitClicked();
        }
    };

    return (
        <Form>
            <input
                className="form-control form-control-light mb-2 "
                onChange={onCommentTextEdit}
                placeholder={props.comment.text}
                onKeyDown={handleKeyDown}
            />
            <button type="button" className="btn btn-primary btn-sm" onClick={onSubmitClicked}>
                Save
            </button>
        </Form>
    );
};
CommentEditForm.propTypes = {
    comment: PropTypes.shape({
        id: PropTypes.number.isRequired,
        text: PropTypes.string.isRequired,
        isDeleted: PropTypes.bool.isRequired,
    }).isRequired,
    currentUser: PropTypes.shape({
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
    changeState: PropTypes.func.isRequired,
    setShowEditForm: PropTypes.func.isRequired,
};

export default CommentEditForm;
