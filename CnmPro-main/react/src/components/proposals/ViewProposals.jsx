import React, { useState, useEffect } from 'react';
import './proposals.css';
import debug from 'sabio-debug';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import { Row, Container } from 'react-bootstrap'
import proposalService from '../../services/proposalService';
import ProposalEntry from './ProposalEntry';
import toastr from 'toastr';

function ViewProposals() {
    const _logger = debug.extend('viewProposal');

    const [proposalData, setProposalData] = useState({
        proposalEntries: [],
        proposalEntriesMapped: [],
    });

    const [pageData, setPageData] = useState({
        currentPage: 1,
        pageSize: 5,
        pageIndex: 0,
        total: 0,
    });

    useEffect(() => {
        getAllCurrentUser();
    }, []);

    const getAllCurrentUser = () => {
        proposalService
            .selectAll(pageData.pageIndex, pageData.pageSize)
            .then(getAllProposalsPaginateSuccess)
            .catch(getAllProposalsPaginateError);
    };

    const getAllProposalsPaginateSuccess = (response) => {
        _logger(response, 'getProposals');
        setProposalData((prevState) => {
            return {
                ...prevState,
                proposalEntries: response.data.item.pagedItems,
                proposalEntriesMapped: response.data.item.pagedItems.map(proposalMapper),
            };
        });
        setPageData((prevState) => {
            return {
                ...prevState,
                total: response.data.item.totalCount,
            };
        });
    };

    _logger(proposalData.proposalEntriesMapped);

    const getAllProposalsPaginateError = (response) => {
        toastr.error('Error getting proposal entries', response);
    };

    const onPageChange = (page) => {
        _logger('onPageChange', page);

        proposalService
            .selectAll(page - 1, pageData.pageSize)
            .then(getAllProposalsPaginateSuccess)
            .catch(getAllProposalsPaginateError);
        setPageData((prevState) => {
            return {
                ...prevState,
                currentPage: page,
                pageIndex: page - 1,
            };
        });
    };

    const proposalMapper = (singleProposal) => {
        return <ProposalEntry key={singleProposal.id} singleProposal={singleProposal} />;
    };

    return (
        <React.Fragment>
            <div className="container">
                        <div className="proposals-homescreen-grid-header">
                            <div className="proposals-homescreen-grid-header-title">Recent Proposals</div>
                        </div>
                        <Container>
                        <Row>{proposalData.proposalEntriesMapped}</Row>
                        </Container>
                        <Pagination
                            currentPage={pageData.currentPage}
                            pageSize={pageData.pageSize}
                            total={pageData.total}
                            onChange={onPageChange}
                        />
            </div>
        </React.Fragment>
    );
}

export default ViewProposals;
