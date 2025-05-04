import { React, useRef, useEffect, useState } from 'react';
import DailyIframe from '@daily-co/daily-js';
import styled from 'styled-components';
import { useLocation } from 'react-router-dom';
import logger from 'sabio-debug';
import NoDaily from './NoDaily';

const _logger = logger.extend('webinar');

const CALL_OPTIONS = {
    iframeStyle: {
        width: '100%',
        height: '100%',
        border: '1px solid #e6eaef',
        borderRadius: '6px 6px 0 0',
    },
    showLeaveButton: true,
    showFullscreenButton: true,
    //   showLocalVideo: false,
    //   showParticipantsBar: false,
};

const DEFAULT_HEIGHT = 710;

const WebinarCall = () => {
    const location = useLocation();
    const videoRef = useRef(null);
    const [height, setHeight] = useState(DEFAULT_HEIGHT);
    const [callframe, setCallframe] = useState(null);

    useEffect(() => {
        setUpVideo();
    }, [videoRef]);

    const setUpVideo = async () => {
        try {
            if (!videoRef || !videoRef?.current || callframe) {
                return;
            }

            if (location.state) {
                _logger('location not null ->', location);

                CALL_OPTIONS.url = location.state.url;
            }
        } catch {
            _logger('location state null ->', location);
        }

        const newCallframe = await DailyIframe.createFrame(videoRef.current, CALL_OPTIONS);
        try {
            if (newCallframe) {
                _logger('newCallframe not null ->', newCallframe);

                newCallframe.join().then(() => {
                    setHeight((videoRef?.current?.clientWidth || 500) * 0.75);
                    setCallframe(newCallframe);
                });
            }
        } catch {
            _logger('newCallframe null ->', newCallframe);
        }
    };

    if (!location.state) {
        return NoDaily();
    } else {
        return (
            <div>
                <Header>Welcome to our Webinar!</Header>
                <VideoContainer height={height}>
                    <Callframe ref={videoRef} />
                </VideoContainer>
            </div>
        );
    }
};

//PREBUILT. Don't change
const Header = styled.div`
    font-size: 36px;
    text-align: center;
    padding: 24px;
`;
const VideoContainer = styled.div`
    margin: auto;
    max-width: 1000px;
    height: ${(props) => (props.hidden ? '100' : props.height)}px;
`;
const Callframe = styled.div`
    width: 100%;
    height: 100%;
`;

export default WebinarCall;
