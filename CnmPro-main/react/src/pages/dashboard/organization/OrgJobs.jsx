import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import orgAdminDashServices from '../../../services/orgAdminDashboardService';
import * as jobsService from '../../../services/jobsService';
import logger from 'sabio-debug';
import Table from './Table';
import './orgdash.css';
import moment from 'moment';
import toastr from 'toastr';
import Swal from 'sweetalert2';

const _logger = logger.extend('OrgAdminChart');

function OrgJobs() {
    let { orgId } = useParams();
    const [jobData, setJobData] = useState();
    const [current, setCurrent] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [isEditClicked, setIsEditClicked] = useState(false);
    const [isDeleteClicked, setIsDeleteClicked] = useState(false);
    const [jobId, setJobId] = useState(null);
    const navigate = useNavigate();
    const currentPageSize = 10;

    const onChangeOfPageIndex = (page) => {
        setCurrent(page - 1);
    };

    useEffect(() => {
        orgAdminDashServices
            .getJobsByOrganizationPaginate(orgId, current, currentPageSize)
            .then(onGetSuccess)
            .catch(onGetError);
    }, [current]);

    useEffect(() => {
        editItemById(jobId);
    }, [isEditClicked]);

    useEffect(() => {
        deleteItemById(jobId);
    }, [isDeleteClicked]);

    const deleteItemById = (id) => {
        if (id !== null) {
            _logger('Row Id', id);
            jobsService.deleteById(id).then(onDeleteSuccess).catch(onDeleteError);
        }
    };

    const onDeleteSuccess = (response) => {
        _logger('Successful Delete', response);

        setJobData((prevState) => {
            const newJobData = [...prevState];
            let indexToDelete = newJobData.findIndex((job) => {
                if (job.id === Number(jobId)) {
                    return true;
                }
                return false;
            });

            if (indexToDelete >= 0) {
                newJobData.splice(indexToDelete, 1);
            }
            return newJobData;
        });
    };

    const onDeleteError = (response) => {
        _logger('Unsuccessful Delete', response);
    };

    const editItemById = (id) => {
        if (id !== null) {
            const dataToEdit = jobData.filter((item) => item.id === Number(id));
            dataToEdit[0].actions = '';
            let stateJob = { type: 'JOB_VIEW', payload: dataToEdit[0] };
            _logger('statejob', stateJob);
            navigate(`/admin/organization/job/edit/${id}`, { state: stateJob });
        }
    };

    const onGetSuccess = (response) => {
        _logger('SuccessJob', response);
        const data = response.item.pagedItems;
        const newData = data.map((item) => {
            item.dateCreated = moment(item.dateCreate).format('MM/DD/YY');

            item.actions = (
                <div>
                    <button id={item.id} className="btn btn-success me-1" onClick={onEditClick}>
                        Edit
                    </button>
                    <button id={item.id} className="btn btn-danger" onClick={onDeleteClick}>
                        Delete
                    </button>
                </div>
            );
            return item;
        });
        setJobData(newData);
        setTotalPages(response.item.totalCount);
    };

    const onEditClick = (event) => {
        event.preventDefault();
        _logger('Job Id', event.target.id);
        setIsEditClicked(true);
        setJobId(event.target.id);
    };

    const onDeleteClick = (event) => {
        event.preventDefault();
        setJobId(event.target.id);

        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger me-1',
            },
            buttonsStyling: false,
        });

        swalWithBootstrapButtons
            .fire({
                title: 'Are you sure?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, Delete it!',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true,
            })
            .then((result) => {
                if (result.isConfirmed) {
                    swalWithBootstrapButtons.fire('Deleted', 'Job has been deleted!', 'success');
                    setIsDeleteClicked(true);
                } else if (
                    /* Read more about handling dismissals below */
                    result.dismiss === Swal.DismissReason.cancel
                ) {
                }
            });
    };

    const onGetError = (response) => {
        _logger('Error', response);
        toastr.error("Couldn't get jobs");
    };

    const columns = [
        { Header: 'Title', accessor: 'title', sort: true },
        { Header: 'Description', accessor: 'description', sort: false },
        { Header: 'Requirements', accessor: 'requirements', sort: false },
        { Header: 'Job Type', accessor: 'jobType.name', sort: false },
        { Header: 'City', accessor: 'location.city', sort: false },
        { Header: 'State', accessor: 'location.state.name', sort: false },
        { Header: 'Zip Code', accessor: 'location.zip', sort: false },
        { Header: 'Date Created', accessor: 'dateCreated', sort: false },
        { Header: 'Actions', accessor: 'actions', sort: false },
    ];
    return (
        <>
            <div className="mt-3">
                <h1 className="orgDash-color-black ms-1 orgDash-mb-2">Jobs</h1>
                <Table
                    columns={columns}
                    data={jobData}
                    isSortable={true}
                    hasPagination={true}
                    totalPages={totalPages}
                    current={current}
                    onChangeOfPageIndex={onChangeOfPageIndex}
                />
            </div>
        </>
    );
}

export default OrgJobs;
