import React, { useState } from 'react';
import { PropTypes } from 'prop-types';
import { ListGroup } from 'react-bootstrap';
import debug from 'sabio-debug';
import './surveys.css';
const _logger = debug.extend('surveys');
_logger();

const OpenEndedListItemTemplate = (props) => {
    const [showMore, setShowMore] = useState(false);
    const toggleShow = () => {
        setShowMore(!showMore);
    };

    let answerString = props.answerString;
    let listItem = <ListGroup.Item>{answerString}</ListGroup.Item>;
    if (props.answerString?.length > 100) {
        listItem = (
            <ListGroup.Item>
                {showMore ? answerString : `${answerString.substring(0, 100)}...`}{' '}
                {!showMore && (
                    <p className="show-more-less" onClick={toggleShow}>
                        see more
                    </p>
                )}
                {showMore && (
                    <p className="show-more-less" onClick={toggleShow}>
                        see less
                    </p>
                )}
            </ListGroup.Item>
        );
    }
    return listItem;
};
OpenEndedListItemTemplate.propTypes = {
    answerString: PropTypes.string,
};

export default OpenEndedListItemTemplate;
