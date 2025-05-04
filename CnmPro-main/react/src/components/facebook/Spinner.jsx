import React from 'react';
import classNames from 'classnames';
import PropTypes from 'prop-types';

const SpinnerBanana = (props) => {
    const children = props.children || null;
    const Tag = props.tag || 'div';
    const color = props.color || 'secondary';
    const size = props.size || '';

    return (
        <Tag
            role="status"
            className={classNames(
                {
                    'spinner-border': props.type === 'bordered',
                    'spinner-grow': props.type === 'grow',
                },
                [`text-${color}`],
                { [`avatar-${size}`]: size },
                props.className
            )}>
            {children}
        </Tag>
    );
};

SpinnerBanana.defaultProps = {
    tag: 'div',
    type: 'bordered',
};
SpinnerBanana.propTypes = {
    children: PropTypes.oneOf([PropTypes.element,PropTypes.arrayOf(PropTypes.element)]),
    tag: PropTypes.string,
    color: PropTypes.string,
    size: PropTypes.string,
    type: PropTypes.string,
    className: PropTypes.string,
};

export default SpinnerBanana;
