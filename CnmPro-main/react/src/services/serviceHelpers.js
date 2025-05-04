import axios from 'axios';
import logger from 'sabio-debug';
const _xhrLogger = logger.extend('xhr');
axios.defaults.withCredentials = true;
// Add a request interceptor
axios.interceptors.request.use(function (config) {
    config.withCredentials = true;
    _xhrLogger(config);
    return config;
});
/**
 * Will unpack the response body from reponse object
 * @param {*} response
 *
 */
const onGlobalSuccess = (response) => {
    /// Should not use if you need access to anything other than the data
    return response.data;
};

const onGlobalError = (err) => {
    return Promise.reject(err);
};

const API_HOST_PREFIX = process.env.REACT_APP_API_HOST_PREFIX;
const API_NODE_HOST_PREFIX = process.env.REACT_APP_API_NODE_HOST_PREFIX;
const STRIPE_PUBLIC_KEY = process.env.REACT_APP_STRIPE_API_PUBLIC_KEY;

logger({ API_HOST_PREFIX, API_NODE_HOST_PREFIX, STRIPE_PUBLIC_KEY });

export { onGlobalError, onGlobalSuccess, API_HOST_PREFIX, API_NODE_HOST_PREFIX, STRIPE_PUBLIC_KEY };
