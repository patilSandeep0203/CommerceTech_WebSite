import React, { useState, useEffect } from 'react';
import { Page, Text, Document, StyleSheet, Link, Image, PDFDownloadLink, } from '@react-pdf/renderer';
import PropTypes from 'prop-types';

let styles;

const DynamicPdfCreator = (props) => {
    let pdfContent = props?.pdfContent;
    styles = StyleSheet.create(props?.pdfStyle);

    const [pdfFileContent, setPdfFileContent] = useState(null);

    useEffect(() => {
        if (props?.pdfContent) {
            setPdfFileContent(<Document>{pdfContent?.pages?.map(mapPage)}</Document>);
        }
    }, []);

    return (
        pdfFileContent && (
            <>
                <PDFDownloadLink document={pdfFileContent} fileName={props.fileName}>
                    {({ loading }) =>
                        loading ? (
                            <div className="text-center">
                                <button className="btn btn-outline-dark my-1">Loading Document...</button>
                            </div>
                        ) : (
                            <div className="text-center">
                                <button className="btn btn-outline-dark my-1">Download as PDF</button>
                            </div>
                        )
                    }
                </PDFDownloadLink>
            </>
        )
    );
};

const mapPage = (aPage, index) => {
    const pageComponent = (
        <Page key={`a_page_key_${index}`} style={styles?.body} size="A4">
            {aPage?.map(mapPageComponent)}
        </Page>
    );

    return pageComponent;
};

const mapPageComponent = (component, index) => {
    let TagName;

    if (component?.type === 'Image') {
        return <Image key={`image_key_${index}`} src={component?.content} style={styles?.image} />;
    }

    if (component?.type === 'Title') {
        return (
            <Text style={styles?.title} key={`a_tag_key_${index}`}>
                {component?.content}
            </Text>
        );
    }

    if (component?.type === 'Link') {
        return <Link key={`link_key_${index}`} src={component?.content} />;
    }

    if (component?.type === 'Text') TagName = Text;

    return (
        <TagName style={styles?.text} key={`a_tag_key_${index}`}>
            {component?.content}
        </TagName>
    );
};

DynamicPdfCreator.propTypes = {
    pdfContent: PropTypes.shape({
        pages: PropTypes.arrayOf(
            PropTypes.arrayOf(
                PropTypes.shape({
                    type: PropTypes.string,
                    content: PropTypes.string,
                })
            )
        ),
    }),
    pdfStyle: PropTypes.shape({
        text: PropTypes.shape({
            margin: PropTypes.number,
        }),
    }),
    fileName: PropTypes.string,
};

export default DynamicPdfCreator;
