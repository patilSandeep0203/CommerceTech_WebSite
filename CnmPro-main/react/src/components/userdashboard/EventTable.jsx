import React from 'react';
import { Card, Table } from 'react-bootstrap';
import { PropTypes } from 'prop-types';

const EventTable = (props) => {
    return (
        <Card>
            <Card.Body>
                <h4 className="header-title">Current Events</h4>
                <Table className="mb-0" hover>
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        {props.eventData.map((event, index) => {
                            return (
                                <tr key={index}>
                                    <td>{event.title}</td>
                                    <td>{event.description}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </Table>
            </Card.Body>
        </Card>
    );
};

EventTable.propTypes = {
    eventData: PropTypes.arrayOf(
        PropTypes.shape({
            title: PropTypes.string,
            description: PropTypes.string,
        }).isRequired
    ).isRequired,
};

export default EventTable;
