import { React, useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import BootstrapTable from 'react-bootstrap-table-next';
import logger from 'sabio-debug';
import adminDashboardService from '../../../services/adminDashboardService';
import paginationFactory from 'react-bootstrap-table2-paginator';
import toastr from 'toastr';
import 'react-bootstrap-table2-paginator/dist/react-bootstrap-table2-paginator.min.css';
import './admindashboard.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.css';

const _logger = logger.extend('OrgTable');

const OrgTableV2 = () => {
    const [organizations, setOrganizations] = useState({ data: [] });

    useEffect(() => {
        adminDashboardService.getOrganizationsStats().then(getOrgSuccess).catch(getOrgError);
    }, []);

    const getOrgSuccess = (data) => {
        let infoOrgAll = data.items;
        infoOrgAll.map(
            (eachOrg) =>
                (eachOrg.address = `${eachOrg.lineOne} ${eachOrg.lineTwo} ${eachOrg.city}, ${eachOrg.stateCode} ${eachOrg.zip}`)
        );
        setOrganizations((prevState) => {
            let updateOrgInfo = { ...prevState };
            updateOrgInfo.data = infoOrgAll;
            return updateOrgInfo;
        });
        _logger('getOrgSuccess', infoOrgAll);
    };

    const getOrgError = (data) => {
        toastr.error('There is no valid data to show in this table');
        _logger('Error', data);
    };

    const columns = [
        { dataField: 'id', text: 'Id', sort: true },
        { dataField: 'name', text: 'Name', sort: true },
        { dataField: 'headline', text: 'Headline', sort: true },
        { dataField: 'siteUrl', text: 'SiteUrl', sort: true },
        { dataField: 'address', text: 'Address', sort: true },
    ];

    const defaultSorted = [
        {
            dataField: 'name',
            order: 'desc',
        },
    ];

    const pagination = paginationFactory({
        page: 1,
        sizePerPage: 3,
        lastPageText: '>>',
        firstPageText: '<<',
        nextPageText: '>',
        prePageText: '<',
        showTotal: false,
        alwaysShowAllBtns: true,
        onPageChange: function (page, sizePerPage) {
            _logger('page', page);
            _logger('sizePerPage', sizePerPage);
        },
    });

    return (
        <>
            <Card className="tilebox-one">
                <Card.Body>
                    <div className="row">
                        <h3 className="mb-3 text-center">Organizations information</h3>
                        <BootstrapTable
                            keyField="id"
                            data={organizations.data}
                            columns={columns}
                            defaultSorted={defaultSorted}
                            pagination={pagination}
                        />
                    </div>
                </Card.Body>
            </Card>
        </>
    );
};

export default OrgTableV2;
