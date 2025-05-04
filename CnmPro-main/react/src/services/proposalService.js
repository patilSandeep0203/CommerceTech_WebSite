import axios from 'axios';
import { API_HOST_PREFIX } from './serviceHelpers';
import { onGlobalError, onGlobalSuccess } from './serviceHelpers';

const proposalsApi = {
    endpoint: `${API_HOST_PREFIX}/api/proposals`,
};

const addProposal = (payload) => {
    const config = {
        method: 'POST',
        url: proposalsApi.endpoint,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const getProposalById = (proposalId) => {
    const config = {
        method: 'GET',
        url: `${proposalsApi.endpoint}/${proposalId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const getProposalByCreatedBy = (createdBy, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${proposalsApi.endpoint}/?createdBy=${createdBy}&pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const selectAll = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${proposalsApi.endpoint}/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const update = (payload) => {
    const config = {
        method: 'PUT',
        url: `${proposalsApi.endpoint}/${payload.id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};
const deleteProposal = (proposalId) => {
    const config = {
        method: 'DELETE',
        url: `${proposalsApi.endpoint}/${proposalId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getLookups = (payload) => {
    const config = {
        method: 'POST',
        url: `${process.env.REACT_APP_API_HOST_PREFIX}/api/lookups`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const proposalService = {
    addProposal,
    getProposalById,
    getProposalByCreatedBy,
    selectAll,
    update,
    deleteProposal,
    getLookups,
};

export default proposalService;
