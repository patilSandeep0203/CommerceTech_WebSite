// @flow
import React from 'react';
import { Card, Button, Table } from 'react-bootstrap';

const Engagement = () => {
    return (
        <Card>
            <Card.Body>
                <Button variant="link" className="p-0 float-end">
                    Export <i className="mdi mdi-download ms-1"></i>
                </Button>
                <h4 className="header-title mt-1 mb-3">Engagement Overview</h4>

                <Table responsive className="table table-sm table-centered mb-0 font-14">
                    <thead className="table-light">
                        <tr>
                            <th>Duration (Secs)</th>
                            <th>Sessions</th>
                            <th>Views</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>0-30</td>
                            <td>2,250</td>
                            <td>4,250</td>
                        </tr>
                        <tr>
                            <td>31-60</td>
                            <td>1,501</td>
                            <td>2,050</td>
                        </tr>
                        <tr>
                            <td>61-120</td>
                            <td>750</td>
                            <td>1,600</td>
                        </tr>
                        <tr>
                            <td>121-240</td>
                            <td>540</td>
                            <td>1,040</td>
                        </tr>
                    </tbody>
                </Table>
            </Card.Body>
        </Card>
    );
};

export default Engagement;
