import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';

const OurCookies = () => {
    return (
        <section>
            <Container>
                <Col>
                    <Row>
                        <div className="col-md-12 ">
                            <h3>Cookie List</h3>
                            <p>
                                A cookie is a small piece of data (text file) that a website – when visited by a user –
                                asks your browser to store on your device in order to remember information about you,
                                such as your language preference or login information. Those cookies are set by us and
                                called first-party cookies. We also use third-party cookies – which are cookies from a
                                domain different than the domain of the website you are visiting – for our advertising
                                and marketing efforts. More specifically, we use cookies and other tracking technologies
                                for the following purposes:
                            </p>
                            <h3>Performance Cookies</h3>
                            <p>
                                {' '}
                                These cookies allow us to count visits and traffic sources so we can measure and improve
                                the performance of our site. They help us to know which pages are the most and least
                                popular and see how visitors move around the site. All information these cookies collect
                                is aggregated and therefore anonymous. If you do not allow these cookies we will not
                                know when you have visited our site, and will not be able to monitor its performance.{' '}
                            </p>
                            <table
                                className="table table-bordered table-striped
                            ">
                                <thead>
                                    <tr>
                                        <th className="table table-striped" ot-host data-label="Cookie Subgroup">
                                            Cookie Subgroup
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookies
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookie used
                                        </th>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/nr-data.net"
                                                data-label="Cookie Subgroup">
                                                nr-data.net
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            JSESSIONID
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookies">
                                            www.aquifer.fr
                                        </td>
                                        <td>
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/utm_campaign"
                                                data-label="Cookie">
                                                utm_campaign
                                            </a>
                                            ,
                                            <a href="https://cookiepedia.co.uk/cookies/utm_content" data-label="Cookie">
                                                utm_content
                                            </a>
                                            ,
                                            <a href="https://cookiepedia.co.uk/cookies/utm_medium" data-label="Cookie">
                                                utm_medium
                                            </a>
                                            ,
                                            <a href="https://cookiepedia.co.uk/cookies/utm_source" data-label="Cookie ">
                                                utm_source
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            First Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            www.aquifer.com
                                        </td>
                                        <td>
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/_hjIncludedInPageviewSample"
                                                data-label="Cookie">
                                                _hjIncludedInPageviewSample
                                            </a>
                                            ,{' '}
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/_hjIncludedInSessionSample"
                                                data-label="Cookie">
                                                _hjIncludedInSessionSample
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            First Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookies">
                                            aquifer.com
                                        </td>
                                        <td>
                                            <a
                                                href="https://cookiepedia.co.uk/host/_gclxxxx"
                                                data-label="Cookie Subgroup">
                                                _gclxxxx
                                            </a>
                                            ,
                                            <a href="https://cookiepedia.co.uk/host/_gid" data-label="Cookie Subgroup">
                                                _gid
                                            </a>
                                            ,
                                            <a
                                                href="https://cookiepedia.co.uk/host/_hjAbsoluteSessionInProgress"
                                                data-label="Cookie Subgroup">
                                                _hjAbsoluteSessionInProgress
                                            </a>
                                            ,
                                            <a
                                                href="https://cookiepedia.co.uk/host/_hjFirstSeen"
                                                data-label="Cookie Subgroup">
                                                _hjFirstSeen
                                            </a>
                                            ,
                                            <a href="https://cookiepedia.co.uk/host/_hjid" data-label="Cookie Subgroup">
                                                _hjid
                                            </a>
                                            ,
                                            <a
                                                href="https://cookiepedia.co.uk/host/_hjTLDTest"
                                                data-label="Cookie Subgroup">
                                                _hjTLDTest
                                            </a>
                                            ,
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/_uetvid
                                                "
                                                data-label="Cookie Subgroup">
                                                _uetvid
                                            </a>
                                            ,
                                            <a href="https://cookiepedia.co.uk/cookies/_gat_UA-" data-label="Cookie ">
                                                _gat_UA-
                                            </a>
                                            ,
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/_gat_gtag_xxxxxxxxxxxxxxxxxxxxxxxxxxx"
                                                data-label="Cookie">
                                                _gat_gtag_xxxxxxxxxxxxxxxxxxxxxxxxxxx
                                            </a>
                                            ,
                                            <a href="https://cookiepedia.co.uk/cookies/_ga" data-label="Cookie ">
                                                _ga
                                            </a>
                                            ,
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            First Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/browser.sentry-cdn.com"
                                                data-label="Cookie Subgroup">
                                                browser.sentry-cdn.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies">
                                            _hjIncludedInPageviewSample, _hjIncludedInSessionSample{' '}
                                        </td>
                                        <td data-label="Cookies used">Third Party</td>
                                    </tr>
                                </thead>
                            </table>
                            <h3>Targeting Cookies</h3>
                            <p>
                                {' '}
                                These cookies may be set through our site by our advertising partners. They may be used
                                by those companies to build a profile of your interests and show you relevant adverts on
                                other sites. They do not store directly personal information, but are based on uniquely
                                identifying your browser and internet device. If you do not allow these cookies, you
                                will experience less targeted advertising.{' '}
                            </p>
                            <table className="table table-bordered table-striped px-0 m-x-0">
                                <thead>
                                    <tr>
                                        <th className="table table-striped" ot-host data-label="Cookie Subgroup">
                                            Cookie Subgroup
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookies
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookie used
                                        </th>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/youtube.com"
                                                data-label="Cookie Subgroup">
                                                youtube.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            YSC, CONSENT, VISITOR_INFO1_LIVE
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/twitter.com"
                                                data-label="Cookie Subgroup">
                                                twitter.com
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            personalization_id,
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/google.com"
                                                data-label="Cookie Subgroup">
                                                google.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            CONSENT, CONSENT, NID
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/www.facebook.com"
                                                data-label="Cookie Subgroup">
                                                www.facebook.com
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies"></td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/facebook.com"
                                                data-label="Cookie Subgroup">
                                                facebook.com
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            fr
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/doubleclick.net"
                                                data-label="Cookie Subgroup">
                                                doubleclick.net
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            test_cookie
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/bing.com"
                                                data-label="Cookie Subgroup">
                                                bing.com
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            MUIDs
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookie Subgroup">
                                            aquifer.com
                                        </td>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/_uetsid"
                                                data-label="Cookie Subgroup">
                                                _uetsid
                                            </a>
                                            ,
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/_fbp"
                                                data-label="Cookie Subgroup">
                                                _fbp
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                            <h3>Strictly Necessary Cookies</h3>
                            <p>
                                {' '}
                                These cookies are necessary for the website to function and cannot be switched off in
                                our systems. They are usually only set in response to actions made by you which amount
                                to a request for services, such as setting your privacy preferences, logging in or
                                filling in forms. You can set your browser to block or alert you about these cookies,
                                but some parts of the site will not then work. These cookies do not store any personally
                                identifiable information.
                            </p>
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr className="table table-striped">
                                        <th className="table table-striped" ot-host data-label="Cookie Subgroup">
                                            Cookie Subgroup
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookies
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookie used
                                        </th>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/ m.stripe.com"
                                                data-label="Cookie Subgroup">
                                                m.stripe.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            m
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/paypal.com"
                                                data-label="Cookie Subgroup">
                                                paypal.com
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            akavpau_ppsd
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/www.paypal.com"
                                                data-label="Cookie Subgroup">
                                                www.paypal.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            x-cdn, ts_c, ts
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookie Subgroup">
                                            www.aquifer.com
                                        </td>

                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/OptanonAlertBoxClosed"
                                                data-label="Cookie Subgroup">
                                                OptanonAlertBoxClosed
                                            </a>
                                            ,{' '}
                                            <a
                                                href="https://cookiepedia.co.uk/host/OptanonConsent"
                                                data-label="Cookie Subgroup">
                                                OptanonConsent
                                            </a>
                                        </td>
                                        <td data-label="Cookies used">First Party</td>
                                    </tr>
                                    <tr>
                                        <td data-label="Cookie Subgroup">aquifer.com</td>

                                        <td className="table table-striped">
                                            <a href="https://cookiepedia.co.uk/host/email" data-label="Cookie Subgroup">
                                                email
                                            </a>
                                            ,{' '}
                                            <a
                                                href="https://cookiepedia.co.uk/host/password"
                                                data-label="Cookie Subgroup">
                                                password
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            First Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookie Subgroup">
                                            www.aquifer.com
                                        </td>

                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/__cflb"
                                                data-label="Cookie Subgroup">
                                                __cflb
                                            </a>
                                            ,{' '}
                                            <a
                                                href="https://cookiepedia.co.uk/host/GTGSESSID"
                                                data-label="Cookie Subgroup">
                                                GTGSESSID
                                            </a>{' '}
                                            ,{' '}
                                            <a href="https://cookiepedia.co.uk/host/vid" data-label="Cookie Subgroup">
                                                vid
                                            </a>{' '}
                                            ,{' '}
                                            <a
                                                href="https://cookiepedia.co.uk/host/__stripe_sid"
                                                data-label="Cookie Subgroup">
                                                __stripe_sid
                                            </a>{' '}
                                            ,{' '}
                                            <a
                                                href="https://cookiepedia.co.uk/host/__stripe_mid"
                                                data-label="Cookie Subgroup">
                                                __stripe_mid
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            First Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/lc.iadvize.com"
                                                data-label="Cookie Subgroup">
                                                lc.iadvize.com
                                            </a>
                                        </td>

                                        <td data-label="Cookies"> iadvize-17909-vuid, isStorageAvailable</td>
                                        <td data-label="Cookies used">Third Party</td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/ www.aquifer.fr"
                                                data-label="Cookie Subgroup">
                                                www.aquifer.fr
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            {' '}
                                            __cflb
                                        </td>

                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                            <h3>Functional Cookies</h3>
                            <p>
                                {' '}
                                These cookies enable the website to provide enhanced functionality and personalisation.
                                They may be set by us or by third party providers whose services we have added to our
                                pages. If you do not allow these cookies then some or all of these services may not
                                function properly.
                            </p>
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th className="table table-striped" ot-host data-label="Cookie Subgroup">
                                            Cookie Subgroup
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookies
                                        </th>
                                        <th className="table table-striped" ot-host>
                                            Cookie used
                                        </th>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/alice0lorenz.wixsite.com"
                                                data-label="Cookie Subgroup">
                                                alice0lorenz.wixsite.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            ssr-caching, hs, bSession, svSession, XSRF-TOKEN, TSxxxxxxxxxx
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/typeform.com"
                                                data-label="Cookie Subgroup">
                                                typeform.com
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            attribution_user_id, __cfduid, __cf_bm, __tld__
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr className="table table-striped">
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/ghost.io"
                                                data-label="Cookie Subgroup">
                                                ghost.io
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookies Subgroup">
                                            __cfduid
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookies">
                                            {' '}
                                            www.aquifer.fr
                                        </td>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/browserupdateorg"
                                                data-label="Cookie Subgroup">
                                                browserupdateorg
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookies">
                                            www.aquifer.com
                                        </td>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/browserupdateorg"
                                                data-label="Cookie Subgroup">
                                                browserupdateorg
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies used">
                                            First Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/browser-update.org
                                                "
                                                data-label="Cookie Subgroup">
                                                browser-update.org
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies">
                                            browserupdateorg, browserupdateorg, cf_use_ob, __cfduid, cf_ob_info
                                        </td>
                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped" data-label="Cookies">
                                            aquifer.com
                                        </td>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/host/ _pin_unauth"
                                                data-label="Cookie Subgroup">
                                                _pin_unauth
                                            </a>
                                        </td>

                                        <td className="table table-striped" data-label="Cookies used">
                                            First Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/ecom.wix.com"
                                                data-label="Cookie Subgroup">
                                                ecom.wix.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookie Subgroup">
                                            {' '}
                                            bSession
                                        </td>

                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/wix.com"
                                                data-label="Cookie Subgroup">
                                                wix.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookie Subgroup">
                                            {' '}
                                            _wixAB3, XSRF-TOKEN
                                        </td>

                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                    <tr>
                                        <td className="table table-striped">
                                            <a
                                                href="https://cookiepedia.co.uk/cookies/wixsite.com"
                                                data-label="Cookie Subgroup">
                                                wixsite.com
                                            </a>
                                        </td>
                                        <td className="table table-striped" data-label="Cookie Subgroup">
                                            {' '}
                                            TSxxxxxxxxxx
                                        </td>

                                        <td className="table table-striped" data-label="Cookies used">
                                            Third Party
                                        </td>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </Row>
                </Col>
            </Container>
        </section>
    );
};

export default OurCookies;
