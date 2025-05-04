import debug from 'sabio-debug';
import React, { useState, useEffect, useCallback } from 'react';
import { Button, Col, Dropdown, Row } from 'react-bootstrap';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as resourceCategoriesService from '../../services/resourceCategoriesService';
import lookUpService from '../../services/lookUpService';
import ResourceCategory from './ResourceCategory';
import Swal from 'sweetalert2';
import PropTypes from 'prop-types';
import './resources.css';

const _logger = debug.extend('ResourceCategories');

function ResourceCategories(props) {
    const [resourceCategoryData, setResourceCategoryData] = useState({
        arrayOfResourceCategories: [],
        cardComponents: [],
        resourceCategory: '',
        isCreated: false,
    });
    const [resourceCategories, setResourceCategories] = useState();
    const [showAddInput, setShowAddInput] = useState(false);
    const [selection, setSelection] = useState('Resource Categories');
    useEffect(() => {
        _logger('Firing useEffect for get resources');
        lookUpService.LookUp(['ResourceCategories']).then(onSuccessLookUp).catch(onLookUpError);
    }, [resourceCategoryData.isCreated]);

    const onSuccessLookUp = (response) => {
        _logger('onSuccessLookup response', response);
        let resourceCategoryArray = response.item.resourceCategories;
        _logger('onSuccessLookup Array', resourceCategoryArray);
        const resources = response.item.resourceCategories.map((resource, index) => {
            _logger(resource);
            return (
                <Dropdown.Item key={`Resource_${index}`} value={resource.id} onClick={onResourceCategorySelected}>
                    {resource.name}
                </Dropdown.Item>
            );
        });
        setResourceCategories(resources);

        setResourceCategoryData((prevState) => {
            let pd = { ...prevState };
            pd.arrayOfResourceCategories = resourceCategoryArray;
            pd.cardComponents = resourceCategoryArray.map(mapResourceCategory);
            pd.isCreated = false;
            _logger('RESPONSE HERE', response);
            return pd;
        });
    };

    const onLookUpError = (err) => {
        _logger('Get resourceCategories error', err);
        Swal.fire({
            showCancelButton: true,
            cancelButtonText: 'Okay',
            title: 'Error occured with rendering.',
            icon: 'error',
        });
    };

    const mapResourceCategory = (resourceCategory) => {
        _logger('premapped resourceCategories', resourceCategory);
        return (
            <div key={resourceCategory.id}>
                <ResourceCategory resourceCategoryProp={resourceCategory} onResourceCatClicked={onClickResCat} />
            </div>
        );
    };

    const handleSubmit = useCallback((values) => {
        _logger('name to be created', values.resourceCategory);
        let data = { category: values.resourceCategory };
        resourceCategoriesService.createResourceCategory(data).then(onCreateSuccess).catch(onCreateError);
    }, []);

    const onCreateSuccess = async (response) => {
        _logger('onCreateSuccessHandler -->', response);
        setResourceCategoryData((prevState) => {
            return { ...prevState, isCreated: true };
        });

        const result = await Swal.fire({
            icon: 'success',
            title: 'Your resource has been added!',
            showConfirmButton: true,
            confirmButtonText: 'Okay',
        });
        if (result.isConfirmed || result.isDismissed) {
            setShowAddInput(false);
            setSelection('Resource Categories');
        }
    };

    const onCreateError = (err) => {
        _logger('Create error', err);
        Swal.fire({
            showCancelButton: true,
            cancelButtonText: 'Okay',
            title: 'Error occured with creation.',
            icon: 'error',
        });
    };

    const onClickResCat = (id) => {
        props.onGetByResCat(id);
    };
    const onResourceCategorySelected = (e) => {
        _logger(e);
        let id = e.target.attributes[0].value;
        let category = e.target.innerText;
        id = parseInt(id);
        props.onGetByResCat(id);
        setSelection(category);
    };
    const onResetClicked = () => {
        props.resetClicked();
        setShowAddInput(false);
        setSelection('Resource Categories');
    };
    const onAddCategoryClicked = () => {
        setShowAddInput(true);
    };
    return (
        <>
            <Col>
                <Dropdown>
                    <Dropdown.Toggle className="btn btn-primary">{selection}</Dropdown.Toggle>
                    <Dropdown.Menu>
                        {resourceCategories}
                        {props.currentUser.isAdmin && (
                            <Dropdown.Item onClick={onAddCategoryClicked}>Add Resource Category</Dropdown.Item>
                        )}
                        <Dropdown.Item onClick={onResetClicked}>Reset</Dropdown.Item>
                    </Dropdown.Menu>
                </Dropdown>
            </Col>
            {showAddInput && (
                <Row className="mt-2">
                    <Col>
                        <Formik enableReinitialize={true} initialValues={resourceCategoryData} onSubmit={handleSubmit}>
                            {() => (
                                <Form>
                                    <div className="input-group mb-3">
                                        <Field
                                            placeholder="Resource Category Name"
                                            type="text"
                                            name="resourceCategory"
                                            className="form-control input-res-cnmpro"
                                        />
                                        <ErrorMessage name="name" component="div" className="has-error-res-cnmpro" />
                                        <Button className="btn-primary" type="submit">
                                            Add
                                        </Button>
                                    </div>
                                </Form>
                            )}
                        </Formik>
                    </Col>
                </Row>
            )}
        </>
    );
}
ResourceCategories.propTypes = {
    currentUser: PropTypes.shape({
        isOrgAdmin: PropTypes.bool.isRequired,
        isAdmin: PropTypes.bool.isRequired,
    }),
    onGetByResCat: PropTypes.func.isRequired,
    resetClicked: PropTypes.func.isRequired,
};
export default ResourceCategories;
