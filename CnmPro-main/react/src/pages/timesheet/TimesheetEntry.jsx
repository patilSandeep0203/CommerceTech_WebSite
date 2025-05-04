import React from 'react';
import { PropTypes } from 'prop-types';
import { BsFillPencilFill } from 'react-icons/bs';

function TimesheetEntry(props) {
    const timesheetEntry = props.timesheetEntry;

    const onEditClicked = () => {
        props.onEdit(timesheetEntry);
    };

    const newDate = (date) => {
        return date.split('T')[0];
    };

    const time = (time) => {
        time = time.match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
        if (time.length > 1) {
            time = time.slice(1);
            time[5] = +time[0] < 12 ? 'AM' : 'PM';
            time[0] = +time[0] % 12 || 12;
        }
        return time.join('');
    };

    return (
        <tr key={timesheetEntry.id}>
            <td className="text-center">
                <BsFillPencilFill type="button" onClick={onEditClicked} className="col-3 mb-2 mt-2 me-4">
                    edit
                </BsFillPencilFill>
            </td>
            <td>{newDate(timesheetEntry.date)}</td>
            <td>
                {timesheetEntry.title}, {timesheetEntry.organization}, {timesheetEntry.city}
            </td>
            <td>{timesheetEntry.comment}</td>
            <td>{time(timesheetEntry.timeFrom)}</td>
            <td>{time(timesheetEntry.timeTo)}</td>
            <td>{timesheetEntry.totalHrs}</td>
        </tr>
    );
}

TimesheetEntry.propTypes = {
    timesheetEntry: PropTypes.shape({
        id: PropTypes.number.isRequired,
        date: PropTypes.string.isRequired,
        timeFrom: PropTypes.string.isRequired,
        timeTo: PropTypes.string.isRequired,
        totalHrs: PropTypes.number.isRequired,
        organizationId: PropTypes.number.isRequired,
        comment: PropTypes.string.isRequired,
        organization: PropTypes.string.isRequired,
        title: PropTypes.string.isRequired,
        city: PropTypes.string.isRequired,
    }).isRequired,
    onEdit: PropTypes.func.isRequired,
};

export default TimesheetEntry;
