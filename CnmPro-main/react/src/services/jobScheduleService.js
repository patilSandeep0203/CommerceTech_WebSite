import axios from 'axios';
import * as helper from './serviceHelpers';

const create = (payload) => {
    const config = {
        method: 'POST',
        url: `${helper.API_HOST_PREFIX}/api/jobschedules`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getCreatedBy = () => {
    const config = {
        method: 'GET',
        url: `${helper.API_HOST_PREFIX}/api/jobschedules/createdby`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getByOrgId = (id) => {
    const config = {
        method: 'GET',
        url: `${helper.API_HOST_PREFIX}/api/jobschedules/organization/${id}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const deleteById = (id) => {
    const config = {
        method: 'DELETE',
        url: `${helper.API_HOST_PREFIX}/api/jobschedules/${id}`,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const getJobByOrgId = (id) => {
    const config = {
        method: 'GET',
        url: `${helper.API_HOST_PREFIX}/api/jobs/organization/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const update = (id, payload) => {
    const config = {
        method: 'PUT',
        url: `${helper.API_HOST_PREFIX}/api/jobschedules/${id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalSuccess);
};

const JobsScheduleService = { create, getCreatedBy, getByOrgId, getJobByOrgId, deleteById, update };

export default JobsScheduleService;
