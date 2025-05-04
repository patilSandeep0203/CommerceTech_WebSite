import { useEffect, useState } from 'react';
import React from 'react';
import debug from "sabio-debug";
import * as stripeService from "../../services/stripeServices";
import MembershipsCard from "./MembershipsCard";
import propTypes from 'prop-types';
import './stripe.css';
import toastr from 'toastr';

function Memberships() {
    const _logger = debug.extend("Stripe");
    const _loggerPage = _logger.extend("MembershipPage");

    const [stripePageData, setStripePageData] = useState({
        stripeProducts: [],
        stripeProductComponents: []
    })
    const onGetStripeProductsSuccess = (response) => {
        const newStripeProducts = response.item;

        setStripePageData((prevState) => {
            const pageData = { ...prevState };
            pageData.stripeProducts = newStripeProducts;
            pageData.stripeProductComponents = newStripeProducts.map(MappingStripeDataToCard);
            return pageData;
        })
    }
    const onGetStripeProductsError = (error) => {
        toastr.error('stripe service call error, please refresh');
        _loggerPage("stripe products error:", error);
    }

    useEffect(() => {
        _loggerPage("Firing get Stripe Data");
        stripeService.getProducts().then(onGetStripeProductsSuccess).catch(onGetStripeProductsError);
    }, []);

    const MappingStripeDataToCard = (aProduct) => {
        return (
            <MembershipsCard product={aProduct}
                key={"ListProduct-" + aProduct.id}
            />
        )
    };

    return (

        <div className="products-container">
            <script src="https://js.stripe.com/v3/"></script>

            <div>
                <div className="row ">
                    <div className="row render-area stripe-product-layout-cnmpro">
                        {stripePageData.stripeProductComponents}
                        <div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

Memberships.propTypes = {
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

export default Memberships;
