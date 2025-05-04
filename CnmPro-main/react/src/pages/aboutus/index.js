// @flow
import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import profile from '../../assets/images/cnmPro/profile.png';
import linkedin from '../../assets/images/cnmPro/linkedin.png';
import engineering from '../../assets/images/engineering.png';
import vision from '../../assets/images/bg-auth.jpg';

import './about.css';

import { BsLinkedin } from 'react-icons/bs';

const About = () => {
    return (
        <section className="container-block py-lg-6-cnmpro">
            <Container id="about-container ">
                <Row className="align-items-center">
                    <Col className=" text-center ">
                        <div>
                            <div>
                                <h1 className="text-aligment-cnmpro cnmpro-h3 mb-3">ABOUT US</h1>
                                <img
                                    src={engineering}
                                    alt="HTML5 Icon"
                                    width="500"
                                    height="350"
                                    className=" mb-3 img-fluid"></img>
                                <h2 className="text-alignment-cnmpro cnmpro-header cnmpro-text-color-grey mb-3 ">
                                    Working Marketplace for Engineering and Construction
                                </h2>

                                <p className=" cnmpro-p col-7 mt-3 mb-3 ">
                                    Aquifer is a freelance platform specialized in the civil engineering and
                                    construction industry. Aquifer facilitates the optimization of professional human
                                    resource utilization. Aquifer helps companies find specialized professionals that
                                    best fit your project needs while unlocking the engineers earning potential with
                                    compensation per project delivery.{' '}
                                </p>
                            </div>
                        </div>

                        <div className="cnmpro-vision">
                            <h2>
                                <b>Vision</b>
                            </h2>
                            <img
                                className=" mt-3 mb-3 img-fluid"
                                src={vision}
                                alt="HTML5 Icon"
                                width="500"
                                height="350"></img>
                            <p className=" cnmpro-p col-7 mt-3 mb-3 ">
                                We are the solution to the labor shortage in Engineering by optimizing resource
                                allocation and utilization through hiring practices that allow professionals to be fluid
                                and service multiple infrastructure projects, creating flexibility and ability to work
                                from anywhere anytime.{' '}
                            </p>
                        </div>
                        <div className="cnmpro-vision">
                            <h2>
                                <b>Why Aquifer</b>
                            </h2>
                            <p className="mt-3 mb-3 cnmpro-p col-7">
                                Founded by an engineer for engineers, Aquifer is committed to be the solution to the
                                labor shortage in Engineering by optimizing the current hiring practices. This allows
                                professionals to be fluid and service multiple infrastructure projects on a dynamic
                                timeline.
                            </p>
                        </div>
                        <div className="profiles mt-5 mb-3">
                            <div className="profile text-center">
                                <a className="text-center" href="https://www.linkedin.com/in/nicole-mi">
                                    <img className="online" src={profile} alt="Profile" />
                                </a>
                                <div className="info">
                                    <h2>
                                        Nicole Mi <div></div>
                                    </h2>
                                    <hr />
                                    <p className="info-content text-center">
                                        CEO| Civil Engineer | Infrastructure Manager| Passionate Entrepreneur.
                                    </p>
                                    <a
                                        className="btn btn-primary"
                                        href="https://www.linkedin.com/in/nicole-mi"
                                        src={linkedin}>
                                        LINKEDIN <BsLinkedin className="mb-1" />
                                    </a>
                                </div>
                            </div>
                        </div>
                    </Col>
                </Row>
            </Container>
        </section>
    );
};

export default About;
