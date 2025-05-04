// @flow
import React, { useEffect, Suspense } from 'react';
import PropTypes from 'prop-types';
import { Container } from 'react-bootstrap';
const loading = () => <div className=""></div>;

const Navbar = React.lazy(() => import('../pages/landing/NavBar.jsx'));
const Footer = React.lazy(() => import('../pages/landing/Footer.jsx'));

const DefaultLayout = (props) => {
    useEffect(() => {
        if (document.body) document.body.classList.add('authentication-bg');

        return () => {
            if (document.body) document.body.classList.remove('authentication-bg');
        };
    }, []);

    const children = props.children || null;

    return (
        <>
            <div className="wrapper">
                <div className="content-page">
                    <div className="content">
                        <Suspense fallback={loading()}>
                            <Navbar user={props.user} />
                        </Suspense>
                        <Container fluid>
                            <Suspense fallback={loading()}>{children}</Suspense>
                        </Container>
                        <Suspense fallback={loading()}>
                            <Footer />
                        </Suspense>
                    </div>
                </div>
            </div>
        </>
    );
};
DefaultLayout.propTypes = {
    children: PropTypes.element,
    user: PropTypes.shape({
        id: PropTypes.number,
        roles: PropTypes.arrayOf(PropTypes.string),
        email: PropTypes.string,
        isLoggedIn: PropTypes.bool,
    }),
};
export default DefaultLayout;
