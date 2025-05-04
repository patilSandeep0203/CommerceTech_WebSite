import React from 'react';
import { Container, Row, Col, Card } from 'react-bootstrap';
import ContactUsForm from './ContactUsForm';
import { FaMapMarkerAlt } from 'react-icons/fa';
import { AiFillPhone } from 'react-icons/ai';
import { MdEmail } from 'react-icons/md';
import { BsFillClockFill } from 'react-icons/bs';
import '../contactus/contactstyle.css';

const ContactUs = () => {
    return (
        <>
            <section className="py-5 bg-light-lighten border-top border-bottom border-light">
                <Container>
                    <Row>
                        <Col>
                            <div className="text-center">
                                <h3>
                                    Get In <span className="text-primary">Touch</span>
                                </h3>
                                <p className="text-muted mt-2">
                                    Please fill out the following form and we will get back to you shortly. For more
                                    <br />
                                    information please contact us.
                                </p>
                            </div>
                        </Col>
                    </Row>
                    <Card>
                        <Card.Body>
                            <div className="page-aside-left">
                                <Row className="mt-3 position-relative ps-1">
                                    <Col>
                                        <p className="text-muted">
                                            <span className="fw-bold">Customer Support:</span>
                                            <br />{' '}
                                            <span className="d-block mt-1">
                                                <AiFillPhone className="contact-support" /> +1 234 56 7894
                                            </span>
                                        </p>
                                        <p className="text-muted mt-4">
                                            <span className="fw-bold">Email Address:</span>
                                            <br />{' '}
                                            <span className="d-block mt-1">
                                                <MdEmail className="contact-email" /> info@gmail.com
                                            </span>
                                        </p>
                                        <p className="text-muted mt-4">
                                            <span className="fw-bold">Office Address:</span>
                                            <br />{' '}
                                            <span className="d-block mt-1">
                                                <FaMapMarkerAlt className="contact-address" /> 4461 Cedar Street Moro,
                                                AR 72368
                                            </span>
                                        </p>
                                        <p className="text-muted mt-4">
                                            <span className="fw-bold">Office Time:</span>
                                            <br />{' '}
                                            <span className="d-block mt-1">
                                                <BsFillClockFill className="contact-time" /> 9:00AM To 6:00PM
                                            </span>
                                        </p>
                                        <div className="icon">
                                            <i className="fa-solid fa-map-location-dot"></i>
                                        </div>
                                    </Col>
                                    <Card></Card>
                                </Row>
                            </div>
                            <div className="page-aside-right">
                                <div className="d-flex justify-content-between align-items-center">
                                    <Col>
                                        <ContactUsForm></ContactUsForm>
                                    </Col>
                                </div>
                            </div>
                        </Card.Body>
                    </Card>
                </Container>
            </section>
        </>
    );
};

export default ContactUs;
