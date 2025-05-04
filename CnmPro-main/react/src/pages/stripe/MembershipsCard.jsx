import React, { useState, useEffect } from 'react';
import debug from "sabio-debug";
import propTypes from 'prop-types';
import './stripe.css';
import { useNavigate } from "react-router-dom";
import * as stripeService from "../../services/stripeServices";
import toastr from 'toastr';

function MembershipCard(props) {
    const navigate = useNavigate();

    const _logger = debug.extend("Stripe");
    const _loggerPage = _logger.extend("MembershipPageCard");
    const [stripePriceData, setStripePriceData] = useState({
        stripePrice: {},
    })

    const aProduct = props.product;

    useEffect(() => {

        stripeService.getPriceAmt(aProduct.defaultPriceId)
            .then(onGetPriceSuccess)
            .catch(onGetPriceError);
    }, []);

    const onGetPriceError = (error) => {
        _loggerPage("stripe price error:", error);
        toastr.error('stripe service call error, please refresh');
    }

    const onGetPriceSuccess = (response) => {
        const priceData = response.item;
        setStripePriceData((prevState) => {
            const stripePriceData = { ...prevState };
            stripePriceData.stripePrice = priceData;
            return stripePriceData;
        })
    }

    const onClickAddSubToCheckoutPreview = (e) => {
        _loggerPage("navigating to Checkout Preview", e.currentTarget);
        const stateForSendingSub = { type: "SUBSCRIPTION_VIEW", payload: [aProduct, { stripePriceData }] }
        navigate(`/checkoutForm`, { state: stateForSendingSub })
    };

    return (
        <div className="row">

            <div className="card stripe-card-cnmpro">
                <div className="card-header stripe-title">{aProduct.name}</div>
                <div className="card-body card-stripe-details">
                    <div>
                        <span className="d-block pt-1 bg-success stripe-card-banner" ><img src={aProduct.images}
                            className="card-img"
                            style={{ height: "50px", width: "50px" }}
                            alt="pic"
                        />{stripePriceData.stripePrice.nickname}</span>
                    </div>
                    <div className="mx-1 card-text" id={stripePriceData.stripePrice.id} ></div>
                    <div className="stripe-description-cnmpro"> {aProduct.description}</div>
                </div>
                <div >
                    <button className="btn btn-secondary p-1 mx-2" style={{ margin: "auto" }}
                        type="button"
                        data-page="/checkout/"
                        id="checkout-btn"
                        value={aProduct.defaultPriceId}
                        name="checkout-btn" onClick={onClickAddSubToCheckoutPreview}>Purchase</button>
                </div>
            </div>
        </div>
    )
}

MembershipCard.propTypes = {
    product: propTypes.shape({
        id: propTypes.string.isRequired,
        object: propTypes.string.isRequired,
        defaultPriceId: propTypes.string.isRequired,
        description: propTypes.string.isRequired,
        images: propTypes.arrayOf(propTypes.string),
        name: propTypes.string.isRequired,
        type: propTypes.string.isRequired
    })
}

export default MembershipCard;
