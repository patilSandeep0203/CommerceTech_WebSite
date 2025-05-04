import axios from 'axios';
import { API_HOST_PREFIX, onGlobalError, onGlobalSuccess } from './serviceHelpers';

  const contactEmail = (payload) => {
    const config = {
      method: 'POST',
      url: `${API_HOST_PREFIX}/api/contact`,
      data: payload,
      withCredentials: true,
      crossdomain: true,
      headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError)
      };


export { contactEmail };


