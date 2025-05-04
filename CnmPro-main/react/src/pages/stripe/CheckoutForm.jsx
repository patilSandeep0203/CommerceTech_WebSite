import React from 'react';
import { useState, useEffect } from 'react'; //, useState
import debug from "sabio-debug";
import * as stripeService from "../../services/stripeServices";
import { loadStripe } from "@stripe/stripe-js";
import userService from '../../services/userService';
import propTypes from 'prop-types';
import { useLocation, useNavigate } from "react-router-dom";
import './stripe.css';
import toastr from 'toastr';
import { STRIPE_PUBLIC_KEY } from '../../services/serviceHelpers';

const CheckoutForm = (props) => {

    const _logger = debug.extend("Stripe");
    const _loggerPage = _logger.extend("CheckoutForm");
    const location = useLocation();
    const { state } = location;
    const navigate = useNavigate();

    _loggerPage("current user info:", props.user);

    const stripePromise = loadStripe(STRIPE_PUBLIC_KEY);

    const [custId, setCustId] = useState({ email: "" });
    const [subPrice, setSubPrice] = useState({ priceId: "", priceNickname: "" });
    const [subSeletion, setSubSelection] = useState({ selectionId: "", selectionName: "" })

    useEffect(() => {

        const onGetUserIdSuccess = (response) => {

            setCustId((prevState) => {
                const custId = { ...prevState };
                custId.email = response.item.name;
                return custId;
            })
        }
        const onGetUserIdError = (error) => {
            toastr.error('User not logged in, please login again');
            _loggerPage("stripe userId error:", error);
        }
        userService.current()
            .then(onGetUserIdSuccess).catch(onGetUserIdError)

        if (state?.type === "SUBSCRIPTION_VIEW" && state?.payload.length === 2) {

            setSubPrice((prevState) => {
                const subPrice = { ...prevState };
                subPrice.priceId = state.payload[1].stripePriceData.stripePrice.id;
                subPrice.priceNickname = state.payload[1].stripePriceData.stripePrice.nickname;
                return subPrice;
            })
            setSubSelection((prevState) => {
                const subSeletion = { ...prevState };
                subSeletion.selectionId = state.payload[0].id;
                subSeletion.selectionName = state.payload[0].name;
                return subSeletion;
            })
        } else {
            toastr.error('Product error, Returning to Subscriptions');
            navigate('/subscriptions');
        }
    }, [])

    //Customer Portal Sequence
    const onGetCustError = (error) => {
        toastr.error('Stripe Submission , please return to subscription page');
        _loggerPage("stripe products error:", error);
    }
    const onGetCustSuccess = (response) => {
        const customerId = response;

        stripeService.customerPortalSession(customerId)
            .then(onPortalSessionSuccess).catch(onPortalSessionError)
    };

    const onClickCustomerPortal = () => {

        stripeService.getCustomer(custId.email)
            .then(onGetCustSuccess).catch(onGetCustError)
    }

    const onPortalSessionSuccess = async (response) => {

        window.location.href = (`${response}`);

    }
    const onPortalSessionError = (error) => {
        toastr.error('Stripe Session Failed, please return to subscription page');
        _loggerPage("error getting new session:", error);
    }

    const onClickReturnToSubscription = () => {
        navigate('/subscriptions');
    };
    // Checkout Btn Service
    const onGetSessionSuccess = async (response) => {
        _loggerPage("new session success:", response);
        const stripe = await stripePromise
        stripe.redirectToCheckout({ sessionId: response }).then(function (result) {
            _loggerPage(result)
        });
    }
    const onGetSessionError = (error) => {
        toastr.error('Stripe Session Failed');
        _loggerPage("error getting new session:", error);
    }
    const goToCheckoutOnClick = () => {
        stripeService.createCheckoutSession(subPrice.priceId).then(onGetSessionSuccess).catch(onGetSessionError);
    }

    return (

        <div className="stripe-container-layout-cnmpro pt-5">
            <div className="row pt-5">
                <div className="row text center">
                    <h4 className="text-center">Confirm your Selection</h4>
                    <div>
                        <div className="stripe-card-cnmpro">
                            <div className="card-header stripe-title">
                                {subSeletion.selectionName}</div>
                            <div className="card-body p-1">
                                <div>
                                    <span className="d-block pt-1 bg-success text-white text-center" >
                                        {subPrice.priceNickname}</span>
                                </div>
                                <div className="mx-1 card-text" id={subSeletion.selectionId} ></div>
                            </div>
                            <h4 className="text-center"><span >If you wish to subscribe...</span></h4>
                            <div className="stripe-checkout-buttons-area-cnmpro pb-2">
                                <button className="btn btn-secondary stripe-checkout-buttons-cnmpro"
                                    type="button"
                                    id="customer-create-btn"
                                    name="customer-create-btn" onClick={goToCheckoutOnClick}>Subscribe Now</button>
                            </div>

                        </div>
                    </div>
                    <div className="stripe-checkout-buttons-area-cnmpro pt-2">
                        <button className="btn btn-secondary stripe-checkout-buttons-cnmpro"
                            type="button"
                            id="return-to-subs-btn"
                            name="return-to-subs-btn" onClick={onClickCustomerPortal}>Go To Billing Portal</button>
                        <button className="btn btn-secondary stripe-checkout-buttons-cnmpro"
                            type="button"
                            id="return-to-subs-btn"
                            name="return-to-subs-btn" onClick={onClickReturnToSubscription}>Return to Subscriptions</button>
                    </div>
                </div>
            </div>
        </div>

    );
};

CheckoutForm.propTypes = {
    user: propTypes.shape({
        email: propTypes.string,
    })
};

export default CheckoutForm;
