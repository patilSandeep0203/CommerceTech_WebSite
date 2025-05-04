import React, { useState } from 'react';
import FacebookLogin from 'react-facebook-login/dist/facebook-login-render-props';
import Logger from 'sabio-debug';
import UserService from '../../services/userService';
import { useNavigate } from 'react-router-dom';
import swal from '@sweetalert/with-react';
import Spinner from './Spinner.jsx';
import './facebook.css';

const _logger = Logger.extend('Login');

const appId = process.env.REACT_APP_FB_APP_ID;

function Facebook() {
    const navigate = useNavigate();
    const [login, setLogin] = useState(false);
    const [data, setData] = useState({});
    const [loading, setLoading] = useState(false);

    const responseFacebook = (response) => {
        _logger(response);
        setData(response);
        var payload = {
            email: 'test@email.com',
            token: 'abc-22222',
            tokentype: 3,
        };

        handleSubmit(payload);

        if (response.accessToken) {
            setLogin(true);
        } else {
            setLogin(false);
        }

        setLoading(false);
    };

    const handleSubmit = (values) => {
        UserService.facebookLogin(values).then(onSubmitSuccess).catch(onSubmitError);
    };
    const onSubmitSuccess = (response) => {
        const data = response.payload;
        const state = {
            type: 'CREDENTIAL_MATCH',
            payload: { data },
        };
        navigate('/login/verify', { state });
    };

    const onSubmitError = () => {
        swal({
            buttons: {
                cancel: 'Try again',
            },
            title: 'Login failed',
            icon: 'error',
        });
    };

    return (
        <div className="container">
            <div style={{ padding: '10px' }}>
                {' '}
                <FacebookLogin
                    appId={appId}
                    autoLoad={false}
                    fields="email"
                    scope="public_profile,user_friends"
                    callback={responseFacebook}
                    cssClass="facebook-login-button"
                    onClick={() => setLoading(true)}
                    data-auto-logout-link="true"
                    data-use-continue-as="false"
                    render={(renderProps) => (
                        <button className="facebook-login-button" {...renderProps}>
                            {loading ? (
                                <Spinner className="spinner-border-sm" tag="span" color="white" />
                            ) : (
                                'Login with Facebook'
                            )}
                        </button>
                    )}
                />
                {login}
                {data.name}
                {data.email}
            </div>
        </div>
    );
}

export default Facebook;
