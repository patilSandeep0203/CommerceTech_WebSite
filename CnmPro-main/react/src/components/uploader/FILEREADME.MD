+# File Uploader Integration

> Example code is provided below. You will need to create a function that allows you to set state for the modal. The modal's state should be set to FALSE since we are using it to Toggle. Within this primary function you also will need to declare another function called toggleModal. The toggleModal function will instantiate setShowModal. The setShowModal will set state to the opposite of what it currently is.

> const YouNameTheFunctionHere = () => {

    const toggleModal = () => {
        setShowModal(!showModal);
        _logger('Upload Modal Launched');
    };

    const [showModal, setShowModal] = useState(false);

> Within the HTML of your return statement you will now need to include the ternary statement that will toggle the modal and launch the File Uploader. You will need to create another div to contain the the code needed to toggle the modal. Provided below is the code required.

    <div>{showModal && <FileUploadModal isShowModal={showModal} toggleModal={toggleModal} /></div>

> If you are using a button to launch the File Uploader, you will need to add onClick = {toggleModal}

    <button className="btn btn-primary" onClick={toggleModal}>Show Modal</button>

> Complete code for the HTML component is provided below.

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

The last part of integrating the File Uploader is to make sure you are able to capture the success or error response.

onHandleUploadSucccess is instantiated using the axios response and is passed as Props allowing you to capture the response.

    const onUploadFileSuccess = (response) => {
            toastr.success('Upload Success');
            _logger('File Upload Success', response);
            onHandleUploadSuccess(response);
