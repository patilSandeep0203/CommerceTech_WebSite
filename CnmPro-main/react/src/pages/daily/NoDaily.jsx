import React from 'react';
import { Link } from 'react-router-dom';
import './daily.css';

function NoDaily() {
    return (
        <div>
            <div className="noSetUpHeader">
                <h2>A webinar room was not setup.</h2>
            </div>
            <div className="noSetUpLink">
                <Link to="/webinarsetup">Click here to set up the webinar room.</Link>
            </div>
        </div>
    );
}

export default NoDaily;
