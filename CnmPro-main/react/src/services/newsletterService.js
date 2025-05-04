import axios from 'axios';
import { API_HOST_PREFIX } from './serviceHelpers';

const newsletterSubscription = {
    endpoint: `${API_HOST_PREFIX}/api/newsletter/subscriptions`,
};

const newsletterEmail = {
    endpoint: `${API_HOST_PREFIX}/api/emails/newsletter`,
};

const newsletterContent = {
    endpoint: `${API_HOST_PREFIX}/api/newsletter/content`,
};

let addSubscription = (payload) => {
    const config = {
        method: 'POST',
        data: payload,
        url: newsletterSubscription.endpoint,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then((response) => {
        return {
            response: response,
            email: payload.email,
        };
    });
};

let sendNewsletterConfirmation = (payload) => {
    const config = {
        method: 'POST',
        data: payload,
        url: newsletterEmail.endpoint,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config).then((response) => {
        return {
            response: response,
            email: payload.email,
        };
    });
};

let addContent = (payload) => {
    const config = {
        method: 'POST',
        data: payload,
        url: newsletterContent.endpoint,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};

let displayAllSubscribers = () => {
    const config = {
        method: 'GET',
        url: newsletterSubscription.endpoint,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};

let displayAllSubsciptionsByPage = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${newsletterSubscription.endpoint}/paginate/pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};

let displayAllConntentByPage = (pageIndex, pageSize) => {
    const config = {
        method: 'GET',
        url: `${newsletterContent.endpoint}/paginated/pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};

let updateSubscriptionStatus = (email, payload) => {
    const config = {
        method: 'PUT',
        url: `${newsletterSubscription.endpoint}/${email}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};

let updateNewsContent = (email, payload) => {
    const config = {
        method: 'PUT',
        url: `${newsletterSubscription.endpoint}/${email}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };

    return axios(config);
};

const newsletterService = {
    addSubscription,
    displayAllSubsciptionsByPage,
    updateSubscriptionStatus,
    displayAllSubscribers,
    addContent,
    updateNewsContent,
    displayAllConntentByPage,
    sendNewsletterConfirmation,
};

export default newsletterService;
