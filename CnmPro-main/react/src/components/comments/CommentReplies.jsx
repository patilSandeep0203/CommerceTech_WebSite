import React, { useState } from 'react';
import { Card } from 'react-bootstrap';
import { PropTypes } from 'prop-types';
import Swal from '@sweetalert/with-react';
import { BsFillTrashFill, BsFillPencilFill } from 'react-icons/bs';
import ReplyEditForm from './ReplyEditForm';
import './comment.css';

import debug from 'sabio-debug';
const _logger = debug.extend('commentReplies');

const CommentReplies = (props) => {
    const [showEdit, setShowEdit] = useState(false);
    const onReplyDeleteClicked = () => {
        _logger('REPLY', props.reply);
        Swal({
            title: 'Are you Sure?',
            icon: 'warning',
            buttons: {
                confirm: 'Yes',
                cancel: 'No',
            },
        }).then((confirm) => {
            if (confirm) {
                _logger('confirm');
                props.deleteExecution(props.reply.id);
                props.changeState();
            } else if (!confirm) {
                _logger('Cancelled');
            }
        });
    };

    const onReplyEditCLicked = (e) => {
        _logger(e.target.id);
        setShowEdit(true);
    };

    return (
        <Card className="comment-reply-box" id={props.reply.id}>
            <div className="d-flex align-items-start mt-2">
                <img src={props.reply.avatarUrl} className="me-3 avatar-sm rounded-circle" alt="" />
                <div className="w-100 overflow-hidden">
                    <h3 className="mt-3 comment-inline">
                        {props.reply.firstName} {props.reply.lastName}
                    </h3>
                    <small className="text-muted text-align-right-comments comment-inline">
                        {props.reply.dateCreated}
                    </small>
                    {showEdit && (
                        <ReplyEditForm
                            reply={props.reply}
                            currentUser={props.currentUser}
                            changeState={props.changeState}
                            setShowEdit={setShowEdit}
                        />
                    )}

                    {!showEdit && <div className="comment-text-padding">{props.reply.text}</div>}
                    <div className="col-md-12 text-right">
                        {props.currentUser.id === props.reply.createdBy && (
                            <BsFillTrashFill
                                className="delete-comment-text-red ms-2 comment-icon-font-size"
                                onClick={onReplyDeleteClicked}
                                cursor="pointer">
                                Delete
                            </BsFillTrashFill>
                        )}
                        {props.currentUser.id === props.reply.createdBy && (
                            <BsFillPencilFill
                                className="comment-edit-paddingleft comment-icon-font-size"
                                onClick={onReplyEditCLicked}
                                cursor="pointer">
                                Edit
                            </BsFillPencilFill>
                        )}
                    </div>
                </div>
            </div>
        </Card>
    );
};
CommentReplies.propTypes = {
    reply: PropTypes.shape({
        id: PropTypes.number,
        subject: PropTypes.string,
        text: PropTypes.string,
        parentId: PropTypes.number.isRequired,
        entityTypeId: PropTypes.number,
        entityId: PropTypes.number,
        dateCreated: PropTypes.string,
        dateModified: PropTypes.string,
        createdBy: PropTypes.number,
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        avatarUrl: PropTypes.string,
        isDeleted: PropTypes.bool,
        replies: PropTypes.arrayOf(PropTypes.shape({})),
    }).isRequired,
    currentUser: PropTypes.shape({
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
    deleteExecution: PropTypes.func.isRequired,
    changeState: PropTypes.func.isRequired,
};

export default CommentReplies;
