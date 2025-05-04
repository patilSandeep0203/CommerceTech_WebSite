import axios from 'axios';
import * as serviceHelper from './serviceHelpers.js';
import { onGlobalSuccess, onGlobalError } from './serviceHelpers.js';


const stripeServices = {
    subEndpoint: `${serviceHelper.API_HOST_PREFIX}/api/subscription`,
    hookEndpoint: `${serviceHelper.API_HOST_PREFIX}/api/stripe/management`
}

const getProducts = () => {
    const config = {
        method: "GET",
        url: `${stripeServices.subEndpoint}/products`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const createCustomerSession = (userId, payload) => {
    const config = {
        method: "POST",
        url: `${stripeServices.subEndpoint}/customers?userId=${userId}`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const createCheckoutSession = (priceId) => {
    const config = {
        method: "POST",
        url: `${stripeServices.subEndpoint}/checkout?priceId=${priceId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getPriceAmt = (priceId) => {
    const config = {
        method: "POST",
        url: `${stripeServices.subEndpoint}/products?priceId=${priceId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getCheckoutSession = () => {
    const config = {
        method: "GET",
        url: `${stripeServices.subEndpoint}/products`,

        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getCustomer = (userId) => {
    const config = {
        method: "POST",
        url: `${stripeServices.subEndpoint}/customers?userId=${userId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const customerPortalSession = (customerId) => {
    const config = {
        method: "POST",
        url: `${stripeServices.subEndpoint}/customer/portal?customerId=${customerId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const webhookEndpoint = () => {
    const config = {
        method: "POST",
        url: `${stripeServices.hookEndpoint}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getEvents = () => {
    const config = {
        method: "GET",
        url: `${stripeServices.subEndpoint}/events`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getInvoice = (sessionId) => {
    const config = {
        method: "GET",
        url: `${stripeServices.subEndpoint}/order/success?session_id=${sessionId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const invoiceToDb = (invoiceId, userId, invoice) => {
    const config = {
        method: "POST",
        url: `${stripeServices.subEndpoint}/invoice?invoiceId=${invoiceId}&&userId=${userId}`,
        payload: invoice,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const subscriptionCounts = (pageIndex, pageSize) => {
    const config = {
        method: "GET",
        url: `${stripeServices.subEndpoint}/admin/status/active?pageIndex=${pageIndex}&pageSize=${pageSize}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const getSubStatusById = (userId) => {
    const config = {
        method: "GET",
        url: `${stripeServices.subEndpoint}/admin/status/${userId}`,
        withCredentials: true,
        crossdomain: true,
        headers: { "Content-Type": "application/json" }
    };

    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

export { getProducts, getSubStatusById, createCheckoutSession, getInvoice, getCheckoutSession, getEvents, getCustomer, createCustomerSession, customerPortalSession, getPriceAmt, webhookEndpoint, invoiceToDb, subscriptionCounts };
