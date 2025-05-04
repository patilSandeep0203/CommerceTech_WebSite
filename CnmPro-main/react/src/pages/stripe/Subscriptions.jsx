import React, { useState, useEffect } from 'react';
import propTypes from 'prop-types';
import debug from "sabio-debug";
import './stripe.css';
import * as stripeService from "../../services/stripeServices";
import toastr from 'toastr';
import MembershipsCard from "./MembershipsCard";

const Subscriptions = () => {
    const _logger = debug.extend("Stripe");
    const _loggerPage = _logger.extend("SubscriptionPage");

    const [stripePageData, setStripePageData] = useState({
        stripeProducts: [],
        stripeProductComponents: []
    })

    const onGetStripeProductsSuccess = (response) => {
        const newStripeProducts = response.item;

        setStripePageData((prevState) => {
            const pageData = { ...prevState };
            pageData.stripeProducts = newStripeProducts;
            pageData.stripeProductComponents = newStripeProducts.map(mappingStripeDataToCard);
            return pageData;
        })
    }
    const onGetStripeProductsError = (error) => {
        _loggerPage("stripe products service error", error);
        toastr.error('stripe product error, please refresh');
    }

    useEffect(() => {

        stripeService.getProducts()
            .then(onGetStripeProductsSuccess)
            .catch(onGetStripeProductsError);
    }, []);

    const mappingStripeDataToCard = (aProduct) => {

        return (
            <MembershipsCard product={aProduct}
                key={"ListProduct-" + aProduct.id}
            />
        )
    };

    return (
        <div className="container stripe-container-layout-cnmpro pt-5">
            <div className="row pt-5">
                <div className="">
                    <h3 className="text-center">Here are the Subscription offerings</h3>

                    <div>
                        {stripePageData.stripeProductComponents}
                    </div>
                </div>
            </div>

        </div>
    )

}
Subscriptions.propTypes = {
    product: propTypes.shape({
        id: propTypes.string.isRequired,
        object: propTypes.string.isRequired,
        defaultPrice: propTypes.string.isRequired,
        description: propTypes.string.isRequired,
        images: propTypes.string,
        name: propTypes.string.isRequired,
        type: propTypes.string.isRequired
    })
}

export default Subscriptions;
