import React, { useState } from 'react';
import FacebookLogin from 'react-facebook-login';
import Logger from 'sabio-debug';
import './login.scss'

const _logger = Logger.extend('Login');

const appId = process.env.REACT_APP_FB_APP_ID;

function Facebook() {
    const [login, setLogin] = useState(false);
    const [data, setData] = useState({});

    const responseFacebook = (response) => {
        _logger(response);
        setData(response);

        if (response.accessToken) {
            setLogin(true);
        } else {
            setLogin(false);
        }
    };
    
    return (
        <div className="container">
            <div style={{ padding: '10px' }}>
                {' '}
                <FacebookLogin
                    appId= {appId}
                    autoLoad={false}
                    fields="email"
                    scope="public_profile,user_friends"
                    callback={responseFacebook}
                    cssClass="facebook-login-button"
                />
                {login}
                {data.name}
                {data.email}
            </div>
        </div>
    );
}

export default Facebook;
