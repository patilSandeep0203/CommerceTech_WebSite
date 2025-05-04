import React, { useRef, useEffect, forwardRef } from 'react';
import {
    useTable,
    useSortBy,
    usePagination,
    useRowSelect,
    useGlobalFilter,
    useAsyncDebounce,
    useExpanded,
} from 'react-table';
import classNames from 'classnames';
import 'rc-pagination/assets/index.css';
import Pagination from 'rc-pagination';
import locale from 'rc-pagination/lib/locale/en_US';
import PropTypes from 'prop-types';
import logger from 'sabio-debug';

const _logger = logger.extend('OrgTable');

const GlobalFilter = ({ preGlobalFilteredRows, globalFilter, setGlobalFilter, searchBoxClass }) => {
    const count = preGlobalFilteredRows.length;
    const [value, setValue] = React.useState(globalFilter);
    const onChange = useAsyncDebounce((value) => {
        setGlobalFilter(value || undefined);
    }, 200);

    return (
        <div className={classNames(searchBoxClass)}>
            <span className="d-flex align-items-center">
                Search :{' '}
                <input
                    value={value || ''}
                    onChange={(e) => {
                        setValue(e.target.value);
                        onChange(e.target.value);
                    }}
                    placeholder={`${count} records...`}
                    className="form-control w-auto ms-1"
                />
            </span>
        </div>
    );
};

const IndeterminateCheckbox = forwardRef(({ indeterminate, ...rest }, ref) => {
    const defaultRef = useRef();
    const resolvedRef = ref || defaultRef;

    useEffect(() => {
        resolvedRef.current.indeterminate = indeterminate;
    }, [resolvedRef, indeterminate]);

    return (
        <>
            <div className="form-check">
                <input type="checkbox" className="form-check-input" ref={resolvedRef} {...rest} />
                <label htmlFor="form-check-input" className="form-check-label"></label>
            </div>
        </>
    );
});

IndeterminateCheckbox.propTypes = {
    indeterminate: PropTypes.isRequired,
};

function Table(props) {
    const isSearchable = props.isSearchable || false;
    const isSortable = props.isSortable || false;
    const pagination = props.hasPagination || false;
    const isSelectable = props.isSelectable || false;
    const isExpandable = props.isExpandable || false;

    const dataTable = useTable(
        {
            columns: props.columns,
            data: props.data || [],
            initialState: { pageSize: props.pageSize || 10 },
        },
        isSearchable && useGlobalFilter,
        isSortable && useSortBy,
        isExpandable && useExpanded,
        pagination && usePagination,
        isSelectable && useRowSelect,
        (hooks) => {
            isSelectable &&
                hooks.visibleColumns.push((columns) => [
                    {
                        id: 'selection',
                        Header: ({ getToggleAllPageRowsSelectedProps }) => (
                            <div>
                                <IndeterminateCheckbox {...getToggleAllPageRowsSelectedProps()} />
                            </div>
                        ),
                        Cell: ({ row }) => (
                            <div>
                                <IndeterminateCheckbox {...row.getToggleRowSelectedProps()} />
                            </div>
                        ),
                    },
                    ...columns,
                ]);
            isExpandable &&
                hooks.visibleColumns.push((columns) => [
                    {
                        id: 'expander',
                        Header: ({ getToggleAllRowsExpandedProps, isAllRowsExpanded }) => (
                            <span {...getToggleAllRowsExpandedProps()}>{isAllRowsExpanded ? '-' : '+'}</span>
                        ),
                        Cell: ({ row }) =>
                            row.canExpand ? (
                                <span
                                    {...row.getToggleRowExpandedProps({
                                        style: {
                                            paddingLeft: `${row.depth * 2}rem`,
                                        },
                                    })}>
                                    {row.isExpanded ? '-' : '+'}
                                </span>
                            ) : null,
                    },
                    ...columns,
                ]);
        }
    );

    let rows = pagination ? dataTable.page : dataTable.rows;

    function onShowSizeChange(current, pageSize) {
        _logger('Current', current);
        _logger('pageSize', pageSize);
    }

    return (
        <>
            {isSearchable && (
                <GlobalFilter
                    preGlobalFilteredRows={dataTable.preGlobalFilteredRows}
                    globalFilter={dataTable.state.globalFilter}
                    setGlobalFilter={dataTable.setGlobalFilter}
                    searchBoxClass={props['searchBoxClass']}
                />
            )}

            <div className="table-responsive">
                <table {...dataTable.getTableProps()} className={classNames('table table-centered react-table')}>
                    <thead>
                        {dataTable.headerGroups.map((headerGroup) => (
                            <tr key={headerGroup.id} {...headerGroup.getHeaderGroupProps()}>
                                {headerGroup.headers.map((column) => {
                                    return (
                                        <th
                                            key={headerGroup.id}
                                            {...column.getHeaderProps(column.sort && column.getSortByToggleProps())}
                                            className={classNames({
                                                sortable: column.sort === true,
                                            })}>
                                            {column.render('Header')}
                                        </th>
                                    );
                                })}
                            </tr>
                        ))}
                    </thead>
                    <tbody {...dataTable.getTableBodyProps()}>
                        {(rows || []).map((row) => {
                            dataTable.prepareRow(row);
                            return (
                                <tr key={row.id} {...row.getRowProps()}>
                                    {row.cells.map((cell) => {
                                        return (
                                            <td key={row.id} {...cell.getCellProps()} className="orgDash-table-text">
                                                {cell.render('Cell')}
                                            </td>
                                        );
                                    })}
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </div>
            <div className="mb-2">
                {pagination && (
                    <Pagination
                        pageSize={10}
                        total={props.totalPages}
                        current={props.current + 1}
                        onChange={props.onChangeOfPageIndex}
                        locale={locale}
                        showSizeChanger
                        onShowSizeChange={onShowSizeChange}
                    />
                )}
            </div>
        </>
    );
}

GlobalFilter.propTypes = {
    preGlobalFilteredRows: PropTypes.isRequired,
    globalFilter: PropTypes.isRequired,
    setGlobalFilter: PropTypes.isRequired,
    searchBoxClass: PropTypes.isRequired,
};

Table.propTypes = {
    isSearchable: PropTypes.isRequired,
    isSortable: PropTypes.isRequired,
    hasPagination: PropTypes.isRequired,
    isSelectable: PropTypes.isRequired,
    isExpandable: PropTypes.isRequired,
    columns: PropTypes.isRequired,
    data: PropTypes.isRequired,
    pageSize: PropTypes.isRequired,
    row: PropTypes.isRequired,
    getToggleAllPageRowsSelectedProps: PropTypes.isRequired,
    searchBoxClass: PropTypes.isRequired,
    getToggleAllRowsExpandedProps: PropTypes.isRequired,
    isAllRowsExpanded: PropTypes.isRequired,
    totalPages: PropTypes.number.isRequired,
    current: PropTypes.number,
    onChangeOfPageIndex: PropTypes.func,
};

export default Table;
