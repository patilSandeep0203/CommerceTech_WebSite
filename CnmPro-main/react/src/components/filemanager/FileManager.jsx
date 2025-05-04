import React, { useState, useEffect } from 'react';
import { Row, Col, Card, Container, Button, Form } from 'react-bootstrap';
import FileCard from './FileCard';
import FManagerActions from './FManagerActions';
import debug from 'sabio-debug';
import toastr from 'toastr';
import fileManagerService from '../../services/fileManagerService';
import lookUpService from '../../services/lookUpService';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import './filemanager.css';

const _logger = debug.extend('FileManager');

function FileManager() {
    const [fileData, setFileData] = useState({
        arrayOfFiles: [],
        fileArrayMapped: [],
        uploadedArrayOfFiles: [],
        current: 1,
        pgIndex: 0,
        pgSize: 12,
        totalCount: 0,
        query: '',
        selectedOption: 'Select By Type',
        myFiles: false,
        recent: false,
        deleted: false,
        deleteStatus: 1,
    });
    const [isPaginate, setIsPaginate] = useState(true);
    const [resultTitle, setResultTitle] = useState('Quick Access');
    const [lookupResult, setLookupResult] = useState({ fileTypes: [] });
    const [callType, setCallType] = useState('');
    const [isNoFile, setIsNoFile] = useState(false);

    useEffect(() => {
        lookUpService.LookUp(['FileTypes']).then(onSuccessLookUp).catch(onErrorLookup);
    }, []);

    useEffect(() => {
        _logger('useEffect ---', fileData.selectedOption, fileData?.arrayOfFiles?.length, fileData.selectedOption);

        if (fileData.selectedOption !== 'Select By Type' && !undefined) {
            fileManagerService
                .getFileType(fileData.selectedOption, fileData.pgIndex, fileData.pgSize)
                .then(onSelectSuccess)
                .catch(onSelectError);
            updateResTitle('File Type Results');
        } else if (fileData.query && !null && !undefined) {
            handleSearchCall();
        } else if (fileData.myFiles !== false && !null && !undefined && callType === 'myfiles') {
            updateResTitle('My Files');
            fileManagerService.getMyFiles(fileData.pgIndex, fileData.pgSize).then(onSuccessMyFile).catch(onErrorMyFile);
        } else if (fileData.recent !== false && !null && !undefined && callType === 'myrecentfiles') {
            updateResTitle('My Recent Files');
            fileManagerService.getMyFiles(fileData.pgIndex, fileData.pgSize).then(onSuccessMyFile).catch(onErrorMyFile);
        } else if (callType === 'deleted') {
            updateResTitle('Deleted Files');
            fileManagerService
                .getDeleted(fileData.pgIndex, fileData.pgSize, fileData.deleted)
                .then(onSuccessGetDeleted)
                .catch(onErrorGetDeleted);
        } else {
            fileManagerService.getAll(fileData.pgIndex, fileData.pgSize).then(onSuccessGetFile).catch(onErrorGetFile);
        }
    }, [
        fileData.selectedOption,
        fileData.query,
        fileData.myFiles,
        fileData.recent,
        fileData.deleted,
        fileData.current,
    ]);

    const handleSearchCall = async () => {
        try {
            const res = await fileManagerService.getSearch(fileData.pgIndex, fileData.pgSize, fileData.query);

            if (res) {
                let arrayOfFiles = res.item.pagedItems;
                _logger('arrayOfFiles', arrayOfFiles);

                setTimeout(
                    () =>
                        setFileData((prevState) => {
                            const fileData = { ...prevState };
                            fileData.arrayOfFiles = arrayOfFiles;
                            fileData.fileArrayMapped = arrayOfFiles.map(mapFile);
                            fileData.totalCount = res.item.totalCount;
                            _logger(fileData.totalCount);
                            return fileData;
                        }),
                    500
                );
                setTimeout(() => updateResTitle('Search Results'), 700);

                setIsNoFile(false);
                setIsPaginate(true);
            }
        } catch {
            setTimeout(() => toastr.error('Unable to match searched results'), 500);
            setIsNoFile(true);
            setIsPaginate(false);
        }
    };

    const updateResTitle = (type) => {
        setResultTitle((prevState) => {
            let updatedTitle = { ...prevState };
            updatedTitle = type;
            return updatedTitle;
        });
    };

    const mapFile = (aFile) => {
        return (
            <FileCard
                file={aFile}
                key={aFile.id}
                deleteCardClicked={deleteCardClicked}
                undoCardClicked={undoCardClicked}
            />
        );
    };

    const onSuccessGetFile = (data) => {
        let arrayOfF = data.item.pagedItems;

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.arrayOfFiles = arrayOfF;
            fileData.fileArrayMapped = arrayOfF.map(mapFile);
            fileData.totalCount = data.item.totalCount;
            _logger('Success GetAll', fileData.totalCount);
            return fileData;
        });
    };

    const onErrorGetFile = (err) => {
        toastr.error('Unable to get Data', err);
        _logger('Get file error', err);
    };

    const onSearchFieldChange = (evt) => {
        const target = evt.target;
        const fileSearchBarValue = target.value;

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.query = fileSearchBarValue.toLowerCase();
            fileData.selectedOption = 'Select By Type';
            fileData.pgIndex = 0;
            fileData.current = 1;

            return fileData;
        });
        setIsPaginate(true);
    };

    const onClearClick = (e) => {
        _logger('Search Field Clear Clicked');
        e.preventDefault(e);
        setIsNoFile(false);
        setIsPaginate(true);

        return searchResetValue();
    };

    const searchResetValue = () => {
        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.query = '';
            return fileData;
        });
    };

    const onSuccessLookUp = (data) => {
        if (data.item.fileTypes) {
            setLookupResult((prevState) => {
                let result = { ...prevState };
                result.fileTypes = data.item.fileTypes;
                return result;
            });
        } else {
            toastr.error('No file type found.');
        }
    };

    const onErrorLookup = (err) => {
        _logger('Get Lookup error', err);
    };

    const mapFileTypesDrop = (fileTypes) => {
        return (
            <option name={fileTypes.name} key={fileTypes.id} value={fileTypes.id} type="number">
                {fileTypes.name}
            </option>
        );
    };

    const onSelectChange = (evt) => {
        evt.stopPropagation();
        const target = evt.target;
        const typeSelectValue = target.value;

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.selectedOption = typeSelectValue;
            fileData.query = '';
            fileData.pgIndex = 0;
            fileData.current = 1;
            return fileData;
        });
        setIsPaginate(true);
    };

    const onSelectSuccess = (data) => {
        _logger('SelectSuccessData', data);
        let arrayOfFiles = data.item.pagedItems;

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.arrayOfFiles = arrayOfFiles;
            fileData.fileArrayMapped = arrayOfFiles.map(mapFile);
            fileData.totalCount = data.item.totalCount;
            return fileData;
        });

        setIsNoFile(false);
    };

    const onSelectError = () => {
        toastr.error('No Files Found by Selected Type. Try again');
        setIsNoFile(true);
        setIsPaginate(false);
    };

    const handleFileUploaded = (data) => {
        _logger('File received from child', data);
        let uploadedArrayOfFiles = data.items;
        _logger('uploadedArrayOfFiles', uploadedArrayOfFiles);

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.uploadedArrayOfFiles = uploadedArrayOfFiles;
            fileData.arrayOfFiles = fileData.arrayOfFiles.concat(uploadedArrayOfFiles);
            fileData.fileArrayMapped = fileData.arrayOfFiles.map(mapFile);
            _logger('FileArrayMapped', fileData.fileArrayMapped, fileData.arrayOfFiles);
            _logger('FileArrayOfFiles', fileData.arrayOfFiles);

            return fileData;
        });
    };

    const myFileClick = (e) => {
        e.preventDefault();

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.myFiles = true;
            fileData.recent = false;
            fileData.pgIndex = 0;
            fileData.current = 1;
            fileData.query = '';
            fileData.selectedOption = 'Select By Type';
            return fileData;
        });
        setIsPaginate(true);
        setCallType('myfiles');
    };

    const onSuccessMyFile = (data) => {
        let arrayOfF = data.item.pagedItems;

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.arrayOfFiles = arrayOfF;
            fileData.fileArrayMapped = arrayOfF.map(mapFile);
            fileData.totalCount = data.item.totalCount;

            _logger('Success My Files', fileData.totalCount);
            return fileData;
        });
        setIsNoFile(false);
    };

    const onErrorMyFile = (err) => {
        toastr.error('Unable to get Data', err);
        setIsNoFile(true);
        setIsPaginate(false);
    };

    const recentClick = (e) => {
        e.preventDefault();

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.recent = true;
            fileData.myFiles = true;
            fileData.deleted = false;
            fileData.current = 1;
            fileData.pgIndex = Math.floor(fileData.totalCount / fileData.pgSize);
            fileData.query = '';
            fileData.selectedOption = 'Select By Type';

            return fileData;
        });
        setIsPaginate(false);
        setCallType('myfiles');
    };

    const isDeletedClick = (e) => {
        e.preventDefault();

        _logger('Deleted Clicked', fileData.deleted);

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.myFiles = false;
            fileData.recent = false;
            fileData.deleted = true;
            fileData.query = '';
            fileData.selectedOption = 'Select By Type';
            fileData.pgIndex = 0;
            fileData.current = 1;

            return fileData;
        });
        setIsPaginate(true);
        setCallType('deleted');
    };

    const onSuccessGetDeleted = (data) => {
        _logger('Deleted success Data', data);
        let arrayOfF = data.item.pagedItems;

        setFileData((prevState) => {
            const fileData = { ...prevState };
            fileData.arrayOfFiles = arrayOfF;
            fileData.fileArrayMapped = arrayOfF.map(mapFile);
            fileData.totalCount = data.item.totalCount;
            _logger('Success Deleted', fileData.fileArrayMapped);
            return fileData;
        });
        setIsNoFile(false);
        setIsPaginate(true);
    };

    const onErrorGetDeleted = (err) => {
        toastr.error('Unable to get Data', err);
        _logger('Deleted error', err);
        setIsNoFile(true);
        setIsPaginate(false);
    };

    const onPaginateClicked = (page) => {
        const pageIndex = page - 1;

        setFileData((prevState) => {
            let pd = { ...prevState };
            pd.current = page;
            pd.pgIndex = pageIndex;
            return pd;
        });
    };

    const displayNoFile = () => {
        return (
            <Row className="nofile-image-filemanager-aquifer">
                <img
                    src="https://sabio-training.s3-us-west-2.amazonaws.com/19730fc5-2511-4a76-8934-10cc6a3e12ba_No%20Files%20Found.jpg"
                    style={{ height: '300px', width: '500px' }}
                    alt="noFile"></img>
            </Row>
        );
    };

    const deleteCardClicked = (id) => {
        _logger('Delete Card Btn Clicked', id);

        const onDeleteSuccess = () => {
            toastr.success('File was deleted');
            _logger('File was deleted');
        };
        const onDeleteError = (err) => {
            toastr.error('unable to delete', err);
            _logger('get delete error!');
        };

        fileManagerService.isDeleted(id, fileData.deleteStatus).then(onDeleteSuccess).catch(onDeleteError);

        setFileData((prevState) => {
            const { arrayOfFiles } = prevState;
            const updatedFiles = arrayOfFiles.filter((file) => file.id !== id);

            return {
                ...prevState,
                arrayOfFiles: updatedFiles,
                fileArrayMapped: updatedFiles.map(mapFile),
            };
        });
    };

    const undoCardClicked = (id) => {
        _logger('undoCard Btn Clicked', id);

        const onDeleteSuccess = () => {
            toastr.success('File was restored');
            _logger('File was deleted');
        };
        const onDeleteError = (err) => {
            toastr.error('unable to restore', err);
            _logger('undo delete error!');
        };
        fileManagerService
            .isDeleted(id, (fileData.deleteStatus = 0))
            .then(onDeleteSuccess)
            .catch(onDeleteError);

        setFileData((prevState) => {
            const { arrayOfFiles } = prevState;
            const updatedFiles = arrayOfFiles.filter((file) => file.id !== id);

            return {
                ...prevState,
                arrayOfFiles: updatedFiles,
                fileArrayMapped: updatedFiles.map(mapFile),
            };
        });
    };

    toastr.options = {
        closeButton: false,
        debug: false,
        newestOnTop: false,
        progressBar: false,
        positionClass: 'toast-top-right',
        preventDuplicates: true,
        onclick: null,
        showDuration: '300',
        hideDuration: '1000',
        timeOut: '5000',
        extendedTimeOut: '1000',
        showEasing: 'swing',
        hideEasing: 'linear',
        showMethod: 'fadeIn',
        hideMethod: 'fadeOut',
    };

    return (
        <React.Fragment>
            <div className="container">
                <div className="row title-filemanager-aquiferpe">
                    <div className="col-3 text-center">
                        <h1>File Manager</h1>
                    </div>
                </div>
            </div>
            <Row>
                <Col>
                    <Card>
                        <Card.Body>
                            <div className="page-aside-left">
                                <Col className="mb-2 position-relative ps-1">
                                    <Form.Select
                                        aria-label="Default select example"
                                        value={fileData.selectedOption}
                                        onChange={onSelectChange}>
                                        <option>Select By Type</option>
                                        {lookupResult.fileTypes.map(mapFileTypesDrop)}
                                    </Form.Select>
                                </Col>
                                <FManagerActions
                                    isDeletedClick={isDeletedClick}
                                    recentClick={recentClick}
                                    myFilesClick={myFileClick}
                                    onSuccessFileUpload={handleFileUploaded}
                                />
                            </div>

                            <div className="page-aside-right">
                                <div className="d-flex justify-content-between align-items-center">
                                    <Col>
                                        <div className="app-search">
                                            <form>
                                                <div className="mb-2 position-relative">
                                                    <input
                                                        type="text"
                                                        className="form-control"
                                                        id="query"
                                                        name="query"
                                                        placeholder="Search files..."
                                                        value={fileData.query}
                                                        onChange={onSearchFieldChange}
                                                    />
                                                    <div></div>
                                                </div>
                                            </form>
                                        </div>
                                    </Col>
                                    <Col className="mb-2 position-relative ps-1">
                                        <Button onClick={onClearClick} variant="primary" size="sm">
                                            Clear
                                        </Button>
                                    </Col>
                                </div>

                                <Container fluid="true">
                                    <h5 className="mb-2">{resultTitle}</h5>
                                    {isNoFile ? (
                                        <Row>{displayNoFile()}</Row>
                                    ) : (
                                        <Row className="mx-n1 g-0">{fileData.fileArrayMapped}</Row>
                                    )}

                                    {isPaginate && (
                                        <Pagination
                                            style={{ fontSize: 15, paddingTop: '18px' }}
                                            current={fileData.current}
                                            pageSize={fileData.pgSize}
                                            total={fileData.totalCount}
                                            onChange={onPaginateClicked}></Pagination>
                                    )}
                                </Container>
                                {/* <FManagerRecent /> */}
                            </div>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </React.Fragment>
    );
}

export default FileManager;
