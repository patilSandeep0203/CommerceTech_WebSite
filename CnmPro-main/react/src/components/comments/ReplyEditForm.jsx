import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import { PropTypes } from 'prop-types';
import commentService from '../../services/commentService';
import toastr from 'toastr';

const ReplyEditForm = (props) => {
    const [replyData, setReplyData] = useState({
        id: props.reply.id,
        text: props.reply.text,
        isDeleted: props.reply.isDeleted,
        userId: props.currentUser.id,
    });

    const onReplyTextEdit = (e) => {
        let newText = e.target.value;
        setReplyData((prevState) => {
            const newComment = {
                ...prevState,
            };
            newComment.text = newText;
            return newComment;
        });
    };

    const onSubmitClicked = () => {
        commentService.updateComment(replyData, props.reply.id).then(onEditSuccess).catch(onEditError);
    };

    const onEditSuccess = () => {
        props.setShowEdit(false);
        props.changeState();
        toastr.success('Reply Edit Success');
    };

    const onEditError = () => {
        toastr.error('Reply Edit Error');
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            onSubmitClicked();
        }
    };
    return (
        <Form>
            <input
                className="form-control form-control-light mb-2 reply-comment-edit-cnmPro"
                onChange={onReplyTextEdit}
                placeholder={props.reply.text}
                onKeyDown={handleKeyDown}
            />
            <button type="button" className="btn btn-primary btn-sm" onClick={onSubmitClicked}>
                Save
            </button>
        </Form>
    );
};
ReplyEditForm.propTypes = {
    reply: PropTypes.shape({
        id: PropTypes.number.isRequired,
        text: PropTypes.string.isRequired,
        isDeleted: PropTypes.bool.isRequired,
    }).isRequired,
    currentUser: PropTypes.shape({
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
    changeState: PropTypes.func.isRequired,
    setShowEdit: PropTypes.func.isRequired,
};

export default ReplyEditForm;
