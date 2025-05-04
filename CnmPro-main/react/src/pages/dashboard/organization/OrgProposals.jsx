import React, { useEffect, useState } from 'react';
import Table from './Table';
import orgAdminDashServices from '../../../services/orgAdminDashboardService';
import logger from 'sabio-debug';
import { useParams } from 'react-router-dom';
import PropTypes from 'prop-types';
import moment from 'moment';
import proposalService from '../../../services/proposalService';
import Swal from 'sweetalert2';
import toastr from 'toastr';

const _logger = logger.extend('OrgAdminChart');
function OrgProposals() {
    const [proposalData, setProposalData] = useState();
    const [totalPages, setTotalPages] = useState(0);
    const [current, setCurrent] = useState(0);
    const [isClickedApproved, setIsClickedApproved] = useState(false);
    const [isClickedDenied, setIsClickedDenied] = useState(false);
    const [rowId, setRowId] = useState(null);

    const onChangeOfPageIndex = (page) => {
        setCurrent(page - 1);
    };

    let { orgId } = useParams();

    useEffect(() => {
        orgAdminDashServices.getProposalsByOrganizationPaginate(orgId, current, 4).then(onGetSuccess).catch(onGetError);
    }, [current]);

    const onGetSuccess = (response) => {
        const data = response.item.pagedItems;

        const newData = data.map((item) => {
            const copyItem = { ...item };
            copyItem.dateCreated = moment(copyItem.dateCreated).format('MM/DD/YY');
            copyItem.actions = (
                <div>
                    <button
                        id={item.id}
                        className="btn btn-success me-1"
                        onClick={onApproveClick}
                        disabled={item.proposalStatus === 'Accepted'}>
                        Approve
                    </button>
                    <button
                        id={item.id}
                        className="btn btn-danger"
                        onClick={onDenyClick}
                        disabled={item.proposalStatus === 'Denied'}>
                        Deny
                    </button>
                </div>
            );
            return copyItem;
        });
        setProposalData(newData);
        setTotalPages(response.item.totalCount);
    };

    const onGetError = (response) => {
        _logger('Error', response);
        toastr.error("Couldn't get proposals");
    };

    useEffect(() => {
        updateItemStatus(rowId, 2);
    }, [isClickedApproved]);

    useEffect(() => {
        updateItemStatus(rowId, 3);
    }, [isClickedDenied]);

    const updateItemStatus = (id, proposalId) => {
        if (rowId !== null) {
            const itemToUpdate = proposalData.filter((item) => {
                return item.id === Number(id);
            });

            const objectData = itemToUpdate[0];

            const payload = {
                id: objectData.id,
                proposalId: proposalId,
                jobId: objectData.jobId,
                bid: objectData.bid,
                duration: objectData.duration,
                details: objectData.details,
            };

            proposalService.update(payload).then(onUpdateSuccess).catch(onUpdateError);
        }
    };

    const onUpdateSuccess = (response) => {
        const data = JSON.parse(response.config.data);
        setProposalData((prevState) => {
            const propData = [...prevState];
            const findIndex = propData.findIndex((item) => item.id === data.id);
            if (data.proposalId === 2) {
                const emailPayload = {
                    subject: 'Proposal Denied',
                    body: 'I am sorry to inform you but proposal has been denied',
                    plainText: 'I am sorry to inform you but proposal has been denied',
                    email: data.email,
                };
                orgAdminDashServices.sendApprovedEmail(emailPayload).then(onEmailSuccess).catch(onEmailError);
                propData[findIndex].proposalStatus = 'Accepted';

                propData[findIndex].actions = (
                    <div>
                        <button id={data.id} className="btn btn-success me-1" onClick={onApproveClick} disabled>
                            Approve
                        </button>
                        <button id={data.id} className="btn btn-danger" onClick={onDenyClick}>
                            Deny
                        </button>
                    </div>
                );
            } else if (data.proposalId === 3) {
                const emailPayload = {
                    subject: 'Proposal Denied',
                    body: 'I am sorry to inform you but proposal has been denied',
                    plainText: 'I am sorry to inform you but proposal has been denied',
                    email: data.email,
                };
                orgAdminDashServices.sendDeniedEmail(emailPayload).then(onEmailSuccess).catch(onEmailError);
                propData[findIndex].proposalStatus = 'Denied';

                propData[findIndex].actions = (
                    <div>
                        <button id={data.id} className="btn btn-success me-1" onClick={onApproveClick}>
                            Approve
                        </button>
                        <button id={data.id} className="btn btn-danger" onClick={onDenyClick} disabled>
                            Deny
                        </button>
                    </div>
                );
            }

            return propData;
        });
        setIsClickedApproved(false);
        setIsClickedDenied(false);
        setRowId(null);
    };

    const onEmailSuccess = (response) => {
        _logger('Email Success', response);
        toastr.success('Email was sent to user');
    };

    const onEmailError = (response) => {
        _logger('Email Error', response);
        toastr.error('Email was not sent to user');
    };

    const onUpdateError = (response) => {
        _logger('Update Error', response);
        toastr.error("Couldn't update proposal");
    };

    const onApproveClick = (event) => {
        event.preventDefault();
        setRowId(event.target.id);
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
                confirmButtonText: 'Yes, Approve it!',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true,
            })
            .then((result) => {
                if (result.isConfirmed) {
                    swalWithBootstrapButtons.fire('Approved', 'Proposal has been accepted!', 'success');
                    setIsClickedApproved(true);
                } else if (
                    /* Read more about handling dismissals below */
                    result.dismiss === Swal.DismissReason.cancel
                ) {
                }
            });
    };

    const onDenyClick = (event) => {
        event.preventDefault();
        setRowId(event.target.id);

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
                confirmButtonText: 'Yes, Deny it!',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true,
            })
            .then((result) => {
                if (result.isConfirmed) {
                    swalWithBootstrapButtons.fire('Denied!', 'Proposal has been denied!', 'success');

                    setIsClickedDenied(true);
                } else if (
                    /* Read more about handling dismissals below */
                    result.dismiss === Swal.DismissReason.cancel
                ) {
                }
            });
    };

    const columns = [
        {
            Header: 'ID',
            accessor: 'id',
            sort: true,
        },
        {
            Header: 'Job ID',
            accessor: 'jobId',
            sort: false,
        },
        {
            Header: 'Job Title',
            accessor: 'jobTitle',
            sort: false,
        },

        {
            Header: 'Job Type',
            accessor: 'jobType.name',
            sort: false,
        },
        {
            Header: 'Occupation',
            accessor: 'occupation',
            sort: false,
        },
        {
            Header: 'Bid',
            accessor: 'bid',
            sort: false,
        },
        {
            Header: 'Details',
            accessor: 'details',
            sort: false,
        },

        {
            Header: 'Duration',
            accessor: 'duration',
            sort: false,
        },
        {
            Header: 'Created By',
            accessor: 'createdBy',
            sort: false,
        },
        {
            Header: 'Email',
            accessor: 'email',
            sort: false,
        },
        {
            Header: 'Date Created',
            accessor: 'dateCreated',
            sort: true,
        },
        {
            Header: 'Proposal Status',
            accessor: 'proposalStatus',
            sort: false,
        },
        {
            Header: 'Actions',
            accessor: 'actions',
            sort: false,
        },
    ];
    return (
        <>
            <div className="mt-3">
                <h1 className="orgDash-color-black ms-1 orgDash-mb-2">Job Proposals</h1>
                <Table
                    columns={columns}
                    data={proposalData}
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

OrgProposals.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
        roles: PropTypes.arrayOf(PropTypes.string).isRequired,
    }),
};

export default OrgProposals;
