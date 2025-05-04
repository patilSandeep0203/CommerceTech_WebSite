import { React, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import logger from 'sabio-debug';
import './daily.css';

const _logger = logger.extend('call-options');

function CallOptions() {
    const navigate = useNavigate();
    const [url, setUrl] = useState('');

    const toWebinar = () => {
        navigate('/webinar', {
            state: {
                url: url,
            },
        });
    };

    const handleSumbit = (e) => {
        e.preventDefault();

        _logger('url  b4->', url);

        toWebinar();
    };

    return (
        <div>
            <div className="webinarSetUpHeader">
                <h1> Daily Webinar Setup </h1>
            </div>

            <p>
                Create you a new room or use an already existing room from Daily.co and submit your URL below to share
                your webinar meeting!{' '}
                <a href="https://dashboard.daily.co/rooms" target="blank">
                    Click Here for Daily.co!
                </a>
            </p>
            <p>Current meeting URL: {url}</p>

            <form onSubmit={handleSumbit}>
                <span className="input-group-text" id="inputGroup-sizing-default">
                    Paste Your Meeting URL Here:
                </span>
                <input
                    type="text"
                    className="form-control"
                    aria-label="Sizing example input"
                    aria-describedby="inputGroup-sizing-default"
                    placeholder="ex: https://[DOMAIN].daily.co/[ROOM-NAME]"
                    onChange={(e) => setUrl(e.target.value)}
                    defaultValue={url}></input>
                <button className="btn btn-primary" type="submit" onClick={handleSumbit}>
                    Submit URL
                </button>
            </form>
        </div>
    );
}
export default CallOptions;
