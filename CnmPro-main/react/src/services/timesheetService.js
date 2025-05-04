import axios from 'axios';
import { API_HOST_PREFIX, onGlobalSuccess, onGlobalError } from './serviceHelpers';

const endpoint = `${API_HOST_PREFIX}/api/timesheets`;

const updateTimesheetEntry = (payload, id) => {
    const config = {
        method: 'PUT',
        data: payload,
        url: `${endpoint}/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const addTimesheetEntry = (payload) => {
    const config = {
        method: 'POST',
        data: payload,
        url: `${endpoint}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAllCurrentUserPaginate = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/user/current/paginate/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const filter = (organizationId, dateRange, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/search/filter/?organizationId=${organizationId}&dateRange=${dateRange}&pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const searchByOrg = (organizationId, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint}/search/organization/?organizationId=${organizationId}&pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const totalHours = () => {
    const config = {
        method: 'GET',
        url: `${endpoint}/total`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const userJobs = () => {
    const config = {
        method: 'GET',
        url: `${endpoint}/jobs`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const userOrganizations = () => {
    const config = {
        method: 'GET',
        url: `${endpoint}/organizations`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const timesheetService = {
    updateTimesheetEntry,
    addTimesheetEntry,
    getAllCurrentUserPaginate,
    filter,
    searchByOrg,
    totalHours,
    userJobs,
    userOrganizations,
};

export default timesheetService;
