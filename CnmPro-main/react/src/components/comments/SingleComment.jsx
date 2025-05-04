import React, { useState, useEffect } from 'react';
import { PropTypes } from 'prop-types';
import { Card } from 'react-bootstrap';
import { BsFillTrashFill, BsFillPencilFill, BsArrowReturnLeft } from 'react-icons/bs';
import ReplyBox from './ReplyBox';
import './comment.css';
import CommentReplies from './CommentReplies';
import CommentEditForm from './CommentEditForm';
import Rating from '../../pages/rating/Rating';

import debug from 'sabio-debug';
const _logger = debug.extend('singlecomment');

const SingleComment = (props) => {
    const [isShow, setIsShown] = useState(false);
    const [mappedReplies, setMappedReplies] = useState([]);
    const [showEditForm, setShowEditForm] = useState(false);

    useEffect(() => {
        if (props.comment.replies) {
            const mappedReplies = props.comment.replies.map(mapReplies);
            setMappedReplies(mappedReplies);
        }
    }, [props]);

    const mapReplies = (aReply) => {
        return (
            <CommentReplies
                key={aReply.id}
                reply={aReply}
                parentCommentId={props.comment.id}
                currentUser={props.currentUser}
                deleteExecution={props.deleteExecution}
                changeState={props.changeState}
            />
        );
    };

    const onReplyClicked = (e) => {
        e.preventDefault();
        let parentCommentId = e.target.id;
        _logger(parentCommentId);
        _logger(mappedReplies);
        setIsShown(true);
        return <ReplyBox />;
    };

    const date = new Date(props.comment.dateCreated);
    const localDate = date.toLocaleString();

    const onEditClicked = (e) => {
        let commentId = e.target.id;
        _logger(commentId, ' edit clicked ');
        setShowEditForm(true);
    };

    return (
        <>
            <Card>
                <div className=" single-commentbox-cnmPro shadow-sm">
                    <Rating
                        entityId={props.comment.entityId}
                        entityTypeId={props.comment.entityTypeId}
                        commentId={props.comment.id}
                    />
                    <div className="d-flex align-items-start mt-3">
                        <img src={props.comment.avatarUrl} className="me-3 avatar-sm rounded-circle" alt="" />
                        <div className="w-100 overflow-hidden">
                            <h3 className="mt-3 comment-inline">
                                {props.comment.firstName} {props.comment.lastName}
                            </h3>
                            <small className="text-muted text-align-right-comments comment-inline">{localDate}</small>
                            {!showEditForm && <div className="comment-text-padding">{props.comment.text}</div>}
                            {showEditForm && (
                                <CommentEditForm
                                    comment={props.comment}
                                    currentUser={props.currentUser}
                                    changeState={props.changeState}
                                    setShowEditForm={setShowEditForm}
                                />
                            )}
                            {props.currentUser.id === props.comment.createdBy && (
                                <BsFillTrashFill
                                    id={props.comment.id}
                                    className="delete-comment-text-red ms-2 comment-icon-font-size"
                                    onClick={props.onDeleteClicked}
                                    cursor="pointer">
                                    Delete
                                </BsFillTrashFill>
                            )}
                            {props.currentUser.id === props.comment.createdBy && (
                                <BsFillPencilFill
                                    onClick={onEditClicked}
                                    id={props.comment.id}
                                    className="comment-edit-paddingleft comment-icon-font-size"
                                    cursor="pointer">
                                    Edit
                                </BsFillPencilFill>
                            )}
                            <div className="comment-replyEdit ">
                                <div>
                                    <BsArrowReturnLeft
                                        id={props.comment.id}
                                        className="comment-icon-font-size"
                                        cursor="pointer"
                                        onClick={onReplyClicked}></BsArrowReturnLeft>
                                    <a
                                        className="reply-hover-cursor-cnmpro"
                                        id={props.comment.id}
                                        onClick={onReplyClicked}>
                                        Reply
                                    </a>
                                </div>
                            </div>
                            {isShow && (
                                <ReplyBox
                                    comment={props.comment}
                                    changeState={props.changeState}
                                    setIsShown={setIsShown}
                                />
                            )}
                            <div className="replies ">
                                {mappedReplies.length > 0 ? (
                                    <div className="replies-container">{mappedReplies}</div>
                                ) : null}
                            </div>
                        </div>
                    </div>
                </div>
            </Card>
        </>
    );
};

SingleComment.propTypes = {
    comment: PropTypes.shape({
        id: PropTypes.number.isRequired,
        subject: PropTypes.string,
        text: PropTypes.string.isRequired,
        parentId: PropTypes.number,
        entityTypeId: PropTypes.number.isRequired,
        entityId: PropTypes.number.isRequired,
        dateCreated: PropTypes.string,
        dateModified: PropTypes.string,
        createdBy: PropTypes.number.isRequired,
        firstName: PropTypes.string.isRequired,
        lastName: PropTypes.string.isRequired,
        avatarUrl: PropTypes.string.isRequired,
        isDeleted: PropTypes.bool,
        replies: PropTypes.arrayOf(PropTypes.shape({})),
    }).isRequired,
    currentUser: PropTypes.shape({
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
    onDeleteClicked: PropTypes.func.isRequired,
    setParentFormData: PropTypes.func.isRequired,
    deleteExecution: PropTypes.func.isRequired,
    changeState: PropTypes.func.isRequired,
};

export default SingleComment;
