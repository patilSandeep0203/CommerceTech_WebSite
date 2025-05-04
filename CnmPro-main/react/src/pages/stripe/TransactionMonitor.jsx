import { useEffect } from 'react';
import React from 'react';
import debug from "sabio-debug";
import * as stripeService from "../../services/stripeServices";
import propTypes from 'prop-types';
import './stripe.css';
import toastr from 'toastr';


function TransactionMonitor() {
    const _logger = debug.extend("Stripe");
    const _loggerPage = _logger.extend("Transaction Monitor");


    const onGetHookSuccess = (response) => {
        _loggerPage("Event response Data -->:", response);
        toastr.success('New Transaction Occurred');

    }
    const onGetHookError = (error) => {
        _loggerPage("stripe products error:", error);
        toastr.error('stripe event service error, please check store page on stripe.com');
    }

    useEffect(() => {
        _loggerPage("GETTING Event Data");
        stripeService.getEvents().then(onGetHookSuccess).catch(onGetHookError);
    }, []);


    return (

        <div className="products-container pt-5">
            <script src="https://js.stripe.com/v3/"></script>

            <div className="pt-5"> Webhooks-Monitor page is under construction...
                <div className="row ">
                    <div className="row render-area stripe-product-layout-cnmpro">

                        <div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

TransactionMonitor.propTypes = {
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

export default TransactionMonitor;
