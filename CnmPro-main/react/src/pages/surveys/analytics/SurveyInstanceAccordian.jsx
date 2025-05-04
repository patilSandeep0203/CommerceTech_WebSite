import React, { useState, useEffect } from 'react';
import { Accordion } from 'react-bootstrap';
import surveysInstanceService from '../../../services/surveysInstanceService.js';
import { PropTypes } from 'prop-types';
import './surveys.css';
import toastr from 'toastr';
import moment from 'moment-timezone';
import debug from 'sabio-debug';
import InstanceDataList from './InstanceDataList';
import SimpleBar from 'simplebar-react';
const _logger = debug.extend('surveys');

const SurveyInstanceAccordion = (props) => {
    _logger(props);
    const [tableState, setTableState] = useState({
        arrayofInstances: [],
        mappedInstances: [],
        currPage: 1,
        pageIndex: 0,
        pageSize: 20,
        totalCount: 0,
    });
    useEffect(() => {
        _logger('use effect');
        surveysInstanceService
            .getAllByIdPagination(tableState.pageIndex, tableState.pageSize, props.surveyId)
            .then(onGetInstancesSuccess)
            .catch(onGetInstanceFail);
    }, []);

    const onGetInstancesSuccess = (response) => {
        _logger('got instances');
        setTableState((prevState) => {
            const tState = { ...prevState };
            tState.arrayofInstances = response.item.pagedItems;
            tState.totalCount = response.item.totalCount;
            tState.mappedInstances = tState.arrayofInstances.map(instanceAccordionItem);
            return tState;
        });
    };

    const onGetInstanceFail = (response) => {
        _logger('Failed to get instances', response);
        toastr.error('Failed to get instances');
    };

    const instanceAccordionItem = (instance) => {
        let utc = new Date(moment.tz(instance.dateCreated, 'UTC').format());
        const userLocale = navigator.language;
        const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
        let date = utc.toLocaleString(userLocale, { timeZone: timezone });

        return (
            <Accordion.Item as="div" eventKey={instance.id} key={instance.id} className="surveys-accordion">
                <Accordion.Header as="div" className="surveys-accordion">
                    {date}
                </Accordion.Header>
                <Accordion.Body>
                    <InstanceDataList detail={instance} />
                </Accordion.Body>
            </Accordion.Item>
        );
    };

    return (
        <React.Fragment>
            <p className="instance-accordian-title fw-bold">RECENT RESPONSES</p>
            <SimpleBar className="px-3" style={{ maxHeight: '285px', width: '100%' }}>
                <Accordion>{tableState.mappedInstances}</Accordion>
            </SimpleBar>
        </React.Fragment>
    );
};

SurveyInstanceAccordion.propTypes = {
    surveyId: PropTypes.string.isRequired,
};

export default SurveyInstanceAccordion;
