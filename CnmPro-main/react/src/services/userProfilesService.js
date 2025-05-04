import axios from 'axios';
import { API_HOST_PREFIX, onGlobalSuccess, onGlobalError } from './serviceHelpers';

const endpoint = {
    userProfileUrl: `${API_HOST_PREFIX}/api/userprofiles`,
};

const addUserProfileV2 = (payload) => {
    const config = {
        method: 'POST',
        url: `${endpoint.userProfileUrl}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateUserProfileV2 = (payload, id) => {
    const config = {
        method: 'PUT',
        url: `${endpoint.userProfileUrl}/${id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const deleteUserProfile = (userId) => {
    const config = {
        method: 'PUT',
        url: `${endpoint.userProfileUrl}/${userId}`,
        data: userId,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(() => {
        return userId;
    });
};

const getCurrentUserProfile = () => {
    const config = {
        method: 'GET',
        url: `${endpoint.userProfileUrl}/current`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getAllUserProfiles = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint.userProfileUrl}admin/?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then((res) => res.data);
};

const getUserProfileCreatedBy = (userId, pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${endpoint.userProfileUrl}admin/?pageIndex=${pageIndex}&pageSize=${pageSize}userId=${userId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const addLicense = (payload) => {
    const config = {
        method: 'POST',
        url: `${endpoint.userProfileUrl}licenses`,
        data: payload,
        withCredentials: true,
        crossDomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getDashboard = () => {
    const config = {
        method: 'GET',
        url: `${endpoint.userProfileUrl}/dashboard`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const userProfilesService = {
    addUserProfileV2,
    updateUserProfileV2,
    deleteUserProfile,
    getCurrentUserProfile,
    getAllUserProfiles,
    getUserProfileCreatedBy,
    addLicense,
    getDashboard,
};

export default userProfilesService;
