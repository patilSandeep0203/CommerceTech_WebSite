import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from '../services/serviceHelpers';
const endpoint = `${API_HOST_PREFIX}/api/surveys`;

const createSurvey = (payload) => {
    const config = {
        method: 'POST',
        data: payload,
        url: endpoint,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const surveyCreateService = {
    createSurvey,
};

export default surveyCreateService;
