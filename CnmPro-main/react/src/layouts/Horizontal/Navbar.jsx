// @flow
import React from 'react';
import { Collapse } from 'react-bootstrap';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import AppMenu from './Menu';
import { getMenuItems } from '../../helpers/menu';
import { useNavigate } from 'react-router-dom';
import UserService from '../../services/userService';
import Logger from 'sabio-debug';

const _logger = Logger.extend('Secured Navbar');

const Navbar = (props) => {
    const inputTheme = 'dark';

    const navigate = useNavigate();

    const handleLogout = (event) => {
        event.preventDefault();
        UserService.logout().then(onLogOutSuccess).catch(onLogOutError);
    };

    const onLogOutSuccess = () => {
        const state = {
            type: 'LOGOUT_SUCCESS',
            payload: true,
        };
        navigate('/', { state });
    };

    const onLogOutError = (error) => {
        _logger(error, 'Logout unsuccessful');
    };

    const fitlerItems = (items, user) => {
        return items.filter((item) => {
            const contains = user.roles.some((element) => {
                return item.roles.includes(element);
            });
            return contains;
        });
    };

    const filterMenuItems = (menutItems, user) => {
        const parentItems = fitlerItems(menutItems, user);
        return parentItems.map((item) => {
            if (item.children) {
                let children = fitlerItems(item.children, user);
                return {
                    ...item,
                    children,
                };
            }
            return item;
        });
    };

    return (
        <React.Fragment>
            <div className="topnav">
                <div className="container-fluid">
                    <nav className={classNames('navbar', 'navbar-expand-lg', 'topnav-menu', 'navbar-' + inputTheme)}>
                        <Collapse in={props.isMenuOpened} className="navbar-collapse" id="topnav-menu-content">
                            <div>
                                <AppMenu menuItems={filterMenuItems(getMenuItems(), props.currentUser)} />
                            </div>
                        </Collapse>
                        <div>
                            <button className="btn btn-primary mt-1 mb-1" onClick={handleLogout}>
                                Log Out
                            </button>
                        </div>
                    </nav>
                </div>
            </div>
        </React.Fragment>
    );
};
Navbar.propTypes = {
    isMenuOpened: PropTypes.bool,
    currentUser: PropTypes.shape({
        id: PropTypes.number,
        roles: PropTypes.arrayOf(PropTypes.string),
        email: PropTypes.string,
        isLoggedIn: PropTypes.bool,
    }),
};
export default Navbar;
