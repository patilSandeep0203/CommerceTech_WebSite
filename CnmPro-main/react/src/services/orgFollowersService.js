import axios from "axios";
import {API_HOST_PREFIX, onGlobalError, onGlobalSuccess} from "./serviceHelpers"


const orgfollowersApi = {
    endpoint: `${API_HOST_PREFIX}/api/followers`
};

const getOrgByUserId = (pageIndex, pageSize) => {
    const config = {
      method: "GET",
      url: `${orgfollowersApi.endpoint}/current?pageIndex=${pageIndex}&pageSize=${pageSize}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json"}
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
  };

  const getByOrganizationId = (organizationId, pageIndex, pageSize) => {
    const config = {
      method: "GET",
      url: `${orgfollowersApi.endpoint}"/organization/"${organizationId}"?pageIndex="${pageIndex}"&pageSize="${pageSize}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json"}
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError)
};

const addOrgFol = (OrgId) => {

    const config = {
      method: "POST",
      url: `${orgfollowersApi.endpoint}?orgId=${OrgId}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json"}
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const deleteOrgFol = (organizationId) => {
    const config = {
      method: "DELETE",
      url: `${orgfollowersApi.endpoint}/${organizationId}`,
      withCredentials: true,
      crossdomain: true,
      headers: { "Content-Type": "application/json"}
    };
    return axios(config).then(onGlobalSuccess).catch(onGlobalError);
  };
 const functions = { getOrgByUserId, getByOrganizationId, addOrgFol, deleteOrgFol};

export default functions
