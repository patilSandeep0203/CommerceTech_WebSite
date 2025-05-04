import React, { useState } from 'react';
import debug from 'sabio-debug';
import FileUploadModal from '../../components/uploader/FileUploadModal';

const _logger = debug.extend('File Modal');

const Test = () => {
    const toggleModal = () => {
        setShowModal(!showModal);
        _logger('Upload Modal Launched');
    };

    const [showModal, setShowModal] = useState(false);

    return (
        <div className="container">
            <div className="row">
                <div className="col">
                    <div className="container" style={{ marginTop: '25%', marginLeft: '50%', paddingBottom: '130px' }}>
                        <button className="btn btn-primary" onClick={toggleModal}>
                            Show Modal
                        </button>
                        <div>{showModal && <FileUploadModal isShowModal={showModal} toggleModal={toggleModal} />}</div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Test;
