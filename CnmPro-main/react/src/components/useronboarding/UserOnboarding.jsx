import React, { useState, useEffect } from 'react';
import debug from 'sabio-debug';
import Loki from 'react-loki';
import { FaUserCircle } from 'react-icons/fa';
import { ImLocation } from 'react-icons/im';
import { CgPlayListCheck } from 'react-icons/cg';
import { TbLicense } from 'react-icons/tb';
import UserBasicInformation from './UserBasicInformation';
import UserLicenseInformation from './UserLicenseInformation';
import UserLocation from './UserLocation';
import ReviewAndSubmit from './ReviewAndSubmit';
import '../../components/useronboarding/userOnboarding.css';
import userProfilesService from '../../services/userProfilesService';
import { useNavigate } from 'react-router-dom';
import swal from '@sweetalert/with-react';
import toastr from 'toastr';
import lookUpService from '../../services/lookUpService';
import { useLocation } from 'react-router-dom';

const _logger = debug.extend('Onboarding');

function UserOnboarding() {
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        mi: '',
        avatarUrl: '',
        dob: '',
        email: '',
        phone: '',
        license: [],
        yearsOfExperience: '',
        desiredHourlyRate: '',
        profession: [],
        isActive: true,
        locationTypeId: '',
        lineOne: '',
        lineTwo: '',
        city: '',
        zip: '',
        stateId: '',
        latitude: '',
        longitude: '',
    });

    const [lookUpResults, setLookUpResults] = useState({
        states: [],
        locationTypes: [],
        licenseTypes: [],
        professions: [],
    });

    const navigate = useNavigate();

    useEffect(() => {
        lookUpService
            .LookUp(['States', 'LicenseTypes', 'LocationTypes', 'ProfessionTypes'])
            .then(onSuccessLookUp)
            .catch(onErrorLookup);
    }, []);

    const onSuccessLookUp = (response) => {
        if (response.item.states || response.item.locationTypes || response.item.licenseTypes) {
            setLookUpResults((prevState) => {
                let result = { ...prevState };
                result.states = response.item.states;
                result.licenseTypes = response.item.licenseTypes;
                result.locationTypes = response.item.locationTypes;
                result.professions = response.item.professionTypes;
                return result;
            });
        }
    };

    const onErrorLookup = (err) => {
        _logger('Get Lookup error', err);
        toastr.error('Error getting lookup data.');
    };

    const onChange = (values) => {
        setFormData((prevState) => {
            const sd = { ...prevState, ...values };
            return sd;
        });
    };
    const location = useLocation();
    const userProfileState = location.state;
    _logger(userProfileState);

    const onFinish = () => {
        _logger('Finish button clicked', formData);
        if (userProfileState?.type === 'USERPROFILE_VIEW') {
            userProfilesService
                .updateUserProfileV2({ ...formData, id: userProfileState.payload.id }, userProfileState.payload.id)
                .then(userProfileSuccess)
                .catch(userProfileError);
        } else {
            userProfilesService.addUserProfileV2(formData).then(userProfileSuccess).catch(userProfileError);
        }
    };

    const userProfileSuccess = (response) => {
        _logger(response, 'User profile added successfully');
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your profile has successfully been updated!',
            showConfirmButton: false,
            timer: 5000,
        });
        navigate('/dash/user');
    };

    const userProfileError = (response) => {
        _logger(response, 'User profile add failed');
    };

    const onBoadingSteps = [
        {
            label: 'Step 1',
            icon: <FaUserCircle className="mt-2 text-center" />,
            component: <UserBasicInformation formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 2',
            icon: <TbLicense className="mt-2 text-center" />,
            component: <UserLicenseInformation formData={formData} onChange={onChange} lookUpResults={lookUpResults} />,
        },
        {
            label: 'Step 3',
            icon: <ImLocation className="mt-2 text-center" />,
            component: <UserLocation formData={formData} onChange={onChange} lookUpResults={lookUpResults} />,
        },
        {
            label: 'Step 4',
            icon: <CgPlayListCheck className="mt-2" />,
            component: <ReviewAndSubmit formData={formData} onChange={onChange} lookUpResults={lookUpResults} />,
        },
    ];

    return (
        <React.Fragment>
            <div className="userOnboardingWizard">
                <Loki steps={onBoadingSteps} onFinish={onFinish} onBack={onChange} onNext={onChange} noActions />
            </div>
        </React.Fragment>
    );
}

export default UserOnboarding;
