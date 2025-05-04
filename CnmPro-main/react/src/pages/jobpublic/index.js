import React, { useEffect } from 'react';

const Jobs = React.lazy(() => import('../jobpublic/Jobs'));

const JobsPublic = () => {
    useEffect(() => {
        if (document.body) document.body.classList.remove('authentication-bg');
    }, []);

    return (
        <>
            <div className="wrapper">
                <div className="content-page">
                    <div className="content">
                        <Jobs />
                    </div>
                </div>
            </div>
        </>
    );
};

export default JobsPublic;
