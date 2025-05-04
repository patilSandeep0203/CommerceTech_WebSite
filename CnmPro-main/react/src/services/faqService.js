import axios from 'axios';
import * as helper from './serviceHelpers';

const faqUrl = `${helper.API_HOST_PREFIX}/api/faq/`;

const addFaq = (payload) => {
    const config = {
        method: 'POST',
        url: `${faqUrl}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

let getFaq = () => {
    const config = {
        method: 'GET',
        url: `${faqUrl}paginate?pageIndex=0&pageSize=100`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

let deleteFaq = (id) => {
    const config = {
        method: 'DELETE',
        url: `${faqUrl}${id}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

let updateFaq = (payload, id) => {
    const config = {
        method: 'PUT',
        url: `${faqUrl}${id}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then(helper.onGlobalSuccess).catch(helper.onGlobalError);
};

const UserService = { addFaq, updateFaq, deleteFaq, getFaq };

export default UserService;
