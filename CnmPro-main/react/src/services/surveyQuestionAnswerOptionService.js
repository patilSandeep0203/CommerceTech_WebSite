import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from './serviceHelpers';

const endpoint = `${API_HOST_PREFIX}/api/surveys/qansweroptions`;

const addQAnswerOption = (payload) => {
    const config = {
        method: 'POST',
        url: endpoint,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateQAnswerOption = (payload) => {
    const config = {
        method: 'PUT',
        url: endpoint,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const deleteQAnswerOption = (qAnswerOptionId) => {
    const config = {
        method: 'DELETE',
        url: `${endpoint}/${qAnswerOptionId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const QAnswerOptionService = { addQAnswerOption, updateQAnswerOption, deleteQAnswerOption };

export default QAnswerOptionService;
