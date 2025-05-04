import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from './serviceHelpers';

const endpoint = 'api/user';

const add = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/${endpoint}`,
        data: payload,
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

const login = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/${endpoint}/login`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then((response) => {
        return { response: response.data, payload: payload };
    });
};

const facebookRegister = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/${endpoint}/facebook/register`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
     return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};
const facebookLogin = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/${endpoint}/facebook/login`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then((response) => {
        return { response: response.data, payload: payload };
    });
};

const emailVerification = (payload) => {
    const config = {
        method: 'PUT',
        url: `${API_HOST_PREFIX}/${endpoint}/confirm/${payload}`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const current = () => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/${endpoint}/current`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const currentV2 = () => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/${endpoint}/currentv2`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const logout = () => {
    const config = {
        method: 'GET',
        url: `${API_HOST_PREFIX}/${endpoint}/logout`,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config);
};

const forgotPassword = (payload) => {
    const config = {
        method: 'PUT',
        url: `${API_HOST_PREFIX}/${endpoint}/forgotpassword`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const changePassword = (payload) => {
    const config = {
        method: 'PUT',
        url: `${API_HOST_PREFIX}/${endpoint}/changepassword`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const sendAuthenticationToken = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/${endpoint}/login/verify`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then((response) => {
        return {
            response: response,
            phoneNumber: payload,
        };
    });
};

const verifyAuthenticationToken = (payload) => {
    const config = {
        method: 'POST',
        url: `${API_HOST_PREFIX}/${endpoint}/login/verify/auth`,
        data: payload,
        withCredentials: true,
        crossdomain: true,
        headers: { 'Content-Type': 'application/json' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const UserService = {
    add,
    facebookRegister,
    login,
    facebookLogin,
    emailVerification,
    current,
    currentV2,
    logout,
    forgotPassword,
    changePassword,
    sendAuthenticationToken,
    verifyAuthenticationToken,
};

export default UserService;
