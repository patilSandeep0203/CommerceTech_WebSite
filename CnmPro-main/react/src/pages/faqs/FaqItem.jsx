import React from 'react';
import PropTypes from 'prop-types';
import Accordion from 'react-bootstrap/Accordion';

function FaqItem(props) {
    const anFaq = props.faq;

    const onLocalSelected = (evt) => {
        evt.preventDefault();

        props.onDeleteClicked(props.faq, evt);
    };

    const onLocalEditClicked = (evt) => {
        evt.preventDefault();
        props.onEditClicked(anFaq);
    };

    return (
        <React.Fragment>
            <>
                <Accordion.Item eventKey={props.id}>
                    <Accordion.Header>
                        <span className="fw-bolder">{anFaq.question}</span>
                    </Accordion.Header>
                    <Accordion.Body>
                        {anFaq.answer}

                        {props.roles.includes('Admin') && (
                            <div className="my-2">
                                <button onClick={onLocalSelected} className="btn btn-outline-danger mx-1">
                                    Delete
                                </button>
                                <button className="btn btn-outline-success mx-1" onClick={onLocalEditClicked}>
                                    Edit
                                </button>
                            </div>
                        )}
                    </Accordion.Body>
                </Accordion.Item>
            </>
        </React.Fragment>
    );
}

FaqItem.propTypes = {
    faq: PropTypes.shape({
        question: PropTypes.string.isRequired,
        answer: PropTypes.string.isRequired,
        id: PropTypes.number.isRequired,
    }),
    onDeleteClicked: PropTypes.func,
    onEditClicked: PropTypes.func,
    isLoggedIn: PropTypes.bool,
    id: PropTypes.number,
    roles: PropTypes.arrayOf(PropTypes.string),
};

export default React.memo(FaqItem);
