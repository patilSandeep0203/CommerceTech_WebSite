import axios from 'axios';
import { API_HOST_PREFIX } from './serviceHelpers';

const endpoint = `${API_HOST_PREFIX}/api/comments`;

const addComment = (payload) => {
    const config = {
        method: 'POST',
        url: `${endpoint}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getComment = (commentId) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/${commentId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getCommentCreatedBy = (createdBy, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/createdby?pageIndex=${pageIndex}&pageSize=${pageSize}&createdBy=${createdBy}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getCommentParentId = (ParentId) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/parentId/${ParentId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getCommentEntityId = (entityId, entityTypeId, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/entityId/${entityId}/entityTypeId/${entityTypeId}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getAllComments = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/paginate/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getAllReplies = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/replies/paginate/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const updateComment = (payload, commentId) => {
    const config = {
        method: 'PUT',
        url: `${endpoint}/update/${commentId}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const deleteComment = (commentId) => {
    const config = {
        method: 'DELETE',
        url: `${endpoint}/delete/${commentId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(() => {
        return commentId;
    });
};

const commentService = {
    addComment,
    getComment,
    getCommentCreatedBy,
    getCommentParentId,
    getCommentEntityId,
    getAllComments,
    getAllReplies,
    updateComment,
    deleteComment,
};

export default commentService;
