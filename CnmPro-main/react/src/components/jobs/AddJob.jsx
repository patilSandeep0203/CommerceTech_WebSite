import React, { useState, useEffect } from 'react';
import Loki from 'react-loki';
import debug from 'sabio-debug';
import JobName from './JobName';
import JobContact from './JobContact';
import JobLocation from './JobLocation';
import JobFinish from './JobFinish';
import JobPreview from './JobPreview';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { BsFileEarmarkTextFill } from 'react-icons/bs';
import { FaClipboardCheck, FaPhoneAlt, FaPenAlt, FaMapMarkedAlt } from 'react-icons/fa';
import swal from '@sweetalert/with-react';
import * as jobServices from '../../services/jobsService';
import './jobs.css';
import PropTypes from 'prop-types';

const _logger = debug.extend('JobAdd');

function AddJob(props) {
    const { state } = useLocation();
    const coolState = useLocation();

    const [formData, setFormData] = useState({
        jobTypeId: '',
        title: '',
        description: '',
        requirements: '',
        organizationId: '',
        locationTypeId: '',
        lineOne: '',
        lineTwo: '',
        city: '',
        zip: '',
        stateId: '',
        latitude: '',
        longitude: '',
        contactName: '',
        contactPhone: '',
        contactEmail: '',
        isActive: true,
        createdBy: props.currentUser.id,
    });

    const [isUpdate, setIsUpdate] = useState(false);

    let { jobId } = useParams();

    useEffect(() => {
        _logger('State', state);
        _logger('coolstate ', coolState);
        if (state?.type === 'JOB_VIEW' && state?.payload) {
            _logger('State', state);
            const initialState = changeStateData(state.payload);
            setIsUpdate(true);
            setFormData({ ...initialState });
            _logger('jobIdToEdit', jobId);
        } else {
            _logger('Else Test');
        }
    }, [jobId, state]);

    const changeStateData = (payload) => {
        const newPayload = {
            jobTypeId: payload.jobType.id,
            title: payload.title,
            description: payload.description,
            requirements: payload.requirements,
            organizationId: payload.organization.id,
            locationTypeId: payload.location.locationType.id,
            lineOne: payload.location.lineOne,
            lineTwo: payload.location.lineTwo,
            city: payload.location.city,
            zip: payload.location.zip,
            stateId: payload.location.state.id,
            latitude: payload.location.latitude,
            longitude: payload.location.longitude,
            contactName: payload.contactName,
            contactPhone: payload.contactPhone,
            contactEmail: payload.contactEmail,
            isActive: payload.isActive,
            createdBy: payload.createdBy.id,
        };

        return newPayload;
    };

    const navigate = useNavigate();
    const onChange = (values) => {
        _logger('OnChange', values);
        setFormData((prevState) => {
            const sd = { ...prevState, ...values };
            return sd;
        });
    };

    const onFinish = () => {
        _logger('Finish button clicked', formData);
        if (isUpdate === true) {
            const newFormData = { ...formData };
            newFormData.locationId = state.payload.location.id;
            _logger('New Form Data', newFormData);
            jobServices.update(newFormData, jobId).then(onUpdateJobSuccess).catch(onUpdateJobError);
        } else {
            jobServices.add(formData).then(onAddJobSuccess).catch(onAddJobError);
        }
    };

    const onUpdateJobSuccess = (response) => {
        _logger('Successfuly updated', response);
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your job has successfully been update!',
            showConfirmButton: false,
            timer: 5000,
        });
        navigate(`/admin/organization/dashboard/jobs/${state.payload.organization.id}`);
    };

    const onUpdateJobError = (response) => {
        _logger('Couldnt Update Job', response);
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Please check the missing fields',
            icon: 'error',
        });
    };

    const onAddJobSuccess = (response) => {
        _logger('Successfuly submitted', response);
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your job has successfully been created!',
            showConfirmButton: false,
            timer: 5000,
        });
        navigate('/admin/organization/dashboard');
    };

    const onAddJobError = (error) => {
        _logger(error, 'Could Not Add Job');
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Please check the missing fields',
            icon: 'error',
        });
    };

    const wizardSteps = [
        {
            label: 'Step 1',
            icon: <FaPenAlt className="mt-2 text-center" />,
            component: <JobName isUpdate={isUpdate} formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 2',
            icon: <FaMapMarkedAlt className="mt-2 text-center" />,
            component: <JobLocation formData={formData} onChange={onChange} isUpdate={isUpdate} />,
        },
        {
            label: 'Step 3',
            icon: <FaPhoneAlt className="mt-2 text-center" />,
            component: <JobContact formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 4',
            icon: <BsFileEarmarkTextFill className="mt-2 text-center" />,
            component: <JobPreview formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 5',
            icon: <FaClipboardCheck className="mt-2" />,
            component: <JobFinish formData={formData} onChange={onChange} />,
        },
    ];

    return (
        <React.Fragment>
            <div className="jobWizard">
                <Loki steps={wizardSteps} onNext={onChange} onBack={onChange} onFinish={onFinish} noActions />
            </div>
        </React.Fragment>
    );
}

AddJob.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
        roles: PropTypes.arrayOf(PropTypes.string).isRequired,
    }),
};

export default AddJob;
