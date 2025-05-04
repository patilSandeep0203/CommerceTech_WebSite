import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const Container = styled.div`
    min-height: 692px;
    position: fixed;
    bottom: 0;
    left: 0;
    right: 0;
    top: 0;
    z-index: 0;
    overflow: hidden;
    background-color: #d3d3d3;
`;

export const GlassEffect = styled.div`
    background: linear-gradient(135deg, rgba(255, 255, 255, 0.1), rgba(255, 255, 255, 0));
    -webkit-backdrop-filter: blur(20px);
    backdrop-filter: blur(20px);
    box-shadow: 0 8px, 32px, 0 rgba(0, 0, 0, 0.37);
    border: 1px solid rgba(255, 255, 255, 0.18);
    border-radius: 32px;
`;

export const UnsubBG = styled.div`
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    width: 100%;
    height: 100%;
    overflow: hidden;
`;

export const VideoBG = styled.video`
    width: 100%;
    height: 100%;
    -o-object-fit: cover;
    object-fit: cover;
    background: #232a34;
`;

export const FormWrap = styled.div`
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;

    @media screen and (max-width: 480px) {
        height: 80%;
    }
`;

export const Icon = styled(Link)`
    margin-left: 32px;
    margin-top: 32px;
    text-decoration: none;
    color: #fff;
    font-weight: 700;
    font-size: 32px;

    @media screen and (max-width: 480px) {
        margin-left: 16px;
        margin-top: 8px;
    }
`;

export const FormContent = styled.div`
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;

    @media screen and (max-width: 400px) {
        padding: 10px;
    }
`;

export const Form = styled.form`
    background: linear-gradient(125deg, rgba(220, 220, 220, 0.1), rgba(220, 220, 220, 0));
    -webkit-backdrop-filter: blur(3px);
    backdrop-filter: blur(3px);
    font-family: 'Encode Sans', sans-serif;
    border-radius: 50px;
    white-space: nowrap;
    color: #010606;
    outline: none;
    border: none;
    height: auto;
    width: 400px;
    z-index: 1;
    display: grid;
    margin: 0 auto;
    padding: 50px 90px;
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.9);
    margin-bottom: 50px;

    @media screen and (max-width: 400px) {
        padding: 32px 32px;
    }
`;

export const FormH1 = styled.h1`
    margin-bottom: 40px;
    color: #fff;
    font-size: 20px;
    font-weight: 400;
    text-align: center;
`;

export const FormLabel = styled.label`
    margin-bottom: 8px;
    font-size: 14px;
    color: #fff;
`;

export const FormInput = styled.input`
    padding: 16px 16px;
    margin-bottom: 32px;
    border: none;
    border-radius: 4px;
    color: #000000;
    ::placeholder {
        text-align: center;
    }
`;

export const FormButton = styled.button`
    border-radius: 50px;
    background: #d3d3d3;
    white-space: nowrap;
    padding: 10px 22px;
    color: #010606;
    outline: none;
    border: none;
    font-size: 20px;
    cursor: pointer;
    transition: all 0.2s ease-in-out;
    text-decoration: none;

    &:hover {
        transition: all 0.2s ease-in-out;
        background: #00ff7f;
        color: #010606;
    }
`;

export const Text = styled.span`
    text-align: center;
    margin-top: 24px;
    color: #fff;
    font-size: 14px;
`;
