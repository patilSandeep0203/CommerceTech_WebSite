import React, { useEffect } from 'react';
import { withFormik } from 'formik';
import { Form, Card } from 'react-bootstrap';
import * as wizardPropTypes from './eventsPropTypes';
import { BsCheck2All } from 'react-icons/bs';
import eventFinish from '../../schema/events/eventFinishSchema';

const EventFinish = (props) => {
    const { values, isSubmitting, handleSubmit, onBack, backLabel, nextLabel, onNext, cantBack } = props;

    useEffect(() => {
        onChange();
    }, [values]);

    const onChange = () => {
        props.onChange(values);
    };

    const onNextClicked = () => {
        onNext(values);
    };

    const onBackClicked = () => {
        onBack(values);
    };

    return (
        <React.Fragment>
            <Form onSubmit={handleSubmit} className="p-1">
                <Card className="p-4 mx-auto" style={{ width: 950 }}>
                    <Card.Header>
                        <h3 className="text-center">Submit</h3>
                    </Card.Header>
                    <div className="form-group mt-2">
                        <div className="text-center">
                            <h2 className="mt-0">
                                <BsCheck2All />
                            </h2>
                            <div className="mb-3">
                                <Form.Check type="checkbox" className="d-inline-block" name="acceptCheck">
                                    <Form.Check.Input type="checkbox" />{' '}
                                    <Form.Check.Label>I agree with the Terms and Conditions</Form.Check.Label>
                                </Form.Check>
                            </div>
                        </div>

                        <div className="button-group pt-3 row">
                            <div className="col-sm-1">
                                <button
                                    type="submit"
                                    className="btn btn-secondary"
                                    onClick={onBackClicked}
                                    disabled={isSubmitting || cantBack}>
                                    {backLabel}
                                </button>
                            </div>
                            <div className="col-sm-1">
                                <button type="submit" className="btn btn-primary" onClick={onNextClicked}>
                                    {nextLabel}
                                </button>
                            </div>
                        </div>
                    </div>
                </Card>
            </Form>
        </React.Fragment>
    );
};

EventFinish.propTypes = wizardPropTypes.eventPropTypes;

export default withFormik({
    enableReinitialize: true,
    validationSchema: eventFinish,
    mapPropsToValues: (props) => ({
        ...props.formData,
    }),
    handleSubmit: (values, { props }) => {
        props.onNext(values);
    },
})(EventFinish);
