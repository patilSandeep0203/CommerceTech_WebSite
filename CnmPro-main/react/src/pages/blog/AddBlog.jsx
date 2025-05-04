import './blog.css';
import BlogPreviewCard from './BlogPreviewCard';
import BlogService from '../../services/blogService';
import blogValidation from '../../schema/blog/blogValidation';
import debug from 'sabio-debug';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import lookUpService from '../../services/lookUpService';
import React, { useState, useEffect } from 'react';
import TextEditor from './TextEditor';
import toastr from 'toastr';
import UserService from '../../services/userService';
import FileUploader from '../../components/uploader/FileUploader';
import { useLocation } from 'react-router-dom';
import { onGlobalError, onGlobalSuccess } from '../../services/serviceHelpers';
import PropTypes from 'prop-types';

const _logger = debug.extend('Blog');

function AddBlog(props) {
    _logger(props.currentUser.id, 'currentUser Id');

    const location = useLocation();
    const [editorLoaded, setEditorLoaded] = useState();
    const [arrayOfBlogTypes, setArrayOfBlogTypes] = useState([]);

    _logger('array of blogs:', arrayOfBlogTypes);

    const [blogFormData, setBlogFormData] = useState({
        title: '',
        subject: '',
        blogTypeId: '',
        imageUrl: '',
        content: '',
        datePublish: '',
    });

    useEffect(() => {
        _logger('useEffect for content editor');
        setEditorLoaded('true');
        _logger('useEffect for lookup service');
        lookUpService.LookUp(['BlogTypes']).then(onTypeSuccess, onGlobalSuccess).catch(onError, onGlobalError);
        _logger('useEffect for get current user');
        UserService.current().then().catch(onError);
        _logger('useEffect for userService');
    }, []);

    useEffect(() => {
        if (location.state) {
            _logger('location', location);
            let payload = location.state.payload;
            setBlogFormData({
                title: payload?.title,
                subject: payload?.subject,
                blogTypeId: payload?.blogTypeId,
                imageUrl: payload?.imageUrl,
                content: payload?.content,
                datePublish: payload?.datePublish,
                authorId: props.currentUser.id,
            });
        }
    }, []);

    const mapBlogTypes = (arrayOfBlogTypes) => {
        return (
            <option key={arrayOfBlogTypes.id} value={arrayOfBlogTypes.id}>
                {arrayOfBlogTypes.name}
            </option>
        );
    };

    const onTypeSuccess = (response) => {
        _logger(response);

        let arrayOfTypes = response.item.blogTypes;
        _logger({ arrayOfTypes });

        setArrayOfBlogTypes(() => {
            return arrayOfTypes;
        });
    };

    const onError = (err) => {
        toastr.error('Blog Type Menu Failed');
        _logger(err);
    };

    const handleSubmit = (values) => {
        _logger('onSubmit', values);
        _logger(blogFormData);
        if (values && values.id) {
            BlogService.updateBlog(values, values.id).then(onUpdateSuccess).catch(onUpdateError);
        } else {
            BlogService.addBlog(values).then(onAddSuccess).catch(onAddError);
        }
    };

    const onAddSuccess = (response) => {
        toastr.success('Blog Created');
        _logger(response);
    };
    const onAddError = (err) => {
        toastr.error('Submit Failed');
        _logger(err);
    };
    const onUpdateSuccess = (response) => {
        toastr.success('Update Successful');
        _logger('Blog updated', response);
    };
    const onUpdateError = (err) => {
        toastr.error('Blog Update Failed');
        _logger('Update Error', err);
    };

    const onFormFieldChange = (values) => {
        _logger('from CKEditor', values);
        setBlogFormData((prevState) => {
            let pd = { ...prevState };
            pd.content = values;
            return pd;
        });
    };

    const handleChange = (event) => {
        _logger('onChange');
        const target = event.target;
        const newBlogValue = target.value;
        const nameOfField = target.name;

        setBlogFormData((prevState) => {
            _logger('updater onChange');
            const newBlogObject = {
                ...prevState,
            };
            newBlogObject[nameOfField] = newBlogValue;
            return newBlogObject;
        });
        _logger('end onChange');
    };

    const handleUploadSuccess = (response, setFieldValue) => {
        _logger(response);
        let imageFile = response.items.map((item) => item.url);
        _logger(imageFile);
        setFieldValue('imageUrl', imageFile[0]);
    };

    return (
        <React.Fragment>
            <div className="container">
                <div className="row">
                    <div className="col-4 title-blog-cnmpro">
                        <h1>Create a Blog</h1>
                    </div>
                </div>
            </div>
            <div className="container">
                <Formik
                    enableReinitialize={true}
                    initialValues={blogFormData}
                    values={blogFormData}
                    validationSchema={blogValidation}
                    onSubmit={handleSubmit}
                    onChange={handleChange}>
                    {({ values, setFieldValue }) => (
                        <Form className="body-blog-cnmpro">
                            <div className="row">
                                <div className="col-8">
                                    <div className="row label-blog-cnmpro">
                                        <div className="col">
                                            <div className="form-group">
                                                <label htmlFor="title">Title</label>
                                                <Field
                                                    type="text"
                                                    name="title"
                                                    className="form-control"
                                                    onChange={handleChange}
                                                />
                                                <ErrorMessage name="title" component="div" style={{ color: 'red' }} />
                                            </div>
                                        </div>
                                        <div className="col">
                                            <div className="form-group">
                                                <label htmlFor="subject">Subject</label>
                                                <Field
                                                    type="text"
                                                    name="subject"
                                                    className="form-control"
                                                    onChange={handleChange}
                                                />
                                                <ErrorMessage name="subject" component="div" style={{ color: 'red' }} />
                                            </div>
                                        </div>
                                    </div>

                                    <div className="row label-blog-cnmpro">
                                        <div className="col-6">
                                            <div className="form-group">
                                                <label htmlFor="blogType">Blog Type</label>
                                                <Field
                                                    component="select"
                                                    name="blogTypeId"
                                                    className="form-control"
                                                    onChange={handleChange}
                                                    value={values.blogTypeId}>
                                                    <option value="">Please select a blog type</option>
                                                    {arrayOfBlogTypes.map(mapBlogTypes)}
                                                </Field>
                                            </div>
                                        </div>
                                        <div className="col-3">
                                            <div className="form-group">
                                                <label htmlFor="datePublish">Date Published</label>
                                                <Field
                                                    type="date"
                                                    name="datePublish"
                                                    className="form-control"
                                                    onChange={handleChange}
                                                />
                                                <ErrorMessage
                                                    name="datePublish"
                                                    component="div"
                                                    style={{ color: 'red' }}
                                                />
                                            </div>
                                        </div>
                                        <div className="col">
                                            <div className="form-check check-blog-cnmpro">
                                                <Field
                                                    type="checkbox"
                                                    name="isPublished"
                                                    className="form-check-input"
                                                />
                                                <label className="form-check-label" htmlFor="isPublisherd">
                                                    Is Published
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="content">Content</label>
                                        <div className="form-group label-blog-cnmpro">
                                            <TextEditor
                                                name="content"
                                                editorLoaded={editorLoaded}
                                                onChange={onFormFieldChange}
                                            />
                                        </div>
                                    </div>
                                    <div className="row"></div>
                                    <FileUploader
                                        onHandleUploadSuccess={(response) =>
                                            handleUploadSuccess(response, setFieldValue)
                                        }
                                        name="fileUploader"
                                    />
                                    <div className="row">
                                        <div className="col blog-button-cnmpro">
                                            <button type="submit" className="btn btn-primary">
                                                {location.state?.isEditMode === true ? 'Update' : 'Submit'}
                                            </button>
                                        </div>
                                    </div>
                                    <hr />
                                </div>
                                <div className="col-4 text-right">
                                    <div className="row">
                                        <div className="row">{<BlogPreviewCard data={values} />}</div>
                                    </div>
                                </div>
                            </div>
                        </Form>
                    )}
                </Formik>
            </div>
        </React.Fragment>
    );
}

AddBlog.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
};

export default AddBlog;
