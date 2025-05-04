// @flow
import React from 'react';
import { Card, Table } from 'react-bootstrap';
import { PropTypes } from 'prop-types';

const JobTable = (props) => {
    return (
        <Card>
            <Card.Body>
                <h4 className="header-title">Current Working Jobs</h4>
                <Table className="mb-0" hover>
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        {props.jobData.map((job, index) => {
                            return (
                                <tr key={index}>
                                    <td>{job.title}</td>
                                    <td>{job.description}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </Table>
            </Card.Body>
        </Card>
    );
};

JobTable.propTypes = {
    jobData: PropTypes.arrayOf(
        PropTypes.shape({
            title: PropTypes.string,
            description: PropTypes.string,
        }).isRequired
    ).isRequired,
};

export default JobTable;
