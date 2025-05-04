import axios from 'axios';
import { API_HOST_PREFIX } from './serviceHelpers';
import { onGlobalError, onGlobalSuccess } from './serviceHelpers';

const url = `${API_HOST_PREFIX}/api/AdminDashboard`;

const adminDashboardWeek = () => {
    const config = {
        method: 'GET',
        url: `${url}/weeks`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const adminDashboard = () => {
    const config = {
        method: 'GET',
        url: `${url}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getUserWeekStats = () => {
    const config = {
        method: 'GET',
        url: `${url}/stats/users/week`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getUserTwoWeekStats = () => {
    const config = {
        method: 'GET',
        url: `${url}/stats/users/twoweeks`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getUserMonthStats = () => {
    const config = {
        method: 'GET',
        url: `${url}/stats/users/month`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getUserYearStats = () => {
    const config = {
        method: 'GET',
        url: `${url}/stats/users/year`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getOrganizationsStats = () => {
    const config = {
        method: 'GET',
        url: `${url}/stats/organizations/`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const adminDashboardService = {
    getUserWeekStats,
    getUserTwoWeekStats,
    getUserMonthStats,
    getUserYearStats,
    adminDashboardWeek,
    adminDashboard,
    getOrganizationsStats,
};

export default adminDashboardService;
