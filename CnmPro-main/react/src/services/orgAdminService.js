import axios from 'axios';
import * as helper from './serviceHelpers';
import { API_HOST_PREFIX } from './serviceHelpers';

const endpoint = {
    orgadminUrl: `${API_HOST_PREFIX}/api/organizations`,
};

const addOrgAdmin = (payload) => {
    const config = {
        method: 'POST',
        url: `${endpoint.orgadminUrl}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const updateOrgAdmin = (payload, id) => {
    const config = {
        method: 'PUT',
        url: `${endpoint.orgadminUrl}/${id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getByIdOrgAdmin = (id) => {
    const config = {
        method: 'GET',
        url: `${endpoint.orgadminUrl}/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getPaginatedOrgAdmin = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint.orgadminUrl}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getCreatedByOrgAdmin = (pageIndex, pageSize, createdBy) => {
    const config = {
        method: 'GET',
        url: `${endpoint.orgadminUrl}?pageIndex=${pageIndex}&pageSize=${pageSize}&createdBy=${createdBy}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

export { addOrgAdmin, updateOrgAdmin, getByIdOrgAdmin, getPaginatedOrgAdmin, getCreatedByOrgAdmin };
