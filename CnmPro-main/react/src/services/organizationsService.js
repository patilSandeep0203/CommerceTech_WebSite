import axios from 'axios';
import { API_HOST_PREFIX } from './serviceHelpers';

const organizationsApi = {
    endpoint: `${API_HOST_PREFIX}/api/organizations`,
};

const CreateOrganization = (payload) => {
    const config = {
        method: 'POST',
        url: organizationsApi.endpoint,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const GetOrganizationById = (organizationId) => {
    const config = {
        method: 'GET',
        url: `${organizationsApi.endpoint}/${organizationId}`,
        data: organizationId,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const GetOrganizationByCreatedBy = (userId) => {
    const config = {
        method: 'GET',
        url: `${organizationsApi.endpoint}/search?pageIndex=0&pagesize=10&userId=${userId}`,
        data: userId,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const GetAllOrganizations = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${organizationsApi.endpoint}/paginate?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const getLookups = (payload) => {
    const config = {
        method: 'POST',
        url: process.env.REACT_APP_API_HOST_PREFIX + '/api/lookups',
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const getStates = () => {
    const config = {
        method: 'GET',
        url: `${organizationsApi.endpoint}/states`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const UpdateOrganization = (organizationId, payload) => {
    const config = {
        method: 'PUT',
        url: `${organizationsApi.endpoint}/${organizationId}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const DeleteOrganization = (organizationId) => {
    const config = {
        method: 'DELETE',
        url: `${organizationsApi.endpoint}/${organizationId}`,
        data: organizationId,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

export {
    CreateOrganization,
    GetOrganizationById,
    GetOrganizationByCreatedBy,
    GetAllOrganizations,
    UpdateOrganization,
    DeleteOrganization,
    getLookups,
    getStates,
};
