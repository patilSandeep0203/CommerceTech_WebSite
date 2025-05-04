import axios from 'axios';
import { API_HOST_PREFIX, onGlobalSuccess, onGlobalError } from './serviceHelpers';

const add = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/api/events`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const deleteById = (id) => {
    const config = {
        method: 'DELETE',
        url: `${API_HOST_PREFIX}/api/events/${id}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const update = (payload, id) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/api/events/${id}`,
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

const getEventById = (id) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/events/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAll = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/events/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getPaginated = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/events/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getCreatedBy = (pageIndex, pageSize, createdBy) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/listings/createdby?pageIndex=${pageIndex}&pageSize=${pageSize}&createdBy=${createdBy}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getSearchedEvents = (pageIndex, pageSize, query) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/events/search?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getEventsSearchByLocation = (pageIndex, pageSize, query) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/events/search/location?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getEventSearchWithDistance = (pageIndex, pageSize, query, latParam, longParam, distance) => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/events/search/radius?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}&lat=${latParam}&long=${longParam}&distance=${distance}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
export {
    add,
    update,
    getEventById,
    getAll,
    getPaginated,
    getCreatedBy,
    getSearchedEvents,
    getEventsSearchByLocation,
    getEventSearchWithDistance,
    lookUp,
    deleteById,
};
