import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from './serviceHelpers';

const ratingsUrl = `${API_HOST_PREFIX}/api/ratings`;

const addRating = (payload) => {
    const config = {
        method: 'POST',
        url: `${ratingsUrl}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const GetRatingByComment = (commentId) => {
    const config = {
        method: 'GET',
        url: `${ratingsUrl}/by/comment/?commentId=${commentId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const GetRatingCreatedBy = (entityId, entityTypeId) => {
    const config = {
        method: 'GET',
        url: `${ratingsUrl}/by/createdby?entityId=${entityId}&entityTypeId=${entityTypeId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getRatingPaginate = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${ratingsUrl}/paginate/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const ratingByAuthor = (pageIndex, pageSize, createdById) => {
    const config = {
        method: 'GET',
        url: `${ratingsUrl}/createdby/?pageIndex=${pageIndex}&pageSize=${pageSize}&createdBy=${createdById}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const ratingById = (id) => {
    const config = {
        method: 'GET',
        url: `${ratingsUrl}/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const ratingByEntity = (pageIndex, pageSize, entityId, entityTypeId) => {
    const config = {
        method: 'GET',
        url: `${ratingsUrl}/by/entityid/?pageIndex=${pageIndex}&pageSize=${pageSize}&entityId=${entityId}&entityTypeId=${entityTypeId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const ratingAverage = (entityId, entityTypeId) => {
    const config = {
        method: 'GET',
        url: `${ratingsUrl}/average/rating?entityId=${entityId}&entityTypeId=${entityTypeId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateRating = (payload, id) => {
    const config = {
        method: 'PUT',
        url: `${ratingsUrl}/${id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const deleteRating = (id) => {
    const config = {
        method: 'DELETE',
        url: `${ratingsUrl}/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const ratingService = {
    getRatingPaginate,
    ratingById,
    ratingAverage,
    addRating,
    ratingByAuthor,
    ratingByEntity,
    updateRating,
    deleteRating,
    GetRatingByComment,
    GetRatingCreatedBy,
};

export default ratingService;
