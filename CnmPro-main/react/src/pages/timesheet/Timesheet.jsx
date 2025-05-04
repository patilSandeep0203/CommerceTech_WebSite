import React, { useState, useEffect } from 'react';
import './timesheet.css';
import timesheetService from '../../services/timesheetService';
import debug from 'sabio-debug';
import TimesheetEntry from './TimesheetEntry';
import SearchForm from './SearchForm';
import TimesheetForm from './TimesheetForm';
import toastr from 'toastr';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import { Table, Card, Row, Col } from 'react-bootstrap';

function Timesheet() {
    const _logger = debug.extend('Timesheet');

    const [isShowing, setIsShowing] = useState(false);
    const [filters, setFilters] = useState({});

    const [timesheetData, setTimesheetData] = useState({
        timesheetEntries: [],
        timesheetEntriesMapped: [],
        filteredEntries: [],
        filteredEntriesMapped: [],
    });

    const [timesheetEntryData, setTimesheetEntryData] = useState({
        date: '',
        timeFrom: '',
        timeTo: '',
        organizationId: 0,
        comment: '',
        jobId: 0,
    });

    const [pageData, setPageData] = useState({
        currentPage: 1,
        pageSize: 10,
        pageIndex: 0,
        total: 0,
    });

    const [jobs, setJobs] = useState([]);
    const [orgs, setOrgs] = useState([]);
    const [totalHours, setTotalHours] = useState([]);

    useEffect(() => {
        getAllCurrentUser();
        timesheetService.userJobs().then(userJobsSuccess).catch(userJobsError);
        timesheetService.userOrganizations().then(userOrgsSuccess).catch(userOrgsError);
        timesheetService.totalHours().then(totalHoursSuccess).catch(totalHoursError);
    }, []);

    const getAllCurrentUser = () => {
        timesheetService
            .getAllCurrentUserPaginate(pageData.pageIndex, pageData.pageSize)
            .then(getAllCurrentUserPaginateSuccess)
            .catch(getAllCurrentUserPaginateError);
    };

    const getAllCurrentUserPaginateSuccess = (response) => {
        setTimesheetData((prevState) => {
            return {
                ...prevState,
                timesheetEntries: response.item.pagedItems,
                timesheetEntriesMapped: response.item.pagedItems.map(timesheetEntryMapper),
            };
        });
        setPageData((prevState) => {
            return {
                ...prevState,
                total: response.item.totalCount,
            };
        });
    };

    const timesheetEntryMapper = (timesheetEntry) => {
        return <TimesheetEntry key={timesheetEntry.id} timesheetEntry={timesheetEntry} onEdit={onEditClicked} />;
    };

    const getAllCurrentUserPaginateError = (response) => {
        toastr.error('Error getting timesheet entries', response);
    };

    const userJobsSuccess = (response) => {
        setJobs(() => {
            return response.items;
        });
    };

    const userJobsError = (response) => {
        _logger(response, 'jobs error');
        toastr.error('Error getting jobs');
    };

    const userOrgsSuccess = (response) => {
        setOrgs(() => {
            return response.items;
        });
    };

    const userOrgsError = (response) => {
        _logger(response, 'org error');
        toastr.error('Error getting organizations');
    };

    const totalHoursSuccess = (response) => {
        setTotalHours(() => {
            return response.item;
        });
    };

    const totalHoursError = (response) => {
        _logger(response, 'total error');
        toastr.error('Error getting total');
    };

    const hide = () => {
        setIsShowing(() => false);
        setTimesheetEntryData(() => {
            return { date: '', timeFrom: '', timeTo: '', organizationId: 0, comment: '', jobId: 0 };
        });
    };

    const show = () => {
        setIsShowing(() => true);
    };

    const onEditClicked = (timesheetEntry) => {
        setTimesheetEntryData((prevState) => ({
            ...prevState,
            date: timesheetEntry.date,
            timeFrom: timesheetEntry.timeFrom,
            timeTo: timesheetEntry.timeTo,
            organizationId: timesheetEntry.organizationId,
            comment: timesheetEntry.comment,
            jobId: timesheetEntry.jobId,
            id: timesheetEntry.id,
        }));
        show();
    };

    const onSearchClicked = (values) => {
        _logger('search clicked', values.timePeriod);
        if (values.timePeriod === '2' || values.timePeriod === undefined) {
            timesheetService
                .searchByOrg(values.organizationId, pageData.pageIndex, pageData.pageSize)
                .then(onSearchClickedSuccess)
                .catch(onSearchClickedError);
        } else {
            timesheetService
                .filter(values.organizationId, values.timePeriod, pageData.pageIndex, pageData.pageSize)
                .then(onSearchClickedSuccess)
                .catch(onSearchClickedError);
        }
        setFilters(() => {
            return { ...values };
        });
    };

    const onSearchClickedSuccess = (response) => {
        _logger(response, 'onSearchClickedSuccess');
        setTimesheetData((prevState) => {
            return {
                ...prevState,
                timesheetEntries: response.item.pagedItems,
                timesheetEntriesMapped: response.item.pagedItems.map(timesheetEntryMapper),
            };
        });
        setPageData((prevState) => {
            return {
                ...prevState,
                total: response.item.totalCount,
            };
        });
    };

    const onSearchClickedError = (response) => {
        _logger(response, 'onSearchClickedError');
        toastr.error('Error getting timesheet entries', response);
    };

    const onClearClicked = () => {
        getAllCurrentUser();
        setFilters(() => {});
    };

    const onAddClicked = () => {
        show();
        _logger(isShowing, 'onAddClicked');
    };

    const onSubmitClicked = (values) => {
        _logger('onSubmitClicked', values);
        if (values.id) {
            timesheetService
                .updateTimesheetEntry(values, values.id)
                .then(onUpdateTimesheetEntrySuccess)
                .catch(onUpdateTimesheetEntryError);
        } else {
            timesheetService.addTimesheetEntry(values).then(onAddTimesheetEntrySuccess).catch(onAddTimesheetEntryError);
        }
    };

    const onUpdateTimesheetEntrySuccess = (response) => {
        _logger('onUpdateTimesheetEntrySuccess', response);
        hide();
        toastr.success('Timesheet entry updated successfully');
        setTimesheetEntryData(() => ({
            date: '',
            timeFrom: '',
            timeTo: '',
            organizationId: 0,
            organization: '',
            comment: '',
            jobId: 0,
        }));

        getAllCurrentUser();
    };

    const onUpdateTimesheetEntryError = (response) => {
        _logger('onUpdateTimesheetEntryError', response);
        toastr.error('Error updating timesheet entry');
    };

    const onAddTimesheetEntrySuccess = (response) => {
        _logger('onAddTimesheetEntrySuccess', response);
        hide();
        toastr.success('Timesheet entry added successfully');
        getAllCurrentUser();
    };

    const onAddTimesheetEntryError = (response) => {
        _logger('onAddTimesheetEntryError', response);
        toastr.error('Error adding timesheet entry');
    };

    const onPageChange = (page) => {
        _logger('onPageChange', page);
        !filters === {}
            ? timesheetService
                  .timesheetFilter(filters.organizationId, filters.dateRange, page - 1, pageData.pageSize)
                  .then(onSearchClickedSuccess)
                  .catch(onSearchClickedError)
            : timesheetService
                  .getAllCurrentUserPaginate(page - 1, pageData.pageSize)
                  .then(getAllCurrentUserPaginateSuccess)
                  .catch(getAllCurrentUserPaginateError);
        setPageData((prevState) => {
            return {
                ...prevState,
                currentPage: page,
                pageIndex: page - 1,
            };
        });
    };

    return (
        <React.Fragment>
            <div className="timesheetContainer">
                <Row>
                    <div className="col-4">
                        <h1>Timesheet</h1>
                    </div>
                    <Col xs="12" lg={10} className="timesheetTableContainer text-center">
                        <Row>
                            <Card>
                                <Card.Body>
                                    <Table className="mb-0" responsive hover>
                                        <thead>
                                            <tr>
                                                <th>
                                                    <button
                                                        className="btn btn-primary btn-sm"
                                                        type="button"
                                                        onClick={onAddClicked}
                                                        id="addBtn">
                                                        Add
                                                    </button>
                                                </th>
                                                <th>Date</th>
                                                <th>Job</th>
                                                <th>Comment</th>
                                                <th>TimeFrom</th>
                                                <th>TimeTo</th>
                                                <th>Total (hrs)</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {!filters === {}
                                                ? timesheetData.filteredEntriesMapped
                                                : timesheetData.timesheetEntriesMapped}
                                            <tr>
                                                <td colSpan="12" className="text-end">
                                                    Total: {totalHours}hrs
                                                </td>
                                            </tr>
                                        </tbody>
                                    </Table>
                                    <Pagination
                                        currentPage={pageData.currentPage}
                                        pageSize={pageData.pageSize}
                                        total={pageData.total}
                                        onChange={onPageChange}
                                        prevIcon="<<<"
                                        nextIcon=">>>"
                                    />
                                </Card.Body>
                            </Card>
                        </Row>
                    </Col>
                    <Col lg={2}>
                        <Card className="searchFormContainer">
                            <Card.Body>
                                <Row>
                                    <Col lg={12}>
                                        <SearchForm onSearch={onSearchClicked} onClear={onClearClicked} orgs={orgs} />
                                    </Col>
                                </Row>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </div>
            <div className="TimesheetModalContainer">
                {isShowing && (
                    <TimesheetForm
                        timesheetEntryData={timesheetEntryData}
                        onSubmit={onSubmitClicked}
                        isShowing={isShowing}
                        toggle={hide}
                        jobs={jobs}
                        orgs={orgs}
                    />
                )}
            </div>
        </React.Fragment>
    );
}

export default Timesheet;
