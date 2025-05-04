import './organizations.css';
import { Row } from 'react-bootstrap';
import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import debug from 'sabio-debug';
import * as organizationService from "../../services/organizationsService"
import Organization from "../organizations/Organization"
import Pagination from "rc-pagination";
import locale from 'rc-pagination/lib/locale/en_US';
import "rc-pagination/assets/index.css";
import swal from '@sweetalert/with-react';

const _logger = debug.extend('Organizations');

function Organizations() {
    const navigate = useNavigate();
    const goToForm = () =>
    {
      navigate(`/organization/new`);
    }
    const [organizationData, setOrganizationData] = useState({
        arrayOfOrganizations: [],
        organizationComponents: [],
        page:{ 
        pageIndex: 0,
        pageSize: 4, 
        totalCount: 0,
        totalPages: 0 },
    })

    false && _logger(organizationData.arrayOfOrganizations);

    const onDeleteOrganization = useCallback((aOrganization, eObj) => {
        _logger(aOrganization.id, { aOrganization, eObj });

        const handler = getDeleteSuccessHandler(aOrganization.id);
        organizationService.DeleteOrganization(aOrganization.id).then(handler).catch(onDeleteError);
    }, []);

    const getDeleteSuccessHandler = (orgToBeDeleted) => {
        _logger("getDeleteSuccessHandler", orgToBeDeleted);
        return () =>{
        
        setOrganizationData((prevState)=>{
        const oData = {...prevState};
        oData.arrayOfOrganizations = [...oData.arrayOfOrganizations]
            
            const idxOf = oData.arrayOfOrganizations.findIndex((organization)=>{
                let result = false;
                if(organization.id === orgToBeDeleted){
                    result = true;
                }
                return result;
            })
            if(idxOf >= 0){
                oData.arrayOfOrganizations.splice(idxOf,1)
                oData.organizationComponents = oData.arrayOfOrganizations.map(mapOrganization)
            }
            return oData
        });
    }
}

const mapOrganization = (aOrganization) => {
    return (<Organization organization={aOrganization}
    key={aOrganization.id}
    onOrgClicked={onDeleteOrganization}
    />
    )
}

useEffect(()=> {
    _logger("Firing useEffect for get Organizations")
    organizationService.GetAllOrganizations(0, 4).then(onGetOrganizationSuccess).catch(onGetOrganizationError)
}, [])  

    const onGetOrganizationSuccess = (response) => {
        _logger(response);
        let arrayOfOrgs = response.data.item.pagedItems;
        _logger(arrayOfOrgs);

    setOrganizationData((prevState) => {
        let pd = { ...prevState };
        pd.arrayOfOrganizations = arrayOfOrgs;
        pd.organizationComponents = arrayOfOrgs.map(mapOrganization);
        pd.page.pageIndex = response.data.item.pageIndex;
        pd.page.pageSize = response.data.item.pageSize;
        pd.page.totalCount = response.data.item.totalCount;
        pd.page.totalPages = response.data.item.totalPages;
        _logger("RESPONSE HERE", response)
        return pd;
    });
}

    const onGetOrganizationError = (err) => {
        _logger('Get Organization error', err);
    };

const onDeleteError = (err) => {
    _logger("Delete error", err)
    swal ({
        buttons: {
        cancel: 'Ok',
        },
        title: 'You do not have authorization to delete this Organization',
        icon: 'error'
    });
}

const onChange = (currentPage) => {
    _logger("CURRENT PAGE", currentPage)
    
    organizationService
    .GetAllOrganizations(currentPage - 1, organizationData.page.pageSize)
    .then(onGetOrganizationSuccess)
    .catch(onGetOrganizationError)
}

    return  (
        <div className="container">
            <div className="row">
                <div className="container">
                <h1 className='card-header-orgs-cnmpro'>Organizations</h1>
            <button 
                type="button" 
                id='neworganization'
                className="btn btn-primary buttonpress-organization-cnmpro"
                onClick={goToForm}
            >
                Create an Organization
            </button>
            </div>
        </div>
          
    <div className="container pagedScroll-org-cnmpro">
        <Pagination 
        onChange={onChange}
        current={organizationData.page.pageIndex + 1}
        total={organizationData.page.totalCount}
        locale={locale}
        pageSize={organizationData.page.pageSize}        
        />
    </div>

        <Row className="row">
                {organizationData.organizationComponents}  
        </Row>
        <div className="container pagedScroll-bottom-org-cnmpro">
        <Pagination 
        onChange={onChange}
        current={organizationData.page.pageIndex + 1}
        total={organizationData.page.totalCount}
        locale={locale}
        pageSize={organizationData.page.pageSize}       
        />
        </div>
    </div>
    );
}

export default Organizations;
