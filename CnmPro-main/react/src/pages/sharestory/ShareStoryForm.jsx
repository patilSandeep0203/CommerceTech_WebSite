import React, { useEffect, useState } from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { Row, Col, Container } from 'react-bootstrap';
import debug from 'sabio-debug';
import shareStorySchema from './shareStorySchema';
import * as shareStoryService from '../../services/shareStoryService';
import FileUploader from '../../components/uploader/FileUploader';
import toastr from 'toastr';
import { useLocation, useNavigate, Link } from 'react-router-dom';
import Preview from './StoryPreviewCard';

const _logger = debug.extend('sharestory');

function ShareStoryForm(data) {
    const location = useLocation();
    const email = data.currentUser.email;
    const [formData, setFormData] = useState({
        name: '',
        email: email,
        story: '',
        fileIds: [],
        urls: [],
    });

    useEffect(() => {
        if (location.state) {
            let { payload } = location.state;
            setFormData({
                id: payload.id,
                name: payload?.name,
                email: email,
                story: payload?.story,
                fileIds: payload?.fileIds,
            });
        }
    }, []);
    const navigate = useNavigate();
    function handleSubmit(values) {
        _logger('values', values);
        // const {  type } = location.state;
        if (location?.state?.type === 'update') {
            shareStoryService.update(values).then(onUpdateSuccess).catch(onUpdateFail);
        } else {
            shareStoryService.addStory(values).then(onAddSuccess).catch(onAddFail);
        }
    }
    const onAddSuccess = (response) => {
        const stateForTransport = { type: 'story_view' };
        toastr.success('Story added Success');
        _logger('Story was added', response);
        navigate(`/sharestories`, { state: stateForTransport });
    };
    const onAddFail = (error) => {
        toastr.error('Story add Error?');
        _logger('Story add Error', error);
    };
    const onUpdateSuccess = (response) => {
        const stateForTransport = { type: 'story_view' };
        toastr.success('Story was updated Success');
        _logger('Story was updated', response);
        navigate(`/sharestories`, { state: stateForTransport });
    };
    const onUpdateFail = (error) => {
        toastr.error('Story update Error?');
        _logger('Story update Error', error);
    };

    const handleUploadSuccess = (response, setFieldValue) => {
        let fileIds = response.items.map((item) => item.id);
        setFormData((prev) => ({ ...prev, fileIds, urls: response.items.map((x) => x.url) }));
        setFieldValue('fileIds', fileIds);
    };

    const handlePreview = (event) => {
        _logger('preview showing', event);
        const { name, value } = event.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    return (
        <React.Fragment>
            <Container>
                <Link to="/sharestories" className="btn btn-primary" type="button">
                    {' '}
                    Back
                </Link>
                <h1 className="row d-flex justify-content-center " style={{ padding: '10px' }}>
                    Share Your Mentoring Story
                </h1>
                <Row>
                    <Col>
                        <Container fluid style={{ maxWidth: '550px', textAlign: 'center' }}>
                            <Row className="row d-flex justify-content-center">
                                <Col className="col-md-12 bg-light border rounded-3 p-3">
                                    <Formik
                                        enableReinitialize={true}
                                        initialValues={formData}
                                        onSubmit={handleSubmit}
                                        validationSchema={shareStorySchema}>
                                        {({ setFieldValue, values, submitForm }) => (
                                            <Form className="mt-4">
                                                <div className="form-group">
                                                    <label htmlFor="name">Name</label>
                                                    <Field
                                                        type="text"
                                                        name="name"
                                                        className="form-control"
                                                        value={values.name}
                                                        onKeyUp={handlePreview}
                                                    />
                                                    <ErrorMessage name="name" component="div" className="text-danger" />
                                                </div>
                                                <div className="d-none">
                                                    <label htmlFor="email">Email</label>
                                                    <Field
                                                        type="email"
                                                        name="email"
                                                        className="form-control"
                                                        value={values.email}
                                                    />
                                                    <ErrorMessage
                                                        name="email"
                                                        component="div"
                                                        className="text-danger"
                                                    />
                                                </div>
                                                <div className="form-group">
                                                    <label htmlFor="story">Write your story</label>
                                                    <Field
                                                        component="textarea"
                                                        name="story"
                                                        rows="5"
                                                        className="form-control"
                                                        value={values.story}
                                                        onKeyUp={handlePreview}
                                                    />
                                                    <ErrorMessage
                                                        name="story"
                                                        component="div"
                                                        className="text-danger"
                                                    />
                                                </div>
                                                <div>
                                                    <FileUploader
                                                        onHandleUploadSuccess={(response) =>
                                                            handleUploadSuccess(response, setFieldValue)
                                                        }
                                                        name="fileUploader"
                                                    />
                                                </div>
                                                <Row
                                                    style={{ marginBottom: '-18px' }}
                                                    className="justify-content-center">
                                                    {' '}
                                                    <button
                                                        type="button"
                                                        onClick={async () => {
                                                            _logger('Clicked');
                                                            await submitForm();
                                                        }}
                                                        className="btn btn-light"
                                                        style={{ marginTop: 'flex' }}>
                                                        {location.state?.isEditMode === true ? 'UPDATE' : 'SUBMIT'}
                                                    </button>
                                                </Row>
                                            </Form>
                                        )}
                                    </Formik>
                                </Col>
                            </Row>
                        </Container>
                    </Col>
                    <Col className="col-4" style={{ marginTop: '100px' }}>
                        <div className="row-4">{<Preview story={formData} />}</div>
                    </Col>
                </Row>
            </Container>
        </React.Fragment>
    );
}
export default ShareStoryForm;
