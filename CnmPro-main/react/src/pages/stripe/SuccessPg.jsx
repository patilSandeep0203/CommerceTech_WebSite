import React from 'react';
import debug from 'sabio-debug';
import * as stripeService from '../../services/stripeServices';
import { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import './stripe.css';
import toastr from 'toastr';
import aquiferPEOriginal from '../../assets/images/aquiferPEOriginal.png';

const _loggerPage = debug.extend('SubscriptionSuccess');

const SuccessPg = () => {
    const location = useLocation();
    const navigate = useNavigate();

    const sessionId = location.search.substring(12);
    const [invoiceInfo, setInvoiceInfo] = useState({
        invoice: {},
    });

    const onGetReceiptSuccess = (response) => {
        _loggerPage('Event response Data -->:', response);
        toastr.success('Thank you for your purchase');
        setInvoiceInfo((prevState) => {
            const invoiceInfo = { ...prevState };
            invoiceInfo.invoice = response.item;
            return invoiceInfo;
        });
    };
    _loggerPage('invoice information: -->>', invoiceInfo.invoice);

    const onGetReceiptError = (error) => {
        _loggerPage('stripe products error:', error);
    };

    useEffect(() => {
        _loggerPage('GETTING ORDER Data');

        stripeService.getInvoice(sessionId).then(onGetReceiptSuccess).catch(onGetReceiptError);
    }, []);

    const onClickNavToOrgPage = (e) => {
        _loggerPage('navigating to Org page', e.currentTarget);
        navigate(`/admin/organization/dashboard`);
    };

    return (
        <div className="products-container">
            <script src="https://js.stripe.com/v3/"></script>

            <div className="">
                <div className="row ">
                    <div className="row render-area">
                        <div className="text-center my-2">
                            <h3 className="fw-bolder display-6 text-primary">Thank You For Subscribing</h3>
                        </div>
                        {invoiceInfo?.invoice?.amountPaid && (
                            <div className="w-50 mx-auto text-center">
                                <div className="card">
                                    <div className="card-body">
                                        <img
                                            src={aquiferPEOriginal}
                                            className="card-img-top mb-2 invoice-logo"
                                            alt="..."
                                        />
                                        <h4 className="card-title ">Invoice Details</h4>
                                    </div>

                                    <h2 className="text-success">${invoiceInfo?.invoice?.amountPaid / 100}</h2>

                                    <ul className="list-group list-group-flush">
                                        <li className="list-group-item">
                                            <span className="fw-bold">Invoice Number: </span>{' '}
                                            {invoiceInfo?.invoice?.number}
                                        </li>
                                        <li className="list-group-item">
                                            <span className="fw-bold">Payment Date: </span>{' '}
                                            {invoiceInfo?.invoice?.created}
                                        </li>
                                        <li className="list-group-item">
                                            <span className="fw-bold">Customer Name: </span>{' '}
                                            {invoiceInfo?.invoice?.customerName}
                                        </li>
                                        <li className="list-group-item">
                                            <span className="fw-bold">Email: </span>{' '}
                                            {invoiceInfo?.invoice?.customerEmail}
                                        </li>
                                    </ul>
                                    <div className="mb-2">
                                        <div className="text-center p-1">
                                            <a
                                                href={invoiceInfo.invoice.invoicePdf}
                                                className="btn btn-success mx-1 p-1">
                                                Download Invoice
                                            </a>
                                            <a
                                                href={invoiceInfo.invoice.hostedInvoiceUrl}
                                                target="_blank"
                                                rel="noreferrer"
                                                className="btn btn-outline-success mx-1 p-1">
                                                Check Details
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        )}
                        <div className="text-center my-2">
                            <h5 className="mx-3">
                                You can print this page or simply close and return to your Organization Page
                            </h5>
                            <button
                                className="btn btn-outline-dark p-1 mx-2"
                                type="button"
                                id="success-redirect-btn"
                                name="success-redirect-btn"
                                onClick={onClickNavToOrgPage}>
                                Close Page
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SuccessPg;
