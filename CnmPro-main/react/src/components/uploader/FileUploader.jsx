import React from 'react';
import Dropzone from 'react-dropzone';
import debug from 'sabio-debug';
import fileService from '../../services/fileService';
import PropTypes from 'prop-types';
import toastr from 'toastr';
import './filestyle.css';

const _logger = debug.extend('Files');

const FileUploader = ({ onHandleUploadSuccess }) => {
    const handleAcceptedFiles = (files) => {
        let formData = new FormData();
        for (let i = 0; i < files.length; i++) {
            _logger('Now adding files to form data', files);
            formData.append('files', files[i]);
        }
        fileService.fileUpload(formData).then(onUploadFileSuccess).catch(onUploadFileError);
    };

    const onUploadFileSuccess = (response) => {
        toastr.success('Upload Success');
        _logger('File Upload Success', response);
        onHandleUploadSuccess(response);
    };

    const onUploadFileError = (error) => {
        toastr.error('File Type Error?');
        _logger('File Upload Error', error);
    };

    return (
        <Dropzone onDrop={(acceptedFiles) => handleAcceptedFiles(acceptedFiles)}>
            {({ getRootProps, getInputProps }) => (
                <div className="dropzone">
                    <div className="files" {...getRootProps()}>
                        <input className="input color" {...getInputProps()} />
                        <img
                            src="https://bootstrapious.com/i/snippets/sn-file-upload/img.png"
                            alt=""
                            width="115px"
                            className="d-block mx-auto mb-4 rounded-pill"
                        />
                        <h5 className="title-dz">Drop files here or click to upload.</h5>
                        <span className="text-muted font-13">Please only select Images, such as JPEG, or a PDF.</span>
                    </div>
                </div>
            )}
        </Dropzone>
    );
};
<FileUploader />;
FileUploader.propTypes = {
    onHandleUploadSuccess: PropTypes.func,
};

export default FileUploader;
