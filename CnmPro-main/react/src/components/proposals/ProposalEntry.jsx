import React from 'react';
import { ListGroup, Card, Col } from 'react-bootstrap';
import { PropTypes } from 'prop-types';

function ProposalEntry(props) {
    const singleProposal = props.singleProposal;

    return (
        <Col sm={12} lg={4} md={6}>
            <Card>
                <Card.Body>
                    <Card.Title as="h3"> {singleProposal.job.title} </Card.Title>
                    <Card.Text>Some quick example text to build on the card..</Card.Text>
                </Card.Body>

                <ListGroup variant="flush">
                    <ListGroup.Item>
                        <p> Bid: {singleProposal.bid}</p>
                        <p> Duration: {singleProposal.duration} </p>
                        <p> Details: {singleProposal.details} </p>
                    </ListGroup.Item>
                </ListGroup>
            </Card>
        </Col>
    );
}
ProposalEntry.propTypes = {
    singleProposal: PropTypes.shape({
        job: PropTypes.shape({
            title: PropTypes.string.isRequired,
        }),
        bid: PropTypes.string,
        duration: PropTypes.string,
        details: PropTypes.string,
    }).isRequired,
};
export default ProposalEntry;
