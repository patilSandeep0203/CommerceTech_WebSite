import React from 'react';
import {
    Container,
    FormWrap,
    Icon,
    FormContent,
    Form,
    FormH1,
    FormLabel,
    FormInput,
    FormButton,
} from './styledcomponents/unsubStyledComponents.js';
import Video from './videos/bgvideo.mp4';
import { Unsub, VideoBG } from './styledcomponents/unsubStyledComponents.js';

const UnSubscribe = () => {
    return (
        <>
            <Container>
                <Unsub className="unsubscribe-glass-effect">
                    <VideoBG autoPlay loop muted src={Video} type="video/mp4" />
                </Unsub>
                <FormWrap>
                    <Icon to="/">CNM</Icon>
                    <FormContent>
                        <Form action="#">
                            <FormH1>Come Back Soon!</FormH1>
                            <FormLabel htmlFor="for"></FormLabel>
                            <FormInput type="email" placeholder="Email" required />
                            <FormButton type="submit">Unsubscribe</FormButton>
                        </Form>
                    </FormContent>
                </FormWrap>
            </Container>
        </>
    );
};

export default UnSubscribe;
