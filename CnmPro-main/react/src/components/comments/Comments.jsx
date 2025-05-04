import React, { useState, useEffect } from 'react';
import { Card, Button } from 'react-bootstrap';
import { PropTypes } from 'prop-types';
import toastr from 'toastr';
import commentService from '../../services/commentService';
import SingleComment from './SingleComment';
import Swal from '@sweetalert/with-react';
import { Link } from 'react-router-dom';

import debug from 'sabio-debug';
const _logger = debug.extend('comment');

const Comment = (props) => {
    const [comments, setComments] = useState([]);
    const [commentCount, setCommentCount] = useState(0);
    const [pagedCommentCount, setPagedCommentCount] = useState(3);
    const [isShow, setIsShow] = useState(false);
    const [isNewCommentAdded, setIsNewCommentAdded] = useState(false);
    const [formData, setFormData] = useState({
        text: '',
        parentId: '',
        entityTypeId: props.entTypeId,
        entityId: props.entId,
        isDeleted: false,
        replies: [],
    });

    useEffect(() => {
        _logger('useEffect firing');
        commentService
            .getCommentEntityId(props.entId, props.entTypeId, 0, 3)
            .then(onGetAllSuccess)
            .catch(onGetAllError);
    }, [isNewCommentAdded]);

    const onGetAllSuccess = (data) => {
        let allComments = data.data.item.pagedItems;
        _logger(allComments);
        setComments(allComments);
        toastr.success('All Comments Displayed');
        setCommentCount(data.data.item.totalCount);
        if (pagedCommentCount < data.data.item.totalCount) {
            _logger('load more displayed');
            setIsShow(!isShow);
        }
    };

    const onGetAllError = (error) => {
        _logger(error);
    };

    const changeState = () => {
        setIsNewCommentAdded(!isNewCommentAdded);
    };

    const onCommentInput = (e) => {
        const value = e.target.value;
        setFormData((prevState) => {
            const newComment = {
                ...prevState,
            };
            newComment.text = value;
            newComment.parentId = null;
            return newComment;
        });
    };

    const SubmitCommentClicked = () => {
        _logger(formData);
        commentService.addComment(formData).then(onSubmitSuccess).catch(onSubmitError);
    };
    const onSubmitSuccess = () => {
        changeState();
        formData.text = '';
        toastr.success('Comment Added');
    };
    const onSubmitError = (error) => {
        toastr.error('Error adding comments', error);
    };

    const mapComment = (aComment) => {
        if (aComment.parentId === 0) {
            return (
                <SingleComment
                    key={aComment.id}
                    comment={aComment}
                    onDeleteClicked={onDeleteClicked}
                    currentUser={props.currentUser}
                    setParentFormData={setFormData}
                    deleteExecution={deleteExecution}
                    isNewCommentAdded={isNewCommentAdded}
                    changeState={changeState}
                />
            );
        }
    };

    const onDeleteClicked = (e) => {
        let commentId = e.target.id;
        _logger(commentId + 'Delete Clicked');
        Swal({
            title: 'Are you Sure?',
            icon: 'warning',
            buttons: {
                confirm: 'Yes',
                cancel: 'No',
            },
        }).then(function (confirm) {
            if (confirm) {
                _logger('confirm');
                deleteExecution(commentId);
            }
            if (!confirm) {
                _logger('Cancelled');
            }
        });
    };

    const deleteExecution = (commentId) => {
        commentService.deleteComment(commentId).then(deleteSuccess).catch(deleteError);
    };

    const deleteSuccess = (res) => {
        if (comments.length === 1) {
            setComments([]);
            setCommentCount(0);
        }
        setComments(comments.filter((comment) => comment.id !== parseInt(res)));
        changeState();
        _logger('Deleted');
        toastr.success('Delete Successful');
        setPagedCommentCount(comments.length);
    };

    const deleteError = (error) => {
        _logger('Delete Error');
        toastr.error('Delete Error', error);
    };

    const displayAllOnLoadMore = () => {
        commentService
            .getCommentEntityId(props.entId, props.entTypeId, 0, 50)
            .then(onGetAllSuccess)
            .catch(onGetAllError);
        _logger(commentCount);
        setPagedCommentCount(commentCount);
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            SubmitCommentClicked();
            e.target.textarea = '';
        }
    };

    return (
        <>
            <Card className=" main-commentbox-cnmPro shadow-sm">
                <h4 className="mt-0 mb-3">Comments ({commentCount})</h4>
                <textarea
                    type="text"
                    className="form-control form-control-light mb-2 overflow-y: scroll;"
                    placeholder="Write comment"
                    value={formData.text}
                    rows="3"
                    onChange={onCommentInput}
                    maxLength="3000"
                    onKeyDown={handleKeyDown}></textarea>

                <div className="text-end">
                    <div className="btn-group mb-2 ms-2">
                        <Button onClick={SubmitCommentClicked} type="button" className="btn btn-primary btn-sm">
                            Submit
                        </Button>
                    </div>
                </div>
                {comments.map(mapComment)}
                <div className="text-center mt-2"></div>
                <div className="text-center mt-2">
                    {isShow && (
                        <Link to="#" className="text-danger" onClick={displayAllOnLoadMore}>
                            Load more
                        </Link>
                    )}
                </div>
            </Card>
        </>
    );
};

Comment.propTypes = {
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
    }),
    currentUser: PropTypes.shape({
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
    entTypeId: PropTypes.number.isRequired,
    entId: PropTypes.number.isRequired,
};

export default Comment;
