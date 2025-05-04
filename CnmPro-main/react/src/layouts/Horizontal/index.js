// @flow
import React, { Suspense, useEffect } from 'react';
import { Container } from 'react-bootstrap';
import { useDispatch } from 'react-redux';
import PropTypes from 'prop-types';

// actions
import { changeLayout } from '../../redux/actions';
import * as layoutConstants from '../../constants/layout';

const Navbar = React.lazy(() => import('./Navbar'));
const Footer = React.lazy(() => import('../Footer'));

const loading = () => <div className="text-center"></div>;

const HorizontalLayout = ({ children, user }) => {
    const dispatch = useDispatch();

    /**
     * Open the menu when having mobile screen
     */

    useEffect(() => {
        dispatch(changeLayout(layoutConstants.LAYOUT_HORIZONTAL));
    }, [dispatch]);

    return (
        <>
            <div className="wrapper">
                <div className="content-page">
                    <div className="content">
                        <Suspense fallback={loading()}>
                            <Navbar currentUser={user} />
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
HorizontalLayout.propTypes = {
    children: PropTypes.shape({}),
    user: PropTypes.shape({
        id: PropTypes.number,
        roles: PropTypes.arrayOf(PropTypes.string),
        email: PropTypes.string,
        isLoggedIn: PropTypes.bool,
    }),
};
export default HorizontalLayout;
