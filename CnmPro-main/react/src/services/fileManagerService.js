import axios from 'axios';
import { onGlobalError, onGlobalSuccess, API_HOST_PREFIX } from './serviceHelpers';

const fileUrl = `${API_HOST_PREFIX}/api/files`;

const getAll = (pgIndex, pgSize) => {
    const config = {
        method: 'GET',
        url: `${fileUrl}/paginate?pageIndex=${pgIndex}&pageSize=${pgSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getSearch = async (pgIndex, pgSize, query) => {
    const config = {
        method: 'GET',
        url: `${fileUrl}/search/?pageIndex=${pgIndex}&pageSize=${pgSize}&query=${query}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getFileType = (id, pgIndex, pgSize) => {
    const config = {
        method: 'GET',
        url: `${fileUrl}/${id}?pageIndex=${pgIndex}&pageSize=${pgSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getMyFiles = (pgIndex, pgSize) => {
    const config = {
        method: 'GET',
        url: `${fileUrl}/user/current?pageIndex=${pgIndex}&pageSize=${pgSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getRecent = (pgIndex, pgSize) => {
    const config = {
        method: 'GET',
        url: `${fileUrl}/paginate?pageIndex=${pgIndex}&pageSize=${pgSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getDeleted = (pgIndex, pgSize, deleted) => {
    const config = {
        method: 'GET',
        url: `${fileUrl}/deleted?pageIndex=${pgIndex}&pageSize=${pgSize}&isdeleted=${deleted}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const isDeleted = (id, isDeleted) => {
    const config = {
        method: 'PUT',
        url: `${fileUrl}/${id}/${isDeleted}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const fileManagerService = { getAll, getSearch, getFileType, getMyFiles, getRecent, getDeleted, isDeleted };

export default fileManagerService;
