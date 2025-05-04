import React from 'react';
import DynamicPdfCreator from './DynamicPdfCreator';

const PdfRenderExample = () => {
    const pdfContentData = {
        pages: [
            [
                {
                    type: 'Title',
                    content: 'A Job Post',
                },
                {
                    type: 'Image',
                    content: 'https://panampost.com/wp-content/uploads/don-quijote-lessons.jpg',
                },

                {
                    type: 'Text',
                    content:
                        'Test: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
                },
                {
                    type: 'Text',
                    content:
                        'Test V2: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
                },
            ],
        ],
    };

    const pdfStyleObj = {
        body: {
            paddingTop: 35,
            paddingBottom: 65,
            paddingHorizontal: 35,
        },

        text: {
            margin: 12,
            fontSize: 14,
            textAlign: 'justify',
        },
        image: {
            marginVertical: 15,
            marginHorizontal: 100,
        },
        title: {
            fontSize: 24,
            textAlign: 'center',
        },
    };

    const myPdfDoc = <DynamicPdfCreator pdfContent={pdfContentData} pdfStyle={pdfStyleObj} fileName={'testingFile'} />;

    return myPdfDoc;
};

export default PdfRenderExample;
