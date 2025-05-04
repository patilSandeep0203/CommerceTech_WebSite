import axios from 'axios';
import * as helper from './serviceHelpers';
import { API_HOST_PREFIX } from './serviceHelpers';

const add = (payload) => {
    const config = {
        method: 'POST',
        url: `${helper.API_HOST_PREFIX}/api/jobs`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const deleteById = (id) => {
    const config = {
        method: 'DELETE',
        url: `${helper.API_HOST_PREFIX}/api/jobs/${id}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const update = (payload, id) => {
    const config = {
        method: 'PUT',
        url: `${helper.API_HOST_PREFIX}/api/jobs/${id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then((response) => {
        return {
            id: response.data.item,
            ...payload,
        };
    });
};

const lookUp = (payload) => {
    const config = {
        method: 'POST',
        url: process.env.REACT_APP_API_HOST_PREFIX + '/api/lookups',
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getById = (id) => {
    const config = {
        method: 'GET',
        url: `${helper.API_HOST_PREFIX}/api/jobs/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getPaginated = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${helper.API_HOST_PREFIX}/api/jobs/pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getCreatedBy = (pageIndex, pageSize, createdBy) => {
    const config = {
        method: 'GET',
        url: `${helper.API_HOST_PREFIX}/api/listings/createdby?pageIndex=${pageIndex}&pageSize=${pageSize}&createdBy=${createdBy}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getJobsSearch = (pageIndex, pageSize, query) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/jobs/search?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess);
};

const getJobsSearchByLocation = (pageIndex, pageSize, query) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/jobs/search/location?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess);
};

const getJobSearchWithDistance = (pageIndex, pageSize, query, latParam, longParam, distance) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/jobs/search/radius?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}&lat=${latParam}&long=${longParam}&distance=${distance}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess);
};
export {
    add,
    update,
    getById,
    getPaginated,
    getCreatedBy,
    getJobsSearch,
    getJobsSearchByLocation,
    getJobSearchWithDistance,
    lookUp,
    deleteById,
};
