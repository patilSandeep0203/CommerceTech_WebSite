import axios from 'axios';

import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from './serviceHelpers';
const endpoint = `${API_HOST_PREFIX}/api/surveys`;

const add = (payload) => {
    const config = {
        method: 'POST',
        url: endpoint,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateStatus = (payload) => {
    const config = {
        method: 'PUT',
        url: `${endpoint}/instances/${payload.id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getById = (id) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/instances/${id}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAll = () => {
    const config = {
        method: 'GET',
        url: `${endpoint}/instances`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAllPagination = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/instances/paginate?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAllByIdPagination = (pageIndex, pageSize, id) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/instances/paginate/${id}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const search = (pageIndex, pageSize, query) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/instances?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const searchInsights = (pageIndex, pageSize, query) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/insights/search?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getInsightById = (id) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/insights/${id}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAllInsights = () => {
    const config = {
        method: 'GET',
        url: `${endpoint}/insights`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAllInsightsPagination = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/insights/paginate?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const surveysInstanceService = {
    add,
    updateStatus,
    getById,
    getAll,
    getAllPagination,
    search,
    getInsightById,
    getAllInsights,
    getAllByIdPagination,
    getAllInsightsPagination,
    searchInsights,
};

export default surveysInstanceService;
