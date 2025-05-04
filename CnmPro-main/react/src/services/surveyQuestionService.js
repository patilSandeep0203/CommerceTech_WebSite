import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from '../services/serviceHelpers';
const endpoint = `${API_HOST_PREFIX}/api/surveys/questions`;

const createQuestion = (payload) => {
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

const createQuestionWithOptions = (payload) => {
    const config = {
        method: 'POST',
        url: `${endpoint}/questionwithoptions`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateQuestion = (payload) => {
    const config = {
        method: 'PUT',
        url: `${endpoint}/${payload.id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const surveyQuestionService = {
    createQuestion,
    createQuestionWithOptions,
    updateQuestion,
};

export default surveyQuestionService;
