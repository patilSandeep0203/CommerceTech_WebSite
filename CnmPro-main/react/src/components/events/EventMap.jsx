import React, { useState } from 'react';
import { GoogleMap, Marker, LoadScript } from '@react-google-maps/api';

const appId = process.env.REACT_APP_GOOGLE_MAPS_API_KEY;
const EventMap = () => {
    const [mapConfig] = useState({
        zoom: 2,
        address: null,
        center: { lat: 43.45, lng: -80.49 },
        location: {},
    });

    return (
        <>
            <LoadScript googleMapsApiKey={appId}>
                <GoogleMap
                    zoom={mapConfig.zoom}
                    center={mapConfig.center}
                    mapContainerStyle={{
                        height: '400px',
                        width: '700px',
                        margin: '0 auto',
                    }}>
                    {mapConfig.location && <Marker position={mapConfig.center} />}
                </GoogleMap>
            </LoadScript>
        </>
    );
};

export default React.memo(EventMap);
