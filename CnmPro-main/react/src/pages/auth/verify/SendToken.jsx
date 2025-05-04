import React, { useEffect, useState } from 'react';
import AccountLayout from '../AccountLayout';
import Logger from 'sabio-debug';
import { useLocation, useNavigate } from 'react-router-dom';
import UserService from '../../../services/userService';
import MaskedInput from 'react-text-mask';
import swal from '@sweetalert/with-react';

const _logger = Logger.extend('Verify Page');

export default function Verify() {
    const navigate = useNavigate();
    const { state } = useLocation();
    const [formData, setFormData] = useState({
        phoneNumber: '',
    });

    useEffect(() => {
        if (state && state.type === 'CREDENTIAL_MATCH') {
            setFormData((prev) => {
                let pd = { ...prev };
                pd.phoneNumber = '';
                pd.email = state.payload.data.email;
                pd.password = state.payload.data.password;
                return pd;
            });
        }
    }, []);

    const onFormChange = (e) => {
        const target = e.target;
        const value = target.value;
        const name = target.name;

        setFormData((prev) => {
            const pd = { ...prev };
            pd[name] = value;
            return pd;
        })
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        _logger(formData, '<--- form data')
        UserService.sendAuthenticationToken(formData)
            .then(onSendAuthenticationTokenSuccess)
            .catch(onSendAuthenticationTokenError);
    };

    const onSendAuthenticationTokenError = () => {
        swal({
            buttons: {
                cancel: 'Try again',
            },
            title: 'Please enter a valid phone number.',
            icon: 'error',
        });
    };

    const onSendAuthenticationTokenSuccess = () => {
        const data = {
            PhoneNumber: formData.phoneNumber,
            Email: formData.email,
            Password: formData.password,
        };
        const state = {
            type: 'VERIFY_TOKEN',
            payload: { data },
        };
        navigate('/login/verify/auth', { state });
    };

    return (
        <AccountLayout>
            <div className="d-flex flex-column text-center justify-content-center">
                <h2>Two-Factor Authentication</h2>
                <p>Enter the mobile phone number for a device you trust. We will text you a 6-digit security code to verify your phone.</p>
            </div>
            <form onSubmit={handleSubmit}>
                <div className="form-group m-2">
                    <div className="form-group">
                        <label>US Phone Number</label> <br />
                        <MaskedInput
                            mask={[
                                '(',
                                /[1-9]/,
                                /\d/,
                                /\d/,
                                ')',
                                ' ',
                                /\d/,
                                /\d/,
                                /\d/,
                                '-',
                                /\d/,
                                /\d/,
                                /\d/,
                                /\d/,
                            ]}
                            placeholder="(___) ___-____"
                            className="form-control"
                            name="phoneNumber"
                            required
                            onChange={onFormChange}
                        />
                    </div>
                </div>
                <div className="d-flex justify-content-center pt-2">
                    <button type="submit" className="btn btn-primary">Submit</button>
                </div>
            </form>
        </AccountLayout>
    );
}
