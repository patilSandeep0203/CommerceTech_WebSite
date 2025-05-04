import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from './serviceHelpers';

const addLink = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/api/externallinks`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config)
        .then((response) => {
            return response.data.item;
        })
        .catch(onGlobalError);
};

const getByCreatedBy = () => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/api/externallinks`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateLink = (payload) => {
    const config = {
        method: 'PUT',
        url: `${API_HOST_PREFIX}/api/externallinks`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const deleteById = (id) => {
    const config = {
        method: 'DELETE',
        url: `${API_HOST_PREFIX}/api/externallinks/${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(() => {
        return id;
    });
};

const externalLinkService = {
    getByCreatedBy,
    addLink,
    updateLink,
    deleteById,
};
export default externalLinkService;
