import React, { useState, useEffect } from 'react';
import Loki from 'react-loki';
import debug from 'sabio-debug';
import EventName from './EventName';
import EventLocation from './EventLocation';
import EventFinish from './EventFinish';
import EventPreview from './EventPreview';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { BsFileEarmarkTextFill } from 'react-icons/bs';
import { FaClipboardCheck, FaPenAlt, FaMapMarkedAlt } from 'react-icons/fa';
import swal from '@sweetalert/with-react';
import * as eventServices from '../../services/eventsService';
import './events.css';
import PropTypes from 'prop-types';

const _logger = debug.extend('EventAdd');

function AddEvent(props) {
    const { state } = useLocation();
    const coolState = useLocation();

    const [formData, setFormData] = useState({
        eventTypeId: '',
        title: '',
        description: '',
        imageUrl: '',
        organizationId: '',
        locationTypeId: '',
        lineOne: '',
        lineTwo: '',
        city: '',
        zip: '',
        stateId: '',
        latitude: '',
        longitude: '',
        isMultiDay: true,
        createdBy: props.currentUser.id,
    });

    const [isUpdate, setIsUpdate] = useState(false);

    let { eventId } = useParams();

    useEffect(() => {
        _logger('State', state);
        _logger('coolstate ', coolState);
        if (state?.type === 'EVENT_VIEW' && state?.payload) {
            _logger('State', state);
            const initialState = changeStateData(state.payload);
            setIsUpdate(true);
            setFormData({ ...initialState });
            _logger('eventIdToEdit', eventId);
        } else {
            _logger('Else Test');
        }
    }, [eventId, state]);

    const changeStateData = (payload) => {
        const newPayload = {
            eventTypeId: payload.eventType.id,
            title: payload.title,
            description: payload.description,
            imageUrl: payload.imageUrl,
            organizationId: payload.organization.id,
            locationTypeId: payload.location.locationType.id,
            lineOne: payload.location.lineOne,
            lineTwo: payload.location.lineTwo,
            city: payload.location.city,
            zip: payload.location.zip,
            stateId: payload.location.state.id,
            latitude: payload.location.latitude,
            longitude: payload.location.longitude,
            isMultiDay: payload.isMultiDay,
        };

        return newPayload;
    };

    const navigate = useNavigate();
    const onChange = (values) => {
        _logger('OnChange', values);
        setFormData((prevState) => {
            const sd = { ...prevState, ...values };
            return sd;
        });
    };

    const onFinish = () => {
        _logger('Finish button clicked', formData);
        if (isUpdate === true) {
            const newFormData = { ...formData };
            newFormData.locationId = state.payload.location.id;
            _logger('New Form Data', newFormData);
            eventServices.update(newFormData, eventId).then(onUpdateEventSuccess).catch(onUpdateEventError);
        } else {
            eventServices.add(formData).then(onAddEventSuccess).catch(onAddEventError);
        }
    };

    const onUpdateEventSuccess = (response) => {
        _logger('Successfuly updated', response);
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your event has successfully been update!',
            showConfirmButton: false,
            timer: 5000,
        });
        navigate(`/admin/organization/dashboard/events/${state.payload.organization.id}`);
    };

    const onUpdateEventError = (response) => {
        _logger('Couldnt Update Event', response);
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Please check the missing fields',
            icon: 'error',
        });
    };

    const onAddEventSuccess = (response) => {
        _logger('Successfuly submitted', response);
        swal({
            position: 'top-end',
            icon: 'success',
            title: 'Your event has successfully been created!',
            showConfirmButton: false,
            timer: 5000,
        });
        navigate('/admin/organization/dashboard');
    };

    const onAddEventError = (error) => {
        _logger(error, 'Could Not Add Event');
        swal({
            buttons: {
                cancel: 'Ok',
            },
            title: 'Please check the missing fields',
            icon: 'error',
        });
    };

    const wizardSteps = [
        {
            label: 'Step 1',
            icon: <FaPenAlt className="mt-2 text-center" />,
            component: <EventName isUpdate={isUpdate} formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 2',
            icon: <FaMapMarkedAlt className="mt-2 text-center" />,
            component: <EventLocation formData={formData} onChange={onChange} isUpdate={isUpdate} />,
        },
        {
            label: 'Step 3',
            icon: <BsFileEarmarkTextFill className="mt-2 text-center" />,
            component: <EventPreview formData={formData} onChange={onChange} />,
        },
        {
            label: 'Step 4',
            icon: <FaClipboardCheck className="mt-2" />,
            component: <EventFinish formData={formData} onChange={onChange} />,
        },
    ];

    return (
        <React.Fragment>
            <div className="eventWizard">
                <Loki steps={wizardSteps} onNext={onChange} onBack={onChange} onFinish={onFinish} noActions />
            </div>
        </React.Fragment>
    );
}

AddEvent.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
        roles: PropTypes.arrayOf(PropTypes.string).isRequired,
    }),
};

export default AddEvent;
