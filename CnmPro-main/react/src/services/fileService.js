import axios from 'axios';
import { onGlobalError, onGlobalSuccess } from './serviceHelpers';
import * as serviceHelpers from './serviceHelpers';
var fileService = {
    endpoint: `${serviceHelpers.API_HOST_PREFIX}/api/files`,
};

fileService.fileUpload = (payload) => {
    const config = {
        method: 'POST',
        url: `${fileService.endpoint}`,
        data: payload,
        crossdomain: true,
        headers: { 'Content-Type': 'multipart/form-data' },
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

export default fileService;
