import React, { useEffect } from 'react';

const Events = React.lazy(() => import('./Events'));

const EventsPublic = () => {
    useEffect(() => {
        if (document.body) document.body.classList.remove('authentication-bg');
    }, []);

    return (
        <>
            <div className="wrapper">
                <div className="content-page">
                    <div className="content">
                        <Events />
                    </div>
                </div>
            </div>
        </>
    );
};

export default EventsPublic;
