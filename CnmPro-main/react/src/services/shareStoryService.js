import axios from 'axios';
import { API_HOST_PREFIX } from './serviceHelpers';

var storyServices = {
    endpoint: `${API_HOST_PREFIX}/api/sharestory`,
};

const addStory = (payload) => {
    const config = {
        method: 'POST',
        url: `${storyServices.endpoint}`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};
const getShareStoryId = (id) => {
    const config = {
        method: 'GET',
        url: `${storyServices.endpoint}/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const getAllPage = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${storyServices.endpoint}/paginate/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};
const update = (payload) => {
    const config = {
        method: 'PUT',
        url: `${storyServices.endpoint}/${payload.id}`,
        data: payload,
        crossdomain: true,
        withCredentials: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};
const deleteStory = (storyId) => {
    const config = {
        method: 'DELETE',
        url: `${storyServices.endpoint}/${storyId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

export { addStory, getAllPage, update, getShareStoryId, deleteStory };
