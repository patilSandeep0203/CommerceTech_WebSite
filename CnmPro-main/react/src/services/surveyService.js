import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from '../services/serviceHelpers';

let userUrl = `${API_HOST_PREFIX}/api/surveys`;

let getSurveyDetails = (surveyId) => {
    const config = {
        method: 'GET',
        url: `${userUrl}/details/${surveyId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

let getAllSurveys = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${userUrl}/paginate?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

let searchSurveysPaginated = (pageIndex, pageSize, query) => {
    const config = {
        method: 'GET',
        url: `${userUrl}/search?pageIndex=${pageIndex}&pageSize=${pageSize}&query=${query}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

let submitSurvey = (payload) => {
    const config = {
        method: 'POST',
        url: `${userUrl}/submit`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const surveyService = {
    getSurveyDetails,
    submitSurvey,
    getAllSurveys,
    searchSurveysPaginated,
};

export default surveyService;
