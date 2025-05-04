import React from 'react';
import { Formik, Field, Form } from 'formik';
import { PropTypes } from 'prop-types';
import searchFormSchema from '../../schema/timesheet/searchFormSchema';

function SearchForm(props) {
    const formData = { organizationId: 0, timePeriod: 0 };

    const { orgs } = props;

    const onSearchClicked = (values) => {
        props.onSearch(values);
    };

    const onClearClicked = () => {
        props.onClear();
    };

    const mapOrg = (org) => {
        return (
            <option key={org.organizationId} value={org.organizationId}>
                {org.organization}
            </option>
        );
    };

    return (
        <Formik
            initialValues={formData}
            validationSchema={searchFormSchema}
            enableReinitialize={true}
            onSubmit={onSearchClicked}
            className="justify-content-end">
            {({ handleChange }) => (
                <Form className="form-group">
                    <div className="col-12">
                        <Field
                            component="select"
                            name="organizationId"
                            className="form-control"
                            onChange={handleChange}>
                            <option value="">Select Organization</option>
                            {orgs.map(mapOrg)}
                        </Field>
                    </div>
                    <div className="col-12">
                        <label htmlFor="timePeriod"></label>
                        <Field component="select" name="timePeriod" className="form-control" onChange={handleChange}>
                            <option value="">Select Time Period</option>
                            <option value="1">Last Day</option>
                            <option value="7">Last Week</option>
                            <option value="30">Last Month</option>
                            <option value="2">All</option>
                        </Field>
                    </div>
                    <div className="row">
                        <div className="col-6">
                            <button className="btn btn-primary" type="submit">
                                Search
                            </button>
                        </div>
                        <div className="col-6">
                            <button className="btn btn-primary" type="reset" onClick={onClearClicked}>
                                Clear
                            </button>
                        </div>
                    </div>
                </Form>
            )}
        </Formik>
    );
}

SearchForm.propTypes = {
    onSearch: PropTypes.func.isRequired,
    onClear: PropTypes.func.isRequired,
    orgs: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
};

export default SearchForm;
