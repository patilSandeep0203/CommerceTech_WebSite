import React, { useState, useEffect } from 'react';
import AccountLayout from '../AccountLayout';
import UserService from '../../../services/userService';
import { useLocation, useNavigate } from 'react-router-dom';
import swal from '@sweetalert/with-react';
import debug from 'sabio-debug';
import * as organizationService from '../../../services/organizationsService';
import toastr from 'toastr';

const _logger = debug.extend('VerifyToken');

export default function Verify() {
    const { state } = useLocation();
    const navigate = useNavigate();

    const [loginData, setLoginData] = useState({});
    const [authCode, setAuthCode] = useState({ token: '' });

    const handleSubmit = (e) => {
        e.preventDefault();
        const payload = {
            Email: loginData.Email,
            Password: loginData.Password,
            PhoneNumber: loginData.PhoneNumber,
            Token: authCode.token,
        };
        _logger(payload, 'test');
        UserService.verifyAuthenticationToken(payload)
            .then(onVerifyAuthenticationTokenSuccess)
            .catch(onVerifyAuthenticationTokenError);
    };

    const onVerifyAuthenticationTokenSuccess = () => {
        UserService.currentV2().then(onGetCurrentSuccess).catch(onGetCurrentError);
    };

    const onVerifyAuthenticationTokenError = () => {
        swal({
            buttons: {
                cancel: 'Try again',
            },
            title: 'Login failed',
            icon: 'error',
        });
    };

    const onGetCurrentSuccess = (response) => {
        _logger(response, 'onGetCurrentSuccess');
        const state = {
            type: 'LOGIN_SUCCESS',
            payload: response.item,
        };

        let userRoles = response.item.roles;
        let userId = response.item.id;

        if (userRoles.includes('Admin')) {
            navigate('/dashboard', { state });
        } else if (userRoles.includes('Org Admin')) {
            // navigate('/admin/organization/dashboard', { state });

            organizationService
                .GetOrganizationByCreatedBy(userId)
                .then((res) => onGetOrgSuccess(res, state))
                .catch(onGetOrgError);
        } else {
            if (state.payload.hasProfile) {
                navigate('/dash/user', { state });
            } else {
                navigate('/onboarding', { state });
            }
        }
    };

    const onGetOrgSuccess = (response, state) => {
        _logger('Get Org Success', response);
        _logger('Get Org Success', state);
        if (response.data.item === null) {
            navigate('/orgadmin/initialsetup', { state });
        } else {
            navigate('/admin/organization/dashboard', { state });
        }
    };

    const onGetOrgError = (error) => {
        _logger('Get Org Error', error.response);
        toastr.error('Status Error', error.response.status);
    };

    const onGetCurrentError = (error) => {
        _logger(error);
    };

    const onFormChange = (e) => {
        const target = e.target;
        const value = target.value;
        const name = target.name;

        setAuthCode((prev) => {
            const pd = { ...prev };
            pd[name] = value;
            return pd;
        });
    };

    useEffect(() => {
        if (state && state.type === 'VERIFY_TOKEN') {
            _logger(state.payload.data);
            setLoginData(state.payload.data);
        }
    }, []);

    return (
        <AccountLayout>
            <div className="d-flex flex-column text-center justify-content-center">
                <h2>Two-Factor Authentication</h2>
                <p>
                    We&apos;ve sent verification code to: <strong>{loginData.PhoneNumber}</strong>{' '}
                </p>
            </div>
            <form onSubmit={handleSubmit}>
                <div className="form-group m-2">
                    <div className="form-group">
                        <label>Code:</label> <br />
                        <input className="form-control" name="token" onChange={onFormChange} required type="number" />
                    </div>
                    <div className="d-flex justify-content-center pt-3">
                        <button type="submit" className="btn btn-primary">
                            Submit
                        </button>
                    </div>
                </div>
            </form>
        </AccountLayout>
    );
}
