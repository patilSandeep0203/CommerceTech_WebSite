import React from 'react';
import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function NewsletterCard() {
    return (
        <>
            <Card className="orgDash-latest-height m-0">
                <img
                    className="card-img-top orgDash-cover orgDash-card-img-height"
                    src="https://images.pexels.com/photos/462331/pexels-photo-462331.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"
                    alt=""
                />
                <div className="card-img-overlay">
                    <div className="bg-success badge">Latest NewsLetter</div>
                </div>
                <Card.Body className="position-relative">
                    <h4 className="mt-0">
                        <Link to="#" className="text-title">
                            How to Write Newsletter
                        </Link>
                    </h4>
                    <p className="text-muted font-13 my-1">
                        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ut ex consequuntur odit quod
                        perferendis praesentium, voluptatum earum hic dolore adipisci amet, nam accusantiu...
                    </p>
                    <p className="mb-1">
                        <span className="pe-2 text-nowrap mb-2 d-inline-block">
                            <i className="mdi mdi-calendar text-muted me-1"></i>
                            <b className="me-1">Published</b>
                            08/10/22
                        </span>
                    </p>
                    <Link to="#">
                        <button className="btn btn-primary">View More</button>
                    </Link>
                </Card.Body>
            </Card>
        </>
    );
}

export default NewsletterCard;
