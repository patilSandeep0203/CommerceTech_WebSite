import React, { useState, useEffect } from 'react';
import { LoadScript, Autocomplete } from '@react-google-maps/api';
import PropTypes from 'prop-types';
import debug from 'sabio-debug';
import lookUpService from '../../services/lookUpService';
import './locationStyle.css';

const _loggerAutocomplete = debug.extend('Autocomplete');

const AddressAutocomplete = (props) => {
    const apiKey = process.env.REACT_APP_GOOGLE_MAPS_API_KEY;
    const onAddressSelected = props.onAddressSelected;
    const [statesData, setStatesData] = useState(null);
    const autocompleteConfig = {
        componentRestrictions: { country: 'us' },
        fields: ['address_components', 'geometry'],
    };
    const [selectedAddress, setSelectedAddress] = useState(null);

    useEffect(() => {
        lookUpService.LookUp(['states', 'locationtypes']).then(onGetStatesSuccess).catch(onGetStatesError);
    }, []);

    const onGetStatesSuccess = (response) => {
        if (response?.item?.states?.length > 0) {
            setStatesData(response.item.states);
        }
    };

    const onGetStatesError = (error) => {
        _loggerAutocomplete(error);
    };

    const onLoad = (autocomplete) => {
        setSelectedAddress(autocomplete);
    };

    const onAddressChange = () => {
        if (selectedAddress !== null) {
            const place = selectedAddress.getPlace();
            let lineOne = '';
            let lineTwo = '';
            let zipCode;
            let stateCode;
            let stateId;
            let city;

            place.address_components.forEach((addressComponent) => {
                addressComponent.types.includes('street_number')
                    ? (lineOne += addressComponent.short_name)
                    : (lineOne += '');
                addressComponent.types.includes('route')
                    ? (lineOne += ' ' + addressComponent.short_name)
                    : (lineOne += '');

                addressComponent.types.includes('country')
                    ? (lineTwo += ', ' + addressComponent.long_name)
                    : (lineTwo += '');
                addressComponent.types.includes('administrative_area_level_1')
                    ? (stateCode = addressComponent.short_name)
                    : (stateCode += '');

                if (!city) {
                    addressComponent.types.includes('locality')
                        ? (city = addressComponent.short_name)
                        : (lineTwo += '');
                }

                if (!zipCode) {
                    addressComponent.types.includes('postal_code')
                        ? (zipCode = addressComponent.short_name)
                        : (zipCode = null);
                }
            });

            if (stateCode) {
                stateId = statesData.filter((x) => x.name === stateCode);
            }

            const objResult = {
                lineOne: lineOne,
                lineTwo: '',
                city: city,
                zip: zipCode,
                stateId: stateId[0].id,
                latitude: place.geometry.location.lat(),
                longitude: place.geometry.location.lng(),
            };

            onAddressSelected(objResult);
        }
    };

    return (
        <>
            <div className="mb-3">
                <LoadScript googleMapsApiKey={apiKey} libraries={['places']}>
                    <Autocomplete options={autocompleteConfig} onLoad={onLoad} onPlaceChanged={onAddressChange}>
                        <input type="text" placeholder="Search an Address" className="form-control" />
                    </Autocomplete>
                </LoadScript>
            </div>
        </>
    );
};

AddressAutocomplete.propTypes = {
    onAddressSelected: PropTypes.func.isRequired,
};

export default AddressAutocomplete;
