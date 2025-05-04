import React, { useState } from 'react';
import { GoogleMap, Marker } from '@react-google-maps/api';

const Map = () => {
    const [mapConfig] = useState({
        zoom: 2,
        address: null,
        center: { lat: 43.45, lng: -80.49 },
    });

    return (
        <>
            <h1 className="text-center text-primary">Autocomplete Test</h1>

            <GoogleMap
            
                zoom={mapConfig.zoom}
                center={mapConfig.center}
                mapContainerStyle={{
                    height: '600px',
                    width: '1000px',
                    margin: '0 auto',
                }}>
                {mapConfig.address && <Marker position={mapConfig.center} />}
            </GoogleMap>
        </>
    );
};

export default Map;
