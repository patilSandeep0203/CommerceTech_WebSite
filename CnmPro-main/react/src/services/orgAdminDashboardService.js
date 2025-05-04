import axios from 'axios';
import { API_HOST_PREFIX } from './serviceHelpers';
import { onGlobalError, onGlobalSuccess } from './serviceHelpers';

let baseurl = `${API_HOST_PREFIX}/api/orgadmindashboard`;

const getJobTypeStats = (id) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/stats/jobtypes/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getJobsByActiveStatus = (id) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/job/status/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getJobsByOrganzation = (id) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/jobs/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getJobsByOrganizationPaginate = (id, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/jobs/organization/${id}`,
        params: { pageIndex: pageIndex, pageSize: pageSize },
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getProposalsByOrganizationPaginate = (id, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/proposals/organization/${id}`,
        params: { pageIndex: pageIndex, pageSize: pageSize },
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getProposalLast7Days = (orgId, timezoneOffset) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/proposal/weekly`,
        params: { id: orgId, timezoneOffset: timezoneOffset },
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getProposalLast15Days = (orgId, timezoneOffset) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/proposal/biweekly`,
        params: { id: orgId, timezoneOffset: timezoneOffset },
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getProposalLast30Days = (orgId, timezoneOffset) => {
    const config = {
        method: 'GET',
        url: `${baseurl}/proposal/monthly`,
        params: { id: orgId, timezoneOffset: timezoneOffset },
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const sendApprovedEmail = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/api/emails/approvedEmail`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const sendDeniedEmail = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/api/emails/deniedEmail`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

let orgAdminDashServices = {
    getJobTypeStats,
    getJobsByActiveStatus,
    getJobsByOrganzation,
    getProposalLast7Days,
    getProposalLast15Days,
    getProposalLast30Days,
    getJobsByOrganizationPaginate,
    getProposalsByOrganizationPaginate,
    sendApprovedEmail,
    sendDeniedEmail,
};

export default orgAdminDashServices;
