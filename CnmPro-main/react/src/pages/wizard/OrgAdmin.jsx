import React, { useState, useEffect } from 'react';
import Loki from 'react-loki';
import debug from 'sabio-debug';
import OrgAdminBasicInfo from './OrgAdminBasicInfo';
import OrgAdminLocation from './OrgAdminLocation';
import OrgAdminJobPosting from './OrgAdminJobPosting';
import OrgAdminEventPosting from './OrgAdminEventPosting';
import OrgAdminSubmit from './OrgAdminSubmit';
import './orgadmin.css';
import { FaBuilding, FaUserPlus, FaAddressBook, FaCheckSquare } from 'react-icons/fa';
import * as orgAdminServices from '../../services/orgAdminService';
import Swal from 'sweetalert2';
import 'sweetalert2/src/sweetalert2.scss';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { Formik } from 'formik';

const _logger = debug.extend('OrgAdmin');

function OrgAdmin() {
    const { state } = useLocation();
    const { orgId } = useParams();
    const [formData, setFormData] = useState({
        id: 0,
        name: '',
        description: '',
        siteUrl: '',
        organizationTypeId: 0,
        locationTypeId: 0,
        locationId: 0,
        lineOne: '',
        lineTwo: '',
        city: '',
        stateId: 0,
        zip: '',
        phone: '',
        logo: '',
        headline: '',
        jobTypeId: 0,
        jobTitle: '',
        email: '',
        requirements: '',
    });

    const onChange = (values) => {
        if (values.eventPhase !== 3 || values.eventPhase !== 4) {
            _logger('values ->', values);
            setFormData((prevState) => {
                return {
                    ...prevState,
                    ...values,
                };
            });
        }
    };

    useEffect(() => {
        if (state?.type === 'ORG_EDIT' && state?.payload) {
            _logger('State in OrgAdmin', state);
            const dataForForm = changeDataForForm(state.payload);
            setFormData(dataForForm);
        }
    }, [state]);

    const changeDataForForm = (payload) => {
        const dataForForm = {
            id: payload.id,
            name: payload.name,
            description: payload.description,
            siteUrl: payload.siteUrl,
            organizationTypeId: payload.organizationType.id,
            locationTypeId: payload.location.locationType.id,
            locationId: payload.location.id,
            lineOne: payload.location.lineOne,
            lineTwo: payload.location.lineTwo,
            city: payload.location.city,
            stateId: payload.location.state.id,
            zip: payload.location.zip,
            phone: payload.phone,
            logo: payload.logo,
            headline: payload.headline,
        };
        return dataForForm;
    };

    const onFinish = () => {
        _logger('Finish button clicked', formData);
        if (formData.id !== 0) {
            _logger('Update Performed');
            orgAdminServices.updateOrgAdmin(formData, orgId).then(onAddSuccess).catch(onAddError);
        } else {
            _logger('Add Performed');
            orgAdminServices.addOrgAdmin(formData).then(onAddSuccess).catch(onAddError);
        }
    };
    const onAddSuccess = () => {
        Swal.fire('Success', 'Organization Added Successfully!', 'success').then(goToOrgAdminDashboard);
    };

    const navigate = useNavigate();
    const goToOrgAdminDashboard = () => {
        navigate(`/admin/organization/dashboard`);
    };

    const onAddError = () => {
        Swal.fire('Error', 'Could Not Add , Please Try Again', 'error');
    };

    const wizardSteps = [
        {
            label: 'Step 1',
            icon: <FaBuilding className="mt-2 text-center" />,
            component: <OrgAdminBasicInfo formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 2',
            icon: <FaAddressBook className="mt-2 text-center" />,
            component: <OrgAdminLocation formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 3',
            icon: <FaUserPlus className="mt-2 text-center" />,
            component: <OrgAdminJobPosting formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 4',
            icon: <FaUserPlus className="mt-2 text-center" />,
            component: <OrgAdminEventPosting formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 5 ',
            icon: <FaCheckSquare className="mt-2 text-center" />,
            component: <OrgAdminSubmit formData={formData} onChange={onChange} />,
        },
    ];

    const wizardStepsForEdit = [
        {
            label: 'Step 1',
            icon: <FaBuilding className="mt-2 text-center" />,
            component: <OrgAdminBasicInfo formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 2',
            icon: <FaAddressBook className="mt-2 text-center" />,
            component: <OrgAdminLocation formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 4 ',
            icon: <FaCheckSquare className="mt-2 text-center" />,
            component: <OrgAdminSubmit formData={formData} onChange={onChange} />,
        },
    ];

    return (
        <React.Fragment>
            <Formik initialValues={formData} enableReinitialize>
                {(props) => (
                    <div className="OrgAdminWizard">
                        <Loki
                            steps={orgId ? wizardStepsForEdit : wizardSteps}
                            onNext={onChange}
                            onBack={onChange}
                            onFinish={onFinish}
                            noActions
                            {...props}
                        />
                    </div>
                )}
            </Formik>
        </React.Fragment>
    );
}

export default OrgAdmin;
